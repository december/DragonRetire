using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//宝箱类
public class Treasure : AbstractGrid {


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
		ability = Ability.Flammable;
	}
	/// <summary>
	/// 火焰魔法，燃烧，半秒后点燃周围物体，1s后宝箱自毁（里面的道具也消失）
	/// </summary>
	public  override bool OnFired()
	{
		if (state == State.Normal) {
			StartCoroutine (FireEvent ());
			Debug.Log ("Treasure is fired");
			return true;
		}
		return false;
	}
	/// <summary>
	/// 冰冻魔法，无效
	/// </summary>
	public  override bool OnFreezed()
	{
		Debug.Log ("Treasure can‘t freezed");
		return false;
	}
	/// <summary>
	/// 反重力无效
	/// </summary>
	public override  bool OnAntiGravity()
	{
		Debug.Log ("Treasure is antigravity");
		return false;
	}
	/// <summary>
	/// 王子打开后会怎样
	/// </summary>
	public override void InteractWithPrince()
	{
		DestroySelf ();
	}

	/// <summary>
	/// 搜索周围一格内的物体,并使之燃烧
	/// </summary>
	private void FireNearBy()
	{
		Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position+new Vector3(0.5f,0.5f,0f),1);

		foreach (Collider2D coll in colls) 
		{
			if (coll.gameObject != gameObject) 
			{
				AbstractGrid ag = coll.GetComponent<AbstractGrid> ();
				if (ag != null &&(ag.ability&Ability.Flammable )!=0)
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
		yield return new WaitForSeconds (0.5f);
		FireNearBy ();
		yield return new WaitForSeconds (0.5f);
		DestroySelf ();
	}
	private void DestroySelf()
	{
		Destroy (gameObject);
	}
		
	#endregion
}
