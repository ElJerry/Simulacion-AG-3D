using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estadisticas : MonoBehaviour {

	public int TiempoDeEjecucion;

	// Estadisticas a enumerar
	public int IndividuosVivos, RazasVivas;
	public GameObject MejorAdaptado;
	GameObject[] individuos;

	void Start(){
		
		StartCoroutine (Iniciar());
	}

	IEnumerator Iniciar(){
		yield return new WaitForSeconds(TiempoDeEjecucion);
		Time.timeScale = 0;
		ObtenerEstadisticas ();
	}

	private void ObtenerEstadisticas(){

		//Obtener todos los individuos;
		individuos = GameObject.FindGameObjectsWithTag("Individuo");

		Dictionary<string, int> razas = new Dictionary<string, int>();

		foreach(GameObject g in individuos){
			//Obtener codigo de familia
			Genes genes = g.GetComponent<Genes>();
			string codigoFam = genes.familia;

			if (!razas.ContainsKey (codigoFam)) {
				razas.Add (codigoFam, 1);
			} else {
				razas [codigoFam]++;
			}

			CalcularMejorAdaptado (g);
		}
		RazasVivas = razas.Count;
		IndividuosVivos = individuos.Length;
		print ("Razas vivas: " + RazasVivas);
	}

	void CalcularMejorAdaptado(GameObject individuo){
		if (MejorAdaptado == null) {
			MejorAdaptado = individuo;
			return;
		}

		int mejor, ind;
		mejor = MejorAdaptado.GetComponent<Genes> ().getCalificacion();
		ind = individuo.GetComponent<Genes> ().getCalificacion();

		if (ind > mejor)
			MejorAdaptado = individuo;
	}

}
