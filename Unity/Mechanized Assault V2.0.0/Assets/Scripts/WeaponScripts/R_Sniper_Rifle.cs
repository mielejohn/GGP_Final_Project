using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XInputDotNetPure;

public class R_Sniper_Rifle : MonoBehaviour {

	[Header("Game Manager")]
	public GameObject GM;
	public GameManager GameManager;

	[Space]
	[Header("Objects and Variables")]
	public GameObject SR_Bullet;
	public GameObject RightBulletSpawn;
	public bool canFire = true;
	[SerializeField]public int AmmoCount = 100;
	private int BaseAmmo = 100;

	[Space]
	[Header("ParticleSystem")]
	public GameObject Particle_GO;
	public ParticleSystem MuzzleFlash;

	[Space]
	[Header("AmmoCount")]
	public Text AmmoCountText;
	public Image R_AmmoImage;
	//Controller Items
	//bool playerIndexSet = false;
	PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;

	void Start () {
		GM = GameObject.FindGameObjectWithTag ("GameManage");
		GameManager = GM.GetComponent<GameManager> ();
		//RightBulletSpawn = GameObject.FindGameObjectWithTag ("RBS");
		//MuzzleFlash.Stop();
		AmmoCountText = GameObject.FindGameObjectWithTag ("RWAC").GetComponent<Text> ();
		R_AmmoImage = GameObject.FindGameObjectWithTag ("RWAF").GetComponent<Image> ();
	}
		
	void Update () {
		AmmoBar ();
		AmmoCountText.text = AmmoCount + "";

		#region keyBoardInputs
		if (!GameManager.prevState.IsConnected) {
			if (Input.GetButtonDown ("Fire2") && AmmoCount >= 0 && canFire == true) {
				Debug.Log ("Firing right");
				Fire ();
				canFire = false;
			} else if(Input.GetButtonUp ("Fire2")){
				print ("MuzzleFlash Stop");
				MuzzleFlash.Stop();
			}
		}
		#endregion

		#region ControllerInputs
		if (GameManager.prevState.IsConnected) {
			if (GameManager.prevState.Buttons.A == ButtonState.Released && GameManager.state.Buttons.A == ButtonState.Pressed && AmmoCount >= 0 && canFire == true) {
				Debug.Log ("Firing right");
				canFire = false;
				Fire ();
			} else if(GameManager.prevState.Buttons.A == ButtonState.Pressed && GameManager.state.Buttons.A == ButtonState.Released) {	
				print ("MuzzleFlash Stop");
				MuzzleFlash.Stop ();
			}
		}
		#endregion
	}

	private void Fire(){
		print ("MuzzleFlash Play");
		MuzzleFlash.Play ();
		GameObject R_Bullet_I = (GameObject) Instantiate (SR_Bullet);
		R_Bullet_I.gameObject.transform.position = RightBulletSpawn.transform.position;
		R_Bullet_I.GetComponent<Rigidbody> ().AddForce (transform.right * 1000f, ForceMode.VelocityChange);
		//MuzzleFlash.Stop ();
		AmmoCount--;
		StartCoroutine (ShotBuffer ());
		Destroy (R_Bullet_I, 2.0f);
	}

	public IEnumerator ShotBuffer(){
		yield return new WaitForSeconds (1.2f);
		canFire = true;
	}

	public void AmmoBar(){
		R_AmmoImage.fillAmount = AmmoBarMap (AmmoCount, 0, BaseAmmo, 0, 1);
	}

	private float AmmoBarMap(float Value, float inMin, float inMax, float outMin, float outMax){
		return (Value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}
}
