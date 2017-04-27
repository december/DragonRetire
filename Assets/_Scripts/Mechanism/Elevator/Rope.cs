using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour {

	public Transform platen;

	private Vector3  originalPos;
	private float originalPlatenY;

	void Start()
	{
		originalPlatenY = platen.position.y;
		originalPos= transform.position;
	}

	void FixedUpdate()
	{
		float delta = platen.position.y - originalPlatenY;

		transform.position = originalPos + delta * Vector3.up;
	
	}
}
