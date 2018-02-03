using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTiempo : MonoBehaviour {

	void FixedUpdate(){

		if(Input.GetKeyDown(KeyCode.Alpha1)){
			Time.timeScale = 1;
			print ("aumentado");
		}

		if(Input.GetKeyDown(KeyCode.Alpha2)){
			Time.timeScale = 2;
			print ("aumentado");
		}

		if(Input.GetKeyDown(KeyCode.Alpha3)){
			Time.timeScale = 3;
			print ("aumentado");
		}

		if(Input.GetKeyDown(KeyCode.Alpha4)){
			Time.timeScale = 4;
			print ("aumentado");
		}

		if(Input.GetKeyDown(KeyCode.Alpha0)){
			Time.timeScale = 0.1f;
			print ("aumentado");
		}

		if(Input.GetKeyDown(KeyCode.R)){
			Application.LoadLevel (Application.loadedLevel);
		}
	}


}
