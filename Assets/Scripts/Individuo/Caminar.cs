using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caminar : MonoBehaviour {

	public bool echo = false;
	bool walk = false;
	bool checarTrabado = false;
	Animator animator;
	Rigidbody rigidBody;
	Vector3 posicionanterior;

	Genes genes;

	void Start () {
		animator = gameObject.GetComponent<Animator> ();
		rigidBody = gameObject.GetComponent<Rigidbody> ();
		genes = GetComponent<Genes> ();
		StartCoroutine (antiTrabado());
	}
	
	void FixedUpdate () {
		animator.SetBool ("walking", walk);
		if (walk) {
			rigidBody.AddRelativeForce (Vector3.forward * ((150/4)*genes.velocidad));

			// Revisar si el personaje se quedo atorado.
			float diferencia = (posicionanterior - transform.position).magnitude;
						
			if ( diferencia < .001f) {
				transform.localPosition = transform.localPosition + (Vector3.left * .2f);
			}
			posicionanterior = transform.position;
			checarTrabado  = false;

			GetComponent<CapsuleCollider> ().center = new Vector3 (0, .8f, -1.06f);
		} else {
			GetComponent<CapsuleCollider> ().center = new Vector3 (0, .8f, 0);
		}
	}

	public void caminar(){
		walk = true;
	}

	public void parar(){
		walk = false;
	}

	IEnumerator antiTrabado(){
		yield return new WaitForSeconds (10);
		checarTrabado = true;
	}
}
