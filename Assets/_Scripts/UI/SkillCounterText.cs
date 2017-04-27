using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillCounterText : MonoBehaviour {

	private Text my_text;
	private SkillManager skill_manager;
	public string skillName;

	void Start()
	{
		my_text = GetComponent<Text> ();
		my_text.text = string.Empty;
		skill_manager = FindObjectOfType<SkillManager> ();
	}
	// Update is called once per frame
	void Update () {
		my_text.text = skill_manager.skillCounter.FindNum (skillName).ToString ();
	}
}
