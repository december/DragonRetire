using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow: MonoBehaviour {

	public Transform target;

	public float smoothTime;
	public Vector3 delta;



	void Start()
	{
	}
	void FixedUpdate () {

		Vector3 currentV = Vector3.zero;
		Vector3 tar = target.position+delta;

		transform.position = Vector3.SmoothDamp (transform.position,tar, ref  currentV, smoothTime);
	}
}
