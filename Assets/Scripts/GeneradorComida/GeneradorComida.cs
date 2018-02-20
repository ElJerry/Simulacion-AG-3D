using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorComida : MonoBehaviour {

	public static bool escaneo = false;
	public static int cantidadComida;

	bool activado = true;

	void Start(){
		if (!escaneo) {
			escaneo = true;
			StartCoroutine (escanear());
		}
	}

	// Update is called once per frame
	void FixedUpdate () {

		if (activado && cantidadComida < 3) {
//			print ("meti comida: " + name);
			activado = false;
			cantidadComida++;
			GameObject.Instantiate<GameObject> ((GameObject)Resources.Load ("Comida"),transform.position,Quaternion.identity);
			StartCoroutine (restart ());
		}
				
	}

	IEnumerator escanear(){
		while (true) {
			cantidadComida = GameObject.FindGameObjectsWithTag ("comida").Length;
//			print ("cantidad comida " + cantidadComida);
			yield return new WaitForSeconds (5);
		}
	}

	IEnumerator restart(){
		yield return new WaitForSeconds (10);
		activado = true;
	}

}
