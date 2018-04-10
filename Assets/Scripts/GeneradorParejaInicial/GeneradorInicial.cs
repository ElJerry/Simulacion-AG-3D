using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorInicial : MonoBehaviour {

	GameObject individuo;
	static int contador = 1;

	public Color colorFamiliar;
	// Use this for initialization
	void Start () {

		individuo = Resources.Load ("individuo/individuo") as GameObject;

		var ind1 = Instantiate(individuo, transform.position, Quaternion.identity);
		Genes genesInd1 = ind1.GetComponent<Genes> ();
		genesInd1.createRandomGene ();

		//asignar el color de familia
		genesInd1.colorFamiliar = colorFamiliar;

		genesInd1.decodeGenes ();

		//ind1.AddComponent <cruzarManual>();
		ind1.name = "Individuo Inicial " + contador.ToString ();
		contador++;
		// Asignar hogar
		ind1.GetComponent<Estado>().hogar = gameObject;

		var ind2 = Instantiate(individuo,transform.position + (Vector3.left*4),Quaternion.identity);
		ind2.name = "Individuo2";
		Genes genesInd2 = ind2.GetComponent<Genes> ();

		//asignar el color de familia
		genesInd2.colorFamiliar = colorFamiliar;

		genesInd2.col = genesInd1.col;
		genesInd2.familia = genesInd1.familia;
		genesInd2.SetGenes(genesInd1.getGenes());
		genesInd2.decodeGenes ();
		ind2.name = "Individuo Inicial " + contador.ToString ();
		contador++;
		// Asignar hogar
		ind2.GetComponent<Estado>().hogar = gameObject;


	}
}
