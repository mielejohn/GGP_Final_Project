using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class RectangleCollision : MonoBehaviour {
	[Space]
	[Header("GameObjects")]
	PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;
	public GameObject player; 
	public float speed = 400;
	public float push = 3;
	[Space]
	[Header("Rigidbody")]
	[SerializeField]
	private Rigidbody rig;
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
	/*
	 void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("player"))
		{
			Debug.Log ("you should be pushed back");

			//Vector3 pushBack = new Vector3 (150.0f, 0.0f, 0.0f);
			//player.GetComponent<Rigidbody> ().AddForce (pushBack);
			rig = other.gameObject.GetComponent<Rigidbody>();

			if(rig == null) {
				print ("Rigidbobdy is null");
				return;
			}
			Vector3 velocity = rig.velocity;
			//print ("Rigidbody velocity is: " + velocity);
			rig.AddForce( -velocity * speed); 
		}
	} */


	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("player")) {
			Debug.Log ("BUMP: " + Time.time + " / " + gameObject.name + " / " + other);
			if (state.ThumbSticks.Left.X >= 0) {
				player.transform.position += -transform.right * Time.deltaTime * push;
			} else if (state.ThumbSticks.Left.Y >= 0) {
				player.transform.position += -transform.up * Time.deltaTime * push;
			}
			/*
		else if (state.ThumbSticks.Left.X >= 0.2) 
		{
			player.transform.position += transform.right * Time.deltaTime;
		}

		else if (state.ThumbSticks.Left.X >= 0.2) 
		{
			player.transform.position += transform.right * Time.deltaTime;
		}
		*/	
		}	
	}


}