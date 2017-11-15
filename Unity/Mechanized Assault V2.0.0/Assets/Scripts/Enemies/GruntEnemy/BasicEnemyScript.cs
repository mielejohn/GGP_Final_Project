using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicEnemyScript : MonoBehaviour {

	[SerializeField]private int health = 7500;
	public GameObject Player;
	public Slider HealthSlider;
	//public GameObject playerCameraObj;
	//public Camera playerCamera;
	//public Canvas E_Canvas;

	protected void Start () {
		
	}

	void Update () {
		HealthSlider.value = health;
		//Player = GameObject.FindGameObjectWithTag ("Player");
		//playerCameraObj = GameObject.FindGameObjectWithTag ("PlayerCamera");
		//playerCamera = playerCameraObj.GetComponent<Camera>();
		//transform.LookAt(transform.position + playerCamera.transform.rotation * Vector3.forward, playerCamera.transform.rotation * Vector3.up);

		if (health <= 0 && Player.gameObject.GetComponent<FrameController> ().LockedOn == true) {
			//Player.gameObject.GetComponent<FrameController> ().ResetRotations ();
			Destroy (this.gameObject);
		} else if (health < -0) {
			Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "SniperBullet") {
			Destroy (other);
			health -= 300;
		}

		if (other.tag == "ARBullet") {
			Destroy (other);
			health -= 200;
		}

		if (other.tag == "SMGBullet") {
			Destroy (other);
			health -= 100;
		}
	}
}
