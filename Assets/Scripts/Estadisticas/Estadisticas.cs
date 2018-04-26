using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Estadisticas : MonoBehaviour {

	public int TiempoDeEjecucion;

	// Estadisticas a enumerar
	public int IndividuosVivos, RazasVivas;
	public GameObject MejorAdaptado;
	public static int mutaciones, individuosNacidos, individuosMuertos;


	// Controles para mostrar la informacion
	Text text;
	GameObject canvas;

	GameObject[] individuos;

	void Start(){
		mutaciones = individuosNacidos = individuosMuertos = 0;		
		canvas = transform.Find ("Canvas").gameObject;
		text = canvas.transform.Find ("Panel").Find("Consola").GetComponent<Text> ();
		canvas.SetActive (false);
		StartCoroutine (Iniciar());
	}

	IEnumerator Iniciar(){
		yield return new WaitForSeconds(TiempoDeEjecucion);
		Time.timeScale = 0;
		//Activar canvas
		ActivarCanvas();

		ObtenerEstadisticas ();
	}

	private void ActivarCanvas(){
		// Activar canvas
		canvas.SetActive(true);

		// Desactivar analogia
		Analogia.Enabled = false;

		// Desactivar control de camara
		GameObject.Find("Camera").GetComponent<ControlCamara>().enabled = false;


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

		// imprimir estadisticas
		text.text = "";
		text.text += ("Razas vivas: " + RazasVivas) + "\n";
		text.text += ("Individuos nacidos: " + individuosNacidos) + "\n";
		text.text += ("Individuos Muertos: " + individuosMuertos) + "\n";
		text.text += ("Mutaciones Generadas: " + mutaciones);
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
