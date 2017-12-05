using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XInputDotNetPure;
using UnityEngine.UI;

public class FrameController : MonoBehaviour {

	[Header("Game Manager")]
	public GameObject GM;
	public GameManager GameManager;

	[Space]
	[Header("Top Object reference")]
	public GameObject topObject;

	[Space]
	[Header("UI Elements")]
	public Image HealthBarImage;
	public Image DashImage;
	public Image FlightImage;
	public GameObject OutofBoundsObject;
	public Text OutOfBoundsCountdown;


	[Space]
	[Header("Camera")]
	//Camera
	public GameObject PlayerCamera;
	private float MouseSensitivity = 175.0f;
	private float CameraLimitSensitivity = 2.0f;
	[SerializeField] public float CameraRotationLimitX = 0f;
	[SerializeField] public float CameraRotationLimitY = 0f;

	[Space]
	[Header("Speed and Movement")]
	//Speed and movement
	[SerializeField]private float Gspeed;
	[SerializeField]private float Jspeed;
	[SerializeField]private float DashBuffer;
	[SerializeField]private float DashCost;
	[SerializeField] private float DashCapacity;
	[SerializeField] private float BaseDashCapacity;
	[SerializeField] private float DashRegenBufferTime;
	[SerializeField] private float DashRegenAmount;
	public Rigidbody RB;
	public float moveX;
	public float moveZ;

	[Space]
	[Header("Stats")]
	//Stats
	[SerializeField]private int BaseHealth;
	private bool Dead = false;
	[SerializeField]private int Health;
	[SerializeField]private float FlyAmount;
	[SerializeField]private float BaseFlyAmount;

	[Space]
	[Header("Spawns")]
	//Spawns
	public GameObject LeftWeapon_Spawn;
	public GameObject RightWeapon_Spawn;

	[Space]
	[Header("Left Weapons")]
	//LeftWeapons
	public GameObject L_Arm;
	public GameObject L_Assault_Rifle;
	public GameObject L_SMG;
	public GameObject L_Sniper_Rifle;

	[Space]
	[Header("Right Weapons")]
	//RightWeapons
	public GameObject R_Arm;
	public GameObject R_Assault_Rifle;
	public GameObject R_SMG;
	public GameObject R_Sniper_Rifle;

	[Space]
	[Header("Resources")]
	//Resource Crates
	[SerializeField]private int RepairBoxStock = 0;

	[Space]
	[Header("Health UI")]
	public Text HealthNumber;

	[Space]
	[Header("Animations")]
	public Animator animator;
	private bool FromLeft;
	private bool FromRight;
	public GameObject Head;
	public float HeadRotationlimit;
	//public GameObject LeftArm;
	//public GameObject RightArm;


	[Space]
	[Header("ParticleSystems")]
	public GameObject Flight_1_PS;
	public GameObject Flight_2_PS;
	public GameObject Flight_3_PS;
	public GameObject Flight_4_PS;
	public GameObject Flight_5_PS;
	public GameObject Flight_6_PS;
	public GameObject Boost_1_PS;
	public GameObject Boost_2_PS;
	public GameObject Dust;
	public GameObject WaterMist;

	[Space]
	[Header("DeathParticleSystems")]
	public GameObject Explosion_Particles;

	[Space]
	[Header("Audio")]
	public AudioSource Audio_S;
	public AudioClip StandardMovement_S;
	public AudioClip Explosion_S;
	public AudioClip Flight_S;
	public AudioClip Boost_S;


	[Space]
	[Header("Misc.")]
	// MISC.
	public bool LockedOn = false;
	[SerializeField]public bool OnGround=true;
	[SerializeField]public bool onWater=true;
	[SerializeField] public bool canBoost = true;
	public GameObject enemy;
	public Camera RayCam;
	public bool Aiming=false;
	public bool DashRegen = false;
	public bool OutofBounds = false;
	public int OutOfBoundsCountdownINT = 10;
	public GameObject AimingCollider;
	public Level_1_LC LC;
	//public Text HelperText;
	private bool FlyRegen;

	[Space]
	[Header("Controller Input")]
	PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;


