using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPlaten : AbstractGrid {

	#region Variables
	public bool __________________________;
	public ElevatorPlaten theOther;
	public float totalMass;
	public Elevator elevator;
	public bool isLeft;
	public float platenWeight;
	public Animator buff;

	private List<Rigidbody2D> rbOver; //存储盘子上的刚体

	private bool onGround; //是否碰到地面，且速度向下
	private bool onTop; // 是否到顶，且速度向上
	private float curV; //速度

	#endregion
	#region Unity Events
	void Start()
	{
		Initialization ();
		totalMass = 0;
		rbOver = new List<Rigidbody2D> ();

	}
	void Update()
	{

		CaculateTotalMass ();
		//if (CheckGoodsOutPlaten()) //石头浮空了。。。
		//	totalMass = 0;
			//Debug.Log (this.name+totalMass.ToString());
	}

	void FixedUpdate()
	{
		onGround = false;
		onTop = false;
		curV = elevator.ApplyForce (isLeft);
		Judgement ();
		if (theOther.PlatenCanMove () && PlatenCanMove ())
			MovePlaten (curV);

	}
	#endregion

	#region Methods
	//盘子可以动么
	public bool PlatenCanMove()
	{
		return !(onGround|onTop);
	}


	private void CaculateTotalMass()
	{

		if (state == State.AntiGing)
			totalMass = 0;
		else
			totalMass = platenWeight;
		
		rbOver.Clear ();
		Collider2D[] cols;

		cols = Physics2D.OverlapBoxAll (transform.position + new Vector3 (0, 4f, 0), new Vector2(0.4f,10f) , 0);
			if (cols != null)
				foreach(Collider2D col in cols)
				{
					if (col.gameObject != gameObject)
					{
						
					if (col.tag == "Rock") 
						{
							Rock myRock = col.GetComponent<Rock> ();  //石头类处理
							if (myRock.onElevator) 
							{
								Rigidbody2D rb2d = col.GetComponent<Rigidbody2D> ();
								rbOver.Add (rb2d); //添加到刚体列表，为后面的刚体运动做准备
								totalMass += rb2d.mass;
							}
						} 
						else
						{
							
						if (col.tag == "Player") 
							{
							//Debug.Log ("got prince");
								Prince p = col.GetComponent<Prince> (); //王子类处理
								if (p.onElevator) 
								{
									Rigidbody2D rb2d = col.GetComponent<Rigidbody2D> ();
									rbOver.Add (rb2d); //添加到刚体列表，为后面的刚体运动做准备
									totalMass += rb2d.mass;
								}
							}
						}
						
					}
				}

	}



	//通过速度和位置确定是否可以移动，速度curV由elevator类计算传入
	public void Judgement()
	{

		//判断是否到顶
		if (curV > 0 && CheckTop ()) {
			onTop = true;
			return;
		}
		//是否到底
		if (curV< 0 && CheckGround ()) {
			onGround = true;
			return;
		}
	}

	//移动盘子
	private void MovePlaten(float v)
	{
		if (rbOver.Count > 0) {
			foreach (Rigidbody2D rb in rbOver) {
				
				rb.velocity = new Vector2 (rb.velocity.x, 0);
				rb.transform.position +=new Vector3 (0,v*Time.fixedDeltaTime,0);
			}
		}

		transform.position += new Vector3 (0,v*Time.fixedDeltaTime,0);
	}

	private bool CheckGround()
	{
		if (transform.position.y <= elevator.bottom) {
			Vector3 tmp = transform.position;
			tmp.y = elevator.bottom;
			transform.position = tmp;
			return true;
		} else
			return false;
	}

	private bool CheckTop()
	{
		if (transform.position.y >= elevator.top) {
			Vector3 tmp = transform.position;
			tmp.y = elevator.top;
			transform.position = tmp;
			return true;
		} else
			return false;
	}


	/*
	//当石头卡住时，totalmass归零
	private bool CheckGoodsOutPlaten()
	{
		Collider2D[] colls;

		colls = Physics2D.OverlapBoxAll (transform.position + new Vector3 (0.5f, 0.04f, 0), new Vector2(0.4f,0.08f) , 0);
		if (colls != null) {
			foreach (Collider2D coll in colls) {
				if (coll.gameObject != gameObject) {
					return false;
				}
			}
		}
		return true;

	}
	*/

	public override void Initialization ()
	{
		state = State.Normal;
		ability = Ability.AntiGable;
	}
	public  override bool OnFired(){return false;}  //被燃烧时调用
	public override bool OnFreezed(){return false;} //被冰冻
	public  override bool OnAntiGravity() //实施反重力
	{
		if (state == State.Normal) {
			state = State.AntiGing;
			buff.SetTrigger ("Antig");
			StartCoroutine (AntiGravityEvent ());
			GameObject.FindObjectOfType<PrinceController> ().elevator = 100;
			return true;
		}
		return false;
	}
	public  override void InteractWithPrince(){}//和王子相互作用


	private IEnumerator AntiGravityEvent()
	{

		//CaculateTotalMass() 中根据状态确定电梯质量
		yield return new WaitForSeconds (5);// 3s 反重力效果
		state = State.Normal;
		buff.SetTrigger ("Normal");
		Debug.Log ("platen antiG end");
	}
	#endregion
}
