using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public interface IAniState  {

	void OnState (UnityArmatureComponent uac,ref IAniState state);
}
