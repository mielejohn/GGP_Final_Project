using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using XInputDotNetPure;

public class MainMenuLC : MonoBehaviour {
	[Header("Game Manager")]
	public GameObject GM;
	public GameManager GameManager;

	[Space]
	[Header("Main Menu Items")]
	//Main Menu Items
	public GameObject MainMenuStuff;

	[Space]
	[Header("Campaign Items")]
	//Campaign
	public RawImage CampaignSelectBackground;
	public RawImage MissionSelect_SelectBackground;
	public RawImage CoopSelectBackground;
	public Text CampaignMainButton;
	public Text CampaignMissionSelectButton;
	public Text CampaignCoOpButton;

	[Space]
	[Header("Multiplayer Items")]
	//Multiplayer
	public RawImage MultiplayerSelectBackground;
	public RawImage HostMatchSelectBackground;
	public RawImage FindMatchSelectBackground;
	public Text MultiPlayerMainButton;
	public Text MultiPlayerHostGameButton;
	public Text MultiPlayerFindGameButton;

	[Space]
	[Header("Practice Items")]
	//Practice
	public RawImage PracticeSelectBackground;
	public RawImage TutorialSelectBackground;
	public RawImage TestArenaSelectBackground;
	public Text PracticeMainButton;
	public Text PracticeTutorialButton;
	public Text PracticeTestArena;

	[Space]
	[Header("Colors")]
	//Color
	public Color SelectColor;
	public Color deSelectColor;

	[Space]
	[Header("Minor Buttons")]
	//Minor buttons
	public Text SettingsButton;
	public Text QuitGameButton;

	[Space]
	[Header("Settings Items")]
	//Settings buttons
	public GameObject SettingsStuff;

	[Space]
	[Header("Volume controls")]
	//VolumeControls
	public Text EffectVolume;
	public Slider EffectVolumeSlider;
	public Text MasterVolume;
	public Slider MasterVolumeSlider;
	public Text MusicVolume;
	public Slider MusicVolumeSlider;

	[Space]
	[Header("Subtitle Items")]
	//Subtitles
	public RawImage SubtitlesSelectOnImage;
	public RawImage SubtitlesSelectOffImage;
	public Text SubtitlesOnText;
	public Text SubtitlesOffText;
	public Color SubtitlesSelectColor;
	public bool SubtitlesChoice = false;

	[Space]
	[Header("Msic.")]
	//Misc Items
	public GameObject ScreenFader;
	public bool Selected;

	[Space]
	[Header("Controller Input")]
	//Controller input
	//bool playerIndexSet = false;
	PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;

	void awake(){

	}

	void Start () {
		GM = GameObject.FindGameObjectWithTag ("GameManage");
		GameManager = GM.GetComponent<GameManager> ();
		StartCoroutine (GameStartAnim ());
		PlayerPrefs.SetFloat ("MasterVolume", MasterVolumeSlider.value);
		PlayerPrefs.SetFloat ("MusicVolume", MasterVolumeSlider.value);
		PlayerPrefs.SetFloat ("EffectVolume", MasterVolumeSlider.value);
		PlayerPrefs.SetString ("SubtitlesChoice", "Off");
	}

	void Update () {
		EffectVolume.text = "" + EffectVolumeSlider.value;
		MasterVolume.text = "" + MasterVolumeSlider.value;
		MusicVolume.text = "" + MusicVolumeSlider.value;

		/*if (GameManager.playerIndexSet == false || GameManager.prevState.IsConnected == false) {
			print ("Game Manager isn't set");
			if (!playerIndexSet || !prevState.IsConnected) {
				for (int i = 0; i < 4; ++i) {
					PlayerIndex testPlayerIndex = (PlayerIndex)i;
					GamePadState testState = GamePad.GetState (testPlayerIndex);
					if (testState.IsConnected) {
						Debug.Log (string.Format ("GamePad found {0}", testPlayerIndex));
						playerIndex = testPlayerIndex;
						playerIndexSet = true;
					}
				}
			}
			prevState = state;
			state = GamePad.GetState (playerIndex);
		} else {*/
		//prevState = GameManager.state;
		//state = GameManager.state;
		//print ("prevState in main menu is: " + prevState);
		//print ("State in main menu is: " + state);
		//}


		if (GameManager.prevState.Buttons.A == ButtonState.Released && GameManager.state.Buttons.A == ButtonState.Pressed){
			Debug.Log ("A button pressed");
			MissionSelectbutton ();
		}
	}


