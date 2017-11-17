using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using XInputDotNetPure;

public class MechSelectionLC : MonoBehaviour {

	public GameObject MainCamera;
	public GameObject ScreenFader;
	public MechRotaterKeyboard MRK;
	public GameObject AllMechItems;
	public GameObject AllWeaponItems;

	[Space]
	[Header("Game Manager")]
	public GameObject GM;
	public GameManager GameManager;

	[Space]
	[Header("Frame Items")]
	//Frames
	public int Frame_Number = 0;
	public Text Frame_Name;
	public GameObject FrameSpawn;
	public GameObject Frame_AdvanceArrow;
	public GameObject Frame_BackstepArrow;
	public GameObject DASH_Frame;
	public GameObject Assault_Frame;
	public GameObject Support_Frame;
	public GameObject chosenFrame;
	public Material MISCU_Frame_Color;

	[Space]
	[Header("Armore Items")]
	//Armor Items
	public int Armor;
	public Text ArmorNumberText;

	[Space]
	[Header("Weight Items")]
	//Weight Items
	public int Weight;
	public Text WeightNumberText;

	[Space]
	[Header("Speed Items")]
	//Speed Items
	public int Speed;
	public Text SpeedNumberText;

	[Space]
	[Header("Dash Items")]
	//Dash Items
	public int DashChargeTime;
	public Text DashTime;

	[Space]
	[Header("Modifiers")]
	//Modifiers
	public Dropdown Modifier1;
	public Dropdown Modifier2;
	public Dropdown Modifier3;


	[Space]
	[Space]
	[Header("WEAPONS")]
	//WEAPONS
	//------------------------------------------------------------------
	[Header("Selector Images")]
	public GameObject rightSelectorImage;
	public GameObject leftSelectorImage;

	[Space]
	[Header("Left Weapons")]
	//Left Weapons
	public GameObject L_SMG;
	public GameObject L_Assault_Rifle;
	public GameObject L_SniperRifle;

	[Space]
	[Header("Right Weapons")]
	//Right Weapons
	public GameObject R_SMG;
	public GameObject R_Assault_Rifle;
	public GameObject R_SniperRifle;

	[Space]
	[Header("Left Weapon Stats")]
	//LeftWeaponStats
	public int LWeapon_Number = 0;
	public Text LWeapon_Name;
	public GameObject LWeaponSpawn;
	public GameObject LWeapon_AdvanceArrow;
	public GameObject LWeapon_BackstepArrow;
	[SerializeField] public int LWeapon_Damage;
	[SerializeField] public int LWeapon_Range;
	[SerializeField] public int LWeapon_Accuray;
	[SerializeField] public int LWeapon_AmmoCount;

	[Space]
	[Header("Left Weapon Text Items")]
	//LeftWeaponTextItems
	public Text L_DamageText;
	public Text L_RangeText;
	public Text L_AccuracyText;
	public Text L_AmmoCountText;

	[Space]
	[Header("Right Weapon Stats")]
	//RightWeaponStats
	public int RWeapon_Number = 0;
	public Text RWeapon_Name;
	public GameObject RWeaponSpawn;
	public GameObject RWeapon_AdvanceArrow;
	public GameObject RWeapon_BackstepArrow;
	[SerializeField] public int RWeapon_Damage;
	[SerializeField] public int RWeapon_Range;
	[SerializeField] public int RWeapon_Accuray;
	[SerializeField] public int RWeapon_AmmoCount;

	[Space]
	[Header("Right Weapon Text Items")]
	//RightWeaponTextItems
	public Text R_DamageText;
	public Text R_RangeText;
	public Text R_AccuracyText;
	public Text R_AmmoCountText;


	//MISC
	//------------------------------------------------------------------
	[Space]
	[Space]
	[Header("MISC.")]
	public string MenuType = "Mech";

	[Space]
	[Header("Contoller Input")]
	//Controller input
	//bool playerIndexSet = false;
	PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;
	public int ControllerSelection;

