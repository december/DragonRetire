using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSpikeweeds : MonoBehaviour {

	public MovableSpike ms;

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.tag == "Player")
			ms.triggered = true;
	}
}
