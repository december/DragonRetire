using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//技能计数器
public class SkillCounter  {

	private  Dictionary<string,int> dic_counter;

	/// <summary>
	/// Initialization this instance.
	/// </summary>
	public void Initialization(Dictionary<string,ISkill> dic)
	{
		dic_counter = new Dictionary<string,int> ();

		foreach (string s in dic.Keys) {
			dic_counter.Add (s, 0);
		}
	}
	public void Reset()
	{
		List<string> myKeys = new List<string> ();
		foreach (string s in dic_counter.Keys) {
			myKeys.Add (s);
		}
		dic_counter.Clear ();
		foreach (string s in myKeys) {
			dic_counter.Add (s,0);
		}
		myKeys.Clear ();
	}
	public void AddOne(string s)
	{
		dic_counter [s] += 1;
	}

	public int FindNum(string s)
	{
		if (dic_counter.ContainsKey (s))
			return dic_counter [s];
		else
			return 0;
	}
}
