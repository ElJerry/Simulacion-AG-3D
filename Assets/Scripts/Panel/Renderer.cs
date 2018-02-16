﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Renderer : MonoBehaviour {

	// Elementos necesarios para mostrar informacion del individuo
	private Text textoNombre;
	private Text textoGenes;
	private Text textoFuerza;
	private Text textoVelocidad;
	private Text textoTve;
	private Image imagenColor;
	private Genes sampleGenes;

	void Awake(){
		textoNombre = transform.GetChild (0).GetChild (0).GetComponent<Text> ();
		textoGenes = transform.GetChild (0).GetChild (2).GetComponent<Text> ();
		textoFuerza = transform.GetChild (0).GetChild (6).GetComponent<Text> ();
		textoVelocidad = transform.GetChild (0).GetChild (7).GetComponent<Text> ();
		textoTve = transform.GetChild (0).GetChild (8).GetComponent<Text> ();
		imagenColor = transform.GetChild (0).GetChild (4).GetComponent<Image> ();
		sampleGenes = transform.GetChild (0).GetChild (5).GetComponent<Genes> ();
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
		textoGenes.text = "Genes: " + genCode;

		textoFuerza.text = "Fuerza: " + genes.fuerza;
		textoVelocidad.text = "Velocidad: " + genes.velocidad;
		textoTve.text = "Tiempo vida\nestmado: " + genes.getLongevidad () + " segundos";

		// Mostrar color del individuo
		imagenColor.color = genes.col;

		//Asignar genes al sample 3D
		sampleGenes.SetGenes(genes.getGenes());
		sampleGenes.decodeGenes ();

	}
}
