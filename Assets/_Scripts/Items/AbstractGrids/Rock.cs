using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//石头类
[RequireComponent(typeof(Rigidbody2D))]

public class Rock : AbstractGrid {

	#region Variables

	public bool onElevator; // 是否在盘子上
	public GameObject iceCube;
	public Animator fireAni;
	public Animator buff;
	private Rigidbody2D m_rb;

	private float originMass;




	#endregion

	#region Unity Events
	void Awake()
	{

		m_rb = GetComponent<Rigidbody2D> ();
	}

	void Start()
	{
		Initialization ();
	}
	#endregion

	public override void Initialization ()
	{
		state = State.Normal;
		ability = Ability.Flammable | Ability.Freezable | Ability.AntiGable;
		originMass = m_rb.mass;

		//GameObject.FindObjectOfType<PrinceController> ().newObj = this.gameObject;
	}
	/// <summary>
	/// 火焰魔法，解冻
	/// </summary>
	public  override bool OnFired()
	{
		if (state == State.Freezing) {
			state = State.Normal;
			buff.SetTrigger ("Normal");
			fireAni.SetTrigger ("Fire");
			iceCube.SetActive (false);
			Debug.Log ("Rock is fired");
			return true;
		}
		return false;
	}
	/// <summary>
	/// 冰冻魔法，冻结,，可被王子击碎
	/// </summary>
	public  override bool OnFreezed()
	{
		if (state == State.Normal) {
			state = State.Freezing;
			buff.SetTrigger ("Ice");
			iceCube.SetActive (true);
			Debug.Log ("Rock is freezed");

			GameObject.FindObjectOfType<PrinceController> ().newObj = this.gameObject;
			return true;
		}
		return false;
	}
	/// <summary>
	/// 反重力,下落速度变慢，无法触发开关，可浮到水面
	/// </summary>
	public override  bool OnAntiGravity()
	{
		if (state == State.Normal) {
			state = State.AntiGing;
			buff.SetTrigger ("Antig");
			Debug.Log ("Rock is antiG");
			StartCoroutine (AntiGravityEvent ());
			return true;
		}
		return false;
	}


	public override void InteractWithPrince()
	{
		if (state==State.Freezing)
			Destroy (gameObject);
	}

	private IEnumerator AntiGravityEvent()
	{
		m_rb.mass = originMass/10.0f;
		yield return new WaitForSeconds (3);// 3s 重力效果减半
		m_rb.mass = originMass;
		state = State.Normal;
		buff.SetTrigger ("Normal");
		Debug.Log ("rock antiG end");
	}


	//石头类中跟电梯相关的实现，目的是石头叠加计算重量
	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll != null) {
			if (coll.collider.tag == "Rock" ) {
				Rock rock = coll.collider.GetComponent<Rock> ();
				if (rock.onElevator == true)
					onElevator = true;
			}

			if (coll.collider.GetComponent<ElevatorPlaten> ())
				onElevator = true;
		}
	}

	/// <summary>
	/// Destroies the on explosion.
	/// </summary>
	public void DestroyOnExplosion()
	{
		m_rb.simulated = false;
		StartCoroutine (DestroyDelay());
	}

	private IEnumerator DestroyDelay()
	{
		yield return new WaitForSeconds (0.15f);
		Debug.Log ("Corutine is on");
		Destroy (gameObject);
	}
}
