using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSkill : MonoBehaviour,ISkill{

	private bool isNull;//是否为空技能
	private bool isReady;
	private static string skillName = "Rock";

	public GameObject rockPrefab;
	public GameObject rockIndicator;


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
			Vector3 pos = Utility.SnappingGrid (Input.mousePosition);
			rockIndicator.SetActive (true);
			rockIndicator.transform.position = pos+new Vector3(0,0,-1);


			Collider2D coll = Utility.GetMouseTargetAbstractGrid (pos);
			if (coll == null) {
				rockIndicator.GetComponent<SpriteRenderer> ().color = Color.blue*0.5f;
				isReady = true;
			} else
				rockIndicator.GetComponent<SpriteRenderer> ().color = Color.red*0.5f;
		} else
			HideIndicator ();
		
	}
	/// <summary>
	/// 关闭指示器
	/// </summary>
	public void HideIndicator()
	{
		isReady = false;
		rockIndicator.SetActive (false);
	}
	/// <summary>
	/// 执行技能
	/// </summary>
	public bool Execute()
	{
		if (isReady) {
			Vector3 pos = Utility.SnappingGrid (Input.mousePosition);
			Instantiate (rockPrefab, pos, Quaternion.identity);
			isReady = false;
			return true;
		} else
			return false;
	}

	#endregion
}
