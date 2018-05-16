using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {

	public static MusicController instance;

	private AudioSource audioSource;

	// Use this for initialization
	void Awake () {
		MakeSingleton();
		audioSource = this.GetComponent<AudioSource>();
		audioSource.loop = true;
	}
	
	// Update is called once per frame
	void MakeSingleton () {
		if (instance){
			Destroy(this.gameObject);
		}else{
			instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
	}

	public void PlayMusic (bool play){
		if(play){
			if(!audioSource.isPlaying){
				audioSource.Play();
			}
		}
		else{
			if(audioSource.isPlaying){
				audioSource.Stop();
			}
		}
	}
}
