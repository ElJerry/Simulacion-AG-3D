using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Genes : MonoBehaviour {

	private List<int> genes = new List<int>();
	private int PuntosAdaptacion = 0;

	public static int probMutacion = 3;
	public int longevidad = 3600; // segundos

	public string familia;
	public Color col;
	public Color colorFamiliar;
	public int fuerza;
	public int velocidad;
	public int tamaño;

	// Descripcion de los Genes
	// 4 bits para longevidad - 1 a 17 minutos
	// fuerza - 3 bits, 8 puntos maximos de fuerza
	// velocidad - 2 bits

	public void createRandomGene(){
		//Fam codename
		familia = Utils.RandomString(5);

		//Longevidad
		for (int i = 0; i < 4; i++) {
			genes.Add (Random.Range (0, 2));
		}

		// fuerza
		for (int i = 0; i < 3; i++) {
			genes.Add (Random.Range (0, 2));
		}

		// velocidad
		for (int i = 0; i < 2; i++) {
			genes.Add (Random.Range (0, 2));
		}
	}

	public void decodeGenes(){
		
		int idx = 0;
		// Longevidad
		longevidad = TraduceRangoGenes(ref idx,4);
		longevidad++; //en caso de que la longevidad sea 0, se aumenta 1 a todo
		longevidad *= 20; //multiplicar longevidad para aumentar tiempo
		//print ("longevidad: " + (longevidad/60) + " minutos");


		// fuerza - 3 bits
		fuerza = TraduceRangoGenes(ref idx,3);
		fuerza++ ; //evitar que la fuerza sea 0

		// velocidad - 2 bits
		velocidad = TraduceRangoGenes(ref idx, 2);
		velocidad++; //evitar que la velocidad sea 0

		//Asignar colores al mesh
		float r = ColorPorcentaje(longevidad, 8*20);
		float g = ColorPorcentaje (fuerza, 8);
		float b = ColorPorcentaje (velocidad, 4);

		Color color = new Color(r,g,b,255);
		col = color;
		gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color = color;

		//asignar el color de ifentificacion familiar - heredado

		Material materialIdentificador = transform.Find ("IdFamilia").GetComponent<MeshRenderer> ().material;
		materialIdentificador.color = colorFamiliar;
	}

	float ColorPorcentaje(int valor, int maximo){
		float puntos = 255f / maximo;
		float res = puntos * valor;
		return res/255f;
	}

	int TraduceRangoGenes(ref int idx, int length){
		int res = 0;

		for (int i = 0; i < length; i++) {
			res = res << 1;
			res += genes [idx];
			idx++;
		}
		return res;
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

	void CalificarGenes(){
		/* Puntos a calificar
		 * Fuerza * 1
		 * Velocidad * 3
		 * tiempo de vida * 4
		 * */

		int puntuacion = 0;
		puntuacion += fuerza * 5;
		puntuacion += velocidad * 10;
		puntuacion += longevidad * 20;

		PuntosAdaptacion = puntuacion;
	}

	public int getCalificacion(){
		if (PuntosAdaptacion == 0)
			CalificarGenes ();

		return PuntosAdaptacion;
	}
}
