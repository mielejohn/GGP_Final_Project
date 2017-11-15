using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

	public GameObject TopObject;

	[Space]
	[Header("Health")]
	[SerializeField] int health = 7000;
	//public GameObject Player;
	public Slider HealthSlider;

	[Space]
	[Header("AI items")]
	public EnemyAiStates state;
	static protected List<GameObject> patrolPoints = null;

	#region Enemy Options
	[Space]
	[Header("Speeds")]
	public float walkingSpeed = 150.0f;
	public float ChaseSpeed = 150.0f;
	public float AttackSpeed =150.5f;

	[Space]
	[Header("Distances")]
	public float attackingDistance = 250.0f;
	public float chasingDistance = 750.0f;
	public float ShootingDistance = 200.0f;
	#endregion

	[Space]
	[Header("Attacking")]
	public GameObject Bullet;
	public float BulletSpeed = 450.0f;
	public GameObject LeftButlletSpawn;
	public GameObject RightButlletSpawn;

	private float fireDelta = 1.00f;
	private float nextFire = 1.00f;
	private float myTime = 0.0f;

	[Space]
	[Header("Misc.")]
	protected GameObject patrollingInterestPoint;
	public GameObject PlayerOfInterest;

	protected virtual void Start () {
		
		if (patrolPoints == null) {
			patrolPoints = new List<GameObject> ();
			foreach (GameObject go in GameObject.FindGameObjectsWithTag("NavPatrolPoints")) {
				patrolPoints.Add (go);
			}
		}
		SwitchToPatrolling ();
	}

	protected void Update () {
		myTime = myTime + Time.deltaTime;
		PlayerOfInterest = GameObject.FindGameObjectWithTag ("Player");
		switch (state) {
			case EnemyAiStates.Attacking:
				OnAttackingUpdate();
				break;
			case EnemyAiStates.Chasing:
				OnChasingUpdate();
				break;
			case EnemyAiStates.Patrolling:
				OnPatrollingUpdate();
				break;

		}

		if (state == EnemyAiStates.Chasing || state == EnemyAiStates.Attacking) {
			Vector3 targetDir = PlayerOfInterest.transform.position - transform.position;
			Quaternion lookRotation = Quaternion.LookRotation (targetDir);
			Vector3 rotation = lookRotation.eulerAngles;
			transform.rotation = Quaternion.Euler (0f, rotation.y, 0f);
			transform.position = new Vector3 (transform.position.x, 5.81f, transform.position.z);
		}

		if (state == EnemyAiStates.Patrolling) {
			transform.rotation = Quaternion.LookRotation (patrollingInterestPoint.transform.position - transform.position,Vector3.up);
		}

		if (health <= 0 && PlayerOfInterest.gameObject.GetComponent<FrameController> ().LockedOn == true) {
			//PlayerOfInterest.gameObject.GetComponent<FrameController> ().ResetRotations ();
			Destroy (this.gameObject);
		} 

		if (health <= 0) {
			Destroy (this.gameObject);
		}

		HealthSlider.value = health;
	}

	protected virtual void OnAttackingUpdate(){
		Debug.Log ("OnAttackingStarted");
		float step = AttackSpeed * Time.deltaTime;

		float distance = Vector3.Distance (transform.position, PlayerOfInterest.transform.position);
		if (distance>attackingDistance) {
			SwitchToChasing (PlayerOfInterest);
			Debug.Log ("SwitchingToChasing");
		}
		if (distance < ShootingDistance) {
			if (myTime > nextFire) {
				nextFire = myTime + fireDelta;
				GameObject Bullet_I = (GameObject)Instantiate (Bullet);
				Bullet_I.gameObject.transform.position = LeftButlletSpawn.transform.position;
				Bullet_I.GetComponent<Rigidbody> ().AddForce (transform.forward * BulletSpeed, ForceMode.VelocityChange);
				nextFire = nextFire - myTime;
				myTime = 0.0f;
				Destroy (Bullet_I, 1.0f);
			}
			Debug.Log ("Too close for comfort...");
		} else {
			transform.position = Vector3.MoveTowards (transform.position, PlayerOfInterest.transform.position, step);
		}
	}

	protected virtual void OnChasingUpdate(){
		Debug.Log ("OnChasingStarted");
		float step = ChaseSpeed * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, PlayerOfInterest.transform.position, step);

		float distance = Vector3.Distance (transform.position, PlayerOfInterest.transform.position);
		Debug.Log ("About to switch to attacking");
		if (distance <= attackingDistance) {
			SwitchToAttacking (PlayerOfInterest);
			Debug.Log ("SwitchingToAttacking");
		}
	}

	protected virtual void OnPatrollingUpdate(){
		Debug.Log ("OnPatrollingStarted");
		float step = walkingSpeed * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, patrollingInterestPoint.transform.position, step);

		float PlayerDistance = Vector3.Distance (transform.position, PlayerOfInterest.transform.position);
		float distance = Vector3.Distance (transform.position, patrollingInterestPoint.transform.position);

		if (PlayerDistance <= chasingDistance) {
			Debug.Log ("SwitchingToChasing");
			SwitchToChasing (PlayerOfInterest);
		}

		if (distance <= 1) {
			Debug.Log ("SelectingRandom point");
			SelectRandomPatrolPoint();

		}

	}

	protected void OnTriggerEnter(Collider other){ 

		if (other.tag == "SniperBullet") {
			Destroy (other.gameObject);
			health -= 300;
		}

		if (other.tag == "ARBullet") {
			Destroy (other.gameObject);
			health -= 200;
		}

		if (other.tag == "SMGBullet") {
			Destroy (other.gameObject);
			health -= 100;
		}
	}

	protected void OnTriggerExit(Collider collider) { 
		SwitchToPatrolling (); 
	}

	protected void SwitchToPatrolling(){
		state = EnemyAiStates.Patrolling;
		SelectRandomPatrolPoint();
		PlayerOfInterest = null;
	}

	protected void SwitchToAttacking(GameObject target){
		state = EnemyAiStates.Attacking;
	}

	protected void SwitchToChasing(GameObject target){
		state = EnemyAiStates.Chasing;
		PlayerOfInterest = target;
	}

	protected virtual void SelectRandomPatrolPoint(){
		Debug.Log ("Choosing a Random point");
		int choice = Random.Range (0, patrolPoints.Count);
		patrollingInterestPoint = patrolPoints [choice];
		Debug.Log ("Patrol points are: " + patrolPoints);
	}

	void HitBySphereCast(){
		print ("hit by the ray");
	}
}
