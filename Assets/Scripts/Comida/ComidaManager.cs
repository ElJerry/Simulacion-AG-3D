using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComidaManager : MonoBehaviour {

	public int puntos = 100;

	public void Comer(){
		puntos -= 5;

		if (puntos == 0)
			GameObject.Destroy (gameObject);
	}
}
