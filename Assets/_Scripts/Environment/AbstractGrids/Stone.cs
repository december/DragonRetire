using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//岩石类（地面）
public class Stone : AbstractGrid {

	#region 变量

	public PhysicsMaterial2D smooth;//冰冻时切换物理材质
	public GameObject iceCube;
	public Animator fireAni;
	public Animator buff;

	//获取组件
	private BoxCollider2D coll;


	//是否开始就处于冰冻
	public bool startWithFreezed;

	#endregion

	#region Unity事件
	void Awake()
	{
		coll = GetComponent<BoxCollider2D> ();

	}
	void Start()
	{
		Initialization ();
		if (startWithFreezed)
			OnFreezed ();
	}
	#endregion

	#region 方法

	/// <summary>
	/// 可点燃，可冰冻，可生长木块
	/// </summary>
	public override void Initialization ()
	{
		state = State.Normal;
		ability = Ability.WoodFriendly | Ability.Flammable | Ability.Freezable;
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
			coll.sharedMaterial = null;  //恢复默认材质
			Debug.Log ("Stone is fired");
			return true;
		}

		return false;
	}
	/// <summary>
	/// 冰冻魔法，冻结
	/// </summary>
	public  override bool OnFreezed()
	{
		if (state == State.Normal) {
			state = State.Freezing;
			iceCube.SetActive (true);
			buff.SetTrigger ("Ice");
			coll.sharedMaterial = smooth;
			GameObject.FindObjectOfType<PrinceController> ().newObj = this.gameObject;
			return true;
//			Debug.Log ("Stone is freezed");
		}
		return false;
	}
	/// <summary>
	/// 反重力无效
	/// </summary>
	public override  bool OnAntiGravity()
	{
		return false;
	}
	public override void InteractWithPrince()
	{
		
	}

	#endregion
}
