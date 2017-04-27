using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSkill : MonoBehaviour,ISkill{

	private bool isNull;//是否为空技能
	private static string skillName = "Ice";
	private bool isReady;

	//public GameObject iceSprite;

	//public Texture2D cursorIcon;
	public GameObject iceIndicator;

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
		iceIndicator.SetActive (false);
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
			//Cursor.SetCursor (cursorIcon, new Vector2 (8, 8), CursorMode.Auto);
			Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			pos.z = -1;
			iceIndicator.transform.position =  pos ;
			iceIndicator.SetActive (true);
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
			if (coll.GetComponent<AbstractGrid> ().OnFreezed ()) {
				//nstantiate (iceSprite, coll.transform.position+new Vector3(0,0,-2), Quaternion.identity);
				return true;
			}
		}
		return false;
	}

	/// <summary>
	/// 关闭指示器
	/// </summary>
	public void HideIndicator()
	{
		isReady = false;
		//Cursor.SetCursor (null, Vector2.zero, CursorMode.Auto);
		iceIndicator.SetActive(false);
	}
	#endregion
}
