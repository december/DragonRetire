using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Elevator : MonoBehaviour {


	public ElevatorPlaten platenL;
	public ElevatorPlaten platenR;
	public float speed;


	public int top; //电梯上到的最高高度
	public int bottom;//电梯下到的最低高度

	void Start()
	{

	}


	void FixedUpdate()
	{
	}

	public float ApplyForce(bool isleft)
	{
		float v = 0;
		float delta = platenL.totalMass - platenR.totalMass; // >0,左臂向下，右臂向上，<0,右臂向下，左臂向上
	//	Debug.Log(delta.ToString());

		if(Mathf.Abs(delta)>0.001f)
			 v = -Mathf.Sign (delta) * speed; //<0,左臂向下
			
		return isleft ? v : -v;
	}
}
