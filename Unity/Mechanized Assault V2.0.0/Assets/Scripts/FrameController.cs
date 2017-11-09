using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XInputDotNetPure;

public class FrameController : MonoBehaviour {

	public GameObject topObject;

	//Camera
	public GameObject PlayerCamera;
	private float MouseSensitivity = 175.0f;
	private float CameraLimitSensitivity = 2.0f;
	[SerializeField] public float CameraRotationLimitX = 0f;
	[SerializeField] public float CameraRotationLimitY = 0f;

	//Speed and movement
	[SerializeField]private float Gspeed;
	[SerializeField]private float Jspeed;
	[SerializeField]private float DashBuffer;
	public Rigidbody RB;
	float tempY = 0f;
	private float moveX;
	private float moveZ;

	//Stats
	[SerializeField]private int BaseHealth;
	[SerializeField]private int Health;

	//Spawns
	public GameObject LeftWeapon_Spawn;
	public GameObject RightWeapon_Spawn;

	//LeftWeapons
	public GameObject L_Assault_Rifle;
	public GameObject L_SMG;
	public GameObject L_Sniper_Rifle;

	//RightWeapons
	public GameObject R_Assault_Rifle;
	public GameObject R_SMG;
	public GameObject R_Sniper_Rifle;

	//Resource Crates
	[SerializeField]private int RepairBoxStock = 0;

	// MISC.
	public bool LockedOn = false;
	[SerializeField]public bool OnGround=true;
	[SerializeField] public bool canBoost = true;
	public GameObject enemy;
	public GameObject TestSpawn;


	//Controller input
	bool playerIndexSet = false;
	PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;


	void Start () {

		switch(PlayerPrefs.GetInt("FrameChoice")){
		case 0:
			Debug.Log ("Chose DASH");
			Gspeed = 5500;
			Jspeed = 90.0f;
			DashBuffer = 0.4f;
			BaseHealth = 35000;
			Health = 35000;
			break;

		case 1:
			Debug.Log ("Chose Assault");
			Gspeed = 4500;
			Jspeed = 82.0f;
			DashBuffer = 0.6f;
			BaseHealth = 42500;
			Health = 42500;
			break;

		case 2:
			Debug.Log ("Chose Support");
			Gspeed = 4000;
			Jspeed = 75.0f;
			DashBuffer = 0.9f;
			BaseHealth = 50000;
			Health = 50000;
			break;


		}


		LeftWeapon_Spawn = GameObject.FindGameObjectWithTag ("LWS");
		RightWeapon_Spawn = GameObject.FindGameObjectWithTag ("RWS");
		//enemy = GameObject.FindGameObjectWithTag ("Enemy");
		TestSpawn = GameObject.FindGameObjectWithTag ("TestSpawn");
		SpawnLeftWeapon ();
		SpawnRightWeapon ();
	}

