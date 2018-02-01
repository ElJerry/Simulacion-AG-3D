using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class Utils : MonoBehaviour
{	
	
	public static GameObject GetClosestGameObject(GameObject me, GameObject[] collection){

//		print ( me.name + " log: Search closest used");

		float diff = float.MaxValue, aux;
		int id = 0;

		for (int i = 0; i < collection.Length; i++) {
			aux = (me.transform.position - collection [i].transform.position).sqrMagnitude;

			if (aux == 0)
				continue;

			if (aux < diff) {
				id = i;
				diff = aux;
			}
		}

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
		nuevo.decodeGenes ();
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
}
