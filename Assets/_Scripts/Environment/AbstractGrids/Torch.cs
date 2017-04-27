using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LightAnimator
{
	public AnimationCurve ac;
	public float period;
}

//火炬（路灯）类
public class Torch : AbstractGrid {

	#region Variables
	private GameObject torchlight;
	private Material lightMat;
	public LightAnimator lightAnimator;

	private float timeCount;
	#endregion

	#region Unity Events
	void Awake()
	{
		torchlight = transform.GetChild (0).gameObject; // 得到灯光物体
		lightMat =torchlight.GetComponent<Renderer>().material;
	}

	void Start()
	{
		Initialization ();
	}

	void Update()
	{
		LightAnimation ();
	}
	#endregion

	#region Methods

	/// <summary>
	/// Initialization this instance.
	/// </summary>
	public override void Initialization ()
	{
		state = State.Normal;
		ability = Ability.Flammable | Ability.Freezable;
		timeCount = 0;
	}
	/// <summary>
	/// 火焰魔法，点燃，灯亮
	/// </summary>
	public  override bool OnFired()
	{
		if (state == State.Normal) {
			state = State.Firing;
			torchlight.SetActive (true);
			Debug.Log ("Torch is on");
			return true;
		}
		return false;
	}
	/// <summary>
	/// 冰冻魔法，熄灭,灯灭
	/// </summary>
	public  override bool OnFreezed()
	{
		if (state == State.Firing) {
			state = State.Normal;
			torchlight.SetActive (false);
			Debug.Log ("Torch is off");
			return true;
		}
		return false;
	}
	/// <summary>
	/// 反重力无效
	/// </summary>
	public override  bool OnAntiGravity()
	{
		Debug.Log ("Torch is antiG");
		return false;
	}
	public override void InteractWithPrince()
	{
	}

	private void LightAnimation()
	{

		if (timeCount > lightAnimator.period)
			timeCount = 0;
	//	Debug.Log (timeCount/lightAnimator.period);
		float glow = 2.5f*lightAnimator.ac.Evaluate (timeCount/lightAnimator.period);
		lightMat.SetFloat ("_Glow", glow);

		timeCount += Time.deltaTime;

	}
	#endregion

}
