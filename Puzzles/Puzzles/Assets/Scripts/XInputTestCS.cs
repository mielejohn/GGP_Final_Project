using UnityEngine;
using XInputDotNetPure; // Required in C#

public class XInputTestCS : MonoBehaviour
{
	bool playerIndexSet = false;
	PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;
	public int Gspeed = 200; 
	private float moveX;
	private float moveZ;

	// Use this for initialization
	void Start()
	{
		// No need to initialize anything for the plugin
	}

	void FixedUpdate()
	{
		// SetVibration should be sent in a slower rate.
		// Set vibration according to triggers
		GamePad.SetVibration(playerIndex, state.Triggers.Left, state.Triggers.Right);
	}

	void Move(float moveX, float moveZ)
	{
		transform.position += transform.up * Time.deltaTime * moveZ;
		transform.position += transform.right * Time.deltaTime * moveX;
	}

	// Update is called once per frame
	void Update()
	{
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

		// Detect if a button was pressed this frame
		if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed && Goal.Me.elligibletoWin == true)
		{
			
			Goal.Me.PlayerUse (gameObject);
			GameController.won = true;

			Goal.Me.buttonPrompt.SetActive (false);
		
				//Application.Quit ();
			
			GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value, 1.0f);
		}
	
		// Make the current object turn
		/* transform.localRotation *= Quaternion.Euler(0.0f, state.ThumbSticks.Left.X * 25.0f * Time.deltaTime, 0.0f);
		if (state.ThumbSticks.Left.X > 0 || state.ThumbSticks.Left.X > 0) {
			transform.localPosition += new Vector3 (state.ThumbSticks.Left.X * Time.deltaTime * 10.0f, state.ThumbSticks.Left.Y * Time.deltaTime * 10.0f, 0.0f); 
		}

		 */

		if (state.ThumbSticks.Left.X > 0 || state.ThumbSticks.Left.Y > 0 ||  state.ThumbSticks.Left.X < 0 || state.ThumbSticks.Left.Y < 0)
		{
			moveX = state.ThumbSticks.Left.X * Gspeed * Time.deltaTime;
			moveZ = state.ThumbSticks.Left.Y * Gspeed * Time.deltaTime;
			//Debug.Log ("You want to move - you should be moving");
			Move(moveX, moveZ); 
		}



		//transform.localPosition = new Vector3 (0,  state.ThumbSticks.Right.Y * 25.0f * Time.deltaTime, 0); 

	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("barrier"))
		{
			//	Debug.Log ("you hit a barrier");
			moveX = 0;
			moveZ = 0;
			Move(moveX, moveZ); 
		}

	}

	/*
	void OnGUI()
	{
		string text = "Use left stick to turn the cube, hold A to change color\n";
		text += string.Format("IsConnected {0} Packet #{1}\n", state.IsConnected, state.PacketNumber);
		text += string.Format("\tTriggers {0} {1}\n", state.Triggers.Left, state.Triggers.Right);
		text += string.Format("\tD-Pad {0} {1} {2} {3}\n", state.DPad.Up, state.DPad.Right, state.DPad.Down, state.DPad.Left);
		text += string.Format("\tButtons Start {0} Back {1} Guide {2}\n", state.Buttons.Start, state.Buttons.Back, state.Buttons.Guide);
		text += string.Format("\tButtons LeftStick {0} RightStick {1} LeftShoulder {2} RightShoulder {3}\n", state.Buttons.LeftStick, state.Buttons.RightStick, state.Buttons.LeftShoulder, state.Buttons.RightShoulder);
		text += string.Format("\tButtons A {0} B {1} X {2} Y {3}\n", state.Buttons.A, state.Buttons.B, state.Buttons.X, state.Buttons.Y);
		text += string.Format("\tSticks Left {0} {1} Right {2} {3}\n", state.ThumbSticks.Left.X, state.ThumbSticks.Left.Y, state.ThumbSticks.Right.X, state.ThumbSticks.Right.Y);
		GUI.Label(new Rect(0, 0, Screen.width, Screen.height), text);
	} 
	 */
}
