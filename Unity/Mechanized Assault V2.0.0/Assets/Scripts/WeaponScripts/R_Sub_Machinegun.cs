using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

	public float fireDelta = 0.10f;
	private float nextFire = 0.10f;
	private float myTime = 0.0f;

	//Controller Items
	bool playerIndexSet = false;
	PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;

	void Start () {
		GM = GameObject.FindGameObjectWithTag ("GameManage");
		GameManager = GM.GetComponent<GameManager> ();
		//RightBulletSpawn = GameObject.FindGameObjectWithTag ("RBS");
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

		if (Input.GetButton("Fire2") && AmmoCount >= 0  && myTime > nextFire) {
			nextFire = myTime + fireDelta;
			GameObject R_Bullet_I = (GameObject) Instantiate (SMG_Bullet);
			R_Bullet_I.gameObject.transform.position = RightBulletSpawn.transform.position;
			R_Bullet_I.GetComponent<Rigidbody> ().AddForce (transform.right * 550f, ForceMode.VelocityChange);
			AmmoCount--;
			nextFire = nextFire - myTime;
			myTime = 0.0f;
			Destroy (R_Bullet_I, 2.0f);
		}

		if (GameManager.prevState.Buttons.A == ButtonState.Pressed && GameManager.state.Buttons.A != ButtonState.Released && AmmoCount >= 0  && myTime > nextFire) {
			nextFire = myTime + fireDelta;
			GameObject R_Bullet_I = (GameObject) Instantiate (SMG_Bullet);
			R_Bullet_I.gameObject.transform.position = RightBulletSpawn.transform.position;
			R_Bullet_I.GetComponent<Rigidbody> ().AddForce (transform.right * 550f, ForceMode.VelocityChange);
			AmmoCount--;
			nextFire = nextFire - myTime;
			myTime = 0.0f;
			Destroy (R_Bullet_I, 2.0f);
		}

	}
}
