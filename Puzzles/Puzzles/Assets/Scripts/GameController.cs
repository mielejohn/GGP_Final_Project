using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour {
	AudioSource mysong;
	public static bool won; 
	public Text timer; 
	public static int count = 20;
	void Start () 
	{
		mysong = GetComponent<AudioSource>();
		mysong.Play ();
		if(GameController.won != true)
			
		InvokeRepeating ("timeCount", 0f, 1f);
	}

	void timeCount ()
	{

		count = count - 1; 
		timer.text = "" + count;

	}

	void Update ()
	{
		if (count <= 5) {
			// count color is red 
		}

		if (GameController.won) 
		{
			Destroy (timer);
		}
			

	}
}
