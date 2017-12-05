using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class Tutorial_Controller : MonoBehaviour {

	[Header("Game Manager")]
	public GameObject GM;
	public GameManager GameManager;

	[Space]
	[Header("Level Controller")]
	public Level_1_LC LC;

	[Space]
	[Header("Dialogue System")]
	public Dialogue_Controller Dialogue;

	[Space]
	[Header("Tasks")]
	[SerializeField] public int Tasks = 0;

	[Space]
	[Header("Movement Tutorial")]
	public bool LeftMove;
	public bool RightMove;
	public bool ForwardMove;	
	public bool ReverseMove;

	[Space]
	[Header("Flight and Boost Tutorial")]
	public bool BoostMove;
	public bool FlightMove;

	[Space]
	[Header("Combat Tutorial")]
	public GameObject[] PracticeEnemies;
	public int PracticeEnemiesCount = 3;

	[Space]
	[Header("ActualCombat")]
	public GameObject[] Enemies;
	public int EnemiesCount = 5;

	[Space]
	[Header("RepairController")]
	public GameObject RepairCrate;

	[Space]
	[Header("MISC")]
	public bool textRunning;
	// Use this for initialization
	void Start () {
		StartCoroutine (FirstDialogue());
		GM = GameObject.FindGameObjectWithTag ("GameManage");
		GameManager = GM.GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Tasks == 1) {
			if (!GameManager.prevState.IsConnected) {
				if (Input.GetButtonDown ("Horizontal")) {
					LeftMove = true;
					RightMove = true;
				}

				if (Input.GetButtonDown ("Vertical")) {
					ForwardMove = true;
					ReverseMove = true;
				}
			}

			if (GameManager.prevState.IsConnected) {
				if (GameManager.state.ThumbSticks.Left.Y > 0) {
					ForwardMove = true;
				}
				if (GameManager.state.ThumbSticks.Left.Y < 0) {
					ReverseMove = true;
				}
				if (GameManager.state.ThumbSticks.Left.X < 0) {
					LeftMove = true;
				}
				if (GameManager.state.ThumbSticks.Left.X > 0) {
					RightMove = true;
				}
			}

			if (ForwardMove == true && ReverseMove == true && LeftMove == true && RightMove == true && Tasks == 1) {
				print ("Task 1 moving to task 2");
				Tasks++;
				Dialogue.nextDialogue ();
			}
		}

		if (Tasks == 2) {
			if (!GameManager.prevState.IsConnected) {
				if (Input.GetButtonDown ("Flight")) {
					FlightMove = true;
				}
				if (Input.GetButtonDown ("Jump")) {
					BoostMove = true;
				}
			}

			if (GameManager.prevState.IsConnected) {
				if (GameManager.state.Triggers.Left > 0) {
					FlightMove = true;
				}
				if (GameManager.state.Triggers.Right > 0.25) {
					BoostMove = true;
				}
			}
			if (BoostMove == true && FlightMove == true && Tasks == 2) {
				print ("Task 2 moving to task 3");
				Tasks++;
				Dialogue.nextDialogue ();
				SpawnPracticeEnemies ();
			}
		}

		if (Tasks == 3) {
			if (PracticeEnemiesCount <= 0) {
				print ("Task 3 moving to task 4");
				Tasks++;
				Dialogue.nextDialogue ();
			}
		}

		if (Tasks == 4 && textRunning == false) {
			print ("Task 4 moving to task 6");
			textRunning = true;
			StartCoroutine(LoopDialogue (2));
		}

		if (Tasks == 6 && textRunning == false) {
			if (!GameManager.prevState.IsConnected) {
				if (Input.GetKey (KeyCode.Alpha1)) {
					textRunning = true;
					StartCoroutine (SeventhDialogue ());
				}
			}

			if (GameManager.prevState.IsConnected) {
				if (GameManager.prevState.DPad.Up == ButtonState.Released && GameManager.state.DPad.Up == ButtonState.Pressed) {
					textRunning = true;
					StartCoroutine (SeventhDialogue ());
				}
			}
		}

		if(Tasks == 7 & textRunning == false){
			print ("On task 7, enemies spawning");
			textRunning = true;
			StartCoroutine (SeventhDialogue());
		}

		if (Tasks == 8) {
			if (EnemiesCount <= 0) {
				Tasks++;
				StartCoroutine (NinthDialogue());
			}
		}
	}

	private IEnumerator FirstDialogue(){
		yield return new WaitForSeconds (10.0f);
		Dialogue.nextDialogue ();
		Tasks++;
	}

	private IEnumerator SeventhDialogue(){
		yield return new WaitForSeconds (4.0f);
		Tasks++;
		Dialogue.nextDialogue ();
		SpawnEnemies ();
		textRunning = false;

	}

	private IEnumerator EigthDialogue(){
		yield return new WaitForSeconds (4.0f);
		SpawnEnemies ();
		Tasks++;
		Dialogue.nextDialogue ();
		textRunning = false;
	}

	private IEnumerator NinthDialogue(){
		yield return new WaitForSeconds (4.0f);

		Dialogue.nextDialogue ();
		StartCoroutine( LC.MissionPassed ());
		//Tasks++;
	}

	public void SpawnPracticeEnemies (){
		for (int i = 0; i < PracticeEnemies.Length; i++) {
			PracticeEnemies [i].SetActive (true);
		}
	}

	private void SpawnEnemies(){
		for (int i = 0; i < Enemies.Length; i++) {
			Enemies [i].SetActive (true);
		}
	}

	private IEnumerator LoopDialogue(int DialogueCount){
		print ("Running Dialogue loop");
		for (int i = 0; i < DialogueCount; i++) {
			Tasks++;
			Dialogue.nextDialogue ();
			if (i == 1) {
				RepairCrate.SetActive (true);
			}
			yield return new WaitForSeconds (4.0f);
		}
		print ("Current task is" + Tasks);
		print("Fell out of the loop");
		textRunning = false;
	}
}