	void Update () {

		//ControlleSync---------------------------------------------------------------------------------------------------------------------------

		if (!playerIndexSet || !prevState.IsConnected)
		{
			for (int i = 0; i < 4; ++i)
			{
				PlayerIndex testPlayerIndex = (PlayerIndex)i;
				GamePadState testState = GamePad.GetState(testPlayerIndex);
				if (testState.IsConnected)
				{
					Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
					playerIndex = testPlayerIndex;
					playerIndexSet = true;
				}
			}
		}

		prevState = state;
		state = GamePad.GetState (playerIndex);

		//Movement---------------------------------------------------------------------------------------------------------------------------------

		if (Input.GetButton ("Horizontal") || Input.GetButton ("Vertical")) {
			moveX = Input.GetAxis ("Horizontal") * Gspeed * Time.deltaTime;
			moveZ = Input.GetAxis ("Vertical") * Gspeed * Time.deltaTime;
			Debug.Log ("Bout to call Keyboard input");
			Move(moveX, moveZ);
			Debug.Log ("Just called Keyboard input");
		}

		if (state.ThumbSticks.Left.X > 0 || state.ThumbSticks.Left.Y > 0 || state.ThumbSticks.Left.X < 0 || state.ThumbSticks.Left.Y < 0) {
			moveX = state.ThumbSticks.Left.X * Gspeed * Time.deltaTime;
			moveZ = state.ThumbSticks.Left.Y * Gspeed * Time.deltaTime;
			Debug.Log ("Bout to call Controller input");
			Move(moveX, moveZ);
			Debug.Log ("Just called Controller input");
		}

		if (Input.GetButton ("Flight") || state.Triggers.Left > 0) {
			RB.velocity = new Vector3 (0, 25, 0);
		} else {
			RB.velocity = new Vector3 (0, -25, 0);
		}

		if (Input.GetAxis ("Horizontal") > 0) {
			Debug.Log ("HOrizontal is greater than 0");
			if (PlayerCamera.transform.localPosition.x < 400) {
				PlayerCamera.transform.Translate (Vector3.right * Time.deltaTime * 5);
			}
		}

		if (Input.GetAxis("Horizontal") < 0) {
			Debug.Log ("HOrizontal is Less than 0");
			if (PlayerCamera.transform.localPosition.x > -400) {
				PlayerCamera.transform.Translate (Vector3.left * Time.deltaTime * 5);
			}
		}

		if (Input.GetAxis ("Flight") > 0 || state.Triggers.Left > 0) {
			if (PlayerCamera.transform.localPosition.y > 100) {
				PlayerCamera.transform.Translate (Vector3.down * Time.deltaTime * 5);
			}
		} else {
			if (PlayerCamera.transform.localPosition.y < 568) {
				PlayerCamera.transform.Translate (Vector3.up * Time.deltaTime * 5);
			}
		}


		//Lockon----------------------------------------------------------------------------------------------------------------------------------

		if (Input.GetButtonDown ("Jump") || state.Triggers.Right > 0) {
			StartCoroutine(Boost ());
		}
		CameraRotationLimitX -= Input.GetAxis ("Mouse Y") * CameraLimitSensitivity;
		CameraRotationLimitX -= state.ThumbSticks.Right.Y * CameraLimitSensitivity;
		CameraRotationLimitX = Mathf.Clamp (CameraRotationLimitX, -10, 10);

		//CameraRotationLimitY -= Input.GetAxis ("Mouse X") * CameraLimitSensitivity;
		//CameraRotationLimitY = Mathf.Clamp (CameraRotationLimitY, -10, 15);
		//if (LockedOn == false) {
			this.gameObject.transform.rotation *= Quaternion.Euler (topObject.transform.rotation.x, Input.GetAxis ("Mouse X") * Time.deltaTime * MouseSensitivity, topObject.transform.rotation.z);// Player Rotation
			this.gameObject.transform.rotation *= Quaternion.Euler (topObject.transform.rotation.x, state.ThumbSticks.Right.X * Time.deltaTime * MouseSensitivity, topObject.transform.rotation.z);
			PlayerCamera.gameObject.transform.localEulerAngles = new Vector3 (CameraRotationLimitX, 0.0f, 0.0f); // Camera Rotation
		//}

		//RB.MoveRotation (transform.rotation * Time.deltaTime);

		if (Input.GetButtonDown ("LockOn") || prevState.Buttons.RightStick == ButtonState.Pressed && state.Buttons.RightStick == ButtonState.Released) {
			tempY = transform.rotation.eulerAngles.y;
			//Debug.Log("the y rotation is" + tempY);
			RaycastHit hit = new RaycastHit();
			//Physics.Raycast (transform.position, transform.forward, out hit);
			//Vector3 fwd = transform.TransformDirection(Vector3.forward);
			//if(Physics.Raycast(transform.forward, fwd, 500)){
			if (Physics.Raycast (transform.position, transform.forward, out hit) && LockedOn == false) {
				if (hit.collider.gameObject.tag == "Enemy") {
					enemy = hit.collider.gameObject;
					Debug.Log ("I hit the enemy, I think...");
					//this.transform.localRotation.y = enemy.transform.position;
					//transform.rotation *= Quaternion.LookRotation(enemy.transform.position - transform.position, Vector3.up);
					LockedOn = true;
				}
			} else {

				ResetRotations ();
				//PlayerCamera.transform.rotation = Quaternion.Euler (0.0f, this.gameObject.transform.rotation.y, 0.0f);
			}
		}

		if (LockedOn == true) {
			Debug.Log ("LockOn is true");
			transform.rotation = Quaternion.LookRotation (enemy.transform.position - transform.position, Vector3.up);
		}

		if (prevState.DPad.Up == ButtonState.Released && state.DPad.Up == ButtonState.Pressed && RepairBoxStock > 0) {
			if (Health + 5000 > BaseHealth) {
				Debug.Log ("Health + 5000 is greater than " + BaseHealth);
			
				Health = BaseHealth;
				RepairBoxStock--;
			} else {
				Health = Health + 5000;
				RepairBoxStock--;
			}
		}

		if (Input.GetKeyDown(KeyCode.Alpha1) && RepairBoxStock > 0) {
			if (Health + 5000 > BaseHealth) {
				Debug.Log ("Health + 5000 is greater than " + BaseHealth);
				int tempHealth = Health + 5000;
				Health += tempHealth - BaseHealth ;
				RepairBoxStock--;
			} else {
				Health = Health + 5000;
				RepairBoxStock--;
			}
		}

	}

