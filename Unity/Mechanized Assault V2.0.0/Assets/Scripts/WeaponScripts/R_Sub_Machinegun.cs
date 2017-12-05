using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XInputDotNetPure;

public class R_Sub_Machinegun : MonoBehaviour {

	[Header("Game Manager")]
	public GameObject GM;
	public GameManager GameManager;

	[Space]
	[Header("Objects and Variables")]
	public GameObject SMG_Bullet;
	public GameObject RightBulletSpawn;
	public int AmmoCount = 400;
	private int BaseAmmo = 400;

	[Space]
	[Header("ParticleSystem")]
	public GameObject Particle_GO;
	public ParticleSystem MuzzleFlash;

	[Space]
	[Header("AmmoCount")]
	public Text AmmoCountText;
	public Image R_AmmoImage;
	private float fireDelta = 0.10f;
	private float nextFire = 0.10f;
	private float myTime = 0.0f;

	//Controller Items
	//bool playerIndexSet = false;
	PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;

	void Start () {
		GM = GameObject.FindGameObjectWithTag ("GameManage");
		GameManager = GM.GetComponent<GameManager> ();
		//RightBulletSpawn = GameObject.FindGameObjectWithTag ("RBS");
		MuzzleFlash.Stop ();
		AmmoCountText = GameObject.FindGameObjectWithTag ("RWAC").GetComponent<Text> ();
		R_AmmoImage = GameObject.FindGameObjectWithTag ("RWAF").GetComponent<Image> ();
	}

	void Update () {
		AmmoBar ();
		AmmoCountText.text = AmmoCount + "";
		myTime = myTime + Time.deltaTime;

		if (!GameManager.prevState.IsConnected) {
			if (Input.GetButton ("Fire2") && AmmoCount >= 0 && myTime > nextFire) {
				nextFire = myTime + fireDelta;
				Fire ();
				nextFire = nextFire - myTime;
				myTime = 0.0f;
			} else if (!Input.GetButton ("Fire2")) {
				MuzzleFlash.Stop ();
			}
		}


		if (GameManager.prevState.IsConnected) {
			if (GameManager.prevState.Buttons.A == ButtonState.Pressed && GameManager.state.Buttons.A != ButtonState.Released && AmmoCount >= 0 && myTime > nextFire) {
				nextFire = myTime + fireDelta;
				Fire ();
				nextFire = nextFire - myTime;
				myTime = 0.0f;
			} else if (GameManager.prevState.Buttons.A != ButtonState.Pressed && GameManager.state.Buttons.A == ButtonState.Released) {
				MuzzleFlash.Stop ();
			}
		}
	}

	private void Fire(){
		MuzzleFlash.Play ();
		GameObject R_Bullet_I = (GameObject) Instantiate (SMG_Bullet);
		R_Bullet_I.gameObject.transform.position = RightBulletSpawn.transform.position;
		R_Bullet_I.GetComponent<Rigidbody> ().AddForce (transform.right * 550f, ForceMode.VelocityChange);
		AmmoCount--;
		Destroy (R_Bullet_I, 2.0f);
	}

	public void AmmoBar(){
		R_AmmoImage.fillAmount = AmmoBarMap (AmmoCount, 0, BaseAmmo, 0, 1);
	}

	private float AmmoBarMap(float Value, float inMin, float inMax, float outMin, float outMax){
		return (Value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}
}
