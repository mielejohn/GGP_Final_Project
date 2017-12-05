using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStandin : MonoBehaviour {


	[Space]
	[Header("Health")]
	[SerializeField] int health = 300;
	public Slider HealthSlider;
	public Tutorial_Controller TC;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0) {
			TC.PracticeEnemiesCount--;
			Destroy (this.gameObject);
		}

		HealthSlider.value = health;
	}

	protected void OnTriggerEnter(Collider other){ 

		if (other.tag == "SniperBullet") {
			Debug.Log ("Sniper hit");
			Destroy (other.gameObject);
			health -= 600;
		}

		if (other.tag == "ARBullet") {
			Debug.Log ("AR hit");
			Destroy (other.gameObject);
			health -= 500;
		}

		if (other.tag == "SMGBullet") {
			Debug.Log ("SMG hit");
			Destroy (other.gameObject);
			health -= 400;
		}
	}
}
