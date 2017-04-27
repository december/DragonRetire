using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//地刺陷阱
public class Spikeweed : AbstractGrid {

	#region Unity Events
	void Start()
	{
		Initialization ();
	}
	#endregion

	#region Methods

	public override void Initialization ()
	{
		state = State.Normal;
		ability = Ability.Flammable | Ability.Freezable;
	}
	/// <summary>
	/// 火焰魔法，解冻
	/// </summary>
	public  override bool OnFired()
	{
		if (state == State.Freezing) {
			state = State.Normal;
			Debug.Log ("Trap is fired");
			return true;
		}
		return false;
	}
	/// <summary>
	/// 冰冻魔法，冻结,失效
	/// </summary>
	public  override bool OnFreezed()
	{
		if (state == State.Normal) {
			state = State.Freezing;
			Debug.Log ("Trap is freezed");
			return true;
		}
		return false;
	}
	/// <summary>
	/// 反重力无效
	/// </summary>
	public override  bool OnAntiGravity()
	{
	//Debug.Log ("Stone is antigravity");
		return false;
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if(state == State.Normal && coll.tag =="Player")
			Debug.Log ("Kill creature");
	}
	public override void InteractWithPrince()
	{
	}
	#endregion
}
