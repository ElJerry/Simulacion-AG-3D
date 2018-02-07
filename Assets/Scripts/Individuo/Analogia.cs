using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Analogia : MonoBehaviour {

	public static GameObject canvasGo = null;
	public Canvas canvas;

	void Start(){
		if (canvasGo == null) {
			canvasGo = GameObject.Find ("Canvas");
			canvasGo.SetActive (false);
		}

	}

	void OnMouseDown(){
		print (name + "fue clicado");
		canvasGo.SetActive(true);
		Time.timeScale = 0;
	}
}
