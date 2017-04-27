using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
//王子类
public class Prince : AbstractGrid {

	#region Variables
	//private int hp;
	private Rigidbody2D m_rb;
	private bool faceRight;
	private PrinceController pc;
	private HpManager hpM;

	public Animator buff;
	public GameObject iceCube;
	public bool onElevator;

	#endregion

	#region Unity Events
	void Awake()
	{
		m_rb = GetComponent<Rigidbody2D> ();
		hpM = FindObjectOfType<HpManager> ();
		pc = GetComponent<PrinceController> ();
	}
	void Start()
	{
		Initialization ();
	}
	#endregion

	#region Methods

	public override void Initialization ()
	{
		state = State.Normal;
		ability = Ability.Flammable | Ability.Freezable | Ability.AntiGable;
		faceRight = true;
	}
	/// <summary>
	/// 火焰魔法，受伤掉血，（冻结时）解冻
	/// </summary>
	public  override bool OnFired()
	{
		if (state == State.Normal) {
			state =State.Firing;
			buff.SetTrigger ("Fire");
			StartCoroutine (FireEvent ());
			Debug.Log ("Prince is fired");
			return true;
		} else if (state == State.Freezing) {
			state = State.Normal;
			iceCube.SetActive (false);
			pc.enabled = true;
			buff.SetTrigger ("Normal");
			return true;
		}
		return false;
	}
	/// <summary>
	/// 冰冻魔法，冻结掉血，数秒后解冻
	/// </summary>
	public  override bool OnFreezed()
	{
		if (state == State.Normal) {
			state = State.Freezing;
			iceCube.SetActive (true);
			pc.enabled = false;
			buff.SetTrigger ("Ice");
			Debug.Log ("Prince is freezed");
			return true;
		}
		return false;
	}
	/// <summary>
	/// 反重力，数秒内下落速度减慢，无法触发机关
	/// </summary>
	public override  bool OnAntiGravity()
	{
		if (state == State.Normal) {
			state = State.AntiGing;
			buff.SetTrigger ("Antig");
			StartCoroutine (AntiGEvent ());
			Debug.Log ("Prince is antigravity");
			return true;
		}
		return false;
	}

	/*
	/// <summary>
	/// 计算屏幕坐标，供shader用
	/// </summary>
	/// <returns>The position on screen.</returns>
	public Vector3 ComputePosOnScreen()
	{
		Vector3 pos = Camera.main.WorldToScreenPoint (transform.position);
		pos.x /= Screen.width;
		pos.y /= Screen.height;

		pos.x *= (float)Screen.width / Screen.height;

		return pos;
	}*/
	//反重力处理
	private IEnumerator AntiGEvent()
	{
		m_rb.mass = 1.5f;
		yield return new WaitForSeconds (5);
		m_rb.mass = 2f;
		state = State.Normal;
		buff.SetTrigger ("Normal");
		Debug.Log ("Prince antiG end");
	}

	//火焰处理
	private IEnumerator FireEvent()
	{
		//hpM.HpAdd (-1);
		yield return new WaitForSeconds (1);
		state = State.Normal;
		buff.SetTrigger ("Normal");
		Debug.Log ("Prince firing end");
	}

	//转身
	public void TurnAround(float h)
	{
		if (h > 0) {
			faceRight = true;
			Vector3 tmp =transform.localScale;
			tmp.x = -Mathf.Abs (tmp.x);
			transform.localScale = tmp;
			//GetComponent<SpriteRenderer> ().flipX = false;
		} else if(h<0) {
			faceRight = false;
			Vector3 tmp =transform.localScale;
			tmp.x = Mathf.Abs (tmp.x);
			transform.localScale = tmp;
			//GetComponent<SpriteRenderer> ().flipX = true;
		}
	}
	public override void InteractWithPrince()
	{
	}
	public void Interact()
	{
		if (faceRight) {
			Collider2D coll = Physics2D.OverlapPoint (transform.position + new Vector3 (1f, 0.5f, 0),LayerMask.GetMask("AbstractGrid"));
			if (coll != null) {
				coll.GetComponent<AbstractGrid> ().InteractWithPrince ();
			}
		} else {
			Collider2D coll = Physics2D.OverlapPoint (transform.position + new Vector3 (-1f, 0.5f, 0),LayerMask.GetMask("AbstractGrid"));
			if (coll != null) {
				coll.GetComponent<AbstractGrid> ().InteractWithPrince ();
			}
		}
	}

