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

	// Use this for initialization
	void Start () {
		animator = gameObject.GetComponent<Animator> ();
		rigidBody = gameObject.GetComponent<Rigidbody> ();
		StartCoroutine (antiTrabado());
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		animator.SetBool ("walking", walk);
		if (walk) {
//			print ("walking");
			rigidBody.AddRelativeForce (Vector3.forward * 150);

			// Revisar si el personaje se quedo atorado.
			float diferencia = (posicionanterior - transform.position).magnitude;
			//print ("diferencia " + diferencia);
						
			if ( diferencia < .001f) {
				transform.localPosition = transform.localPosition + (Vector3.left * .2f);
			}
			posicionanterior = transform.position;
			checarTrabado  = false;
			// ----------------------------------------------

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
