using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using XInputDotNetPure;

public class MissionSelectLC : MonoBehaviour {

	//Animations
	public GameObject ScreenFader;

	//Buttons
	public Text Mission1;
	public GameObject Mission1SelectBG;
	public Text Mission2;
	public GameObject Mission2SelectBG;
	public Text Mission3;
	public GameObject Mission3SelectBG;
	public Text Mission4;
	public GameObject Mission4SelectBG;
	public Text menuButton;

	//MissionInformation
	public GameObject MissionImage;
	public Text MissionInfo;

	//Color
	public Color SelectColor;

	//Controller input
	bool playerIndexSet = false;
	PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;
	private int ControllerSpot = 0;


	//Screen Change
	public Slider loadingBar;
	public GameObject loadingImage;
	private AsyncOperation async;

	void Start () {
		StartCoroutine (GameStartAnim ());
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

		if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed){
			switch(ControllerSpot){

			case 0:
				MissionOneSelect ();
				break;

			case 1:
				MissionTwoSelect ();
				break;

			case 2:
				MissionThreeSelect ();
				break;

			case 3:
				MissionFourSelect ();
				break;
			}
		}

		if (prevState.DPad.Down == ButtonState.Pressed && state.DPad.Down == ButtonState.Released && ControllerSpot < 4) {
			ControllerSpot++;
			ControllerSelect ();
		}

		if (prevState.DPad.Up == ButtonState.Pressed && state.DPad.Up == ButtonState.Released && ControllerSpot > 0) {
			ControllerSpot--;
			ControllerSelect ();
		}
	}

	public void MissionOneSelect(){
		Mission1.color=SelectColor;
		PlayerPrefs.SetInt ("MissionSelect", 3);
		LoadAsync (2);
	}

	public void MissionOneHover(){
		MissionImage.gameObject.SetActive (true);
		Mission1SelectBG.gameObject.SetActive (true);
		Mission1.color = Color.black;
		MissionInfo.text = "This is mission one and will be an introductory mission to the controls";
	}

	public void MissionOneUnHover(){
		MissionImage.gameObject.SetActive (false);
		Mission1SelectBG.gameObject.SetActive (false);
		Mission1.color = Color.white;
		MissionInfo.text = "";
	}

	public void MissionTwoSelect() {
		Mission2.color=SelectColor;
		PlayerPrefs.SetInt ("MissionSelect", 3);
		LoadAsync (2);
	}

	public void MissionTwoHover(){
		MissionImage.gameObject.SetActive (true);
		Mission2SelectBG.gameObject.SetActive (true);
		Mission2.color = Color.black;
		MissionInfo.text = "This is mission two and will introduce the player to real combat";
	}

	public void MissionTwoUnHover(){
		MissionImage.gameObject.SetActive (false);
		Mission2SelectBG.gameObject.SetActive (false);
		Mission2.color = Color.white;
		MissionInfo.text = "";
	}

	public void MissionThreeSelect() {
		Mission3.color=SelectColor;
		PlayerPrefs.SetInt ("MissionSelect", 3);
		LoadAsync (2);
	}

	public void MissionThreeHover(){
		MissionImage.gameObject.SetActive (true);
		Mission3SelectBG.gameObject.SetActive (true);
		Mission3.color = Color.black;
		MissionInfo.text = "This is mission three and is mission three";
	}

	public void MissionThreeUnHover(){
		MissionImage.gameObject.SetActive (false);
		Mission3SelectBG.gameObject.SetActive (false);
		Mission3.color = Color.white;
		MissionInfo.text = "";
	}
		
	public void MissionFourSelect() {
		Mission4.color=SelectColor;
		PlayerPrefs.SetInt ("MissionSelect", 3);
		LoadAsync (2);
	}

	public void MissionFourHover(){
		MissionImage.gameObject.SetActive (true);
		Mission4SelectBG.gameObject.SetActive (true);
		Mission4.color = Color.black;
		MissionInfo.text = "This is mission four and its mission four";
	}

	public void MissionFourUnHover(){
		MissionImage.gameObject.SetActive (false);
		Mission4SelectBG.gameObject.SetActive (false);
		Mission4.color = Color.white;
		MissionInfo.text = "";
	}

	public void MainMenuButton() {
		SceneManager.LoadScene ("TitleScreen",LoadSceneMode.Single);
	}

	public void ControllerSelect (){
		switch(ControllerSpot){

		case 0:
			MissionOneHover ();
			MissionTwoUnHover ();
			break;

		case 1:
			MissionTwoHover ();
			MissionOneUnHover ();
			MissionThreeUnHover ();
			break;

		case 2:
			MissionThreeHover ();
			MissionTwoUnHover ();
			MissionFourUnHover ();
			break;

		case 3:
			MissionFourHover ();
			MissionThreeUnHover ();
			break;
		}
	}

	public void LoadAsync(int Levelnumber){
		loadingImage.SetActive (true);
		StartCoroutine (LoadLevelWithBar (Levelnumber));
	}

	IEnumerator LoadLevelWithBar(int LevelNumber){
		async = SceneManager.LoadSceneAsync (LevelNumber);
		while (!async.isDone) {
			loadingBar.value = async.progress;
			yield return null;
		}
	}

	public IEnumerator GameStartAnim(){
		ScreenFader.gameObject.GetComponent<Animator> ().Play ("Screen_Fade_In", -1, 0.0f);
		yield return new WaitForSeconds (1.5f);
		ScreenFader.gameObject.SetActive(false);
	}
}