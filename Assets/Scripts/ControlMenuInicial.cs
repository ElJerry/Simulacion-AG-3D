using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMenuInicial : MonoBehaviour {

	public void CargarNivel(int id){
		Application.LoadLevel (id);
	}
}
