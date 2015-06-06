using UnityEngine;
using System.Collections;

public class Vendor : MonoBehaviour {

	public float speed = 10;

	private string HORIZONTAL = "BirdHorizontalButton";
	private int coins_collected = 0;

	private Vector2 direction;
	private Rigidbody2D rb2d;

	// Hotdog
	public GameObject dogPrefab;

	private float dogYVelMin;
	private float dogYVelMax;
	private float dogXVelMin;
	private float dogXVelMax;

	void Start(){
		rb2d = GetComponent<Rigidbody2D> ();
	}


	void FixedUpdate(){
		rb2d.velocity = new Vector2 (Input.GetAxisRaw (HORIZONTAL) * speed, rb2d.velocity.y);
		launchHotdog ();
	}

	void launchHotdog(){
		GameObject dog = Instantiate (dogPrefab, rb2d.position, rb2d.transform.rotation ) as GameObject;

		// Set the dogs initial Velocity
		Rigidbody2D dogBody = dog.GetComponent<Rigidbody2D> ();
		dogBody.velocity = new Vector2 (0f, 4f);
	}

	// Collides with other Collider2D
	void OnTriggerEnter2D(Collider2D other) {

		// Collides with coin
		if (other.gameObject.tag.Equals ("Coin")) {
			coins_collected++;
			other.gameObject.SetActive (false);
		}

		// Collides with Bad thing
		else if( other.gameObject.tag.Equals("Baddie") ){
			// Maybe do some death thing
			other.gameObject.SetActive(false);
		}
	}
}
