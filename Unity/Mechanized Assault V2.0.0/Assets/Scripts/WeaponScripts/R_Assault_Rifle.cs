using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class R_Assault_Rifle : MonoBehaviour {

	public GameObject AR_Bullet;
	public GameObject RightBulletSpawn;
	[SerializeField]public int AmmoCount = 400;

	public float fireDelta = 0.5f;
	private float nextFire = 0.5f;
	private float myTime = 0.0f;

	//Controller Items
	bool playerIndexSet = false;
	PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;

	void Start () {
		RightBulletSpawn = GameObject.FindGameObjectWithTag ("RBS");
	}
		
	void Update () {
		
		myTime = myTime + Time.deltaTime;

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


		if (Input.GetButton("Fire2") && AmmoCount >= 0 && myTime > nextFire) {
			nextFire = myTime + fireDelta;
			GameObject R_Bullet_I = (GameObject) Instantiate (AR_Bullet);
			R_Bullet_I.gameObject.transform.position = RightBulletSpawn.transform.position;
			R_Bullet_I.GetComponent<Rigidbody> ().AddForce (transform.right * 400f, ForceMode.VelocityChange);
			AmmoCount--;
			nextFire = nextFire - myTime;
			myTime = 0.0f;
			Destroy (R_Bullet_I, 2.0f);
		}

		if(prevState.Buttons.A == ButtonState.Pressed && state.Buttons.A != ButtonState.Released && AmmoCount >=0  && myTime > nextFire)
		{
			nextFire = myTime + fireDelta;
			GameObject R_Bullet_I = (GameObject)Instantiate (AR_Bullet);
			R_Bullet_I.gameObject.transform.position = RightBulletSpawn.transform.position;
			R_Bullet_I.GetComponent<Rigidbody> ().AddForce (transform.right * 400f, ForceMode.VelocityChange);
			AmmoCount--;
			nextFire = nextFire - myTime;
			myTime = 0.0f;
			Destroy (R_Bullet_I, 2.0f);
		}
	}
}
