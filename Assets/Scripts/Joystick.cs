using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerUpHandler, IPointerDownHandler {
	
	[SerializeField]
	private Player player;

	[SerializeField]
	private Animator anim;

	private bool movingLeft, movingRight;

	private float lastTime;

	void OnAwake(){
		movingLeft = false;
		movingRight = false;
	}

	public void OnPointerDown(PointerEventData data){
		if(gameObject.name == "Left Button"){
			movingLeft = true;
		}
		else if(gameObject.name == "Right Button"){
			movingRight = true;
		}
	}

	public void OnPointerUp(PointerEventData data){
		if(gameObject.name == "Left Button"){
			movingLeft = false;
		}
		else if(gameObject.name == "Right Button"){
			movingRight = false;
		}
	}

	void FixedUpdate(){
		print(movingLeft + " " + movingRight);
		if(movingRight){
			player.PlayerMoveKeyboard(1f);
		}
		else if(movingLeft){
			player.PlayerMoveKeyboard(-1f);
		}
	}
}
