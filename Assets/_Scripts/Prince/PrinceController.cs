using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//王子控制器
public class PrinceController : MonoBehaviour {

	public float horizontalForce; // 水平移动作用力
	public Vector2 launchDir;//凝胶作用力方向
	public float launchForce;//凝胶作用力大小
	public float climbVelocity;//在梯子上移动的速度

	public Rigidbody2D m_rb;
	public bool onGround;
	public bool onLadder;
	public bool stayTooLong; //长时间不动，则为true，调用idle-2动画

	private PrinceAnimator pAnimator;
	private float duration = 5f; //几秒不动，stayTooLong为true

	public int direction;
	public int lastd;
	public int state; //0探索，1逃离，2等待
	public GameObject newObj = null;
	public GameObject obstacle = null;
	public float danger;
	public int elevator = 0;
	private int gelled = 0;
	public bool onWood = false;
	//public float height = -10000;

	void Awake()
	{
		pAnimator = GetComponent<PrinceAnimator> ();
	}

	public void makeDecision(){
		Vector3 pos = m_rb.transform.position;
		state = 0;
		int cnt = 0;
		Torch[] torches = FindObjectsOfType (typeof(Torch)) as Torch[];
		foreach (Torch t in torches) {
			if (t.transform.position.x > pos.x)
				cnt += 1;
		}
		if (cnt > 3)
			direction = 1;
		else
			direction = -1;
		Debug.Log (direction.ToString() + "!");
	}

	void Start () {
		m_rb = GetComponent<Rigidbody2D> ();
		onLadder = false;
		stayTooLong = false;
		makeDecision ();
	}

	void Update()
	{
		
		if (Input.GetKeyDown (KeyCode.J)) {
			GetComponent<Prince> ().Interact ();
			pAnimator.PlayAttack ();
		}
			

		if (Mathf.Abs(m_rb.velocity.x) < 0.1f)
		{
			if (!stayTooLong) 
			{
				duration -= Time.deltaTime;
				if (duration < 0) 
				{
					stayTooLong = true;
					duration = 5f;
				}
			} 
		}else {
			stayTooLong = false;
			duration = 5f;
		}
	}

	void FixedUpdate () {

		if (!onLadder) {
			m_rb.bodyType = RigidbodyType2D.Dynamic;
			onGround = CheckGround (0, -0.5f) && CheckSpike(0, -0.5f);

			//Debug.Log (Input.GetAxis ("Horizontal").ToString());
			//在地面上才可以控制
			if (onGround) {

				if (elevator == 100) {
					//if (height == -10000)
					//	height = transform.position.y;
					//Debug.Log (height);
					//Debug.Log(transform.position.y);
					if (transform.position.y < -3) {
						if (CheckGround (-0.3f, -0.5f)) {
							HorizontalMove (-1);
							GetComponent<Prince> ().TurnAround (-1);
						} else {
							m_rb.velocity = Vector2.zero;
							GetComponent<Prince> ().TurnAround (1);
						}
						return;
					} else {
						Debug.Log ("Let's go!");
						elevator -= 1;
					}
						
				}

				if (elevator > 0) {
					elevator -= 1;
					direction = 1;
					state = 0;
					HorizontalMove (direction);
					GetComponent<Prince> ().TurnAround (direction);
					return;
				}

				if (obstacle != null) {
					if (obstacle.GetComponent<AbstractGrid> ().state == State.Freezing)
						GameObject.FindObjectOfType<Prince> ().Interact ();
					if (obstacle.GetComponent<AbstractGrid> ().state == State.Firing)
						state = 2;
					else
						state = 0;
				}

				if (!(onWood && CheckGround (m_rb.velocity.x * 0.3f, -1.5f)) && (!CheckWood(m_rb.velocity.x * 0.3f, -1.5f) && (!CheckGround(m_rb.velocity.x * 0.3f, -0.5f) || !CheckSpike(m_rb.velocity.x * 0.3f, -0.5f)))) {
					//Debug.Log (newObj);
					Debug.Log("Stop!");
					if (gelled > 0)
						gelled -= 1;
					else
					{
						m_rb.velocity = Vector2.zero;
						state = 1;
						direction = -1 * direction;
						danger = transform.position.x;
						//Debug.Log ("Dangerous!");
						//Debug.Log (danger);
					}
				}

				if (onWood && CheckGround (m_rb.velocity.x * 0.3f, -1.5f) && state == 1) {
					direction = -direction;
					state = 0;
				}
					

				if (state == 2) {
					if (obstacle == null) {
						state = 0;
						direction = -1 * lastd / 2;
					}
					else {
						if (obstacle.transform.position.x > transform.position.x)
							direction = -2;
						else
							direction = 2;
						HorizontalMove (direction);
						GetComponent<Prince> ().TurnAround (direction);
					}
				}



				if (state == 1) {
					
					//Debug.Log (newObj);
					if (newObj != null) {
						float delta = newObj.transform.position.x - transform.position.x;
						if (delta * direction < 0)
							m_rb.velocity = Vector2.zero;
						if (delta > 0)
							direction = 1;
						else
							direction = -1;
						Debug.Log (newObj);
						newObj = null;
						HorizontalMove (direction);
						GetComponent<Prince> ().TurnAround (direction);
						state = 0;

					} else {
						if (Mathf.Abs (transform.position.x - danger) < 1) {
							HorizontalMove (direction);
							GetComponent<Prince> ().TurnAround (direction);
						} else {
							//m_rb.velocity = Vector2.zero;
							GetComponent<Prince> ().TurnAround (danger - transform.position.x);
						}
					}
				}

				Debug.Log (state);

				if (state == 0) {
					

					if (newObj != null) {
						if (newObj.transform.position.x > transform.position.x)
							direction = 1;
						else
							direction = -1;
						newObj = null;
						
					} 
					HorizontalMove (direction);
					GetComponent<Prince> ().TurnAround (direction);
				}


				lastd = direction;
				//float h = Input.GetAxis ("Horizontal");
				
			}
		} else
			MovementOnLadder ();
	}


