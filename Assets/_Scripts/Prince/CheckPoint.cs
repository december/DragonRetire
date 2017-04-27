using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CheckPoint : MonoBehaviour {


	#region Variable

//	private BoxCollider2D boxCollider;
	private CheckPointsHolder cpHolder;
	public int ID;
	#endregion



	#region Unity Events
	void Awake() 
	{
	//	boxCollider = GetComponent<BoxCollider2D> ();
		cpHolder = FindObjectOfType<CheckPointsHolder> ();
	}
	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.tag == "Player") {
			cpHolder.MoveNext (ID); //将触发点的ID作为参数传入
		}
	}
	#endregion
}
