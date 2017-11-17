using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

	public float fireDelta = 0.5f;
	private float nextFire = 0.5f;
	private float myTime = 0.0f;

	//Controller Items
	//bool playerIndexSet = false;
	PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;

	void Start () {
		GM = GameObject.FindGameObjectWithTag ("GameManage");
		GameManager = GM.GetComponent<GameManager> ();
		LeftBulletSpawn = GameObject.FindGameObjectWithTag ("LBS");
	}

	void Update () {

		myTime = myTime + Time.deltaTime;
		/*
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
		state = GamePad.GetState(playerIndex);
		*/

		if (Input.GetButton("Fire1") && AmmoCount >= 0  && myTime > nextFire) {
			nextFire = myTime + fireDelta;
			GameObject L_Bullet_I = (GameObject)Instantiate (AR_Bullet);
			L_Bullet_I.gameObject.transform.position = LeftBulletSpawn.transform.position;
			L_Bullet_I.GetComponent<Rigidbody> ().AddForce (transform.right * 400f, ForceMode.VelocityChange);
			AmmoCount--;
			nextFire = nextFire - myTime;
			myTime = 0.0f;
			Destroy (L_Bullet_I, 2.0f);
		}

		if(GameManager.prevState.Buttons.X == ButtonState.Pressed && GameManager.state.Buttons.X != ButtonState.Released && AmmoCount >=0  && myTime > nextFire)
		{
			nextFire = myTime + fireDelta;
			GameObject L_Bullet_I = (GameObject)Instantiate (AR_Bullet);
			L_Bullet_I.gameObject.transform.position = LeftBulletSpawn.transform.position;
			L_Bullet_I.GetComponent<Rigidbody> ().AddForce (transform.right * 400f, ForceMode.VelocityChange);
			AmmoCount--;
			nextFire = nextFire - myTime;
			myTime = 0.0f;
			Destroy (L_Bullet_I, 2.0f);
		}
	}
}
