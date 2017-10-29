using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashFrameController : MonoBehaviour {

	//Camera
	public GameObject PlayerCamera;
	private float MouseSensitivity = 150.0f;
	private float CameraLimitSensitivity = 2.0f;
	[SerializeField] public float CameraRotationLimit = 0f;

	//Speed and movement
	private float Gspeed = 3000;
	public Rigidbody RB;

	// MISC.
	[SerializeField]public bool OnGround=true;
	[SerializeField] public bool canBoost = true;

	void Start () {

	}

	void Update () {

		OnGroundCheck ();

		float moveX = Input.GetAxis ("Horizontal") * Gspeed * Time.deltaTime;
		float moveZ = Input.GetAxis ("Vertical") * Gspeed * Time.deltaTime;
		float moveY = Input.GetAxis ("Flight") * 2500 * Time.deltaTime;
		//float boost = Input.GetAxis ("Jump") * Gspeed + 2000 * Time.deltaTime;

		if (Input.GetButton ("Horizontal") || Input.GetButton ("Vertical")) {
			Debug.Log ("Bout to call input");
			Move(moveX, moveZ);
			Debug.Log ("Just called input");
		}

		if (Input.GetButton ("Flight")) {
			Fly (moveY);
		}

		if (Input.GetButtonDown ("Jump")) {
			StartCoroutine(Boost ());
		}
		CameraRotationLimit -= Input.GetAxis ("Mouse Y") * CameraLimitSensitivity;
		CameraRotationLimit = Mathf.Clamp (CameraRotationLimit, -20, 29);

		this.gameObject.transform.rotation *= Quaternion.Euler (0.0f, Input.GetAxis ("Mouse X") * Time.deltaTime * MouseSensitivity, 0.0f);
		PlayerCamera.gameObject.transform.localEulerAngles = new Vector3 (CameraRotationLimit, 0.0f, 0.0f);
	}

	void Move(float movex, float movez){
		Vector3 movement = new Vector3 (movex, 0.0f, movez);
		transform.position += transform.forward * Time.deltaTime * movez;
		transform.position += transform.right * Time.deltaTime * movex;
	}

	void Fly(float movey){
		transform.position += transform.up * Time.deltaTime * movey;
	}

	void OnGroundCheck(){

	}

	void OnTriggerStay (Collider other){
		if (other.tag == "Terrain") {
			OnGround = true;
		} else {
			OnGround = false;
		}
	}

	public IEnumerator Boost(){
		if (canBoost == true) {
			canBoost = false;
			Gspeed += 4500;
			yield return new WaitForSeconds (0.5f);
			Gspeed -= 4500;
			StartCoroutine (BoostBuffer ());
		}

	}

	public IEnumerator BoostBuffer(){
		yield return new WaitForSeconds (2);
		canBoost = true;
	}
}