using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//木块类

[RequireComponent(typeof(BoxCollider2D))]
public class Wood : AbstractGrid {

	#region Variables
	public GameObject iceCube;
	public Animator fireAni;
	public Animator buff;


	private float timeCounter;
	private bool fireOnce;
	private GameObject woodLight;
	public bool holdPrince = false;

	#endregion

	#region Unity Events
	void Awake()
	{
		woodLight = transform.FindChild ("WoodLight").gameObject;
	}
	void Start()
	{
		

		timeCounter = 0;
		fireOnce = false; //避免重复多次搜寻周围物体

		Initialization ();
	}
	void Update()
	{
		if(state == State.Firing)
		{
			timeCounter += Time.deltaTime;
			FireEvent ();
		}
	}
	#endregion

	#region Methods
	public override void Initialization ()
	{
		state = State.Normal;
		ability = Ability.WoodFriendly | Ability.Flammable | Ability.Freezable;
		GameObject.FindObjectOfType<PrinceController> ().newObj = this.gameObject;
	}
	/// <summary>
	/// 火焰魔法，解冻，燃烧，1s后引燃相邻物体
	/// </summary>
	public  override bool OnFired()
	{
		if (state == State.Freezing) {
			state = State.Normal;
			buff.SetTrigger ("Normal");
			iceCube.SetActive (false);
			return true;
			} 
		else if (state == State.Normal) {
			state = State.Firing;
			buff.SetTrigger ("Fire");
			fireAni.SetTrigger ("Fire");
			woodLight.SetActive (true);
			return true;
		}
		return false;
	}
	/// <summary>
	/// 冰冻魔法，冻结
	/// </summary>
	public  override bool OnFreezed()
	{
		//Debug.Log ("Wood is freezed");
		if (state == State.Normal) {
			state = State.Freezing;
			buff.SetTrigger ("Ice");
			iceCube.SetActive (true);
			return true;
		}
		return false;
	}
	/// <summary>
	/// 反重力,降低重力的影响
	/// </summary>
	public override  bool OnAntiGravity()
	{
		/*
		//Debug.Log ("Wood is antiG");
		if(state == State.Normal)
		{
			state = State.AntiGing;
			StartCoroutine (AntiGravityEvent()); // 2s 重力效果减半
			return true;
		}*/
		return false;
	}
	public override void InteractWithPrince()
	{
	}

	/// <summary>
	/// 搜索周围一格内的物体,并使之燃烧
	/// </summary>
	private void FireNearBy()
	{
		Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position,1);

		foreach (Collider2D coll in colls) 
		{
			if (coll.gameObject != gameObject) 
			{
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

	/*协程太耗性能（是没有正确使用。。。。）
	private IEnumerator FireEvent()
	{
		yield return new WaitForSeconds (0.5f);
		FireNearBy ();
		yield return new WaitForSeconds (0.5f);
		this.enabled = false;
	}
*/
	private void FireEvent()
	{
		if (timeCounter > 0.5f && !fireOnce) {
			FireNearBy ();
			fireOnce = true;
		}
		if (timeCounter > 1.4f) //不是很有用的部分，因为没设定熄灭火焰
			this.enabled = false;
	}





	private IEnumerator AntiGravityEvent()
	{
		yield return new WaitForSeconds (2);

		state = State.Normal;
	}
	void OnDisable() //组件disable时，摧毁该对象
	{
		Destroy (gameObject);
	}
	#endregion
}
