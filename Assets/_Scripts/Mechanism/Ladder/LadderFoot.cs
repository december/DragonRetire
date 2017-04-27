using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderFoot : MonoBehaviour {


	void OnTriggerStay2D(Collider2D coll)
	{
		if (coll.tag == "Player") {
		
			coll.GetComponent<PrinceController> ().direction = -1;
			coll.GetComponent<PrinceController> ().onLadder = false;
			coll.GetComponent<PrinceController> ().state = 0;
			/*if (Input.GetKey (KeyCode.W)) {
				coll.GetComponent<PrinceController> ().onLadder = true;
				Vector3 tmp = coll.transform.position;
				coll.transform.position = new Vector3 (transform.position.x, tmp.y, tmp.z);
			} else if (Input.GetKey (KeyCode.S)) {
				coll.GetComponent<PrinceController> ().onLadder = false;
			}*/
		}

	}
}
