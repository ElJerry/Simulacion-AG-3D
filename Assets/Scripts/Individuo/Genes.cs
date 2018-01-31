using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Genes : MonoBehaviour {

	private List<int> genes = new List<int>();
	public static int probMutacion = 3;

	//Descripcion de los Genes
	//color 8 bits por RGB

	//rango de vision - metros => min 5 max 100

	public void createRandomGene(){
		//color, RGB
		int times = 3;
		while (times-- > 0) {
			for (int i = 0; i < 8; i++) {
				genes.Add (Random.Range (0, 2)); //2 exclusivo = 0,1
			}
		}
	}

	public void decodeGenes(){
		//Color
		int r,g,b;
		r = g = b = 0;

		//r
		for (int i = 0; i < 8; i++) {
			r += genes [i];
			r = r << 1;
		}

		//g
		for (int i = 8; i < 16; i++) {
			g += genes [i];
			g = g << 1;
		}

		//b
		for (int i = 16; i < 24; i++) {
			b += genes [i];
			b = b << 1;
		}

		//Asignar colores al mesh
		Color color = new Color(r/255,g/255,b/255,0);
		gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color = color;
	}

	public List<int> CruzarGenes(List<int> pareja){

		int tam = genes.Count;
		int puntoCruce = Random.Range(1, tam);

//		print ("Cruce: " + puntoCruce);

		List<int> genesNuevos = new List<int> ();

		for (int i = 0; i < tam; i++) {
			int gen;
			if (i <= puntoCruce) {
				gen = genes [i];	
			} else {
				gen = pareja [i];	
			}

			int mutacion = Random.Range (0, 101);

			if (mutacion < probMutacion) { // mutacion
				gen = gen==1?0:1;
				//print (transform.name + " tubo una mutacion!");
			}

			genesNuevos.Add (gen);

		}

		return genesNuevos;
	}

	public List<int> getGenes(){
		return genes;
	}

	public void SetGenes(List<int> genes){
		this.genes = genes;
	}

	public void printGenes(){
		string s = transform.name+ ": ";
		for (int i = 0; i < genes.Count; i++) {
			s += genes[i].ToString ();
		}
		print (s);
	}



}
