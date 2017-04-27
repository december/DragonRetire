using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_AddForce : MonoBehaviour {

	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		rb.AddForce (-Physics2D.gravity * rb.gravityScale * rb.mass);
	//	rb.AddForce (-Physics2D.gravity * 5 * 3);
	}
}
