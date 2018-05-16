using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour {
	[SerializeField]
	private AudioClip coinClip, lifeClip;

	private CameraController cameraController;

	private Vector3 previousPosition;
	private bool countScore;

	public static int scoreCount, lifeCount, coinCount;

	void Awake(){
		cameraController = Camera.main.GetComponent<CameraController>();
	}
	// Use this for initialization
	void Start () {
		previousPosition = this.transform.position;
		countScore = true;
	}
	
	// Update is called once per frame
	void Update () {
		CountScore();
	}

	void CountScore(){
		if(countScore){
			if (this.transform.position.y + 0.1 < previousPosition.y){
				scoreCount++;
			}
			previousPosition = this.transform.position;
			GameplayController.instance.SetScore(scoreCount);
		}
	}

	void OnTriggerEnter2D(Collider2D target){
		if (target.tag == "Coin"){
			coinCount++;
			scoreCount += 200;

			GameplayController.instance.SetScore(scoreCount);
			GameplayController.instance.SetCoinScore(coinCount);

			AudioSource.PlayClipAtPoint(coinClip, this.transform.position);
			target.gameObject.SetActive(false);
		}
		else if (target.tag == "Life"){
			lifeCount++;

			GameplayController.instance.SetLifeScore(lifeCount);

			AudioSource.PlayClipAtPoint(lifeClip, this.transform.position);
			target.gameObject.SetActive(false);
		}
		else if (target.tag == "Bound" || target.tag == "Deadly"){
			cameraController.isMoving = false;
			countScore = false;

			//GameplayController.instance.GameOverShowPanel(scoreCount, coinCount);
			lifeCount--;

			transform.position = new Vector3(500, 500, 0);
			GameManager.instance.CheckGameStatus(scoreCount, coinCount, lifeCount);
		}
	}
}
