using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	public static GameManager instance;

	[HideInInspector]
	public bool gameStartedFromMainMenu, gameRestartedAfterPlayerDied;

	[HideInInspector]
	public int score, coinScore, lifeScore;

	// Use this for initialization
	void Awake () {
		MakeSingleton();
	}

	void Start(){
		InitialiseVariables();
	}

	void OnLevelWsLoaded(Scene scene, LoadSceneMode sceneMode){
		if(SceneManager.GetActiveScene().name == "Gameplay"){
			if(gameRestartedAfterPlayerDied){
				GameplayController.instance.SetScore(score);
				GameplayController.instance.SetCoinScore(coinScore);
				GameplayController.instance.SetLifeScore(lifeScore);

				PlayerScore.scoreCount = score;
				PlayerScore.coinCount = coinScore;
				PlayerScore.lifeCount = lifeScore;
			}
			else if(gameStartedFromMainMenu){
				PlayerScore.scoreCount = 0;
				PlayerScore.coinCount = 0;
				PlayerScore.lifeCount = 2;

				GameplayController.instance.SetScore(0);
				GameplayController.instance.SetCoinScore(0);
				GameplayController.instance.SetLifeScore(2);

			}
		} 
	} 

	void InitialiseVariables(){
		if (!PlayerPrefs.HasKey("GameInitialized")){
			GamePreferences.SetEasyDifficultyState(0);
			GamePreferences.SetEasyDifficultyHighScore(0);
			GamePreferences.SetEasyDifficultyCoinScore(0);

			GamePreferences.SetMediumDifficultyState(1);
			GamePreferences.SetMediumDifficultyHighScore(0);
			GamePreferences.SetMediumDifficultyCoinScore(0);

			GamePreferences.SetHardDifficultyState(0);
			GamePreferences.SetHardDifficultyHighScore(0);
			GamePreferences.SetHardDifficultyCoinScore(0);

			GamePreferences.SetMusicState(0);

			PlayerPrefs.SetInt("GameInitialized", 0);
		}
	}

	// singleton pattern
	void MakeSingleton() {
		if(!instance){
			instance = this;
			DontDestroyOnLoad(this.gameObject);
			SceneManager.sceneLoaded += OnLevelWsLoaded;
		}else{
			Destroy(this.gameObject);
		}
	}

	public void CheckGameStatus(int score, int coinScore, int lifeScore){
		if(lifeScore < 0){
			if(GamePreferences.GetEasyDifficultyState() == 1){
				
				int highScore = GamePreferences.GetEasyDifficultyHighScore();
				int coin = GamePreferences.GetEasyDifficultyCoinScore();

				if(highScore < score){
					GamePreferences.SetEasyDifficultyHighScore(score);
				}

				if(coin < coinScore){
					GamePreferences.SetEasyDifficultyCoinScore(coinScore);
				}
			}
			else if(GamePreferences.GetMediumDifficultyState() == 1){
				
				int highScore = GamePreferences.GetMediumDifficultyHighScore();
				int coin = GamePreferences.GetMediumDifficultyCoinScore();

				if(highScore < score){
					GamePreferences.SetMediumDifficultyHighScore(score);
				}

				if(coin < coinScore){
					GamePreferences.SetMediumDifficultyCoinScore(coinScore);
				}
			}
			else if(GamePreferences.GetHardDifficultyState() == 1){
				
				int highScore = GamePreferences.GetHardDifficultyHighScore();
				int coin = GamePreferences.GetHardDifficultyCoinScore();

				if(highScore < score){
					GamePreferences.SetHardDifficultyHighScore(score);
				}

				if(coin < coinScore){
					GamePreferences.SetHardDifficultyCoinScore(coinScore);
				}
			}
			gameRestartedAfterPlayerDied = false;
			gameStartedFromMainMenu = false;

			GameplayController.instance.GameOverShowPanel(score, coinScore);
		}
		else{
			this.score = score;
			this.coinScore = coinScore;
			this.lifeScore = lifeScore;

			GameplayController.instance.SetScore(score);
			GameplayController.instance.SetCoinScore(coinScore);
			GameplayController.instance.SetLifeScore(lifeScore);

			gameStartedFromMainMenu = false;
			gameRestartedAfterPlayerDied = true;

			GameplayController.instance.PlayerDiedRestartTheGame();
		}
	}
}