	[Space]
	[Header("Screen Change")]
	//Screen Change
	public Slider loadingBar;
	public GameObject loadingImage;
	private AsyncOperation async;

	void Awake(){
		if (PlayerPrefs.HasKey ("FrameChoice")) {
			DeletePlayerValues ();
		}
	}

	void Start () {
		GM = GameObject.FindGameObjectWithTag ("GameManage");
		GameManager = GM.GetComponent<GameManager> ();
		StartCoroutine (MechSelectStartAnim ());
		FrameChoice ();
		//LeftWeaponChoice ();
		//RightWeaponChoice ();
		Debug.Log ("Frame choice called at start");
	}

	void Update () {
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
		if (GameManager.prevState.Buttons.RightShoulder == ButtonState.Pressed && GameManager.state.Buttons.RightShoulder == ButtonState.Released) {
			Debug.Log ("Right Shoulder Pressed");
			if (ControllerSelection == 0) {
				NextFrame ();
			} else if(ControllerSelection == 1){
				RNextWeapon (); 				
			} else if(ControllerSelection == 2){
				LNextWeapon ();
			}

		}

		if (GameManager.prevState.Buttons.LeftShoulder == ButtonState.Pressed && GameManager.state.Buttons.LeftShoulder == ButtonState.Released) {
			Debug.Log ("Left Shoulder Pressed");
			if(ControllerSelection == 0){
				PreviousFrame ();
			}else if(ControllerSelection == 1){
				RPreviousWeapon ();
			} else if(ControllerSelection == 2){
				LPreviousWeapon (); 				
			}
		}

		if (GameManager.prevState.Buttons.A == ButtonState.Pressed && GameManager.state.Buttons.A == ButtonState.Released) {
			Debug.Log ("'A' Pressed");
			if (ControllerSelection == 0) {
				ContinueButton ();
				ControllerSelection++;
				rightSelectorImage.gameObject.SetActive (true);
			}else if(ControllerSelection == 1){
				rightSelectorImage.gameObject.SetActive (false);
				leftSelectorImage.gameObject.SetActive (true);
				ControllerSelection++;
			}else if(ControllerSelection == 2){
				ContinueButton ();
			}
		}

		if (GameManager.prevState.Buttons.B == ButtonState.Pressed && GameManager.state.Buttons.B == ButtonState.Released) {
			Debug.Log ("'B' Pressed");
			if (ControllerSelection == 0) {
				BackButton ();
			}else if(ControllerSelection == 1){
				rightSelectorImage.gameObject.SetActive (false);
				ControllerSelection--;
				BackButton ();
			}else if(ControllerSelection == 2){
				rightSelectorImage.gameObject.SetActive (true);
				leftSelectorImage.gameObject.SetActive (false);
				ControllerSelection--;
			}
		}


		ArmorNumberText.text = "" + Armor;
		WeightNumberText.text = "" + Weight;
		SpeedNumberText.text = "" + Speed;

	}

	//Starting Animation
	public IEnumerator MechSelectStartAnim(){
		ScreenFader.gameObject.GetComponent<Animator> ().Play ("Screen_Fade_In", -1, 0.0f);
		yield return new WaitForSeconds (1.5f);
		ScreenFader.gameObject.SetActive(false);		
		yield return new WaitForSeconds (0.9f);
		MainCamera.gameObject.GetComponent<Animator> ().Play ("PlayerOneSpace", -1, 0.0f);
		Debug.Log ("Mech select start finished");

	}

	//Next frame button
	public void NextFrame(){
		Debug.Log ("Next frame called");
		if (Frame_Number >= 1) {
			Frame_AdvanceArrow.gameObject.SetActive (false);
			Frame_Number++;
			FrameChoice ();
		} else {
			Frame_AdvanceArrow.gameObject.SetActive (true);
			Frame_BackstepArrow.gameObject.SetActive (true);
			Frame_Number++;
			FrameChoice ();
		}

	}

