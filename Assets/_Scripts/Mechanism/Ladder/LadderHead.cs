using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderHead : MonoBehaviour {

	void OnTriggerStay2D(Collider2D coll)
	{
		if (coll.tag == "Player") {

			//if (Input.GetKey (KeyCode.S)) {
				coll.GetComponent<PrinceController> ().onLadder = true;
				Vector3 tmp = coll.transform.position;
				coll.transform.position = new Vector3 (transform.position.x, tmp.y, tmp.z);
			//}
			//else if (Input.GetKey (KeyCode.W)) {
			//	coll.GetComponent<PrinceController> ().onLadder = false;
			//}
		}

	}
}
