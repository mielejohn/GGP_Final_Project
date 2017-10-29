using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeArenaLC : MonoBehaviour {

	//SpawnPoints
	public GameObject Frame_Spawn;
	public GameObject LeftWeapon_Spawn;
	public GameObject RightWeapon_Spawn;

	//Frames
	public GameObject Dash_Frame;
	public GameObject Assault_Frame;
	public GameObject Support_Frame;

	//LeftWeapons
	public GameObject L_Assault_Rifle;
	public GameObject L_SMG;
	public GameObject L_Sniper_Rifle;

	//RightWeapons
	public GameObject R_Assault_Rifle;
	public GameObject R_SMG;
	public GameObject R_Sniper_Rifle;

	//ShoulderWeapons
	void Start () {
		SpawnFrame ();
		SpawnLeftWeapon ();
		SpawnRightWeapon ();
	}
	

	void Update () {
		
	}

	public void SpawnFrame(){
		switch(PlayerPrefs.GetInt("FrameChoice")){

		case 0:

			break;

		case 1:

			break;

		case 2:

			break;

		}
	}

	public void SpawnLeftWeapon(){

	}

	public void SpawnRightWeapon(){

	}
}
