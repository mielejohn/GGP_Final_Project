using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XInputDotNetPure;

public class L_Sniper_Rifle : MonoBehaviour {

	[Header("Game Manager")]
	public GameObject GM;
	public GameManager GameManager;

	[Space]
	[Header("Objects and Variables")]
	public GameObject SR_Bullet;
	public GameObject LeftBulletSpawn;
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
	public Image L_AmmoImage;
	//Controller Items
	//bool playerIndexSet = false;
	PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;

	void Start () {
		GM = GameObject.FindGameObjectWithTag ("GameManage");
		GameManager = GM.GetComponent<GameManager> ();
		//LeftBulletSpawn = GameObject.FindGameObjectWithTag ("LBS");
		//MuzzleFlash.Stop();
		AmmoCountText = GameObject.FindGameObjectWithTag ("LWAC").GetComponent<Text> ();
		L_AmmoImage = GameObject.FindGameObjectWithTag ("LWAF").GetComponent<Image> ();
	}

	void Update () {
		AmmoBar ();
		AmmoCountText.text = AmmoCount + "";
		if (!GameManager.prevState.IsConnected) {
			if (Input.GetButtonDown ("Fire1") && AmmoCount >= 0 && canFire == true) {
				//Debug.Log ("Firing left");
				Fire ();
				canFire = false;
				StartCoroutine (ShotBuffer ());
			} else if(Input.GetButtonUp ("Fire2")){
				print ("MuzzleFlash Stop");
				MuzzleFlash.Stop();
			}
		}

		if (GameManager.prevState.IsConnected) {
			if (GameManager.prevState.Buttons.X == ButtonState.Released && GameManager.state.Buttons.X == ButtonState.Pressed && AmmoCount >= 0 && canFire == true) {
				Debug.Log ("Firing left");
				canFire = false;
				Fire ();
				StartCoroutine (ShotBuffer ());
			} else if(GameManager.prevState.Buttons.X == ButtonState.Pressed && GameManager.state.Buttons.X == ButtonState.Released) {
				print ("MuzzleFlash stop");
				MuzzleFlash.Stop ();
			}
		}
	}

	private void Fire(){
		//Particle_GO.SetActive (true);
		print ("MuzzleFlash Play");
		MuzzleFlash.Play ();
		GameObject L_Bullet_I = (GameObject) Instantiate (SR_Bullet);
		L_Bullet_I.gameObject.transform.position = LeftBulletSpawn.transform.position;
		L_Bullet_I.GetComponent<Rigidbody> ().AddForce (transform.right * 1000f, ForceMode.VelocityChange);
		AmmoCount--;
		//MuzzleFlash.Stop();
		//Particle_GO.SetActive (false);
		StartCoroutine (ShotBuffer ());
		Destroy (L_Bullet_I, 2.0f);
	}

	public IEnumerator ShotBuffer(){
		yield return new WaitForSeconds (1.2f);
		canFire = true;
	}

	public void AmmoBar(){
		L_AmmoImage.fillAmount = AmmoBarMap (AmmoCount, 0, BaseAmmo, 0, 1);
	}

	private float AmmoBarMap(float Value, float inMin, float inMax, float outMin, float outMax){
		return (Value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}
}
