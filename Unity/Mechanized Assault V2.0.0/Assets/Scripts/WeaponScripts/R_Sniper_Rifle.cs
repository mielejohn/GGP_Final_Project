using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class R_Sniper_Rifle : MonoBehaviour {

	public GameObject SR_Bullet;
	public GameObject RightBulletSpawn;
	public bool canFire = true;
	[SerializeField]public int AmmoCount = 100;


	//Controller Items
	bool playerIndexSet = false;
	PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;

	void Start () {
		//RightBulletSpawn = GameObject.FindGameObjectWithTag ("RBS");
	}
		
	void Update () {

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


		if (Input.GetButtonDown("Fire2") && AmmoCount >= 0 && canFire == true) {
			Debug.Log ("Firing right");
			canFire = false;
			GameObject R_Bullet_I = (GameObject) Instantiate (SR_Bullet);
			R_Bullet_I.gameObject.transform.position = RightBulletSpawn.transform.position;
			R_Bullet_I.GetComponent<Rigidbody> ().AddForce (transform.right * 1000f, ForceMode.VelocityChange);
			AmmoCount--;
			StartCoroutine (ShotBuffer ());
			Destroy (R_Bullet_I, 2.0f);
		}

		if (prevState.Buttons.A == ButtonState.Pressed && state.Buttons.A == ButtonState.Released && AmmoCount >=0 && canFire == true)
		{
			Debug.Log ("Firing right");
			canFire = false;
			GameObject R_Bullet_I = (GameObject) Instantiate (SR_Bullet);
			R_Bullet_I.gameObject.transform.position = RightBulletSpawn.transform.position;
			R_Bullet_I.GetComponent<Rigidbody> ().AddForce (transform.right * 1000f, ForceMode.VelocityChange);
			AmmoCount--;
			StartCoroutine (ShotBuffer ());
			Destroy (R_Bullet_I, 2.0f);
		}
	}

	public IEnumerator ShotBuffer(){
		yield return new WaitForSeconds (1.2f);
		canFire = true;
	}
}
