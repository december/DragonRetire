using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedalAndGates : MonoBehaviour {

	public MoveTween mt1; // end to start
	public MoveTween mt2; // start to end
	private Pedal pedal;

	void Awake()
	{
		pedal = GetComponent<Pedal> ();
	}




	void OnTriggerStay2D(Collider2D coll)
	{
		if (pedal.state == State.Normal) {
			if (coll.tag == "Player" || coll.GetComponent<Rock> () != null) {
				pedal.pressed = true;
				mt1.isOn = true;
				mt1.back = true;

				mt2.isOn = true;
				mt2.back = false;
			}
		}
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		if (pedal.state == State.Normal) {
			pedal.pressed = false;
			if (coll.tag == "Player" || coll.GetComponent<Rock> () != null) {
				mt1.isOn = true;
				mt1.back = false;

				mt2.isOn = true;
				mt2.back = true;
			}
		}
	}
}
