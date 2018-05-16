using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablesScript : MonoBehaviour {

	void DestroyCollectable(){
		gameObject.SetActive(false);
	}

	void OnEnable(){
		Invoke("DestroyCollectable", 6f);
	}
}