	void Start () {
		//animator = GetComponent<Animator> ();
		LC = GameObject.FindGameObjectWithTag("LevelController").GetComponent<Level_1_LC>();
		GM = GameObject.FindGameObjectWithTag ("GameManage");
		GameManager = GM.GetComponent<GameManager> ();
//		HelperText = GameObject.FindGameObjectWithTag ("HelperText").GetComponent<Text> ();

		#region ChoosingStats
		switch(PlayerPrefs.GetInt("FrameChoice")){
		case 0:
			Debug.Log ("Chose DASH");
			Gspeed = 5500;
			Jspeed = 50.0f;
			FlyAmount = 90;
			BaseFlyAmount = 90;
			DashBuffer = 0.3f;
			DashCost = 164;
			DashCapacity = 1000;
			BaseDashCapacity = 1000;
			DashRegenBufferTime = 0.15f;
			DashRegenAmount = 3;
			BaseHealth = 10000;
			Health = 10000;
			break;

		case 1:
			Debug.Log ("Chose Assault");
			Gspeed = 4500;
			Jspeed = 45.0f;
			FlyAmount = 75;
			BaseFlyAmount = 75;
			DashBuffer = 0.4f;
			DashCost = 190;
			DashCapacity = 1200;
			BaseDashCapacity = 1000;
			DashRegenBufferTime = 0.20f;
			DashRegenAmount = 2.55f;
			BaseHealth = 15000;
			Health = 15000;
			break;

		case 2:
			Debug.Log ("Chose Support");
			Gspeed = 4000;
			Jspeed = 40.0f;
			FlyAmount = 60;
			BaseFlyAmount = 60;
			DashBuffer = 0.7f;
			DashCost = 205;
			DashCapacity = 1000;
			BaseDashCapacity = 1000;
			DashRegenBufferTime = 0.25f;
			DashRegenAmount = 2.25f;
			BaseHealth = 20000;
			Health = 20000;
			break;
		}
		#endregion

		LeftWeapon_Spawn = GameObject.FindGameObjectWithTag ("LWS");
		RightWeapon_Spawn = GameObject.FindGameObjectWithTag ("RWS");
		SpawnLeftWeapon ();
		SpawnRightWeapon ();

		AimingCollider.transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
		HealthNumber = GameObject.FindGameObjectWithTag ("HealthNumber").GetComponent<Text> ();
	}

