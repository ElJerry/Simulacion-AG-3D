using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cruzarManual : MonoBehaviour {

	void Update(){
		
		if (Input.GetKeyDown (KeyCode.C)) {
			cruzar ();
		}
	}

	void cruzar(){
		Genes yo, el, nuevo;
		yo = transform.GetComponent<Genes> ();
		el = GameObject.Find ("Individuo2").GetComponent<Genes> ();
		List<int> GenesNuevos = yo.CruzarGenes (el.getGenes());

		//Calcular la posicion enmedio de los padres

		Vector3 posicionHijo = (transform.position + el.transform.position) / 2;

		GameObject hijo = GameObject.Instantiate (Resources.Load ("Individuo/Individuo") as GameObject, posicionHijo, Quaternion.identity);

		nuevo = hijo.GetComponent<Genes> ();
		nuevo.SetGenes (GenesNuevos);
		nuevo.decodeGenes ();

	}
}
