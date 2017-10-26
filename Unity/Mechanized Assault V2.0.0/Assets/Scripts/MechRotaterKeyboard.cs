using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechRotaterKeyboard : MonoBehaviour {

	public bool isRotating;
	public float rotation;

	void Start () {
		rotation = 45f;
	}
	

	void Update () {
		if (Input.GetKey("d")) {
			On_D_down ();
		}

		if (Input.GetKey("a")) {
			On_A_down ();
		}
	}

	public void On_D_down(){
		this.transform.Rotate (new Vector3(0, -rotation, 0) *Time.deltaTime);
	}

	public void On_A_down(){
		this.transform.Rotate (new Vector3(0, rotation, 0) *Time.deltaTime);
	}

	public void RotationReset(){
		this.transform.rotation = Quaternion.Euler (0, 0, 0);
	}
}
