using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class SnapToGrid : MonoBehaviour {

	public bool isSnapOn;

	// LateUpdate is called once per frame
	void LateUpdate () {

		if (isSnapOn) {
			Vector3 pos = transform.position;
			transform.position = new Vector3 (Mathf.Round (pos.x), Mathf.Round (pos.y), Mathf.Round (pos.z));
		}

	}
}
