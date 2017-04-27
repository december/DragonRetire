using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GelIndicator : MonoBehaviour {

	public Sprite[]  sprites;

	public bool isLeft;
	private bool dirty;

	void Start()
	{
		dirty = false;
		isLeft = true;
	}
	// Update is called once per frame
	void Update () {

		if (dirty) {
			if(isLeft)
				GetComponent<SpriteRenderer> ().sprite = sprites [0];
			else
				GetComponent<SpriteRenderer> ().sprite = sprites [1];
		}
	}

	public void ChangeSprite()
	{
		isLeft = !isLeft;
		dirty = true;
	}
}
