using UnityEngine;
using System.Collections;

public class Vendor : MonoBehaviour {

	public bool gameRunning = true;
	public float speed = 10;
	public float launchVel = 10;
	public float dogInterval = 5;

	private string HORIZONTAL = "BirdHorizontalButton";
	private int coins_collected = 0;

	// Movement
	private Vector2 direction;
	private Rigidbody2D rb2d;

	// Hotdog
	public GameObject dogPrefab;

	public float yRotDelta = 5;
	public float yRot = 0;
	
	private float dogYVelMin;
	private float dogYVelMax;
	private float dogXVelMin;
	private float dogXVelMax;

	void Start(){
		rb2d = GetComponent<Rigidbody2D> ();
		StartCoroutine (launchHotdogs ());
	}


	void FixedUpdate(){
		float direction = Input.GetAxisRaw (HORIZONTAL);

		rb2d.velocity = new Vector2 (direction * speed, rb2d.velocity.y);

		if(direction < 0) {
			transform.rotation = Quaternion.Euler (0, yRot, 0);
		} else if (direction > 0) {
			transform.rotation = Quaternion.Euler (0, yRot, 0);
		}
	}

	IEnumerator launchHotdogs(){
		while (gameRunning) {
			yield return new WaitForSeconds (dogInterval);
			GameObject dog = Instantiate (dogPrefab, rb2d.position, rb2d.transform.rotation) as GameObject;
			// Set the dogs initial Velocity
			dog.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, launchVel);
		}
	}

	// Collides with other Collider2D
	void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject.tag.Equals ("Coin")) {
			coins_collected++;
			other.gameObject.GetComponent<Coin>().Reset();
		}

		// Collides with Enemy
		else if( other.gameObject.tag.Equals("Enemy") ){
			Destroy(other.gameObject);
		}
	}
}
