using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pelear : MonoBehaviour {

	Genes genes;
	bool atacar;
	BoxCollider bCollider;

	// Use this for initialization
	void Start () {
		genes = GetComponent<Genes> ();
		atacar = true;
		bCollider = GetComponent<BoxCollider> ();

	}

	void OnTriggerEnter(Collider otro){
		Atacar (this,otro);
	}

	static void Atacar(Pelear yo, Collider otro){
		if (!yo.atacar)
			return;

		if (otro.gameObject.tag != "Individuo")
			return;

		Genes genOtro = otro.gameObject.GetComponent<Genes> ();

		if (yo.genes == null || genOtro == null)
			return;

		int sim = Utils.similitudGenetica (yo.genes,genOtro);

		if (sim < 70) {
			//Atacar!!!
			print("soy " + yo.name + " y estoy atacando a " + otro.name);
			Estado estadoEnemigo = otro.GetComponent<Estado> ();
			Debug.DrawLine (yo.transform.position + Vector3.up, otro.transform.position+ Vector3.up,Color.red,1f);
			estadoEnemigo.salud -= 10;
			yo.atacar = false;
			yo.StartCoroutine (yo.reAtacar ());
		}
	}

	IEnumerator reAtacar(){
		yield return new WaitForSeconds (1);
		atacar = true;
	}
}
