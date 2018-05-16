using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float speed = 10f, maxVelocity = 4f;

	private Rigidbody2D myBody;

	void Awake () {
		myBody = this.GetComponent<Rigidbody2D>();
	}

	void Start () {
		
	}


	void FixedUpdate () {

		//float h = Input.GetAxisRaw("Horizontal"); 
		//PlayerMoveKeyboard(h);
	}

	public void PlayerMoveKeyboard(float h){
		float forceX = 0f;
		float vel = Mathf.Abs(myBody.velocity.x);
		
		if (h != 0){

			//rotate player if it moves left or right
			Vector3 temp = this.transform.localScale;
			temp.x = Mathf.Abs(temp.x) * h;
			this.transform.localScale = temp;

			if (vel < maxVelocity)
				forceX = speed * h;
		}

		myBody.AddForce(new Vector2(forceX, 0));
	}
}
