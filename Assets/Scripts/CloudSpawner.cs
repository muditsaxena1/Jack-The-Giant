﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour {

	[SerializeField]
	private GameObject[] clouds;

	private float distanceBetweenClouds = 3f;

	private float minX, maxX;

	private float lastCloudPositionY;

	[SerializeField]
	private GameObject[] collectables;

	private float controlX;

	private GameObject player;

	void Awake () {
		controlX = 0f;
		SetMinAndMaxX();
		CreateClouds();
		player = GameObject.Find("Player");

		for (int i = 0; i < collectables.Length; i++){
			collectables[i].SetActive(false);
		}
	}

	void Start(){

		PositionThePlayer();
	}
	// shuffles the elments of array
	void Shuffle(GameObject[] arrayToShuffle){
		//print(arrayToShuffle.Length);
		for (int i = 0; i < arrayToShuffle.Length; i++){
			GameObject temp = arrayToShuffle[i];
			int random = Random.Range(i, arrayToShuffle.Length);
			arrayToShuffle[i] = arrayToShuffle[random];
			arrayToShuffle[random] = temp;
			if(i > 1){
				if(arrayToShuffle[i].tag == "Deadly"  && arrayToShuffle[i-1].tag == "Deadly"){
					Shuffle(arrayToShuffle);
				}
			}
		}
	}

	void SetMinAndMaxX () {
		Vector3 bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));


		maxX = bounds.x - 0.5f;
		minX = -maxX;
	}

	void CreateClouds(){
		//Shuffle(clouds);

		float positionY = 0f;

		for (int i = 0; i < clouds.Length; i++){
			Vector3 temp = clouds[i].transform.position;
			temp.y = positionY;

			if (controlX == 0){
				temp.x = Random.Range(0.0f, maxX);
				controlX = 1;
			}
			else if (controlX == 1){
				temp.x = Random.Range(0.0f, minX);
				controlX = 2;
			}
			else if (controlX == 2){
				temp.x = Random.Range(1.0f, maxX);
				controlX = 3;
			}
			else if (controlX == 3){
				temp.x = Random.Range(1.0f, minX);
				controlX = 0;
			}
			lastCloudPositionY = positionY;
			clouds[i].transform.position = temp;
			positionY -= distanceBetweenClouds;
		}
	}

	void PositionThePlayer(){
		GameObject[] darkClouds = GameObject.FindGameObjectsWithTag("Deadly");
		GameObject[] cloudInGame = GameObject.FindGameObjectsWithTag("Cloud");

		for (int i = 0; i < darkClouds.Length; i++){
			if (darkClouds[i].transform.position.y == 0f){
				Vector3 temp = darkClouds[i].transform.position;
				darkClouds[i].transform.position = cloudInGame[0].transform.position;
				cloudInGame[0].transform.position = temp;
			}
		}

		for (int i = 0; i < cloudInGame.Length; i++){
			if (cloudInGame[i].transform.position.y == 0f){
				Vector3 t = cloudInGame[i].transform.position;
				player.transform.position = new Vector3(t.x, t.y + 2f, t.z);
			}
		}
	}
	void OnTriggerEnter2D(Collider2D target){
		if(target.tag == "Cloud" || target.tag == "Deadly"){
			if(target.transform.position.y == lastCloudPositionY){
				//Shuffle(clouds);
				Shuffle(collectables);

				Vector3 temp = target.transform.position;
				for (int i = 0; i < clouds.Length; i++){
					if(!clouds[i].activeInHierarchy){
						if (controlX == 0){
							temp.x = Random.Range(0.0f, maxX);
							controlX = 1;
						}
						else if (controlX == 1){
							temp.x = Random.Range(0.0f, minX);
							controlX = 2;
						}
						else if (controlX == 2){
							temp.x = Random.Range(1.0f, maxX);
							controlX = 3;
						}
						else if (controlX == 3){
							temp.x = Random.Range(1.0f, minX);
							controlX = 0;
						}
						temp.y -= distanceBetweenClouds;

						lastCloudPositionY = temp.y;

						clouds[i].transform.position = temp;
						clouds[i].SetActive(true);

						int random = Random.Range(0, collectables.Length);

						if (clouds[i].tag != "Deadly"){
							if(!collectables[random].activeInHierarchy && Random.value > 0.5f){
								Vector3 temp2 = clouds[i].transform.position;
								temp2.y += 0.7f;

								if(collectables[random].tag == "Life"){
									if(PlayerScore.lifeCount < 2){
										collectables[random].transform.position = temp2;
										collectables[random].SetActive(true);
									}
								}
								else{
									collectables[random].transform.position = temp2;
									collectables[random].SetActive(true);
								}
							}
						}
					}


				}
			}
		}
	}
}