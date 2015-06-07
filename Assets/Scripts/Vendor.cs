using UnityEngine;
using System.Collections;

public class Vendor : MonoBehaviour {

	public bool gameRunning = true;

	private string HORIZONTAL = "VendorHorizontalButton";
	private int coins_collected = 0;

	// Movement
	public float speed = 10;
	private Rigidbody2D rb2d;
	private Vector2 direction;

	// Rotation
	public float rot = 0;

	// Hotdog
	public GameObject dogPrefab;
	public float launchVel = 10;
	public float dogInterval = 5;


	void Start(){
		rb2d = GetComponent<Rigidbody2D> ();
		StartCoroutine (launchHotdogs ());
	}


	void FixedUpdate(){
		float direction = Input.GetAxisRaw (HORIZONTAL);
		rb2d.velocity = new Vector2 (direction * speed, rb2d.velocity.y);

		if (direction > 0) {
			rot = 180;
		} else if (direction < 0) {
			rot = 0;
		}

		transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler(0, rot, 0), .15f);

	}

	IEnumerator launchHotdogs(){
		while (gameRunning) {
			yield return new WaitForSeconds (dogInterval);
			GameObject dog = Instantiate (dogPrefab, new Vector3(rb2d.position.x, rb2d.position.y, -3.83f), rb2d.transform.rotation) as GameObject;
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
		     	Debug.Log("POOPERE");
			Destroy(other.gameObject);
			Destroy(this);
		}
	}
}
