using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue_Controller : MonoBehaviour {

	public int DialogueNumber;
	public Text Textbox;
	private string CurrentTextString;
	public int size = 10;

	[TextArea(3,10)]
	public string[] TextArray;

	// Use this for initialization
	void Start () {
		//TextArray = new string[size];
		Text (DialogueNumber);
	}
	
	// Update is called once per frame
	void Update () {
		Textbox.text = CurrentTextString;
	}

	private void Text(int Dnumber){
		CurrentTextString = TextArray [Dnumber];
	}

	public void nextDialogue(){
		DialogueNumber++;
		Text (DialogueNumber);
	}
}
