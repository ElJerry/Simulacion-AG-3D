using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estado : MonoBehaviour {

	public float hambre = 100f, energia = 100f, salud = 100f, deltaHambre = .3f;
	public GameObject hogar;
	public bool puedeProcrear = true;
	Genes genes;

	void Start(){
		genes = gameObject.GetComponent<Genes> ();
		StartCoroutine (morirPorTiempo(genes.getLongevidad()));
	}

	void FixedUpdate(){
		hambre = hambre - (deltaHambre * Time.deltaTime);

		if (hambre > 80) {
			salud += 1 * Time.deltaTime;
		}

		energia = Mathf.Min (100,energia);
		salud = Mathf.Min (100,salud);

		if (salud <= 0 || hambre <= 0 || energia <= 0)
			GameObject.Destroy (gameObject);
	}

	public void Comer(){
		hambre += 10;
	}

	public void ReinicioProcrear(){
		StartCoroutine ("RutinaReinicioProcrear");
	}

	public void RutinaCrecer(){
		StartCoroutine ("Crecer");
	}

	public void SetEstadoDespuesDeReproducir(){
		hambre = 30f;
		puedeProcrear = false;
		ReinicioProcrear ();
	}

	public void AumentarSalud(int puntos){
		salud += puntos;
		if (salud > 100)
			salud = 100;
	}

	IEnumerator morirPorTiempo(int tiempo){
		yield return new WaitForSeconds (tiempo);
		//print ("---------------Murio " + gameObject.name);
		yield return new WaitForSeconds (1);
		GameObject.Destroy (gameObject);
	}

	IEnumerator RutinaReinicioProcrear(){
		//print ("Inicio rutina procrear");
		yield return new WaitForSeconds (30);
		puedeProcrear = true;
	}

	IEnumerator Crecer(){
		yield return new WaitForSeconds (15);
		puedeProcrear = true;
		transform.localScale = new Vector3 (1.8f, 1.8f, 1.8f);
	}
}
