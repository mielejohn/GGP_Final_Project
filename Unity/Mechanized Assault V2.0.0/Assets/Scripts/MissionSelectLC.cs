using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MissionSelectLC : MonoBehaviour {

	//Animations
	public GameObject ScreenFader;

	//Buttons
	public Text Mission1;
	public Text Mission2;
	public Text Mission3;
	public Text Mission4;
	public Text menuButton;

	//MissionInformation
	public GameObject MissionImage;
	public Text MissionInfo;

	//Color
	public Color SelectColor;

	void Start () {
		StartCoroutine (GameStartAnim ());
	}

	void Update () {
		
	}

	public void MissionOneSelect(){
		Mission1.color=SelectColor;
		SceneManager.LoadScene ("MechSelectionScene",LoadSceneMode.Single);
	}

	public void MissionOneHover(){
		MissionImage.gameObject.SetActive (true);
		MissionInfo.text = "This is mission one and will be an introductory mission to the main storyline";
	}

	public void MissionOneUnHover(){
		MissionImage.gameObject.SetActive (false);
		MissionInfo.text = "";
	}

	public void MissionTwoSelect() {
		Mission2.color=SelectColor;
		SceneManager.LoadScene ("MechSelectionScene",LoadSceneMode.Single);
	}

	public void MissionTwoHover(){
		MissionImage.gameObject.SetActive (true);
		MissionInfo.text = "This is mission two and will introduce the player to real combat";
	}

	public void MissionTwoUnHover(){
		MissionImage.gameObject.SetActive (false);
		MissionInfo.text = "";
	}

	public void MissionThreeSelect() {
		Mission3.color=SelectColor;
		SceneManager.LoadScene ("MechSelectionScene",LoadSceneMode.Single);
	}

	public void MissionThreeHover(){
		MissionImage.gameObject.SetActive (true);
		MissionInfo.text = "This is mission three and is mission three";
	}

	public void MissionThreeUnHover(){
		MissionImage.gameObject.SetActive (false);
		MissionInfo.text = "";
	}
		
	public void MissionFourSelect() {
		Mission4.color=SelectColor;
		SceneManager.LoadScene ("MechSelectionScene",LoadSceneMode.Single);
	}

	public void MissionFourHover(){
		MissionImage.gameObject.SetActive (true);
		MissionInfo.text = "This is mission four and its mission four";
	}

	public void MissionFourUnHover(){
		MissionImage.gameObject.SetActive (false);
		MissionInfo.text = "";
	}

	public void MainMenuButton() {
		SceneManager.LoadScene ("TitleScreen",LoadSceneMode.Single);
	}

	public IEnumerator GameStartAnim(){
		ScreenFader.gameObject.GetComponent<Animator> ().Play ("Screen_Fade_In", -1, 0.0f);
		yield return new WaitForSeconds (1.5f);
		ScreenFader.gameObject.SetActive(false);
	}
}