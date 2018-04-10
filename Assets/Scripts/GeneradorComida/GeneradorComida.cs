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

	void FixedUpdate () {

		if (activado && cantidadComida < 1) { //maximo existiran 2 comidas en el mundo al terminarse las originales
			activado = false;
			cantidadComida++;
			GameObject.Instantiate<GameObject> ((GameObject)Resources.Load ("Comida"),transform.position,Quaternion.identity);
			StartCoroutine (restart ());
		}
				
	}

	IEnumerator escanear(){
		while (true) {
			cantidadComida = GameObject.FindGameObjectsWithTag ("comida").Length;
			yield return new WaitForSeconds (5);
		}
	}

	IEnumerator restart(){
		yield return new WaitForSeconds (10);
		activado = true;
	}

}
