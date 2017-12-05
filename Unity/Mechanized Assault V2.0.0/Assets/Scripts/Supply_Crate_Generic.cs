using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supply_Crate_Generic : MonoBehaviour {

	public bool GroundCheck;
	public GameObject SmokeGO;
	public ParticleSystem Smoke;
	public Terrain terrain;
	public AudioSource Audio;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Terrain") {
			SmokeGO.SetActive (true);
			Smoke.Play ();
		}
	}
}
