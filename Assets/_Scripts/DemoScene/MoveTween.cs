using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTween : MonoBehaviour {

	//这两个点确定运动范围
	public Vector3 startPoint;
	public Vector3 endPoint; // y更大，x更大

	public float velocity;//移动速度
	public bool isOn;//是否工作
	public bool isYDirection;//是否在y方向运动，否则x方向
	public bool back; // 移动从start到end还是相反

	void Start()
	{
		
	}

	void FixedUpdate()
	{
		if (isOn) {
			ClampPos ();
			Movement (back);
		}
		
	}



	/// <summary>
	/// 避免移动出范围
	/// </summary>
	private void ClampPos()
	{
		Vector3 tmp = transform.position;
		if (isYDirection) {

			if (transform.position.y > endPoint.y) {

				tmp.y = endPoint.y;
				transform.position = tmp;
			}
			if (transform.position.y < startPoint.y) {
	
				tmp.y = startPoint.y;
				transform.position = tmp;
			}
		} else {
			if (transform.position.x > endPoint.x) {
		
				tmp.x = endPoint.x;
				transform.position = tmp;
			}
			if (transform.position.x < startPoint.x) {

				tmp.x = startPoint.x;
				transform.position = tmp;
			}
		}
	}

	void Movement(bool back)
	{
		if (isYDirection) {
			if (back) {
				transform.position += -Vector3.up * velocity * Time.deltaTime;
			} else
				transform.position += Vector3.up * velocity * Time.deltaTime;
		} else {
			if (back) {
				transform.position += -Vector3.right * velocity * Time.deltaTime;
			} else
				transform.position += Vector3.right * velocity * Time.deltaTime;
		}
			
	}
		
}