	void Update () {

		HealthBar ();
		BoostBar ();
		FlightBar ();
		HealthNumber.text = Health + "";
		if (Dead == false) {
			if (!GameManager.prevState.IsConnected) {
				#region KeyBoardControls
				if (Input.GetButton ("Horizontal") || Input.GetButton ("Vertical")) {
					moveX = Input.GetAxis ("Horizontal") * Gspeed * Time.deltaTime;
					moveZ = Input.GetAxis ("Vertical") * Gspeed * Time.deltaTime;

					Move (moveX, moveZ);
				}


				CameraRotationLimitX -= Input.GetAxis ("Mouse Y") * CameraLimitSensitivity;
				CameraRotationLimitX = Mathf.Clamp (CameraRotationLimitX, -30, 30);

				this.gameObject.transform.rotation *= Quaternion.Euler (topObject.transform.rotation.x, Input.GetAxis ("Mouse X") * Time.deltaTime * MouseSensitivity, topObject.transform.rotation.z);// Player Rotation
				PlayerCamera.gameObject.transform.localEulerAngles = new Vector3 (CameraRotationLimitX, 0.0f, 0.0f);

				HeadRotationlimit -= Input.GetAxis ("Mouse Y") * CameraLimitSensitivity;
				HeadRotationlimit = Mathf.Clamp (HeadRotationlimit, -5, 10);
				Head.transform.localEulerAngles = new Vector3 (HeadRotationlimit, 0.0f, 0.0f);

				if (FlyAmount > 0 && Input.GetAxis ("Flight") > 0.1) {
					Flight_1_PS.SetActive (true);
					Flight_2_PS.SetActive (true);
					Flight_3_PS.SetActive (true);
					Flight_4_PS.SetActive (true);
					Flight_5_PS.SetActive (true);
					Flight_6_PS.SetActive (true);
					FlyAmount = FlyAmount - 0.5f;
					RB.velocity = new Vector3 (0, Jspeed, 0);
				} else {
					RB.velocity = new Vector3 (0, -Jspeed, 0);
					Flight_1_PS.SetActive (false);
					Flight_2_PS.SetActive (false);
					Flight_3_PS.SetActive (false);
					Flight_4_PS.SetActive (false);
					Flight_5_PS.SetActive (false);
					Flight_6_PS.SetActive (false);
					if (FlyRegen == false) {
						FlyRegen = true;
						StartCoroutine (RegenFly ());
					}
				}

				if (Input.GetKeyDown (KeyCode.Alpha1) && RepairBoxStock > 0) {
					if (Health + 5000 > BaseHealth) {
						Health = BaseHealth;
						RepairBoxStock--;
					} else {
						Health = Health + 5000;
						RepairBoxStock--;
					}
				}


				if (Input.GetButtonDown ("Jump")) {
					StartCoroutine (Boost ());
				}

				if (Input.GetAxis ("Horizontal") > 0) {
					if (PlayerCamera.transform.localPosition.x < 450) {
						PlayerCamera.transform.Translate (Vector3.right * Time.deltaTime * 7);
					}
				}

				if (Input.GetAxis ("Horizontal") < 0) {
					if (PlayerCamera.transform.localPosition.x > -450) {
						PlayerCamera.transform.Translate (Vector3.left * Time.deltaTime * 7);
					}
				}

				if (Input.GetAxis ("Flight") > 0) {
					if (PlayerCamera.transform.localPosition.y > 75) {
						PlayerCamera.transform.Translate (Vector3.down * Time.deltaTime * 7);
					}
				} else {
					if (PlayerCamera.transform.localPosition.y < 568) {
						PlayerCamera.transform.Translate (Vector3.up * Time.deltaTime * 7);
					}
				}


				if (Input.GetAxis ("Horizontal") > 0.1 && OnGround == true) {
					//print (Input.GetAxis ("Horizontal"));
					animator.SetBool ("StandingFromLeft", true);
					animator.SetBool ("StandingFromRight", false);
					animator.SetBool ("Moving_Right", true);
					//animator.SetBool ("Moving_Left", false);
				}


				if (Input.GetAxis ("Horizontal") < -0.1 && OnGround == true) {
					//print (Input.GetAxis ("Horizontal"));
					animator.SetBool ("StandingFromRight", true);
					animator.SetBool ("StandingFromLeft", false);
					animator.SetBool ("Moving_Left", true);
					//animator.SetBool ("Moving_Right", false);
				}

				if (Input.GetAxis ("Horizontal") == 0) {
					animator.SetBool ("Moving_Right", false);
					animator.SetBool ("StandingFromRight", true);
					animator.SetBool ("Moving_Left", false);
					animator.SetBool ("StandingFromLeft", true);
				}
				#endregion
			}

			if (GameManager.prevState.IsConnected) {
				#region ControllerInputs
				if (GameManager.state.ThumbSticks.Left.X > 0 || GameManager.state.ThumbSticks.Left.Y > 0 || GameManager.state.ThumbSticks.Left.X < 0 || GameManager.state.ThumbSticks.Left.Y < 0) {
					moveX = GameManager.state.ThumbSticks.Left.X * Gspeed * Time.deltaTime;
					moveZ = GameManager.state.ThumbSticks.Left.Y * Gspeed * Time.deltaTime;
					Move (moveX, moveZ);
				}

				if ( GameManager.state.ThumbSticks.Left.X > 0) {
					if (PlayerCamera.transform.localPosition.x < 450) {
						PlayerCamera.transform.Translate (Vector3.right * Time.deltaTime * 7);
					}
				}

				if (GameManager.state.ThumbSticks.Left.X < 0) {
					if (PlayerCamera.transform.localPosition.x > -450) {
						PlayerCamera.transform.Translate (Vector3.left * Time.deltaTime * 7);
					}
				}

				if (state.Triggers.Left > 0) {
					if (PlayerCamera.transform.localPosition.y > 75) {
						PlayerCamera.transform.Translate (Vector3.down * Time.deltaTime * 7);
					}
				} else {
					if (PlayerCamera.transform.localPosition.y < 568) {
						PlayerCamera.transform.Translate (Vector3.up * Time.deltaTime * 7);
					}
				}

				if (FlyAmount > 0 && GameManager.state.Triggers.Left > 0.1) {
					Flight_1_PS.SetActive (true);
					Flight_2_PS.SetActive (true);
					Flight_3_PS.SetActive (true);
					Flight_4_PS.SetActive (true);
					Flight_5_PS.SetActive (true);
					Flight_6_PS.SetActive (true);
					FlyAmount = FlyAmount - 0.5f;
					RB.velocity = new Vector3 (0, Jspeed, 0);
				} else {
					Flight_1_PS.SetActive (false);
					Flight_2_PS.SetActive (false);
					Flight_3_PS.SetActive (false);
					Flight_4_PS.SetActive (false);
					Flight_5_PS.SetActive (false);
					Flight_6_PS.SetActive (false);
					RB.velocity = new Vector3 (0, -Jspeed, 0);
					if (FlyRegen == false) {
						FlyRegen = true;
						StartCoroutine (RegenFly ());
					}
				}

				if (GameManager.state.Triggers.Right > 0.10 && canBoost == true) {
					StartCoroutine (Boost ());
				}

				if (GameManager.state.ThumbSticks.Left.X > 0.2 && OnGround == true) {
					animator.SetBool ("StandingFromLeft", true);
					animator.SetBool ("StandingFromRight", false);
					animator.SetBool ("Moving_Right", true);
				}


				if (GameManager.state.ThumbSticks.Left.X < -0.2 && OnGround == true) {
					animator.SetBool ("StandingFromRight", true);
					animator.SetBool ("StandingFromLeft", false);
					animator.SetBool ("Moving_Left", true);
				}

				if (GameManager.state.ThumbSticks.Left.X < 0.2 && GameManager.state.ThumbSticks.Left.X > -0.2) {
					animator.SetBool ("Moving_Right", false);
					animator.SetBool ("StandingFromRight", true);
					animator.SetBool ("Moving_Left", false);
					animator.SetBool ("StandingFromLeft", true);
				}



				if (GameManager.state.Triggers.Right > 0.25 && GameManager.state.ThumbSticks.Right.X > 0.90 && canBoost == true) {
					transform.rotation = Quaternion.Lerp (transform.rotation, transform.rotation *= Quaternion.Euler (0, 70, 0), 40.0f * Time.deltaTime);
					canBoost = false;
					StartCoroutine (Boost ());
				}

				if (GameManager.state.Triggers.Right < -0.25 && GameManager.state.ThumbSticks.Right.X > 0.90 && canBoost == true) {
					transform.rotation = Quaternion.Lerp (transform.rotation, transform.rotation *= Quaternion.Euler (0, 70, 0), 40.0f * Time.deltaTime);
					canBoost = false;
					StartCoroutine (Boost ());
				}


				CameraRotationLimitX -= GameManager.state.ThumbSticks.Right.Y * CameraLimitSensitivity;
				CameraRotationLimitX = Mathf.Clamp (CameraRotationLimitX, -30, 30);

				HeadRotationlimit -= GameManager.state.ThumbSticks.Right.Y * CameraLimitSensitivity;
				HeadRotationlimit = Mathf.Clamp (HeadRotationlimit, -5, 10);
				Head.transform.localEulerAngles = new Vector3 (HeadRotationlimit, 0.0f, 0.0f);

				this.gameObject.transform.rotation *= Quaternion.Euler (topObject.transform.rotation.x, GameManager.state.ThumbSticks.Right.X * Time.deltaTime * MouseSensitivity, topObject.transform.rotation.z);

				PlayerCamera.gameObject.transform.localEulerAngles = new Vector3 (CameraRotationLimitX, 0.0f, 0.0f); // Camera Rotation

				if (GameManager.prevState.DPad.Up == ButtonState.Released && GameManager.state.DPad.Up == ButtonState.Pressed || Input.GetKeyDown (KeyCode.Alpha1) && RepairBoxStock > 0) {
					if (Health + 5000 > BaseHealth) {
						Health = BaseHealth;
						RepairBoxStock--;
					} else {
						Health = Health + 5000;
						RepairBoxStock--;
					}
				}
				#endregion
			}
		}

		#region Aiming
		AimingCollider.transform.localPosition= new Vector3(AimingCollider.transform.localPosition.x,this.transform.localPosition.y,AimingCollider.transform.localPosition.y);
		AimingCollider.transform.rotation = Quaternion.Euler(new Vector3(CameraRotationLimitX,this.transform.localEulerAngles.y,0));

		if (Aiming == true && enemy != null) {
			L_Arm.gameObject.transform.LookAt (enemy.transform);
			R_Arm.gameObject.transform.LookAt (enemy.transform);
		} else if (enemy == null || Aiming == false) {
			L_Arm.transform.localEulerAngles = new Vector3 (CameraRotationLimitX,L_Arm.transform.rotation.y,L_Arm.transform.rotation.z);
			R_Arm.transform.localEulerAngles = new Vector3 (CameraRotationLimitX,R_Arm.transform.rotation.y,R_Arm.transform.rotation.z);
		}
		#endregion

		#region OnGroundParticles
		RaycastHit hit = new RaycastHit ();
		if (Physics.Raycast (transform.position, -Vector3.up, out hit)) {
			var distanceToGround = hit.distance;
			if (distanceToGround < 15) {
				if (hit.transform.gameObject.tag == "Terrain") {
					OnGround = true;
					onWater = false;
				}
				if (hit.transform.gameObject.tag == "water") {
					onWater = true;
					OnGround = false;
				}
			} else {
				OnGround = false;
				onWater = false;
			}
		}

		if(OnGround == false){
			animator.SetBool ("StandingFromRight", false);
			animator.SetBool ("StandingFromRight", false);
			animator.SetBool ("Grounded", false);
			animator.SetBool ("Flying", true);
		} else if(OnGround == true){
			animator.SetBool ("Flying", false);
			animator.SetBool ("Grounded", true);
		}

		if (OnGround == true) {
			WaterMist.SetActive (false);
			Dust.SetActive (true);
		} else if (onWater == true) {
			WaterMist.SetActive (true);
			Dust.SetActive (false);
		} else {
			Dust.SetActive (false);
			WaterMist.SetActive (false);
		}
		#endregion

		#region HealthCheck
		if (Health <= 0 && Dead == false) {
			print ("You are dead");
			Dead = true;
			StartCoroutine(Dead_M ());
		}
		#endregion

		#region OutOfBoundsCheck
		if (OutofBounds == false) {
			OutofBoundsObject.SetActive (false);
			OutOfBoundsCountdownINT = 10;
		}
		#endregion
	}

