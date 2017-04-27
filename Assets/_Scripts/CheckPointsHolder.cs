using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 包含本场景中的所有check points
/// </summary>
public class CheckPointsHolder : MonoBehaviour {

	#region Variables
	public CheckPoint[] checkPoints;
	[SerializeField]private int curID = 0; // 目前进度到第几个触发点
	#endregion


	#region Property
	public int Index{
		get {
			return curID;
		}
	}
	#endregion
	#region Unity Events

	// Use this for initialization
	void Start () {

		for (int i = 0; i < checkPoints.Length; i++) {
			checkPoints [i].ID = i;
		}

	}
	#endregion
	#region Methods
	public void MoveNext(int id)
	{
		if (id < checkPoints.Length && id > curID)
			curID = id;
	}
	#endregion
}