	//Previous frame button
	public void PreviousFrame(){
		Debug.Log ("Previous Frame Called");
		if (Frame_Number <= 1) {
			Frame_BackstepArrow.gameObject.SetActive (false);
			Frame_Number--;
			FrameChoice ();
		} else {
			Frame_BackstepArrow.gameObject.SetActive (true);
			Frame_AdvanceArrow.gameObject.SetActive (true);
			Frame_Number--;
			FrameChoice ();
		}
	}

	//Choosing the frame after an arrow is pressed
	public void FrameChoice(){
		Debug.Log ("Frame choice called");
		switch (Frame_Number) {
		case 0:
			MRK.RotationReset ();
			Assault_Frame.gameObject.SetActive (false);
			DASH_Frame.gameObject.SetActive (true);
			chosenFrame = DASH_Frame;
			Frame_Name.text = "Dash";
			MISCU_Frame_Color.color = Color.blue;

			//Variables
			Armor = 35000;

			Weight = 50;

			Speed = 75;

			break;

		case 1:
			MRK.RotationReset ();
			DASH_Frame.gameObject.SetActive (false);
			Support_Frame.SetActive (false);
			Assault_Frame.gameObject.SetActive (true);
			chosenFrame = Assault_Frame;
			Frame_Name.text = "Assault";
			MISCU_Frame_Color.color = Color.red;

			//Variables
			Armor = 42500;

			Weight = 60;

			Speed = 65;

			break;

		case 2:
			MRK.RotationReset ();
			Assault_Frame.gameObject.SetActive (false);
			Support_Frame.gameObject.SetActive (true);
			chosenFrame = Support_Frame;
			Frame_Name.text = "Support";
			MISCU_Frame_Color.color = Color.green;

			//Variables
			Armor = 50000;

			Weight = 80;

			Speed = 50;
			break;
		}
	}

	public void ModifierAdditions(){
		switch (Modifier1.value) {

		case 0:

			break;

		case 1: 
			
			break;

		case 2: 

			break;


		case 3: 

			break;
		}

		switch (Modifier2.value) {

		case 0:

			break;

		case 1: 

			break;

		case 2: 

			break;


		case 3: 

			break;
		}

		switch (Modifier3.value) {

		case 0:

			break;

		case 1: 

			break;

		case 2: 

			break;


		case 3: 

			break;
		}
	}

	//WEAPONS
	//----------------------------------------------------------------------------------

	//LEFT_WEAPON
	//----------------------------------------------------------------------------------

	public void LNextWeapon(){
		Debug.Log ("Next Left Weapon called");
		if (LWeapon_Number >= 1) {
			LWeapon_AdvanceArrow.gameObject.SetActive (false);
			LWeapon_Number++;
			LeftWeaponChoice();
		} else {
			LWeapon_AdvanceArrow.gameObject.SetActive (true);
			LWeapon_BackstepArrow.gameObject.SetActive (true);
			LWeapon_Number++;
			LeftWeaponChoice();
		}

	}

	public void LPreviousWeapon(){
		Debug.Log ("Previous Left Weapon Called");
		if (LWeapon_Number <= 1) {
			LWeapon_BackstepArrow.gameObject.SetActive (false);
			LWeapon_Number--;
			LeftWeaponChoice();
		} else {
			LWeapon_BackstepArrow.gameObject.SetActive (true);
			LWeapon_AdvanceArrow.gameObject.SetActive (true);
			LWeapon_Number--;
			LeftWeaponChoice();
		}
	}

