using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class PrinceAnimator : MonoBehaviour {

	private UnityArmatureComponent uac;
	private Rigidbody2D rb2d;
	private PrinceController pc;
	private bool jumpRest;
	private bool climb;
	private bool attack;

	void Awake()
	{
		uac = GetComponent<UnityArmatureComponent> ();
		rb2d = GetComponent<Rigidbody2D> ();
		pc = GetComponent<PrinceController> ();

	}
	void Start()
	{
		jumpRest = false;
		climb = false;
		attack = false;
	}

	void Update () {

		if (!attack) {
			//不在梯子上
			if (!pc.onLadder) {
				if (jumpRest && uac.animation.isCompleted)
					jumpRest = false;

				if (pc.onGround)
				if (uac.animation.lastAnimationName == "jump-down") {
					uac.animation.timeScale = 3.5f;
					uac.animation.GotoAndPlayByFrame ("jump-rest", 0, 1);
					jumpRest = true;
				}

				if (pc.onGround && !jumpRest) {

					if (Mathf.Abs (rb2d.velocity.x) > 0.1f && Mathf.Abs (rb2d.velocity.x) <= 2f && uac.animation.lastAnimationName != "walk(fast") {
						uac.animation.timeScale = 1f;
					
						uac.animation.GotoAndPlayByFrame ("walk(fast", 0, 0);
					}

					if (Mathf.Abs (rb2d.velocity.x) > 2f && uac.animation.lastAnimationName == "walk(fast") {
						uac.animation.timeScale = 1.5f;
					}
		
					if (Mathf.Abs (rb2d.velocity.x) < 0.1f && Mathf.Abs (rb2d.velocity.y) < 0.1f && !uac.animation.lastAnimationName.Contains ("idle")) {
						uac.animation.timeScale = 1f;
						uac.animation.GotoAndPlayByFrame ("idle-1", 0, 0);
					}

					if (pc.stayTooLong && uac.animation.lastAnimationName != "idle-2") {
						uac.animation.timeScale = 1f;
						uac.animation.GotoAndPlayByFrame ("idle-2", 0, 0);
					}

				} else {
					/*
					if (!pc.onGround && rb2d.velocity.y > 4f && uac.animation.lastAnimationName != "jump-up") {
						uac.animation.timeScale = 2f;
						uac.animation.GotoAndPlayByFrame ("jump-up", 0, 1);
			
					}
					if (!pc.onGround &&  rb2d.velocity.y<4f && uac.animation.lastAnimationName != "jump-down") {
						uac.animation.timeScale = 1f;
						uac.animation.GotoAndPlayByFrame ("jump-down", 0, 1);

					}


					*/
			
		
					if (!pc.onGround && uac.animation.lastAnimationName != "jump-down") {
						uac.animation.timeScale = 1f;
						uac.animation.GotoAndPlayByFrame ("jump-down", 0, 1);

					}
				}

				if (climb)
					climb = false;
			}

			//在梯子上
			if (pc.onLadder && !climb) {
				uac.animation.timeScale = 1f;
				uac.animation.GotoAndPlayByFrame ("climb", 0, 0);
				climb = true;				
			}

		} else if (uac.animation.isCompleted)
			attack = false;
	}

	public void PlayAttack()
	{
		attack = true;

		uac.animation.timeScale = 2f;
		uac.animation.GotoAndPlayByFrame ("attack" + Mathf.Ceil (Random.value * 2f).ToString (), 0, 1);
	}
}
