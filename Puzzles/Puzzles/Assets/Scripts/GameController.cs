using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour {
	public Text timer; 
	public static int count = 35;
	void Start () 
	{
		InvokeRepeating ("timeCount", 0f, 1f);
	}

	void timeCount ()
	{

		count = count - 1; 
		timer.text = "" + count;

	}
}
