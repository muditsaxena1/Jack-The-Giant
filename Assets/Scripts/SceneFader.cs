using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour {

	public static SceneFader instance;

	[SerializeField]
	private GameObject fadePanel;

	[SerializeField]
	private Animator fadeAnim;

	// Use this for initialization
	void Awake () {
		MakeSingleton();
	}


	void MakeSingleton () {
		if (instance){
			Destroy(this.gameObject);
		}else{
			instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
	}

	public void LoadLevel (string level){
		StartCoroutine(FadeInOut(level));
	}

	IEnumerator FadeInOut(string level){
		fadePanel.SetActive(true);
		fadeAnim.Play("FadeIn");

		yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(0.5f));
		SceneManager.LoadScene(level);
		fadeAnim.Play("FadeOut");

		yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(0.5f));

		fadePanel.SetActive(false);
	}
}
