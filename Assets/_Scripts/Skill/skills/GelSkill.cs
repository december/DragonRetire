using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GelSkill :MonoBehaviour,ISkill{

	private bool isNull;//是否为空技能
	private static  string skillName = "Gel";
	private bool isReady;

	public GameObject gelPrefab;
	public GameObject gelIndicator;

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
			gelIndicator.SetActive (true);
			gelIndicator.transform.position = pos+new Vector3(0,0,-1);//指示器放在上方
		

			if (Input.GetKeyDown (KeyCode.Space)) //按空格键更换凝胶的方向
				gelIndicator.GetComponent<GelIndicator> ().ChangeSprite ();

			Collider2D coll = Utility.GetMouseTargetAbstractGrid (pos);
			if (coll == null) {
				if (FindNearByGelFriend (pos)) {
					gelIndicator.GetComponent<SpriteRenderer> ().color = Color.blue*0.5f;
					isReady = true;
				} else
					gelIndicator.GetComponent<SpriteRenderer> ().color = Color.red*0.5f;
			} else
				gelIndicator.GetComponent<SpriteRenderer> ().color = Color.red*0.5f;
		} else
			HideIndicator ();
	}
	/// <summary>
	/// 执行技能
	/// </summary>
	public bool Execute()
	{
		if (isReady) {
			Vector3 pos = Utility.SnappingGrid (Input.mousePosition);
			GameObject newGel = Instantiate (gelPrefab, pos, Quaternion.identity);
			newGel.GetComponent<Gel> ().SetDirection (gelIndicator.GetComponent<GelIndicator> ().isLeft);
			isReady = false;
			return true;
		} else
			return false;
	}
	/// <summary>
	/// 关闭指示器
	/// </summary>
	public void HideIndicator()
	{
		isReady = false;
		gelIndicator.SetActive (false);
	}

	#endregion

	/// <summary>
	/// 凝胶是否可以放在下方的材质之上
	/// </summary>
	private bool FindNearByGelFriend(Vector3 pos)
	{
		Vector3 tmp = pos ;
		AbstractGrid ag;
		Collider2D coll;

		coll = Physics2D.OverlapPoint (new Vector2 (tmp.x , tmp.y-0.6f),Utility.lmForSkill);
		if (coll != null) {
			ag = coll.GetComponent<AbstractGrid> ();
			if (ag != null && (ag.ability&Ability.WoodFriendly)!=0)
				return true;
		}

		return false;
	}

}
