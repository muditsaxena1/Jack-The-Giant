using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounds : MonoBehaviour {

	private float minX, maxX;

	// Use this for initialization
	void Start () {
		SetMinAndMaxX();
	}
	
	// Update is called once per frame
	void Update () {
		if(this.transform.position.x < minX){
			Vector3 temp = this.transform.position;
			temp.x = minX;
			this.transform.position = temp;
		}
		else if(this.transform.position.x > maxX){
			Vector3 temp = this.transform.position;
			temp.x = maxX;
			this.transform.position = temp;
		}
	}

	void SetMinAndMaxX () {
		Vector3 bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));


		maxX = bounds.x - 0.3f;
		minX = -maxX;
	}
}
