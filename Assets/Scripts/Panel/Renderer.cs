using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Renderer : MonoBehaviour {

	// Elementos necesarios para mostrar informacion del individuo
	private Text textoNombre;
	private Text textoGenes;
	public Image imagenColor;

	void Start(){
		textoNombre = transform.GetChild (0).GetChild (0).GetComponent<Text> ();
		textoGenes = transform.GetChild (0).GetChild (2).GetComponent<Text> ();
		imagenColor = transform.GetChild (0).GetChild (4).GetComponent<Image> ();

	}

	public void Render(GameObject objeto){
		//Mostrar el nombre del individuo
		textoNombre.text = objeto.name;
		// Mostrar los genes del individuo
		Genes genes = objeto.GetComponent<Genes>();
		string genCode = "";
		foreach (int n in genes.getGenes()) {
			genCode += n.ToString ();
		}

		textoGenes.text = "Genes: " + genCode + " gencode";

		imagenColor.color = genes.col;

	}
}
