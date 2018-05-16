using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

	[SerializeField]
	private Button musicButton;

	[SerializeField]
	private Sprite[] musicIcons;

	// Use this for initialization
	void Start () {
		CheckToPlayTheMusic();
	}

	void CheckToPlayTheMusic(){
		if(GamePreferences.GetMusicState() == 1){
			MusicController.instance.PlayMusic(true);
			musicButton.image.sprite = musicIcons[1];
		}
		else{
			MusicController.instance.PlayMusic(false);
			musicButton.image.sprite = musicIcons[0];
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void StartGame(){
		GameManager.instance.gameStartedFromMainMenu = true;
		GameManager.instance.gameRestartedAfterPlayerDied = false;

		//SceneManager.LoadScene("Gameplay");
		SceneFader.instance.LoadLevel("Gameplay");
	}
	public void HighscoreMenu(){
		//SceneManager.LoadScene("HighScoreScene");
		SceneFader.instance.LoadLevel("HighScoreScene");
	}
	public void OptionsMenu(){
		//SceneManager.LoadScene("OptionsMenu");
		SceneFader.instance.LoadLevel("OptionsMenu");
	}
	public void QuitGame(){
		Application.Quit();
	}
	public void MusicButton(){

		if(GamePreferences.GetMusicState() == 0){
			GamePreferences.SetMusicState(1);
			CheckToPlayTheMusic();
		}
		else{
			GamePreferences.SetMusicState(0);
			CheckToPlayTheMusic();
		}
	}
}
