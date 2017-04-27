using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class D_Console : MonoBehaviour {


	public Text d_text;
	private List<string> output;
	private int counter;

	void Start()
	{
		output = new List<string> ();

	}
	void Update()
	{
		ShowLogs ();
	}
	void OnEnable()
	{
		Application.logMessageReceived += HandleLog;
	}
	void OnDisable()
	{
		Application.logMessageReceived -= HandleLog;
	}


	void HandleLog(string logString,string stackType,LogType type)
	{
		if (output.Count > 25)
			output.Clear ();

		output .Add( logString);	

	}

	void ShowLogs()
	{
		d_text.text = string.Empty;
		foreach (string log in output) {
			d_text.text += log+"\n";
		}
	}



}
