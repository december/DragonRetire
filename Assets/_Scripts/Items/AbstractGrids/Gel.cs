using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//凝胶类
public class Gel : AbstractGrid {

	#region Variables
	public Sprite[] sprites; // 0: left , 1:right 
	public Animator fireAni;
	public Animator buff;
	//public float explodeForce;
	[SerializeField]private bool isLeft;
	private SpriteRenderer sp;

	private bool isValid; //避免连续发射
	private GameObject gelLight;
	#endregion

	#region Unity Events
	void Awake()
	{
		sp = GetComponent<SpriteRenderer> ();
		gelLight = transform.FindChild ("GelLight").gameObject;
	}
	void Start()
	{
		Initialization ();
	}
	void FixedUpdate()
	{
		if(isValid)
			CheckPlayerIn ();
	}
	#endregion

	#region Methods

	public override void Initialization ()
	{
		state = State.Normal;
		ability = Ability.Flammable;
		isValid = true;

		GameObject.FindObjectOfType<PrinceController> ().newObj = this.gameObject;
	}

	/// <summary>
	/// 火焰魔法，燃烧，0.5s后引燃相邻可燃物，1s后爆炸，同时引爆相邻凝胶，摧毁相邻木块、弹开石头、杀死生物
	/// </summary>
	public  override bool OnFired()
	{
		if (state == State.Normal) {
			
			state = State.Firing;
			buff.SetTrigger ("Fire");
			gelLight.SetActive (true);
			StartCoroutine (FireEvent ());
			Debug.Log ("Gel is fired");
			return true;
		}
		return false;
	}
	/// <summary>
	/// 冰冻魔法，无效
	/// </summary>
	public  override bool OnFreezed()
	{
		Debug.Log ("Gel can't be freezed");
		return false;
	}
	/// <summary>
	/// 反重力无效
	/// </summary>
	public override  bool OnAntiGravity()
	{
		Debug.Log ("Gel is antigravity");
		return false;
	}
	public override void InteractWithPrince()
	{
	}
	/*
	void OnTriggerEnter2D(Collider2D coll)//弹射主人公
	{
		if (coll.tag == "Player") {
			//Debug.Log ("gel launch!");
		//	coll.transform.position = transform.position + new Vector3 (0,0.2f,0);
			//coll.GetComponent<PrinceController> ().GelLaunch (isLeft);
			StartCoroutine(LaunchDelay(coll.GetComponent<PrinceController>()));
		}
	}*/
	//

	private void CheckPlayerIn()
	{
		Collider2D coll= Physics2D.OverlapBox (transform.position, Vector2.one * 0.8f, 0,LayerMask.GetMask("Player"));

		if (coll != null) {
			StartCoroutine(LaunchDelay(coll.GetComponent<PrinceController>()));
			StartCoroutine (CoolDown ());
			coll.GetComponent<PrinceController> ().makeDecision ();
		}
	}

	//在弹射前加个延时
	IEnumerator LaunchDelay(PrinceController pc)
	{
		yield return new WaitForSeconds (0.2f);
		pc.transform.position = transform.position;
		pc.GelLaunch (isLeft);
	}
	IEnumerator CoolDown()
	{
		isValid = false;
		yield return new WaitForSeconds (1.2f);
		isValid = true;
	}


	/// <summary>
	/// 搜索周围一格内的物体,并使之燃烧
	/// </summary>
	private void FireNearBy()
	{
		Collider2D[] colls = Physics2D.OverlapCircleAll (transform.position + new Vector3 (0.5f, 0.5f, 0f), 1);

		foreach (Collider2D coll in colls) {
			if (coll.gameObject != gameObject) {
				AbstractGrid ag = coll.GetComponent<AbstractGrid> ();
				if (ag != null &&(ag.ability&Ability.Flammable)!=0)
					ag.OnFired ();
			}
		}
	}
	/// <summary>
	/// 处理燃烧事件
	/// </summary>
	/// <returns>The event.</returns>

	private IEnumerator FireEvent()
	{
		fireAni.SetTrigger ("Fire");
		yield return new WaitForSeconds (0.5f);
		FireNearBy ();
		yield return new WaitForSeconds (0.5f);
		Explode ();
		sp.enabled = false;
		yield return new WaitForSeconds (0.5f);
		Destroy (this.gameObject);
	}
	/// <summary>
	/// 爆炸，将邻近的木块摧毁，石头弹开，生物杀死
	/// </summary>
	private void Explode()
	{
		Debug.Log ("gel explode");
		Collider2D[] colls = Physics2D.OverlapCircleAll (transform.position, 1.5f,LayerMask.GetMask("AbstractGrid"));

		foreach (Collider2D coll in colls) {
			if (coll.gameObject != gameObject) {
				AbstractGrid ag = coll.GetComponent<AbstractGrid> ();
				if (ag != null) {
					if (ag.GetComponent<Wood> () != null)
						Destroy (ag.gameObject);
					else if (ag.tag == "Rock") {
						//ag.GetComponent<Rigidbody2D> ().AddForce (explodeForce * (ag.transform.position - transform.position).normalized,
							//ForceMode2D.Impulse);
							ag.GetComponent<Rock> ().DestroyOnExplosion ();
					}
				}
			}
		}
	}
		
	/// <summary>
	/// 判断凝胶的朝向
	/// </summary>
	public void SetDirection(bool left)
	{
		isLeft = left;
	//	Debug.Log ("no problem here");

		if (isLeft)
			sp.sprite = sprites [0];
		else
			sp.sprite = sprites [1];
	}

	#endregion
}
