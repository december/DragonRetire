using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//定义格子的状态
public enum State
{
	Normal,Firing,Freezing,AntiGing
};

//定义格子的可交互性
public static class Ability
{
	public const short Flammable = 0x01;  //System.Int16 = short
	public const short  Freezable = 0x02; 
	public const short AntiGable = 0x04;
	public const short WoodFriendly = 0x08;
}


[RequireComponent(typeof(SnapToGrid))]//自动挂载吸附网格组件

public abstract class AbstractGrid: MonoBehaviour {

	#region 变量，定义格子的特性
	[SerializeField]
	public State state;
	[SerializeField]
	public short ability;
	#endregion

	/*
	public bool freezed = false;   //是否冰冻
	public bool destructable = false; //是否可破坏
	public bool antiGravity = false;  //是否处于反重力状态
	public bool antiGable = false;//是否可以被反重力作用
	public bool woodFriendly = false;//旁边是否可以放木块
	public bool flammable = false; //是否可燃
	public bool onFireNow = false; // 现在在燃烧么
	*/

	#region 方法
	public abstract void Initialization ();//初始化

	public abstract bool OnFired();   //被燃烧时调用，返回true代表技能施放正常
	public abstract bool OnFreezed(); //被冰冻，返回true代表技能施放正常
	public abstract bool OnAntiGravity(); //实施反重力，返回true代表技能施放正常

	public abstract void InteractWithPrince();//和王子相互作用
	#endregion
}
