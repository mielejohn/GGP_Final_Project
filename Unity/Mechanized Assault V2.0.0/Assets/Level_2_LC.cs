using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using XInputDotNetPure;

public class Level_2_LC : MonoBehaviour {

	[Header("References")]
	public FrameController FC;

	[Space]
	[Header("Cutscene")]
	public GameObject CinematicCamera;
	public GameObject Titan_Boss;
	public GameObject Miscu_Boss;
	public GameObject BlackScreen;
	public GameObject[] CinematicShips;

	[Space]
	[Header("Spawn Point")]
	public GameObject Frame_Spawn;

	[Space]
	[Header("Frames")]
	public GameObject Dash_Frame;
	public GameObject Assault_Frame;
	public GameObject Support_Frame;
	public GameObject chosenPlayerFrame;

	[Space]
	[Header("Paused")]
	[SerializeField] private bool Pasued = false;

	[Space]
	[Header("Controller Input")]
	bool playerIndexSet = false;
	PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;

	[Space]
	[Header("Controls images")]
	public bool ControlsUp = false;
	public GameObject ControlsObject;
	public GameObject ControllerHelper;
	public GameObject KeyboardHelper;

	[Space]
	[Header("Mission 2 Enemies Wave 1")]
	public GameObject[] EnemiesWave_1;
	public int Enemies_W_1 = 5;

	[Space]
	[Header("Mission 2 Enemies Wave 2")]
	public GameObject[] EnemiesWave_2;

	[Space]
	[Header("Mission 2 Enemies Wave 3")]
	public GameObject[] EnemiesWave_3;

	[Space]
	[Header("Animated opening and Ending")]
	public GameObject OpeningObject;
	public GameObject PassedObject;

	[Space]
	[Header("Screen Change Items")]
	//Screen Change
	public Slider loadingBar;
	public GameObject loadingImage;
	private AsyncOperation async;
	public bool IntroEnded=false;

	void Start () {
		SpawnFrame ();
		StartCoroutine (GameStart());
		FC = GameObject.FindGameObjectWithTag ("Player").GetComponent<FrameController> ();
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

		if (Input.GetKey (KeyCode.P) && Input.GetKey (KeyCode.LeftAlt)) {
			SceneManager.LoadScene ("PracticeArenaScene", LoadSceneMode.Single);
		}

		if (Input.GetKey (KeyCode.O) && Input.GetKey (KeyCode.LeftAlt)) {
			SceneManager.LoadScene ("TitleScreen", LoadSceneMode.Single);
		}

		if (prevState.Buttons.Start == ButtonState.Released && state.Buttons.Start == ButtonState.Pressed || Input.GetKeyDown(KeyCode.Escape) && Pasued == false ) {
			Pasued = true;
			Time.timeScale = 0.0f;

		} else if (prevState.Buttons.Start == ButtonState.Released && state.Buttons.Start == ButtonState.Pressed || Input.GetKeyDown(KeyCode.Escape) && Pasued == true) {
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

		if (Enemies_W_1 <= 0) {
			StartCoroutine(MissionPassed ());
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

	public IEnumerator GameStart(){
		print ("Game Start");
		OpeningObject.GetComponent<Animator> ().Play ("LevelOpening_ANIMATION", -1, 0.0f);
		yield return new WaitForSeconds (1.5f);
		OpeningObject.SetActive (false);
	}

	public void ControllerControls(){
		ControllerHelper.SetActive (true);
		KeyboardHelper.SetActive (false);
	}

	public void KeyBoardControls(){
		KeyboardHelper.SetActive (true);
		ControllerHelper.SetActive (false);
	}

	public IEnumerator OutofBounds(){
		yield return new WaitForSeconds (1.0f);
	}

	public void SpawnEnemies(){
		for (int i = 0; i < EnemiesWave_1.Length; i++) {
			EnemiesWave_1 [i].SetActive (true);
		}
	}

	public IEnumerator MissionFailed(){
		print ("MISSION FAILURE");
		//FC.enabled = false;
		yield return new WaitForSeconds (4.0f);
		StartCoroutine (LoadLevelWithBar (1));

	}

	public IEnumerator MissionPassed(){
		Debug.Log ("Mission Passed");
		PassedObject.SetActive (true);
		yield return new WaitForSeconds (1.1f);
		PassedObject.GetComponent<Animator> ().speed = 0;
		yield return new WaitForSeconds (4.1f);
		PassedObject.SetActive (false);
		PlayerPrefs.SetString ("Mission1Passed", "True");

		StartCoroutine (Cutscene ());
		//StartCoroutine (LoadLevelWithBar (1));
	}

	IEnumerator LoadLevelWithBar(int LevelNumber){
		async = SceneManager.LoadSceneAsync (LevelNumber);
		while (!async.isDone) {
			loadingBar.value = async.progress;
			yield return null;
		}
	}

	private IEnumerator Cutscene(){
		FC.gameObject.SetActive (false);
		for (int i = 0; i < CinematicShips.Length; i++) {
			CinematicShips [i].SetActive (true);
		}
		CinematicCamera.SetActive (true);
		Titan_Boss.SetActive (true);
		Miscu_Boss.SetActive (true);
		yield return new WaitForSeconds (20.0f);
		BlackScreen.SetActive (true);
		StartCoroutine (LoadLevelWithBar (1));
	}
}