	//CAMPAIGN
	public void CampaignHoverSelect(){
		CampaignSelectBackground.gameObject.SetActive(true);
		CampaignMainButton.color = Color.black;
		CampaignMissionSelectButton.gameObject.SetActive (true);
		CampaignCoOpButton.gameObject.SetActive (true);
	}

	public void CampaignSelect(){
		CampaignSelectBackground.gameObject.SetActive(true);
		CampaignMainButton.color = Color.black;
		Selected = true;
	}
		
	public void CampaignHoverUnSelect(){

		if (Selected == true) {
			CampaignHoverSelect ();
		} else {
			CampaignSelectBackground.gameObject.SetActive(false);
			CampaignMainButton.color = Color.white;
			CampaignMissionSelectButton.gameObject.SetActive (false);
			CampaignCoOpButton.gameObject.SetActive (false);
		}
		Selected = false;
	}
		
	public void MissionSelectHover(){
		MissionSelect_SelectBackground.gameObject.SetActive (true);
		CampaignMissionSelectButton.color = SelectColor;
	}

	public void MissionSelectUnHover(){
		MissionSelect_SelectBackground.gameObject.SetActive (false);
		CampaignMissionSelectButton.color = deSelectColor;
	}

	public void MissionSelectbutton(){
		SceneManager.LoadScene ("MissionSelect",LoadSceneMode.Single);
	}

	public void CoopHover(){
		CoopSelectBackground.gameObject.SetActive (true);
		CampaignCoOpButton.color = SelectColor;
	}

	public void CoopUnHover(){
		CoopSelectBackground.gameObject.SetActive (false);
		CampaignCoOpButton.color = deSelectColor;
	}

	public void CoopSelectbutton(){
		
	}


	//MULTIPLAYER 
	public void MultiplayerMenuHoverSelect(){
		MultiplayerSelectBackground.gameObject.SetActive(true);
		MultiPlayerMainButton.color = Color.black;
		MultiPlayerFindGameButton.gameObject.SetActive (true);
		MultiPlayerHostGameButton.gameObject.SetActive (true);
	}

	public void MultiplayerMenuSelect(){
		MultiplayerSelectBackground.gameObject.SetActive(true);
		MultiPlayerMainButton.color = Color.black;
		Selected = true;
	}

	public void MultiplayerMenuHoverUnSelect(){
		if (Selected == true) {
			MultiPlayerFindGameButton.gameObject.SetActive (true);
			MultiPlayerHostGameButton.gameObject.SetActive (true);
		}else{
			MultiplayerSelectBackground.gameObject.SetActive(false);
			MultiPlayerMainButton.color = Color.white;
			MultiPlayerFindGameButton.gameObject.SetActive (false);
			MultiPlayerHostGameButton.gameObject.SetActive (false);
		}
		Selected = false;
	}

	//MP Find Match
	public void FindMatchHover(){
		FindMatchSelectBackground.gameObject.SetActive (true);
		MultiPlayerFindGameButton.color = Color.black;
	}

	public void FindMatchUnHover(){
		FindMatchSelectBackground.gameObject.SetActive (false);
		MultiPlayerFindGameButton.color = Color.white;
	}

	public void FindMatchSelect(){

	}
		
	//MP Host Match
	public void HostMatchHover(){
	 	HostMatchSelectBackground.gameObject.SetActive (true);
		MultiPlayerHostGameButton.color = Color.black;
	}

