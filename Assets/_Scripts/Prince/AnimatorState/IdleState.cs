using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class IdleState : IAniState {

	private PrinceController pc;

	public IdleState(PrinceController PC)
	{
		pc = PC;
	}

	public void OnState(UnityArmatureComponent uac,ref IAniState state)
	{
		float x = Mathf.Abs (pc.m_rb.velocity.x);

		if (x> 0.1f &&x<1.5f&& uac.animation.lastAnimationName != "walk")
			uac.animation.GotoAndPlayByFrame ("walk", 0, 0);
		if (x> 1.5f && uac.animation.lastAnimationName != "walk(fast")
			uac.animation.GotoAndPlayByFrame ("walk(fast", 0, 0);

		//	if(x<0.1f)


	}
}
