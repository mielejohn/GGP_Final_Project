using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavEnemy : Enemy {
	protected float navPatrolDistance = 0.306f;
	public UnityEngine.AI.NavMeshAgent navMeshAgent;
	//public GameObject PlayerOfInterest;
	//protected float AttackSpeed;

	protected override void Start () {
		navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		if (patrolPoints == null) {
			patrolPoints = new List<GameObject> ();
			foreach (GameObject go in GameObject.FindGameObjectsWithTag("NavPatrolPoints")) {
				//Debug.Log ("Adding NavEnemy Patrol point: " + go.transform.position);
				patrolPoints.Add (go);
			}
		}

		SwitchToPatrolling ();

		Debug.Log ("Chasing speed is " + ChaseSpeed);
		Debug.Log ("Attacking speed is " + AttackSpeed);
		Debug.Log ("Patrolling speed is " + walkingSpeed);
	}

	protected override void OnAttackingUpdate(){
		//float step = AttackSpeed * Time.deltaTime;
		Debug.Log ("OnAttackingStarted");
		navMeshAgent.SetDestination (PlayerOfInterest.transform.position);

		float distance = Vector3.Distance (transform.position, PlayerOfInterest.transform.position);
		if (distance > attackingDistance) {
			SwitchToChasing (PlayerOfInterest);
			Debug.Log ("SwitchingToChasing");
		}
	}

	protected override void OnChasingUpdate(){
		Debug.Log ("OnChasingStarted");
		navMeshAgent.SetDestination(PlayerOfInterest.transform.position);

		float distance = Vector3.Distance (transform.position, PlayerOfInterest.transform.position);
		Debug.Log ("About to switch to attacking");
		if (distance <= attackingDistance) {
			SwitchToAttacking (PlayerOfInterest);
			Debug.Log ("SwitchingToAttacking");
		}
	}

	protected override void OnPatrollingUpdate(){
		Debug.Log ("OnPatrollingStarted");
		navMeshAgent.SetDestination (patrollingInterestPoint.transform.position);

		float distance = Vector3.Distance (transform.position, patrollingInterestPoint.transform.position);
		if (distance <= navMeshAgent.stoppingDistance) {
			SelectRandomPatrolPoint ();
			Debug.Log ("SwitchingToPatrolling");
		}
	}

	protected override void SelectRandomPatrolPoint(){
		Debug.Log ("Choosing random patrol point");
		int choice = Random.Range (0, patrolPoints.Count);
		patrollingInterestPoint = patrolPoints [choice];
		navMeshAgent.SetDestination (patrollingInterestPoint.transform.position);

	}
}
