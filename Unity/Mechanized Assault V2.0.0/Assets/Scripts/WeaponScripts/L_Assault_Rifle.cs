using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XInputDotNetPure;

public class L_Assault_Rifle : MonoBehaviour {

	[Header("Game Manager")]
	public GameObject GM;
	public GameManager GameManager;

	[Space]
	[Header("Objects and Variables")]
	public GameObject AR_Bullet;
	public GameObject LeftBulletSpawn;
	[SerializeField]public int AmmoCount = 400;
	private int BaseAmmo = 400;

	private float fireDelta = 0.15f;
	private float nextFire = 0.15f;
	private float myTime = 0.0f;

	[Space]
	[Header("ParticleSystem")]
	public GameObject Particle_GO;
	public ParticleSystem MuzzleFlash;

	[Space]
	[Header("Sounds")]
	public AudioSource AudioS;

	[Space]
	[Header("AmmoCount")]
	public Text AmmoCountText;
	public Image L_AmmoImage;
	//Controller Items
	PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;

	void Start () {
		GM = GameObject.FindGameObjectWithTag ("GameManage");
		GameManager = GM.GetComponent<GameManager> ();
		LeftBulletSpawn = GameObject.FindGameObjectWithTag ("LBS");
		MuzzleFlash.Stop ();
		AmmoCountText = GameObject.FindGameObjectWithTag ("LWAC").GetComponent<Text> ();
		L_AmmoImage = GameObject.FindGameObjectWithTag ("LWAF").GetComponent<Image> ();
	}

	void Update () {
		AmmoCountText.text = AmmoCount + "";
		myTime = myTime + Time.deltaTime;
		if (!GameManager.prevState.IsConnected) {
			if (Input.GetButton ("Fire1") && AmmoCount >= 0 && myTime > nextFire) {
				nextFire = myTime + fireDelta;
				Fire ();
				nextFire = nextFire - myTime;
				myTime = 0.0f;
			} else if (!Input.GetButton ("Fire1")) {
				MuzzleFlash.Stop ();
				//Particle_GO.SetActive (false);
			}
		}

		if (GameManager.prevState.IsConnected) {
			if (GameManager.prevState.Buttons.X == ButtonState.Pressed && GameManager.state.Buttons.X != ButtonState.Released && AmmoCount >= 0 && myTime > nextFire) {
				nextFire = myTime + fireDelta;
				Fire ();
				nextFire = nextFire - myTime;
				myTime = 0.0f;
			} else if (GameManager.prevState.Buttons.X != ButtonState.Pressed && GameManager.state.Buttons.X == ButtonState.Released) {
				MuzzleFlash.Stop ();
			}
		}
	}

	private void Fire(){
		//Particle_GO.SetActive (true);
		MuzzleFlash.Play ();
		AudioS.Play ();
		GameObject L_Bullet_I = (GameObject)Instantiate (AR_Bullet);
		L_Bullet_I.gameObject.transform.position = LeftBulletSpawn.transform.position;
		L_Bullet_I.GetComponent<Rigidbody> ().AddForce (transform.right * 500f, ForceMode.VelocityChange);

		AmmoCount--;
		Destroy (L_Bullet_I, 2.0f);
	}

	public void AmmoBar(){
		L_AmmoImage.fillAmount = AmmoBarMap (AmmoCount, 0, BaseAmmo, 0, 1);
	}

	private float AmmoBarMap(float Value, float inMin, float inMax, float outMin, float outMax){
		return (Value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}
}
