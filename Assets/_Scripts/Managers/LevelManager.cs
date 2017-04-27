using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager> {

	#region Variables

	public GameObject sceneMenu;

	private SkillCounter sc; // 技能计数器
	private CheckPointsHolder _cpHolder; //alll check points in the scene
	private Prince _prince;// prince
	 

	#endregion
	//构造函数
	protected LevelManager(){
	}


	#region Property
	public CheckPointsHolder checkPointHolder
	{
		get{
			if (_cpHolder == null) {
				_cpHolder = FindObjectOfType<CheckPointsHolder> ();
				if (_cpHolder == null )
					Debug.Log ("the method dont work");
			}
			return _cpHolder;
		}
	}

	public Prince prince
	{
		get{
			if (_prince == null) {
				_prince = FindObjectOfType<Prince> ();
				if (_prince == null )
					Debug.Log ("the method dont work");
			}
			return _prince;
		}
	}
	#endregion

	#region Unity Events
	void Start()
	{
		sc = FindObjectOfType<SkillManager> ().skillCounter;
		if (sc == null)
			Debug.Log ("cann't find skillCounter");
	}

	// Update is called once per frame
	void Update () {
		
	}
	#endregion
	/*
	//进入上一个场景
	public void PreviousScene()
	{
		int id = SceneManager.GetActiveScene ().buildIndex;
		id = id - 1;

		if (id > 0)
			SceneManager.LoadScene (id);
		else
			Debug.Log ("can't return to Scene 0!");
	}
	//进入下一个场景
	public void NextScene()
	{
		int id = SceneManager.GetActiveScene ().buildIndex;
		id = id + 1;

		if (id < SceneManager.sceneCountInBuildSettings)
			SceneManager.LoadScene (id);
		else
			Debug .Log("This is the last scene!");
	}
	*/
	//调到指定id的场景
	public void ToScene(int id)
	{
		if (id < SceneManager.sceneCountInBuildSettings) {
			Clear ();
			SceneManager.LoadScene(id);
			sceneMenu.SetActive (false);
		}
	}
	public void ReloadScene()
	{
		Clear ();
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);		

	}
	public void BackToLastCheckPoint()
	{
		prince.transform.position = checkPointHolder.checkPoints [checkPointHolder.Index].transform.position;
		Camera.main.transform.position = prince.transform.position;
		FindObjectOfType<HpManager> ().Hp = 3;

		MovableSpike ms = FindObjectOfType<MovableSpike> ();
		if(ms!=null)
			ms.Reset ();
	}
	public void Clear()
	{
		_prince = null;
		_cpHolder = null;
		sc.Reset (); //技能计数清零
	}

}