	void Move(float movex, float movez){
		Debug.Log ("Calling move X and Z");
		transform.position += transform.forward * Time.deltaTime * movez;
		transform.position += transform.right * Time.deltaTime * movex;
	}

	void Fly(float movey){
		transform.Translate (Vector3.up * Time.deltaTime * Jspeed); 
		//transform.position += transform.up * Time.deltaTime * movey;
		//RB.AddForce(Vector3.up * Time.deltaTime * Jspeed);
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "RepairCrate") {
			RepairBoxStock++;
			Destroy (other.gameObject);
		}
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
			float BoostSpeed = Gspeed * 2;
			canBoost = false;
			Gspeed += BoostSpeed;
			yield return new WaitForSeconds (0.5f);
			Gspeed -= BoostSpeed;
			StartCoroutine (BoostBuffer ());
		}

	}

	public void ResetRotations(){
		Debug.Log ("Lockon is false");
		LockedOn = false;
		Debug.Log("the y rotation for reset is" + tempY);
		transform.rotation = Quaternion.Euler (0.0f, tempY, 0.0f);
	}

	public IEnumerator BoostBuffer(){
		yield return new WaitForSeconds (DashBuffer);
		canBoost = true;
	}

	public void SpawnLeftWeapon(){
		switch(PlayerPrefs.GetInt("LeftWeaponChoice")){

		case 0:
			GameObject L_SMG_I = Instantiate (L_SMG);
			Debug.Log ("L Weapon Spawned");
			L_SMG_I.transform.position = LeftWeapon_Spawn.transform.position;
			Debug.Log ("L Weapon positon Changed");
			L_SMG_I.transform.parent = transform;
			break;

		case 1:
			GameObject L_AssaultRifle_I = Instantiate (L_Assault_Rifle);
			Debug.Log ("L Weapon Spawned");
			L_AssaultRifle_I.transform.position = LeftWeapon_Spawn.transform.position;
			Debug.Log ("L Weapon positon Changed");
			L_AssaultRifle_I.transform.parent = transform;
			break;

		case 2:
			GameObject L_SniperRifle_I = Instantiate (L_Sniper_Rifle);
			Debug.Log ("L Weapon Spawned");
			L_SniperRifle_I.transform.position = LeftWeapon_Spawn.transform.position;
			Debug.Log ("L Weapon positon Changed");
			L_SniperRifle_I.transform.parent = transform;
			break;

		}
	}

	public void SpawnRightWeapon(){
		switch(PlayerPrefs.GetInt("RightWeaponChoice")){

		case 0:
			GameObject R_SMG_I = Instantiate (R_SMG);
			Debug.Log ("R Weapon Spawned");
			R_SMG_I.gameObject.transform.position = RightWeapon_Spawn.gameObject.transform.position;
			Debug.Log ("R Weapon positon Changed");
			R_SMG_I.transform.parent = transform;
			break;

		case 1:
			GameObject R_AssaultRifle_I = Instantiate (R_Assault_Rifle);
			Debug.Log ("R Weapon Spawned");
			R_AssaultRifle_I.gameObject.transform.position = RightWeapon_Spawn.gameObject.transform.position;
			Debug.Log ("R Weapon positon Changed");
			R_AssaultRifle_I.transform.parent = transform;
			break;

		case 2:
			GameObject R_SniperRifle_I = Instantiate (R_Sniper_Rifle);
			Debug.Log ("R Weapon Spawned");
			R_SniperRifle_I.gameObject.transform.position = RightWeapon_Spawn.gameObject.transform.position;
			Debug.Log ("R Weapon positon Changed");
			R_SniperRifle_I.transform.parent = transform;
			break;

		}
	}
}