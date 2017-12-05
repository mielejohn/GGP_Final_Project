using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingCollider : MonoBehaviour {

	public FrameController FC;

	void Start () {
		
	}
	

	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		//print ("COLLIDING WITH SOMETHING");
		if (other.tag == "Enemy") {
			if (FC.Aiming != true) {
				FC.enemy = other.gameObject;
				FC.Aiming = true;
			}
		}
	}

	void OnTriggerExit(Collider other){
		//print ("SOMETHING IS LEAVING THE COLLIDER");
		if (other.gameObject == FC.enemy || FC.enemy == null) {
			FC.enemy = null;
			FC.Aiming = false;
		}
	}
}
