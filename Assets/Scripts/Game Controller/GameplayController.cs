﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour {

	public static GameplayController instance;

	[SerializeField]
	private Text scoreText, coinText, lifeText, gameOverScoreText, gameOverCoinText;

	[SerializeField]
	private GameObject pausePanel, gameOverPanel;

	[SerializeField]
	private Button readyButton;

	void MakeInstance () {
		if(instance == null){
			instance = this;
		}
	}

	void Awake () {
		MakeInstance();
	}

	void Start(){
		Time.timeScale = 0f;
	}

	public void PauseGame(){
		Time.timeScale = 0f;
		pausePanel.SetActive(true);
	}
	public void ResumeGame(){
		Time.timeScale = 1f;
		pausePanel.SetActive(false);
	}
	public void QuitGame(){
		Time.timeScale = 1f;
		SceneFader.instance.LoadLevel("MainMenu");
	}
	public void SetScore(int score){
		scoreText.text = "" + score;
	}
	public void SetCoinScore(int coinScore){
		coinText.text = "x" + coinScore;
	}
	public void SetLifeScore(int lifeScore){
		lifeText.text = "x" + lifeScore;
	}

	public void GameOverShowPanel(int finalScore, int finalCoins){
		gameOverScoreText.text = finalScore.ToString();
		gameOverCoinText.text = finalCoins.ToString();
		gameOverPanel.SetActive(true);
		StartCoroutine(GameOverLoadMainMenu());
	}

	IEnumerator GameOverLoadMainMenu(){
		yield return new WaitForSeconds(3f);
		//SceneManager.LoadScene("MainMenu");
		SceneFader.instance.LoadLevel("MainMenu");
	}

	public void PlayerDiedRestartTheGame(){
		StartCoroutine(PlayerDiedRestart());
	}
	IEnumerator PlayerDiedRestart(){
		yield return new WaitForSeconds(3f);
		SceneManager.LoadScene("Gameplay");
	}

	public void StartTheGame(){
		Time.timeScale = 1f;
		readyButton.gameObject.SetActive(false);
	}
}
