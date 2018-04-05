using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour {

	Estado estado;
	Caminar caminar;
	Genes genes;
	Vector3 puntoAleatorio;
	bool ObjetivoPuntoAleatorio;

	public GameObject targetIndividuo, targetComida, targetHogar;
	public string accion;
	public float distanciaAceptable = 10f;

	// Use this for initialization
	void Start () {
		estado = GetComponent<Estado> ();
		caminar = GetComponent<Caminar> ();
		genes = GetComponent<Genes> ();
		ObjetivoPuntoAleatorio = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (targetIndividuo == null && targetComida == null)
			caminar.parar ();

		EvaluarEstado ();
	}

	bool EvalEstado1(){

		if (estado.salud < 30) {
			RegresarBase ();
			return true;
		}

		if (estado.hambre < 60) {
			accion = "Comida";
			buscarComida ();
			return true;
		}

		if (estado.hambre > 80 /*&& estado.salud > 80*/ && estado.puedeProcrear) {
			accion = "Reproduccion";
			BuscarPareja ();
			return true;
		}

		return false;
	}

	bool EvalEstado2(){

		if (estado.hambre < 80) {
			accion = "Comida";
			buscarComida ();
			return true;
		}

		if (estado.salud < 100) {
			RegresarBase (); // Cambiar por caminado aleatorio
			return true;
		}

		return false;
	}

	void EvaluarEstado(){

		if (EvalEstado1 ())
			return;
		
		if (EvalEstado2 ())
			return;

		CaminarRandom();

	}

	void RegresarBase(){
		targetHogar = estado.hogar;
		transform.LookAt (Utils.RemoveY(targetHogar.transform.position));

		float distancia = (transform.position - estado.hogar.transform.position).sqrMagnitude;
		if (distancia > distanciaAceptable) {
			caminar.caminar ();
		} else {
			caminar.parar ();
			estado.AumentarSalud (10);
		}
	}

	void CaminarRandom(){
		if (ObjetivoPuntoAleatorio == false) {
			puntoAleatorio = transform.position;
			puntoAleatorio.x += Random.Range (0, 10);
			puntoAleatorio.z += Random.Range (0, 10);
			ObjetivoPuntoAleatorio = true;
		}

		transform.LookAt (Utils.RemoveY(puntoAleatorio));

		float distancia = (transform.position - puntoAleatorio).sqrMagnitude;
		if (distancia > distanciaAceptable) {
			caminar.caminar ();
		} else {
			caminar.parar ();
			ObjetivoPuntoAleatorio = false;
		}
	}

	void BuscarPareja(){

		if (targetIndividuo == null) {
			caminar.parar ();
			targetIndividuo = Utils.GetClosestGameObjectWithFamilyCode (transform.gameObject, GameObject.FindGameObjectsWithTag ("Individuo"), genes.familia);
		}

		// no existen individuos de la misma familia
		if (targetIndividuo == null)
			return;

		transform.LookAt (Utils.RemoveY(targetIndividuo.transform.position));

		float distancia = (transform.position - targetIndividuo.transform.position).sqrMagnitude;

		if (distancia > distanciaAceptable)
			caminar.caminar ();
		else {
			caminar.parar ();

			Utils.CruzarPareja (gameObject, targetIndividuo);
			estado.SetEstadoDespuesDeReproducir ();

			Estado pareja = targetIndividuo.GetComponent<Estado> ();
			pareja.SetEstadoDespuesDeReproducir ();

			targetIndividuo = null;
		}

	}

	void buscarComida(){

		//Encontrar la mas cercana
		if (targetComida == null) {
			caminar.parar ();
			targetComida = Utils.GetClosestGameObject (transform.gameObject, GameObject.FindGameObjectsWithTag ("comida"));
		}

		if (targetComida == null) {
			CaminarRandom ();	
			return;
		}

		transform.LookAt (Utils.RemoveY(targetComida.transform.position));

		float distanciaComida = (targetComida.transform.position - transform.position).sqrMagnitude;
		if (distanciaComida > distanciaAceptable) {
			caminar.caminar();
		} else {
			caminar.parar();
			//Comer
			if (estado.hambre < 90) {
				Comer();
			}
			targetComida = null;
		}
	}

	void Comer(){
		estado.Comer ();
		targetComida.SendMessage ("Comer");
	}

}


// buscar comida
// comer

// buscar pareja
// reproducirse

// buscar enemigos
// acercarse a enemigos
// pelear -> al estar lo suficientemente cerca se pelearan

// huir