	public void LeftWeaponChoice(){
		Debug.Log ("Left Weapon choice called");
		switch (LWeapon_Number) {
		case 0:
			MRK.RotationReset ();

			L_Assault_Rifle.transform.parent = null;

			L_SMG.SetActive (true);
			L_Assault_Rifle.SetActive (false);
			L_SMG.gameObject.transform.position = LWeaponSpawn.gameObject.transform.position;
			L_SMG.gameObject.transform.parent = chosenFrame.transform;

			LWeapon_Name.text = "SMG";

			//Variables
			LWeapon_Damage = 100;
			LWeapon_Accuray = 50;
			LWeapon_Range = 75;
			LWeapon_AmmoCount = 400;

			L_DamageText.text = LWeapon_Damage + "";
			L_AccuracyText.text = LWeapon_Accuray + "";
			L_RangeText.text = LWeapon_Range + "";
			L_AmmoCountText.text = LWeapon_AmmoCount + "";

			break;

		case 1:
			MRK.RotationReset ();

			L_SMG.transform.parent = null;
			L_SniperRifle.transform.parent = null;

			L_Assault_Rifle.SetActive (true);
			L_SMG.SetActive (false);
			L_SniperRifle.SetActive (false);

			L_Assault_Rifle.gameObject.transform.position = LWeaponSpawn.gameObject.transform.position;
			L_Assault_Rifle.gameObject.transform.parent = chosenFrame.transform;

			LWeapon_Name.text = "Assault Rifle";

			//Variables
			LWeapon_Damage = 150;
			LWeapon_Accuray = 60;
			LWeapon_Range = 65;
			LWeapon_AmmoCount = 300;

			L_DamageText.text = LWeapon_Damage + "";
			L_AccuracyText.text = LWeapon_Accuray + "";
			L_RangeText.text = LWeapon_Range + "";
			L_AmmoCountText.text = LWeapon_AmmoCount + "";

			break;

		case 2:
			MRK.RotationReset ();

			L_Assault_Rifle.transform.parent = null;

			L_Assault_Rifle.SetActive (false);
			L_SniperRifle.gameObject.SetActive (true);

			L_SniperRifle.gameObject.transform.position = LWeaponSpawn.gameObject.transform.position;
			L_SniperRifle.gameObject.transform.parent = chosenFrame.transform;

			LWeapon_Name.text = "Sniper";

			//Variables
			LWeapon_Damage = 300;
			LWeapon_Accuray = 80;
			LWeapon_Range = 50;
			LWeapon_AmmoCount = 100;

			L_DamageText.text = LWeapon_Damage + "";
			L_AccuracyText.text = LWeapon_Accuray + "";
			L_RangeText.text = LWeapon_Range + "";
			L_AmmoCountText.text = LWeapon_AmmoCount + "";

			break;
		}
	}

	//RIGHT WEAPON
	//----------------------------------------------------------------------------------
	public void RNextWeapon(){
		Debug.Log ("Next Right weapon called");
		if (RWeapon_Number >= 1) {
			RWeapon_AdvanceArrow.gameObject.SetActive (false);
			RWeapon_Number++;
			RightWeaponChoice();
		} else {
			RWeapon_AdvanceArrow.gameObject.SetActive (true);
			RWeapon_BackstepArrow.gameObject.SetActive (true);
			RWeapon_Number++;
			RightWeaponChoice();
		}

	}

	public void RPreviousWeapon(){
		Debug.Log ("Previous Right Weapon Called");
		if (RWeapon_Number <= 1) {
			RWeapon_BackstepArrow.gameObject.SetActive (false);
			RWeapon_Number--;
			RightWeaponChoice();
		} else {
			RWeapon_BackstepArrow.gameObject.SetActive (true);
			RWeapon_AdvanceArrow.gameObject.SetActive (true);
			RWeapon_Number--;
			RightWeaponChoice();
		}
	}

