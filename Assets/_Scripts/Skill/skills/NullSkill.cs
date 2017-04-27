using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullSkill : MonoBehaviour,ISkill {


	private bool isNull;//是否为空技能
	private static string skillName = "Null";

	public static string GetName()
	{
		return skillName;
	}
	#region 实现接口
	/// <summary>
	/// 继承至ISkill
	/// </summary>
	public bool IsNull{
		get
		{
			return isNull;
		}
	}
	public string Name{
		get
		{
			return skillName;
		}
	}
	/// <summary>
	/// 初始化函数
	/// </summary>
	public void Initialization()
	{
		isNull = true;
	}


	// Use this for initialization
	void Start () {
		Initialization ();
	}

	/// <summary>
	/// 点击技能按钮后，显示指示器
	/// </summary>
	public void ShowIndicator()
	{

	}
	/// <summary>
	/// 执行技能
	/// </summary>
	public bool Execute()
	{
		return false;
	}
	/// <summary>
	/// 关闭指示器
	/// </summary>
	public void HideIndicator()
	{
	}
	#endregion
}
