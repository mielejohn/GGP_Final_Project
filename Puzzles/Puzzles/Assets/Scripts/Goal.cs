using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure; 
using UnityEngine.SceneManagement;


public class Goal : MonoBehaviour {
	bool playerIndexSet = false;
	PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;
	public int startSize = 3; 
	public int minSize = 1; 
	public int maxSize = 6; 
	public float speed = 2.0f;
	private Vector3 targetScale;
	private Vector3 baseScale;
	private int currScale;
	private bool levelOver = false; 
	private bool buttonPress = false; 
	// Use this for initialization

	void Start () {
		baseScale = transform.localScale; 
//transform.localScale = baseScale * startSize;
		currScale = startSize;
//targetScale = baseScale * startSize;
	}
	
	// Update is called once per frame
	void Update () {
		if (buttonPress == true) 
		{
			victory ();
		}
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("player")) {
			Debug.Log ("Goal");
			// shrink goal over time and trigger a button image
			transform.localScale = Vector3.Lerp (transform.localScale, targetScale, speed * Time.deltaTime);
			buttonPress = true; 
		}

	}

	void victory () 
	{
		// spawn button press image and end application if pressed before time runs out
		if (prevState.Buttons.A == ButtonState.Pressed && state.Buttons.A == ButtonState.Released)
		{
			Application.Quit ();
		}
	}
}