	public void HostMatchUnHover(){
		HostMatchSelectBackground.gameObject.SetActive (false);
		MultiPlayerHostGameButton.color = Color.white;
	}

	public void HostMatchSelect(){

	}

	//PRACTICE
	public void PracticeMenuHoverSelect(){
		PracticeSelectBackground.gameObject.SetActive(true);
		PracticeMainButton.color = Color.black;
		PracticeTestArena.gameObject.SetActive (true);
		PracticeTutorialButton.gameObject.SetActive (true);

	}

	public void PracticeMenuSelect(){
		PracticeSelectBackground.gameObject.SetActive(true);
		PracticeMainButton.color = Color.black;
		Selected = true;
	}

	public void PracticeArenaHover(){
		TestArenaSelectBackground.gameObject.SetActive (true);
		PracticeTestArena.color = Color.black;
	}

	public void PracticeArenaUnHover(){
		TestArenaSelectBackground.gameObject.SetActive (false);
		PracticeTestArena.color = Color.white;
	}

	public void PracticeAreanSelect(){
		PlayerPrefs.SetString ("Mission Select", "Practice");
	}

	public void TutorialHover(){
		TutorialSelectBackground.gameObject.SetActive (true);
		PracticeTutorialButton.color = Color.black;
	}

	public void TutorialUnHover(){
		TutorialSelectBackground.gameObject.SetActive (false);
		PracticeTutorialButton.color = Color.white;
	}

	public void TutorialSelect(){
		PlayerPrefs.SetString ("Mission Select", "Tutorial");
	}

	public void PracticeMenuHoverUnSelect(){

		if (Selected == true) {
			PracticeTestArena.gameObject.SetActive (true);
			PracticeTutorialButton.gameObject.SetActive (true);
		} else {
			PracticeSelectBackground.gameObject.SetActive(false);
			PracticeMainButton.color = Color.white;
			PracticeTestArena.gameObject.SetActive (false);
			PracticeTutorialButton.gameObject.SetActive (false);
		}
		Selected = false;
	}

	//Settings
	public void SettingMenuSelect(){
		MainMenuStuff.gameObject.SetActive (false);
		SettingsStuff.gameObject.SetActive (true);
		SubtitlesOff ();
	}

	public void SettingsBackButtonSelect(){
		SettingsStuff.gameObject.SetActive (false);
		MainMenuStuff.gameObject.SetActive (true);
	}

	public void SubtitlesOn(){
		PlayerPrefs.SetString ("SubtitlesChoice", "On");
		SubtitlesOnText.color = SubtitlesSelectColor;
		SubtitlesSelectOnImage.gameObject.SetActive (true);
		SubtitlesSelectOffImage.gameObject.SetActive (false);
		SubtitlesOffText.color = deSelectColor;
	}

	public void SubtitlesOff(){
		PlayerPrefs.SetString ("SubtitlesChoice", "Off");
		SubtitlesOffText.color = SubtitlesSelectColor;
		SubtitlesSelectOnImage.gameObject.SetActive (false);
		SubtitlesSelectOffImage.gameObject.SetActive (true);
		SubtitlesOnText.color = deSelectColor;
	}

	public void QuitGame(){
		Application.Quit ();
	}

	public IEnumerator GameStartAnim(){
		ScreenFader.gameObject.GetComponent<Animator> ().Play ("Screen_Fade_In", -1, -0.5f);
		yield return new WaitForSeconds (2.0f);
		ScreenFader.gameObject.SetActive(false);
	}

	public void SetVolumePrefs(){
		PlayerPrefs.SetFloat ("MasterVolume", MasterVolumeSlider.value);
		PlayerPrefs.SetFloat ("MusicVolume", MasterVolumeSlider.value);
		PlayerPrefs.SetFloat ("EffectVolume", MasterVolumeSlider.value);
	}
}