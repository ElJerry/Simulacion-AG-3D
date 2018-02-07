using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Genes : MonoBehaviour {

	private List<int> genes = new List<int>();
	public static int probMutacion = 3;
	int longevidad = 3600; // segundos

	public string familia;
	public Color col;
	//Descripcion de los Genes
	//color 8 bits por RGB
	//4 bits para longevidad - 1 a 17 minutos

	public void createRandomGene(){
		//Fam codename
		familia = Utils.RandomString(5);
		print ("Familia: " + familia);

		//color, RGB
		int times = 3;
		while (times-- > 0) {
			for (int i = 0; i < 8; i++) {
				genes.Add (Random.Range (0, 2)); //2 exclusivo = 0,1
			}
		}

		//Longevidad
		for (int i = 0; i < 4; i++) {
			genes.Add (Random.Range (0, 2));
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

		// Longevidad
		longevidad = 0;
		for (int i = 24; i < 28; i++) {
			longevidad += genes [i];
			longevidad = longevidad << 1;
		}
		longevidad++; //en caso de que la longevidad sea 0, se aumenta 1 a todo
		longevidad *= 20; //multiplicar longevidad para aumentar tiempo
		//print ("longevidad: " + (longevidad/60) + " minutos");

		//Asignar colores al mesh
		Color color = new Color(r/255,g/255,b/255,255);
		col = color;

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

	public int getLongevidad(){
		return longevidad;
	}
}
