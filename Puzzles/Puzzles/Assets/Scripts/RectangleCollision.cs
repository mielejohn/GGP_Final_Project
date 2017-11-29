using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectangleCollision : MonoBehaviour {
	public GameObject player; 
	public float speed = 400; 
	//public float radius = 5.0F;
	//public float power = 10.0F;

	/*void OnCollisionEnter(Collision c)
	{
		//if (c.gameObject.CompareTag ("player")) 
			Vector3 explosionPos = transform.position;
			Collider[] colliders = Physics.OverlapSphere (explosionPos, radius);
			foreach (Collider hit in colliders) {
				Debug.Log ("you should be pushed back"); 
				Rigidbody rb = hit.GetComponent<Rigidbody> ();

				if (rb != null)
					rb.AddExplosionForce (power, explosionPos, radius, 3.0F);
			}
		
	}
	*/

	/*
	void OnCollisionEnter(Collision c)
	{
		// force is how forcefully we will push the player away from the enemy.
		float force = 50f;
		Debug.Log ("you should be pushed back"); 
		// If the object we hit is the enemy
		if (c.gameObject.CompareTag ("player"))
		{
			Destroy (gameObject);
			// Calculate Angle Between the collision point and the player
			Vector3 dir = c.contacts[0].point - transform.position;

			// We then get the opposite (-Vector3) and normalize it
			dir = -dir.normalized;

			// And finally we add force in the direction of dir and multiply it by force. 
			// This will push back the player
			GetComponent<Rigidbody>().AddForce(dir*force);
		}
}
*/

	 void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("player"))
		{
			Debug.Log ("you should be pushed back");
			//Vector3 pushBack = new Vector3 (150.0f, 0.0f, 0.0f);
			//player.GetComponent<Rigidbody> ().AddForce (pushBack);
			Rigidbody rig = other.gameObject.GetComponent<Rigidbody>();
			if(rig == null) { return;}
			Vector3 velocity = rig.velocity;
			rig.AddForce( -velocity * speed); 
		}
	}




}