using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill  {

	bool IsNull {
		get;
	}
	string Name {
		get;
	}

	void Initialization();
	void ShowIndicator ();
	bool Execute(); //成功执行，返回true
	void HideIndicator();
}
