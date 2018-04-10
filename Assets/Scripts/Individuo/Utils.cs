using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class Utils : MonoBehaviour
{	
	
	public static GameObject GetClosestGameObject(GameObject me, GameObject[] collection){

//		print ( me.name + " log: Search closest used");

		float diff = float.MaxValue, aux;
		int id = -10;

		for (int i = 0; i < collection.Length; i++) {

			if (collection [i] == me)
				continue;

			aux = (me.transform.position - collection [i].transform.position).sqrMagnitude;

			if (aux == 0)
				continue;
			
			if (aux < diff) {
				id = i;
				diff = aux;
			}
		}

		if (id == -10)
			return null;
		
		return collection [id];
	}

	public static GameObject GetClosestGameObjectWithFamilyCode(GameObject me, GameObject[] collection,string familia){

		//		print ( me.name + " log: Search closest used");

		float diff = float.MaxValue, aux;
		int id = -10;

		for (int i = 0; i < collection.Length; i++) {

			if (collection [i] == me)
				continue;

			//comprobar que tengan el mismo codigo familiar
			if (collection [i].GetComponent<Genes> ().familia != familia)
				continue;
			
			aux = (me.transform.position - collection [i].transform.position).sqrMagnitude;

			if (aux == 0)
				continue;

			if (aux < diff) {
				id = i;
				diff = aux;
			}
		}

		if (id == -10)
			return null;

		return collection [id];
	}

	public static void CruzarPareja(GameObject padre, GameObject madre){
		Genes papa, mama, nuevo;

		papa = padre.GetComponent<Genes> ();
		mama = madre.GetComponent<Genes> ();

		List<int> genesHijo = papa.CruzarGenes (mama.getGenes ());

		Vector3 posicionHijo = (padre.transform.position + madre.transform.position) / 2;


		GameObject hijo = GameObject.Instantiate (Resources.Load ("Individuo/Individuo") as GameObject, posicionHijo, Quaternion.identity);

		nuevo = hijo.GetComponent<Genes> ();
		nuevo.SetGenes (genesHijo);
		nuevo.colorFamiliar = papa.colorFamiliar;
		nuevo.decodeGenes ();
		nuevo.familia = papa.familia;
		hijo.transform.localScale = new Vector3 (1,1,1);


		Estado estadoHijo = hijo.GetComponent<Estado> ();
		estadoHijo.hambre = 50;
		estadoHijo.energia = 50;
		estadoHijo.hogar = papa.GetComponent<Estado> ().hogar;
		estadoHijo.puedeProcrear = false;
		estadoHijo.RutinaCrecer ();
	}

	public static Vector3 RemoveY(Vector3 position){
		return new Vector3(position.x,0,position.z);
	}

	public static int similitudGenetica(Genes g1, Genes g2){
		List<int> gen1 = g1.getGenes ();
		List<int> gen2 = g2.getGenes ();

		int sz = gen1.Count;

		int similares = 0;
		for (int i = 0; i < gen1.Count; i++) {
			if (gen1 [i] == gen2 [i])
				similares++;
		}

		int sim = (similares * 100) / sz;
		//print ("la similitud es " + sim);
		return sim;
	}

	public static string RandomString(int lenght){
		string letras = "abcdefghi123456789";
		int sz = letras.Length;
		string fam = "";

		for (int i = 0; i < lenght; i++) {
			fam += letras [Random.Range(0,sz)];
		}
		return fam;
	}
}
