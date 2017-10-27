using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MechSelectionLC : MonoBehaviour {

	public GameObject MainCamera;
	public GameObject ScreenFader;
	public MechRotaterKeyboard MRK;

	//Frames
	[SerializeField] public int Frame_Number = 0;
	public Text Frame_Name;
	public GameObject FrameSpawn;
	public GameObject Frame_AdvanceArrow;
	public GameObject Frame_BackstepArrow;
	public GameObject DASH_Frame;
	public GameObject Assault_Frame;
	public GameObject Support_Frame;

	//Armor Items
	[SerializeField] public int Armor;
	public Slider ArmorSlider;
	public Text ArmorNumberText;

	//Weight Items
	[SerializeField] public int Weight;
	public Slider WeightSlider;
	public Text WeightNumberText;

	//Speed Items
	[SerializeField] public int Speed;
	public Slider SpeedSlider;
	public Text SpeedNumberText;

	void Start () {
		StartCoroutine (MechSelectStartAnim ());
		FrameChoice ();
		Debug.Log ("Frame choice called at start");
	}

	void Update () {
		ArmorNumberText.text = "" + Armor;
		WeightNumberText.text = "" + Weight;
		SpeedNumberText.text = "" + Speed;

	}

	public IEnumerator MechSelectStartAnim(){
		ScreenFader.gameObject.GetComponent<Animator> ().Play ("Screen_Fade_In", -1, 0.0f);
		yield return new WaitForSeconds (1.5f);
		ScreenFader.gameObject.SetActive(false);		
		yield return new WaitForSeconds (0.9f);
		MainCamera.gameObject.GetComponent<Animator> ().Play ("PlayerOneSpace", -1, 0.0f);
		Debug.Log ("Mech select start finished");

	}

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

	public void FrameChoice(){
		Debug.Log ("Frame choice called");
		switch (Frame_Number) {
		case 0:
			MRK.RotationReset ();
			Assault_Frame.gameObject.SetActive (false);
			DASH_Frame.gameObject.SetActive (true);
			Frame_Name.text = "Dash";

			//Variables
			Armor = 45;
			ArmorSlider.value = Armor;
			Weight = 50;
			WeightSlider.value = Weight;
			Speed = 75;
			SpeedSlider.value = Speed;
			break;

		case 1:
			MRK.RotationReset ();
			DASH_Frame.gameObject.SetActive (false);
			Support_Frame.SetActive (false);
			Assault_Frame.gameObject.SetActive (true);
			Frame_Name.text = "Assault";

			//Variables
			Armor = 55;
			ArmorSlider.value = Armor;
			Weight = 60;
			WeightSlider.value = Weight;
			Speed = 65;
			SpeedSlider.value = Speed;
			break;

		case 2:
			MRK.RotationReset ();
			Assault_Frame.gameObject.SetActive (false);
			Support_Frame.gameObject.SetActive (true);
			Frame_Name.text = "Support";

			//Variables
			Armor = 75;
			ArmorSlider.value = Armor;
			Weight = 80;
			WeightSlider.value = Weight;
			Speed = 50;
			SpeedSlider.value = Speed;
			break;
		}
	}

	public void hoverTest(){
		Debug.Log ("HoverTest seucessful");
	}
}
