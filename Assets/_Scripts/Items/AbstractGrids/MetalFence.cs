using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//金属栅栏类
public class MetalFence : AbstractGrid {

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
		ability = Ability.Freezable;
	}
	/// <summary>
	/// 火焰魔法，解冻
	/// </summary>
	public  override bool OnFired()
	{
		if (state == State.Freezing) {
			state = State.Normal;
			Debug.Log ("MetalFence is fired");
			return true;
		}
		return false;
	}
	/// <summary>
	/// 冰冻魔法，冻结，可被王子击碎
	/// </summary>
	public  override bool OnFreezed()
	{
		if (state == State.Normal) {
			state = State.Freezing;
			Debug.Log ("MetalFence is freezed");
			return true;
		}
		return false;
	}
	/// <summary>
	/// 反重力无效
	/// </summary>
	public override  bool OnAntiGravity()
	{
		Debug.Log ("MetalFence is antigravity");
		return false;
	}
	public override void InteractWithPrince()
	{
	}
	#endregion
}
