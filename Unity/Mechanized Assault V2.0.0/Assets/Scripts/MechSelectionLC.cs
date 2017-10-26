using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MechSelectionLC : MonoBehaviour {

	public GameObject MainCamera;
	public GameObject ScreenFader;
	public GameObject ORSK;

	//Arms
	public GameObject ArmsSpawn;
	public Text ArmsPart_Name;
	public GameObject Arms_AdvanceArrow;
	public GameObject Arms_BackstepArrow;
	public int ArmParts_Number;
	public GameObject DASH_Arms;
	public GameObject Assault_Arms;
	public GameObject Support_Arms;

	//Chest
	public GameObject Chest;

	void Start () {
		StartCoroutine (MechSelectStartAnim ());
		ArmsChoice ();
	}

	void Update () {

	}

	public IEnumerator MechSelectStartAnim(){
		ScreenFader.gameObject.GetComponent<Animator> ().Play ("Screen_Fade_In", -1, 0.0f);
		yield return new WaitForSeconds (1.5f);
		ScreenFader.gameObject.SetActive(false);		
		yield return new WaitForSeconds (0.9f);
		MainCamera.gameObject.GetComponent<Animator> ().Play ("PlayerOneSpace", -1, 0.0f);

	}

	/*public void RotateRight(){
		ORSK.gameObject.GetComponent<MechRotaterKeyboard> ().On_D_down ();
	}

	public void RotateLeft(){
		ORSK.gameObject.GetComponent<MechRotaterKeyboard> ().On_A_down();	
	}*/

	public void NextArm(){
		if (ArmParts_Number >= 1) {
			Arms_AdvanceArrow.gameObject.SetActive (false);
			ArmParts_Number++;
			ArmsChoice ();
		} else {
			Arms_AdvanceArrow.gameObject.SetActive (true);
			Arms_BackstepArrow.gameObject.SetActive (true);
			ArmParts_Number++;
			ArmsChoice ();
		}

	}

	public void PreviousArm(){
		if (ArmParts_Number <= 1) {
			Arms_BackstepArrow.gameObject.SetActive (false);
			ArmParts_Number--;
			ArmsChoice ();
		} else {
			Arms_BackstepArrow.gameObject.SetActive (true);
			Arms_AdvanceArrow.gameObject.SetActive (true);
			ArmParts_Number--;
			ArmsChoice ();
		}
	}

	public void ArmsChoice(){
		switch (ArmParts_Number) {
		case 0:
			ORSK.gameObject.GetComponent<MechRotaterKeyboard> ().RotationReset ();
			Assault_Arms.gameObject.SetActive (false);
			DASH_Arms.gameObject.SetActive (true);
			DASH_Arms.transform.parent = Chest.transform;
			ArmsPart_Name.text = "Dash";
			break;

		case 1:
			ORSK.gameObject.GetComponent<MechRotaterKeyboard> ().RotationReset ();
			DASH_Arms.gameObject.SetActive (false);
			Support_Arms.SetActive (false);
			Assault_Arms.gameObject.SetActive (true);
			Assault_Arms.transform.parent = Chest.transform;
			ArmsPart_Name.text = "Assault";
			break;

		case 2:
			ORSK.gameObject.GetComponent<MechRotaterKeyboard> ().RotationReset ();
			Assault_Arms.gameObject.SetActive (false);
			Support_Arms.gameObject.SetActive (true);
			Support_Arms.transform.parent = Chest.transform;
			ArmsPart_Name.text = "Support";
			break;
		}
	}
}
