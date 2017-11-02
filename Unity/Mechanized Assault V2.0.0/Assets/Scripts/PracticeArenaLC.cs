using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PracticeArenaLC : MonoBehaviour {

	//References
	public FrameController FC;

	//SpawnPoints
	public GameObject Frame_Spawn;
	//public GameObject LeftWeapon_Spawn;
	//public GameObject RightWeapon_Spawn;

	//Frames
	public GameObject Dash_Frame;
	public GameObject Assault_Frame;
	public GameObject Support_Frame;

	public GameObject chosenPlayerFrame;

	/*
	//LeftWeapons
	public GameObject L_Assault_Rifle;
	public GameObject L_SMG;
	public GameObject L_Sniper_Rifle;

	//RightWeapons
	public GameObject R_Assault_Rifle;
	public GameObject R_SMG;
	public GameObject R_Sniper_Rifle;
	*/
	//ShoulderWeapons



	//HelperText
	public Text BoostText; 
	
	void Start () {



		SpawnFrame ();
		FC = GameObject.FindGameObjectWithTag ("Player").GetComponent<FrameController>();
		//LeftWeapon_Spawn = GameObject.FindGameObjectWithTag ("LWS");
		//RightWeapon_Spawn = GameObject.FindGameObjectWithTag ("RWS");
		//SpawnLeftWeapon ();
		//SpawnRightWeapon ();

	}
	

	void Update () {
		if (FC.canBoost == false) {
			BoostText.text = "No";
		} else {
			BoostText.text = "Yes";
		}

		if (Input.GetKey (KeyCode.P) && Input.GetKey (KeyCode.LeftAlt)) {
			SceneManager.LoadScene ("PracticeArenaScene", LoadSceneMode.Single);
		}

		if (Input.GetKey (KeyCode.O) && Input.GetKey (KeyCode.LeftAlt)) {
			SceneManager.LoadScene ("TitleScreen", LoadSceneMode.Single);
		}
	}

	public void SpawnFrame(){
		switch(PlayerPrefs.GetInt("FrameChoice")){

		case 0:
			chosenPlayerFrame = Dash_Frame;
			GameObject chosenPlayerFrame_I = Instantiate (chosenPlayerFrame);
			chosenPlayerFrame_I.gameObject.transform.position = Frame_Spawn.transform.position;
			break;

		case 1:
			chosenPlayerFrame = Assault_Frame;
			GameObject chosenPlayerFrame_II = Instantiate (chosenPlayerFrame);
			chosenPlayerFrame_II.gameObject.transform.position = Frame_Spawn.transform.position;
			break;

		case 2:
			chosenPlayerFrame = Support_Frame;
			GameObject chosenPlayerFrame_III = Instantiate (chosenPlayerFrame);
			chosenPlayerFrame_III.gameObject.transform.position = Frame_Spawn.transform.position;
			break;

		}
	}

	/*public void SpawnLeftWeapon(){
		switch(PlayerPrefs.GetInt("LeftWeaponChoice")){

		case 0:
			GameObject L_SMG_I = Instantiate (L_SMG);
			L_SMG_I.gameObject.transform.position = LeftWeapon_Spawn.transform.position;
			L_SMG_I.transform.parent = chosenPlayerFrame.transform;
			break;

		case 1:
			GameObject L_AssaultRifle_I = Instantiate (L_Assault_Rifle);
			L_AssaultRifle_I.gameObject.transform.position = LeftWeapon_Spawn.transform.position;
			L_AssaultRifle_I.transform.parent = chosenPlayerFrame.transform;
			break;

		case 2:
			GameObject L_SniperRifle_I = Instantiate (L_Sniper_Rifle);
			L_SniperRifle_I.gameObject.transform.position = LeftWeapon_Spawn.transform.position;
			L_SniperRifle_I.transform.parent = chosenPlayerFrame.transform;
			break;

		}
	}

	public void SpawnRightWeapon(){
		switch(PlayerPrefs.GetInt("RightWeaponChoice")){

		case 0:
			GameObject R_SMG_I = Instantiate (R_SMG);
			R_SMG_I.gameObject.transform.position = RightWeapon_Spawn.transform.position;
			R_SMG_I.transform.parent = chosenPlayerFrame.transform;
			break;

		case 1:
			GameObject R_AssaultRifle_I = Instantiate (R_Assault_Rifle);
			R_AssaultRifle_I.gameObject.transform.position = RightWeapon_Spawn.transform.position;
			R_AssaultRifle_I.transform.parent = chosenPlayerFrame.transform;
			break;

		case 2:
			GameObject R_SniperRifle_I = Instantiate (R_Sniper_Rifle);
			R_SniperRifle_I.gameObject.transform.position = RightWeapon_Spawn.transform.position;
			R_SniperRifle_I.transform.parent = chosenPlayerFrame.transform;
			break;

		}
	}*/
}
