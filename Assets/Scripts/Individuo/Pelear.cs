using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pelear : MonoBehaviour {

	Genes genes, otro;
	bool atacar;
	BoxCollider bCollider;
	GameObject targetEnemigo;

	LineRenderer laser;

	// Use this for initialization
	void Start () {
		genes = GetComponent<Genes> ();
		atacar = true;
		bCollider = GetComponent<BoxCollider> ();
		targetEnemigo = null;
		laser = gameObject.GetComponentInChildren<LineRenderer> ();
		laser.enabled = false;
		laser.material = new Material(Shader.Find("Particles/Additive"));
		laser.startColor = laser.endColor = genes.col;
	}

	void FixedUpdate(){
		Collider[] colliders = Physics.OverlapSphere (transform.position,4f);

		if (!atacar)
			return;
		
		foreach (Collider c in colliders) {

			// ignorar si el objeto no es un individuo
			if (c.gameObject.tag != "Individuo")
				continue;

			otro = c.GetComponent<Genes> ();					

			// no atacar miembros de la familia
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
			// decrementar la salud enemiga
			estadoEnemigo.salud -= ((20/8)*genes.fuerza); // la fuerza maxima es 8, por lo tanto se divide 20(maximo de ataque) entre 8 y se multiplica por su propia fuerza

			targetEnemigo = estadoEnemigo.gameObject;
			laser.SetPosition (0, transform.position);
			laser.SetPosition (1, otro.transform.position);
			laser.enabled = true;

			yo.atacar = false;
			yo.StartCoroutine (yo.reAtacar ());
		}
	}

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		Vector3 direction = targetEnemigo.transform.position - transform.position;
		Gizmos.DrawRay(transform.position, Vector3.up*6);
	}

	IEnumerator reAtacar(){
		yield return new WaitForSeconds (1);
		targetEnemigo = null;
		atacar = true;
		laser.enabled = false;
	}
}
