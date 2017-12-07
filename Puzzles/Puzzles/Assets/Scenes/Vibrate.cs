using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure; // Required in C#


public class Vibrate : MonoBehaviour {
	PlayerIndex playerIndex;
	bool playerIndexSet = false;
	GamePadState state;
	GamePadState prevState;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// Find a PlayerIndex, for a single player game
		// Will find the first controller that is connected ans use it
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
		
	}

	void OnTriggerEnter(Collider other)
	{
		GamePad.SetVibration(playerIndex, 0.5f, 0.5f);
		Debug.Log ("Vibrate");
	}
}
// give cube RB, is kinematic no gravity, add script to cube, make rect the trigger on triggerstay for continuous vibrate 