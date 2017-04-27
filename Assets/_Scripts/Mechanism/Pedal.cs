using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedal : AbstractGrid {
	#region var
	public Sprite[] sprites;  //0 : normal , 1:pressed
	public GameObject iceCube;
	public bool startWithIce;
	public bool pressed;
	public Animator fireAni;

	private SpriteRenderer sp;
	private BoxCollider2D boxColl2D;
	#endregion

	
	#region Unity Events
	void Awake()
	{
		sp = GetComponent<SpriteRenderer> ();
		boxColl2D = GetComponent<BoxCollider2D> ();
	}


	// Use this for initialization
	void Start () {
		Initialization ();
		if (startWithIce)
			OnFreezed ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (pressed)

			sp.sprite = sprites [1];
		else
			sp.sprite = sprites [0];
	}
	#endregion

	#region Methods
	public override void Initialization ()
	{

		state = State.Normal;
		ability = Ability.Flammable | Ability.Freezable;

	}
	public override bool OnFired ()
	{
		if (state == State.Freezing) {
			iceCube.SetActive (false);
			fireAni.SetTrigger ("Fire");
			boxColl2D.isTrigger = true;
			state = State.Normal;
			Debug.Log ("pedal is valid now");
			return true;
		}
		return false;
	}
	public override bool OnFreezed ()
	{
		if (state == State.Normal ) {
			iceCube.SetActive (true);
			boxColl2D.isTrigger = false;
			state = State.Freezing;
			Debug.Log ("pedal is invalid now");
			return true;
		}
		return false;
		
	}
	public override bool OnAntiGravity ()
	{
		return false;
	}
	public override void InteractWithPrince ()
	{
	}

	#endregion
}
