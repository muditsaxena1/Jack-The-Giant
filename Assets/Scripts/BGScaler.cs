﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScaler : MonoBehaviour {

	void Start () {
		SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
		Vector3 tempScale = this.transform.localScale;

		// width of BG
		float width = sr.sprite.bounds.size.x;

		float worldHeight = Camera.main.orthographicSize * 2f;
		float worldWidth = worldHeight * Screen.width / Screen.height;

		tempScale.x = worldWidth / width;

		transform.localScale = tempScale;
	}
}
