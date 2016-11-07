using UnityEngine;
using System.Collections;

public class FollowScript : MonoBehaviour {

	public bool facingLeft = true;
	public float targetDistance;
	public float enemyLookDistance = 5f;
	public float enemyMovementSpeed = -5f;
	public Transform player;
	Controller c;

	Rigidbody2D rigidBody;
	Renderer renderer;
	void Start()
	{
		c = GetComponent<Controller> ();
		renderer = GetComponent<Renderer> ();
		rigidBody = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate ()
	{
		targetDistance = Vector2.Distance (player.position, transform.position);


		if (player.transform.position.x > transform.position.x && facingLeft) {
			print ("a");
			Flip ();
			if (targetDistance < enemyLookDistance) 
			{
				follow ();
			}
		}

		if (player.transform.position.x > transform.position.x && !facingLeft) {
			if (targetDistance < enemyLookDistance) {
				follow1 ();
			}
		}

		if (player.transform.position.x < transform.position.x  && !facingLeft) {
			print ("c");
			Flip ();
			if (targetDistance < enemyLookDistance) {
				follow1 ();
			}
		}

		if (player.transform.position.x < transform.position.x && facingLeft) {
			if (targetDistance < enemyLookDistance) {
				follow ();
			}
		}
	}

	void Flip()
	{
		print ("Flip");
		facingLeft = !facingLeft;
		Vector2 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
		
	void follow()
	{
		transform.Translate (enemyMovementSpeed * Vector2.right * Time.deltaTime);
	}

	void follow1()
	{
		transform.Translate (enemyMovementSpeed* Vector2.left * Time.deltaTime);
	}
}