using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pelear : MonoBehaviour {

	Genes genes;

	// Use this for initialization
	void Start () {
		genes = GetComponent<Genes> ();
	}

	void OnTriggerEnter(Collider otro){

		if (otro.gameObject.tag != "Individuo")
			return;

		Genes genOtro = otro.gameObject.GetComponent<Genes> ();

		if (genes == null || genOtro == null)
			return;

		int sim = Utils.similitudGenetica (genes,genOtro);

		if (sim < 80) {
			//Atacar!!!
			print("soy " + name + " y estoy atacando a " + otro.name);
			Estado estadoEnemigo = otro.GetComponent<Estado> ();
			estadoEnemigo.salud -= 10;
		}
	}
}
