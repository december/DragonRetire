using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedalAndStone : MonoBehaviour {


	public MoveTween[] mts;
	private Pedal pedal;

	void Awake()
	{
		pedal = GetComponent<Pedal> ();
	}

	void OnTriggerStay2D(Collider2D coll)
	{
		if (coll.tag == "Player") {

			pedal.pressed = true;
			foreach (MoveTween mt in mts) {
				if (mt.GetComponent<SnapToGrid> () != null)
					mt.GetComponent<SnapToGrid> ().isSnapOn = false;

				if (pedal.state == State.Normal)
					mt.isOn = true;
			}
		}
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.tag == "Player") 
			pedal.pressed = false;	
	}
}
