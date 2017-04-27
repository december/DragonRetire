 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 火焰技能类
/// </summary>
public class FireSkill :MonoBehaviour,ISkill{


	//public Texture2D cursorIcon;
	public GameObject fireIndicator;

	private bool isNull;//是否为空技能
	private static  string skillName = "Fire";
	private bool isReady;


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
		isNull = false;
		isReady = false;
		fireIndicator.SetActive (false);
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
		isReady = false;
		if (Utility.WithinLightRange ()) {
		//	Cursor.SetCursor (cursorIcon, new Vector2 (8, 8), CursorMode.Auto);
			fireIndicator.SetActive(true);
			Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			pos.z = -1;
			fireIndicator.transform.position = pos;//指示器放在上方
			isReady = true;
		}
		else
			HideIndicator ();
	}
	/// <summary>
	/// 执行技能
	/// </summary>
	public bool Execute()
	{
		if (isReady) {
			Collider2D coll = Utility.GetMouseTargetAbstractGrid ();
			if (coll != null)
				return coll.GetComponent<AbstractGrid> ().OnFired ();
		}
		return false;
			
	}

	/// <summary>
	/// 关闭指示器
	/// </summary>
	public void HideIndicator()
	{
		fireIndicator.SetActive (false);
		isReady = false;
	}
	#endregion
}
