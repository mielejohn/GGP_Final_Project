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
	public Text SettingsText;
	public GameObject SettingsHoverObject;
	public GameObject SettingsUnhoverObject;

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
	[Header("Credits")]
	public GameObject Credits;
	public GameObject CreditsHoverObject;
	public Text CreditsText;

	[Space]
	[Header("Quite")]
	public GameObject QuitButton;
	public GameObject QuitHoverObject;
	public Text QuitText;

	[Space]
	[Header("Msic.")]
	//Misc Items
	public GameObject ScreenFader;
	public bool Selected;
	[SerializeField]
	private string MenuType;
	public int MenuSelected;
	[SerializeField]
	//private string MenuControllerSelected;
	public GameObject NotMenuItems;

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

		if(GameManager.prevState.IsConnected){
			Cursor.visible = false;
			CampaignHoverSelect ();
			//MenuControllerSelected = "Campaign";
		}
	}

	void Update () {
		EffectVolume.text = "" + EffectVolumeSlider.value;
		MasterVolume.text = "" + MasterVolumeSlider.value;
		MusicVolume.text = "" + MusicVolumeSlider.value;

		if (GameManager.prevState.IsConnected) {
			if (GameManager.prevState.Buttons.A == ButtonState.Released && GameManager.state.Buttons.A == ButtonState.Pressed) {
				switch (MenuSelected) {
				case 0:
					CampaignHoverSelect ();
					break;
				case 1:
					MissionSelectbutton ();
					break;

				case 2:
					CreditsSelect ();
					break;

				case 3:
					//SettingMenuSelect ();
					break;

				case 4:
					QuitGame ();
					break;
				}
			}

			if (GameManager.prevState.Buttons.B == ButtonState.Released && GameManager.state.Buttons.B == ButtonState.Pressed && !MenuType.Equals ("Main")) {
				BacktoMainMenu (NotMenuItems);
			}

			if (GameManager.prevState.DPad.Up == ButtonState.Released && GameManager.state.DPad.Up == ButtonState.Pressed && MenuSelected > 0) {
				MenuSelected--;
			}

			if (GameManager.prevState.DPad.Down == ButtonState.Released && GameManager.state.DPad.Down == ButtonState.Pressed && MenuSelected < 4) {
				MenuSelected++;
			}

			switch (MenuSelected) {
			case 0:
				CampaignHoverSelect ();
				MissionSelectUnHover ();
				break;

			case 1:
				CampaignHoverSelect ();
				MissionSelectHover ();
				CreditsUnHover ();
				break;

			case 2:
				CampaignHoverUnSelect ();
				MissionSelectUnHover ();
				CreditsHover ();
				SettingsUnHover ();
				break;

			case 3:
				CreditsUnHover ();
				SettingsHover ();
				QuiteUnHover ();
				break;

			case 4:
				SettingsUnHover ();
				QuitHover ();
				break;
			}
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
		NotMenuItems = SettingsStuff;
		SubtitlesOff ();
		MenuType = "Settings";
	}

	public void SettingsBackButtonSelect(){
		BacktoMainMenu (NotMenuItems);
		MenuType = "Main";
	}

	public void SettingsHover(){
		SettingsText.color = SelectColor;
		SettingsHoverObject.SetActive (true);
	}

	public void SettingsUnHover(){
		SettingsText.color = deSelectColor;
		SettingsHoverObject.SetActive (false);
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

	//credits

	public void CreditsSelect(){
		MainMenuStuff.SetActive (false);
		Credits.SetActive (true);
		NotMenuItems = Credits;
		MenuType = "Credits";
	}

	public void CreditsBackButton(){
		BacktoMainMenu (NotMenuItems);
		MenuType = "Main";
	}

	public void CreditsHover(){
		CreditsHoverObject.SetActive (true);
		CreditsText.color = SelectColor;
	}

	public void CreditsUnHover(){
		CreditsHoverObject.SetActive (false);
		CreditsText.color = deSelectColor;
	}

	public void BacktoMainMenu(GameObject MenuItems){
		MenuItems.SetActive (false);
		MainMenuStuff.SetActive (true);
	}

	public void QuitGame(){
		Application.Quit ();
	}

	public void QuitHover(){
		QuitText.color = SelectColor;
		QuitHoverObject.SetActive (true);
	}

	public void QuiteUnHover(){
		QuitText.color = deSelectColor;
		QuitHoverObject.SetActive (false);
	}
}