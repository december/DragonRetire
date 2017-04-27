using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 一些公用的工具类静态函数
/// </summary>
public class Utility {


	public static LayerMask lmForSkill = LayerMask.GetMask("AbstractGrid","Trigger","Player"); 


	/// <summary>
	/// 指示器的位置确定，实现按格放置
	/// </summary>

	public static Vector3  SnappingGrid(Vector3 mousePos)
	{
		mousePos = Camera.main.ScreenToWorldPoint (mousePos);
		mousePos.z = 0;

		return new Vector3 (Mathf.Round(mousePos.x ),Mathf.Round (mousePos.y),mousePos.z);
	}

	/// <summary>
	/// 鼠标悬停处在光照范围内么(Light层)
	/// </summary>

	static public bool WithinLightRange()  
	{
		Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);

		if (Physics2D.OverlapPoint (pos, LayerMask.GetMask ("Light")) != null) //这点在光的范围内，返回光的碰撞体
			return true;
		else
			return false;
	}


	/// <summary>
	/// 返回当前鼠标位置的2d碰撞体(abstractgrid层，//改为lmForSkill层了，名字未改）
	/// </summary>

	static public Collider2D GetMouseTargetAbstractGrid()
	{
		Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		return Physics2D.OverlapPoint (pos,lmForSkill);
	}
	public static Collider2D GetMouseTargetAbstractGrid(Vector3 pos) 
	{
		return Physics2D.OverlapCircle (pos,0.4f,lmForSkill);
	}

	/// <summary>
	/// 返回当前鼠标位置的2d碰撞体
	/// </summary>

	public static Collider2D GetMouseTarget()
	{
		Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		return Physics2D.OverlapPoint (pos);
	}


}
