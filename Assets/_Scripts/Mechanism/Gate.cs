using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour {

	private MoveTween mt;

	void Awake()
	{
		mt = GetComponent<MoveTween> ();

	}
	void FixedUpdate()
	{
		mt.isOn = true;
		Collider2D[] colls = Physics2D.OverlapCircleAll (transform.position + Vector3.down*0.5f, 0.05f);
		if (colls != null) {
			foreach(Collider2D coll in colls)
			{
				if (coll.tag == "Player" || coll.tag == "Rock"||coll.tag == "Wood") {
					mt.isOn = false;
				}
			} 
		}	//mt.isOn = true;

	}
}
