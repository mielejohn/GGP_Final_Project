using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XInputDotNetPure; 
using UnityEngine.SceneManagement;


public class Goal : MonoBehaviour {
	public static Goal Me;
	public GameObject buttonPrompt; 
	public Text secondTimer; 
	public static int secondCount = 3;
	public GameObject VictoryPage; 
	public GameObject FailPage; 
	PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;
	public int startSize = 3; 
	public int minSize = 1; 
	public int maxSize = 6; 
	public float speed = 4.0f;
	private Vector3 targetScale;
	private Vector3 baseScale;
	private int currScale;
	private bool levelOver = false; 
	public bool elligibletoWin = false; 
	public bool buttonpromptgone = false; 
	// Use this for initialization

	void Start () {
		Goal.Me = this;
		buttonPrompt.SetActive (false);
		VictoryPage.SetActive (false);
		FailPage.SetActive (false);
		baseScale = transform.localScale; 
//transform.localScale = baseScale * startSize;
		currScale = startSize;
//targetScale = baseScale * startSize;
	}
	
	// Update is called once per frame
	void Update () {



		if (elligibletoWin == true) 
		{
			//buttonPrompt.SetActive (false);
		}


		if (GameController.count == 0)
		{
			FailPage.SetActive (true);
			Invoke ("LevelOver", 3);
		}
	}
	void OnTriggerStay(Collider other)
	{
		elligibletoWin = true;
		if (other.gameObject.CompareTag ("player")) {
			Debug.Log ("Goal");
			// shrink goal over time and trigger a button image
			transform.localScale = Vector3.Lerp (transform.localScale, targetScale, speed * Time.deltaTime);
			//buttonPress = true; 
		//	Invoke ("secondaryTimer", 1);
			Invoke ("TimeDestroy", 3);
			//buttonPrompt.SetActive (true);

		}

	}

	void OnTriggerEnter(Collider other)
	{
			buttonPrompt.SetActive (true);

		}

	void secondaryTimer () 
	{
		secondCount = secondCount - 1; 
		secondTimer.text = "" + secondCount * Time.deltaTime;
		// spawn button press image and end application if pressed before time runs out

	}



	void LevelOver ()
	{
		GameController.won = false; 
	}

	public void PlayerUse(GameObject player)
	{
		if (elligibletoWin)
			VictoryPage.SetActive (true);
	   
		
	}

}
