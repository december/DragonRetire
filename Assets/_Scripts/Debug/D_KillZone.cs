using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class D_KillZone : MonoBehaviour {


	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.tag == "Player") {
			LevelManager.Instance.BackToLastCheckPoint ();
		}
		else if (coll.tag == "Rock")
			Destroy (coll.gameObject);
	}
		
}