	//控制水平移动
	public void HorizontalMove(float h)
	{
		m_rb.AddForce (horizontalForce * h * Vector2.right);
	}

	/// <summary>
	/// 凝胶作用
	/// </summary>
	/// <param name="isLeft">凝胶的作用方向是否是向左</param>
	public void GelLaunch(bool isLeft)
	{
		int sign;
		Vector2 dir = launchDir.normalized;

		if (isLeft)
			sign = -1;
		else
			sign = 1;

		dir.x *= sign;

		m_rb.velocity = Vector2.zero;
		m_rb.AddForce (dir*launchForce,ForceMode2D.Impulse);
		gelled = 10;
		makeDecision ();
	}

	private bool CheckWood(float dx, float dy)
	{
		Collider2D[] colls = Physics2D.OverlapCircleAll (transform.position+new Vector3(dx,dy,0),0.1f,LayerMask.GetMask("AbstractGrid","Default"));

		foreach (Collider2D coll in colls) {
			if (coll.gameObject != gameObject && coll.GetComponent<Wood> () != null) { 
				Debug.Log ("Has wood!");
				return true;	
			}
		}
		Debug.Log ("No Wood!");
		return false;
	}

	private bool CheckSpike(float dx, float dy)
	{
		if (state != 0)
			return true;
		Collider2D[] colls = Physics2D.OverlapCircleAll (transform.position+new Vector3(dx,dy,0),0.1f,LayerMask.GetMask("AbstractGrid","Default"));

		foreach (Collider2D coll in colls)
			if (coll.gameObject != gameObject && coll.GetComponent<Spikeweed> () == null) 
				return true;	
		Debug.Log ("Spikeweed!");

		return false;
	}

	private bool CheckGround(float dx, float dy)
	{
		Collider2D[] colls = Physics2D.OverlapCircleAll (transform.position+new Vector3(dx,dy,0),0.1f,LayerMask.GetMask("AbstractGrid","Default"));

		foreach (Collider2D coll in colls) {
			if (coll.gameObject != gameObject && coll.GetComponent<Gel> () == null && !coll.isTrigger) { 
				return true;	
			}
		}
		Debug.Log ("Too high right"+dy.ToString());
		return false;
	}

	/// <summary>
	/// 在梯子上时的移动
	/// </summary>
	private void MovementOnLadder()
	{
		if (m_rb.bodyType == RigidbodyType2D.Dynamic) {
			m_rb.velocity = Vector2.zero;
			m_rb.bodyType = RigidbodyType2D.Kinematic;
		} else {
			//if (Input.GetKey (KeyCode.W)) 
			//	transform.position +=  Vector3.up * climbVelocity * Time.fixedDeltaTime;
			//else 
			//	if(Input.GetKey(KeyCode.S))
					transform.position -=  Vector3.up * climbVelocity * Time.fixedDeltaTime;
			//Debug.Log ("Ladder");
		}
	}
}
