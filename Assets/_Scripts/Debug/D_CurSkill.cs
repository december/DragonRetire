using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class D_CurSkill : MonoBehaviour {

	public SkillManager sm;
	public Text skillText;


	// Update is called once per frame
	void Update () {

		skillText.text = "鼠标右键取消技能\nCurrent Skill: \n" + sm.curSkill.Name.ToString ();
	}
}
