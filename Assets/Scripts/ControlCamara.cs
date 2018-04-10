using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCamara : MonoBehaviour {

	public int speed = 15;

	bool mostrarMouse = false;

	// rotacion de la camara ========
	public float speedH = 2.0f;
	public float speedV = 2.0f;

	private float yaw = 0.0f;
	private float pitch = 0.0f;
	// ==============================

	void Start(){
		Cursor.visible = mostrarMouse;
	}

	void Update () {
		// controles de direccion =============================================
		if (Input.GetKey (KeyCode.W)) {
			transform.Translate (Vector3.forward * Time.deltaTime * speed);
		}

		if (Input.GetKey (KeyCode.A)) {
			transform.Translate (Vector3.left * Time.deltaTime * speed);
		}

		if (Input.GetKey (KeyCode.D)) {
			transform.Translate (Vector3.right * Time.deltaTime * speed);
		}

		if (Input.GetKey (KeyCode.S)) {
			transform.Translate (Vector3.back * Time.deltaTime * speed);
		}

		if (Input.GetKey (KeyCode.Q)) {
			transform.Translate (Vector3.up * Time.deltaTime * speed);
		}

		if (Input.GetKey (KeyCode.E)) {
			transform.Translate (Vector3.down * Time.deltaTime * speed);
		}
		// ====================================================================

		// toggle mouse
		if (Input.GetKeyDown (KeyCode.F)) {
			mostrarMouse = !mostrarMouse;

			Cursor.visible = mostrarMouse;
		}

		// rotacion camara
		if (!mostrarMouse) {
			yaw += speedH * Input.GetAxis("Mouse X");
			pitch -= speedV * Input.GetAxis("Mouse Y");

			transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
		}
		// =======================


	}
}