	void Move(float movex, float movez){
		transform.position += transform.forward * Time.deltaTime * movez;
		transform.position += transform.right * Time.deltaTime * movex;
	}

	void Fly(float movey){
		transform.Translate (Vector3.up * Time.deltaTime * Jspeed); 
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "RepairCrate") {
			other.gameObject.GetComponent<Supply_Crate_Generic> ().Audio.Play();
			RepairBoxStock++;
			Destroy (other.gameObject);
		}

		if (Health > 0) {
			if (other.tag == "L1_Bullet") {
				Health -= 75;
				Destroy (other.gameObject);
			}
		}
	}

	/*void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Terrain") {
			OnGround = true;
		}
	}

	void OnCollisionExit(Collision col){
		if (col.gameObject.tag == "Terrain") {
			OnGround = false;
		}
	}*/

	public IEnumerator Boost(){
		Boost_1_PS.SetActive(true);
		Boost_2_PS.SetActive(true);
		if (canBoost == true && DashCapacity - DashCost > 0) {
			canBoost = false;
			DashCapacity -= DashCost;
			StartCoroutine (DashRecharge ());
			float BoostSpeed = Gspeed * 2;
			Gspeed += BoostSpeed;
			yield return new WaitForSeconds (0.25f);
			StartCoroutine (BoostBuffer ());
			yield return new WaitForSeconds (0.25f);
			Gspeed -= BoostSpeed;
		}
		Boost_1_PS.SetActive(false);
		Boost_2_PS.SetActive(false);
	}

	public IEnumerator DashRecharge(){
		while(DashCapacity < 1000){
			DashCapacity += DashRegenAmount;
			yield return new WaitForSeconds (DashRegenBufferTime);
		}
	}

	public IEnumerator BoostBuffer(){
		yield return new WaitForSeconds (DashBuffer);
		canBoost = true;
	}

	private IEnumerator RegenFly(){
		while (FlyAmount < BaseFlyAmount) {
			yield return new WaitForSeconds (0.5f);
			FlyAmount += 4;
		}
		FlyRegen = false;
	}

	public IEnumerator OutofBounds_m(){
		int time = 10;
		OutofBoundsObject.SetActive (true);
		for (int i = time; i >= 0; i--) {
			OutOfBoundsCountdown.text = i + "";
			time--;
			yield return new WaitForSeconds (1.0f);
		}
		print ("fell out of the loop");

		if (time <= 1 && OutofBounds == true) {
			print ("MissionFailed Called");
			StartCoroutine( LC.MissionFailed ());
		} else if(OutofBounds == false){
			time = 10;
		}
	}


	//UI Elements--------------------------------------------------------------------------------

	public void HealthBar(){
		HealthBarImage.fillAmount = HealthBarMap (Health, 0, BaseHealth, 0, 1);
	}

	private float HealthBarMap(float Value, float inMin, float inMax, float outMin, float outMax){
		return (Value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}

	public void BoostBar(){
		DashImage.fillAmount = BoostBarMap (DashCapacity, 0, BaseDashCapacity, 0, 1);
	}

	private float BoostBarMap(float Value, float inMin, float inMax, float outMin, float outMax){
		return (Value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}

	public void FlightBar(){
		FlightImage.fillAmount = FlightBarMap (FlyAmount, 0, BaseFlyAmount, 0, 1);
	}

	private float FlightBarMap(float Value, float inMin, float inMax, float outMin, float outMax){
		return (Value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}


	//Weapon Spawning-----------------------------------------------------------------------------

	public void SpawnLeftWeapon(){
		switch(PlayerPrefs.GetInt("LeftWeaponChoice")){

		case 0:
			GameObject L_SMG_I = Instantiate (L_SMG);
			L_SMG_I.transform.position = LeftWeapon_Spawn.transform.position;
			L_SMG_I.transform.parent = L_Arm.gameObject.transform;
			break;

		case 1:
			GameObject L_AssaultRifle_I = Instantiate (L_Assault_Rifle);
			L_AssaultRifle_I.transform.position = LeftWeapon_Spawn.transform.position;
			L_AssaultRifle_I.transform.parent = L_Arm.gameObject.transform;
			break;

		case 2:
			GameObject L_SniperRifle_I = Instantiate (L_Sniper_Rifle);
			L_SniperRifle_I.transform.position = LeftWeapon_Spawn.transform.position;
			L_SniperRifle_I.transform.parent = L_Arm.gameObject.transform;
			break;

		}
	}

	public void SpawnRightWeapon(){
		switch(PlayerPrefs.GetInt("RightWeaponChoice")){

		case 0:
			GameObject R_SMG_I = Instantiate (R_SMG);
			R_SMG_I.gameObject.transform.position = RightWeapon_Spawn.gameObject.transform.position;
			R_SMG_I.transform.parent = R_Arm.gameObject.transform;
			break;

		case 1:
			GameObject R_AssaultRifle_I = Instantiate (R_Assault_Rifle);
			R_AssaultRifle_I.gameObject.transform.position = RightWeapon_Spawn.gameObject.transform.position;
			R_AssaultRifle_I.transform.parent = R_Arm.gameObject.transform;
			break;

		case 2:
			GameObject R_SniperRifle_I = Instantiate (R_Sniper_Rifle);
			R_SniperRifle_I.gameObject.transform.position = RightWeapon_Spawn.gameObject.transform.position;
			R_SniperRifle_I.transform.parent = R_Arm.gameObject.transform;
			break;

		}
	}

	private IEnumerator Dead_M(){
		Audio_S.clip = Explosion_S;
		Audio_S.Play ();
		for (int i = 0; i < 5; i++) {
			GameObject Explosion_Clone = Instantiate (Explosion_Particles);
			Explosion_Clone.transform.position = this.gameObject.transform.position;
			yield return new WaitForSeconds (0.7f);
		}
		LC.MissionFailed ();
	}
}