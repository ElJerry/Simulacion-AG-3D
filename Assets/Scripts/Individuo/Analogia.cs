using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Analogia : MonoBehaviour {

	public static GameObject canvasGo = null;
	public static Renderer renderer;

	public static bool Enabled;

	void Start(){
		if (canvasGo == null) {
			canvasGo = GameObject.Find ("Canvas");
			canvasGo.SetActive (false);

			renderer = canvasGo.GetComponent<Renderer> ();
			enabled = true;
		}
	}

	void OnMouseDown(){
		if (!Enabled)
			return;
		
		canvasGo.SetActive(true);
		Time.timeScale = 0;

		renderer.Render(gameObject);
	}
}