	void OnCollisionStay2D(Collision2D coll)
	{
		Rock tmpRock = coll.collider.GetComponent<Rock> (); 

		if (tmpRock != null) {


			if (tmpRock.onElevator) //踩在电梯的石头上
				onElevator = true;

			//石头下砸到玩家
			Vector2 dir = tmpRock.transform.position - (transform.position +Vector3.up*0.6f);

			if (Vector2.Dot (dir.normalized, Vector2.up) > 0.9f ) {
				hpM.HpAdd (-1);
				Destroy (tmpRock.gameObject);
			}

			GameObject.FindObjectOfType<PrinceController> ().obstacle = tmpRock.gameObject;
			Debug.Log ("obstacle");
		}

		Wood tmpWood = coll.collider.GetComponent<Wood> ();
		if (tmpWood != null) {
			foreach (ContactPoint2D c in coll.contacts)
			{
				//Debug.Log (c.point.y - tmpWood.transform.position.y);
				if ((c.point.y - tmpWood.transform.position.y) < 0) {
					GameObject.FindObjectOfType<PrinceController> ().obstacle = tmpWood.gameObject;
					Debug.Log ("obstacle");
					break;
				}
			}
		}

		Treasure tmpTreasure = coll.collider.GetComponent<Treasure> ();
		if (tmpTreasure != null)
			Interact ();
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll != null) {


			if (coll.collider.GetComponent<ElevatorPlaten> () != null)//站在电梯上
				onElevator = true;
			
			Rock tmpRock = coll.collider.GetComponent<Rock> (); 

			if (tmpRock != null) {


				if (tmpRock.onElevator) //踩在电梯的石头上
					onElevator = true;
				
				//石头下砸到玩家
				Vector2 dir = tmpRock.transform.position - (transform.position +Vector3.up*0.6f);

				if (Vector2.Dot (dir.normalized, Vector2.up) > 0.9f ) {
					hpM.HpAdd (-1);
					Destroy (tmpRock.gameObject);
				}

				GameObject.FindObjectOfType<PrinceController> ().obstacle = tmpRock.gameObject;
				Debug.Log ("obstacle");
			}

			Wood tmpWood = coll.collider.GetComponent<Wood> ();
			if (tmpWood != null) {
				coll.collider.GetComponent<Wood> ().holdPrince = true;
				foreach (ContactPoint2D c in coll.contacts)
				{
					//Debug.Log (c.point.y - tmpWood.transform.position.y);
					if ((c.point.y - tmpWood.transform.position.y) < 0) {
						GameObject.FindObjectOfType<PrinceController> ().obstacle = tmpWood.gameObject;
						coll.collider.GetComponent<Wood> ().holdPrince = false;
						Debug.Log ("obstacle");
						break;
					}
				}
				if (coll.collider.GetComponent<Wood> ().holdPrince)
					GameObject.FindObjectOfType<PrinceController> ().onWood = true;
			}

			Treasure tmpTreasure = coll.collider.GetComponent<Treasure> ();
			if (tmpTreasure != null)
				Interact ();

			//背门砸死
			if (coll.gameObject.GetComponent<AbstractGrid> () == null && coll.collider.tag !="Ladder") {

				Vector2 dir = coll.transform.position - (transform.position+Vector3.up*0.6f);
				if (Vector2.Dot (dir.normalized, Vector2.up) > 0.9f )
				{
					LevelManager.Instance.BackToLastCheckPoint ();
				}
			}

		}


		}

	void OnCollisionExit2D(Collision2D coll)
	{
		if (coll != null) {
			if (coll.collider.GetComponent<ElevatorPlaten> () != null)
				onElevator = false;

			Rock tmpRock = coll.collider.GetComponent<Rock> (); 

			if (tmpRock != null) {

				if (tmpRock.onElevator) //离开电梯的石头上
					onElevator = false;
			}

			Wood tmpWood = coll.collider.GetComponent<Wood> ();
			if (tmpWood != null && coll.collider.GetComponent<Wood> ().holdPrince) 
				GameObject.FindObjectOfType<PrinceController> ().onWood = false;
		}

	}
	#endregion
}
