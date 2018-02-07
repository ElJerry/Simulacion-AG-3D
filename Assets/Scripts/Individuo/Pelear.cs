using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pelear : MonoBehaviour {

	Genes genes, otro;
	bool atacar;
	BoxCollider bCollider;

	// Use this for initialization
	void Start () {
		genes = GetComponent<Genes> ();
		atacar = true;
		bCollider = GetComponent<BoxCollider> ();

	}

	void FixedUpdate(){
		Collider[] colliders = Physics.OverlapSphere (transform.position,4f);

		if (!atacar)
			return;
		
		foreach (Collider c in colliders) {

			if (c.gameObject.tag != "Individuo")
				continue;

			otro = c.GetComponent<Genes> ();					

			if (genes.familia == otro.familia)
				continue;

			Atacar (this,c);
			break; // Solo deberia atacar a uno a la vez
		}

	}


//	void OnTriggerStay(Collider otro){
//		Atacar (this,otro);
//	}

	 void Atacar(Pelear yo, Collider otro){	

		Genes genOtro = otro.GetComponent<Genes> ();

		if (genes == null || genOtro == null)
			return;	

		int sim = Utils.similitudGenetica (yo.genes,genOtro);

		if (sim < 70) {
			//Atacar!!!
			//print("soy " + yo.name + " y estoy atacando a " + otro.name);
			Estado estadoEnemigo = otro.GetComponent<Estado> ();
			Debug.DrawLine (yo.transform.position + Vector3.up, otro.transform.position+ Vector3.up,genes.col,1f);
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