	public void RightWeaponChoice(){
		Debug.Log ("Right Weapon choice called");
		switch (RWeapon_Number) {
		case 0:
			MRK.RotationReset ();

			R_Assault_Rifle.transform.parent = null;

			R_SMG.SetActive (true);
			R_Assault_Rifle.SetActive (false);

			R_SMG.gameObject.transform.position = RWeaponSpawn.gameObject.transform.position;
			R_SMG.gameObject.transform.parent = chosenFrame.transform;

			RWeapon_Name.text = "SMG";

			//Variables
			RWeapon_Damage = 100;
			RWeapon_Accuray = 50;
			RWeapon_Range = 75;
			RWeapon_AmmoCount = 400;

			R_DamageText.text = RWeapon_Damage + "";
			R_AccuracyText.text = RWeapon_Accuray + "";
			R_RangeText.text = RWeapon_Range + "";
			R_AmmoCountText.text =RWeapon_AmmoCount + "";

			break;

		case 1:
			MRK.RotationReset ();

			R_SMG.transform.parent = null;
			R_SniperRifle.transform.parent = null;

			R_Assault_Rifle.SetActive (true);
			R_SMG.SetActive (false);
			R_SniperRifle.SetActive (false);

			R_Assault_Rifle.gameObject.transform.position = RWeaponSpawn.gameObject.transform.position;
			R_Assault_Rifle.gameObject.transform.parent = chosenFrame.transform;

			RWeapon_Name.text = "Assault Rifle";

			//Variables
			RWeapon_Damage = 150;
			RWeapon_Accuray = 60;
			RWeapon_Range = 65;
			RWeapon_AmmoCount = 300;

			R_DamageText.text = RWeapon_Damage + "";
			R_AccuracyText.text = RWeapon_Accuray + "";
			R_RangeText.text = RWeapon_Range + "";
			R_AmmoCountText.text =RWeapon_AmmoCount + "";
			break;

		case 2:
			MRK.RotationReset ();

			R_Assault_Rifle.transform.parent = null;

			R_Assault_Rifle.SetActive (false);
			R_SniperRifle.gameObject.SetActive (true);

			R_SniperRifle.gameObject.transform.position = RWeaponSpawn.gameObject.transform.position;
			R_SniperRifle.gameObject.transform.parent = chosenFrame.transform;

			RWeapon_Name.text = "Sniper";

			//Variables
			RWeapon_Damage = 300;
			RWeapon_Accuray = 80;
			RWeapon_Range = 50;
			RWeapon_AmmoCount = 100;

			R_DamageText.text = RWeapon_Damage + "";
			R_AccuracyText.text = RWeapon_Accuray + "";
			R_RangeText.text = RWeapon_Range + "";
			R_AmmoCountText.text =RWeapon_AmmoCount + "";
			break;
		}
	}


	//MISC. METHODS
	//----------------------------------------------------------------------------------
	public void BackButton(){
		if (MenuType.Equals ("Weapons")){
			AllMechItems.gameObject.SetActive (true);
			AllWeaponItems.gameObject.SetActive (false);
			FrameChoice ();
			MenuType = "Mech";
			}else{
			SceneManager.LoadScene ("MissionSelect", LoadSceneMode.Single);
		}	
	}
		
	public void ContinueButton(){
		if (MenuType.Equals ("Mech")) {
			AllMechItems.gameObject.SetActive (false);
			AllWeaponItems.gameObject.SetActive (true);
			LeftWeaponChoice ();
			RightWeaponChoice ();
			MenuType = "Weapons";
		} else {
			StorePayerValues ();
			Debug.Log ("Mission select player prefs is" + PlayerPrefs.GetInt("MissionSelect"));
			LoadAsync(PlayerPrefs.GetInt("MissionSelect"));
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


	public void hoverTest(){
		Debug.Log ("HoverTest seucessful");
	}

	public void DeletePlayerValues(){
		PlayerPrefs.DeleteKey ("FrameChoice");
		PlayerPrefs.DeleteKey ("LeftWeaponChoice");
		PlayerPrefs.DeleteKey ("RightWeaponChoice");
	}

	public void StorePayerValues(){
		PlayerPrefs.SetInt ("FrameChoice", Frame_Number);
		PlayerPrefs.SetInt ("LeftWeaponChoice", LWeapon_Number);
		PlayerPrefs.SetInt ("RightWeaponChoice", RWeapon_Number);
	}
}