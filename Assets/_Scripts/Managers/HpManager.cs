using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HpManager : MonoBehaviour {

	private int hp;
	public Text hpText;

	// Use this for initialization
	void Start () {
		hp = 3;
	}
	
	// Update is called once per frame
	void Update () {

		hpText .text = hp.ToString ();

	}
	void LateUpdate()
	{
		if (hp < 1) {
			hp = 3;
			LevelManager.Instance.BackToLastCheckPoint ();
		}
	}
	public int Hp
	{
		get{
			return hp;
		}
		set{
			hp = value;
		}
	}
	public void HpAdd(int i)
	{
		hp += i;
	}

}
