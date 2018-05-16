using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	[SerializeField]
	private float speed = 1f;
	private float acceleration = 0.1f;
	private float maxSpeed = 3.2f;

	private float easySpeed = 3.2f;
	private float mediumSpeed = 3.7f;
	private float hardSpeed = 4.2f;

	[HideInInspector]
	public bool isMoving;
	// Use this for initialization
	void Start () {
		isMoving = true;
		if(GamePreferences.GetEasyDifficultyState() == 1){
			maxSpeed = easySpeed;
		}
		else if(GamePreferences.GetMediumDifficultyState() == 1){
			maxSpeed = mediumSpeed;
		}
		else if(GamePreferences.GetHardDifficultyState() == 1){
			maxSpeed = hardSpeed;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(isMoving){
			MoveCamera();
		}
	}

	void MoveCamera(){
		Vector3 temp = this.transform.position;

		float newY = temp.y - (speed * Time.deltaTime);
		temp.y = newY;
		this.transform.position = temp;
		speed += acceleration * Time.deltaTime;

		if(speed > maxSpeed){
			speed = maxSpeed;
		}
	}


}
