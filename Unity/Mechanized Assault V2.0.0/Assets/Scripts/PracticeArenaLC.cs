using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using XInputDotNetPure;


public class PracticeArenaLC : MonoBehaviour {

	[Header("References")]
	//References
	public FrameController FC;

	[Space]
	[Header("Spawn Point")]
	//SpawnPoints
	public GameObject Frame_Spawn;
	//public GameObject LeftWeapon_Spawn;
	//public GameObject RightWeapon_Spawn;

	[Space]
	[Header("Frames")]
	//Frames
	public GameObject Dash_Frame;
	public GameObject Assault_Frame;
	public GameObject Support_Frame;

	public GameObject chosenPlayerFrame;


	[SerializeField] private bool Pasued = false;

	[Space]
	[Header("Controller Input")]
	//Controller input
	bool playerIndexSet = false;
	PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;

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

	[Space]
	[Header("Helper text")]
	//HelperText
	public Text BoostText; 
	public bool ControlsUp = false;
	public GameObject ControlsObject;
	public GameObject ControllerHelper;
	public GameObject KeyboardHelper;

	void Start () {
		
		SpawnFrame ();
		FC = GameObject.FindGameObjectWithTag ("Player").GetComponent<FrameController>();
		//LeftWeapon_Spawn = GameObject.FindGameObjectWithTag ("LWS");
		//RightWeapon_Spawn = GameObject.FindGameObjectWithTag ("RWS");
		//SpawnLeftWeapon ();
		//SpawnRightWeapon ();

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

		if (prevState.Buttons.Start == ButtonState.Released && state.Buttons.Start == ButtonState.Pressed && Pasued == false) {
			Pasued = true;
			Time.timeScale = 0.0f;

		} else if (prevState.Buttons.Start == ButtonState.Released && state.Buttons.Start == ButtonState.Pressed && Pasued == true) {
			Pasued = false;
			Time.timeScale = 1.0f;
		} 

		if (Input.GetKeyDown (KeyCode.C) && ControlsUp == false) {
			Time.timeScale = 0.0f;
			ControlsObject.gameObject.SetActive (true);
			KeyboardHelper.SetActive (true);
			ControllerHelper.SetActive (false);
			ControlsUp = true;
		} else if (Input.GetKeyDown (KeyCode.C) && ControlsUp == true) {
			Time.timeScale = 1.0f;
			ControlsObject.SetActive (false);
			KeyboardHelper.SetActive (true);
			ControllerHelper.SetActive (false);
			ControlsUp = false;
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

	public void ControllerControls(){
		ControllerHelper.SetActive (true);
		KeyboardHelper.SetActive (false);
	}

	public void KeyBoardControls(){
		KeyboardHelper.SetActive (true);
		ControllerHelper.SetActive (false);
	}
}
