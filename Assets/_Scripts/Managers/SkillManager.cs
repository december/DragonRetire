using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//技能管理类
[RequireComponent(typeof(FireSkill))]
[RequireComponent(typeof(IceSkill))]
[RequireComponent(typeof(AntiGSkill))]
[RequireComponent(typeof(NullSkill))]
[RequireComponent(typeof(WoodSkill))]
[RequireComponent(typeof(GelSkill))]
[RequireComponent(typeof(RockSkill))]

public class SkillManager : Singleton<SkillManager> {

	public ISkill curSkill;  //Fire,Ice,AntiG,Wood,Gel,Rock,Null

	public SkillCounter skillCounter; //技能计数器

	public RectTransform rectOfSkillPanel; //当鼠标点击时，在技能栏内不应该施放技能
	private Vector3 forRectUtility;

	private Dictionary<string,ISkill> my_dic; //存储技能的字典
	private Vector3 preMousePos=Vector3.zero;//上一帧鼠标位置;

	protected SkillManager(){
	}// 阻止使用new创建


	void Awake () {
		skillCounter = new SkillCounter ();
		Initilization ();
	
	}
	
	// Update is called once per frame
	void Update () {

		if (curSkill.IsNull) {
			Cursor.visible = true;
		} else
			Cursor.visible = false;

		//右键取消技能
		if (Input.GetMouseButtonDown (1)) {
			curSkill.HideIndicator ();
			curSkill = my_dic [NullSkill.GetName()];
			preMousePos = Vector3.zero;
		}


	//执行非null技能
		if (!curSkill.IsNull) {
			curSkill.ShowIndicator ();
			if (Input.GetMouseButton (0) && !RectTransformUtility.RectangleContainsScreenPoint (rectOfSkillPanel, Input.mousePosition)) {
				if (Vector3.Magnitude(Input.mousePosition - preMousePos) > 5f)
				if (curSkill.Execute ()) { 
					skillCounter.AddOne (curSkill.Name);
				}

				preMousePos = Input.mousePosition;
				//成功执行后，变回空技能
				//curSkill.HideIndicator ();
				//curSkill = my_dic [NullSkill.GetName ()];
			} else
				preMousePos = Vector3.zero;
		}
				
		
	}

	/// <summary>
	/// 初始化技能字典和技能计数器
	/// </summary>
	private void Initilization()
	{
		ISkill[] skills = GetComponents<ISkill> ();
		my_dic = new Dictionary<string, ISkill> ();

		foreach (ISkill skill in skills) {
			my_dic.Add (skill.Name, skill);
		}

		curSkill = my_dic[NullSkill.GetName()];
		//Debug.Log (my_dic.Count.ToString());
		skillCounter.Initialization (my_dic);
	}




	#region
	// 留给技能UI的接口
	public void UseFireSkill()
	{
		curSkill.HideIndicator ();
		curSkill = my_dic[FireSkill.GetName()];
	}
	public void UseIceSkill()
	{
		curSkill.HideIndicator ();
		curSkill = my_dic[IceSkill.GetName()];
	}
	public void UseAntiGSkill()
	{
		curSkill.HideIndicator ();
		curSkill = my_dic[AntiGSkill.GetName()];
	}
	public void UseWoodSkill()
	{
		curSkill.HideIndicator ();
		curSkill = my_dic[WoodSkill.GetName()];
	}
	public void UseGelSkill()
	{
		curSkill.HideIndicator ();
		curSkill = my_dic[GelSkill.GetName()];
	}

	public void UseRockSkill()
	{
		curSkill.HideIndicator ();
		curSkill = my_dic[RockSkill.GetName()];
	}
	#endregion


}
