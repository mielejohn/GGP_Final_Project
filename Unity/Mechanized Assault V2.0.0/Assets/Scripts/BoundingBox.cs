using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundingBox : MonoBehaviour {
	[SerializeField]
	//private GameObject Player;
	//[SerializeField]
	private FrameController Player_FC;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerExit(Collider other){
		if (other.tag == "Player") {
			Player_FC = other.GetComponent<FrameController> ();
			Player_FC.OutofBounds = true;
			StartCoroutine (Player_FC.OutofBounds_m ());
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player") {
			Player_FC = other.GetComponent<FrameController> ();
			Player_FC.OutofBounds = false;
			//Player = null; 
		}
	}
}

