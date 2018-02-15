using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Analogia : MonoBehaviour {

	public static GameObject canvasGo = null;
	public static Renderer renderer;

	void Start(){
		if (canvasGo == null) {
			canvasGo = GameObject.Find ("Canvas");
			canvasGo.SetActive (false);

			renderer = canvasGo.GetComponent<Renderer> ();
		}
	}

	void OnMouseDown(){
		canvasGo.SetActive(true);
		Time.timeScale = 0;

		renderer.Render(gameObject);
	}
}