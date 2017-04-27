using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour {

	public GameObject panel;

	public void OnOff()
	{
		panel.SetActive (!panel.activeSelf);
	}
}
