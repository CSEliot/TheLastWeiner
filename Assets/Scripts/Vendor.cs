using UnityEngine;
using System.Collections;

public class Vendor : MonoBehaviour {

	public bool gameRunning = true;

	private string HORIZONTAL = "VendorHorizontalButton";
	private int coins_collected = 0;

	// Movement
	public float speed = 10;
	private Rigidbody2D rb2d;
	private float direction;
	private bool JUMP;

	// Rotation
	public float rot = 0;

	public float JumpHeight;
	// Hotdog
	public GameObject dogPrefab;
	public float launchVel = 10;
	public float dogInterval = 5;

	public CoinManager TheCoinManager;

	public AudioSource HotDogThrow;

	void Start(){
		rb2d = GetComponent<Rigidbody2D> ();
		StartCoroutine (launchHotdogs ());
		JUMP = false;

	}

	void Update(){
		if(Input.GetKeyDown("space") && transform.position.y < -4.5f){
			JUMP = true;
		}
		direction = Input.GetAxisRaw (HORIZONTAL);
	}

	void FixedUpdate(){

		rb2d.velocity = new Vector2 (direction * speed, rb2d.velocity.y);

		if (direction > 0) {
			rot = 180;
		} else if (direction < 0) {
			rot = 0;
		}

		transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler(0, rot, 0), .15f);

		if(JUMP){
			Debug.Log("Attempting to Jump");
			JUMP = false;
			rb2d.velocity += new Vector2(0f, JumpHeight);		
		}
	}

	IEnumerator launchHotdogs(){
		while (gameRunning) {
			yield return new WaitForSeconds (dogInterval);
			GameObject dog = Instantiate (dogPrefab, new Vector3(rb2d.position.x, rb2d.position.y, -3.83f), rb2d.transform.rotation) as GameObject;
			// Set the dogs initial Velocity
			dog.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, launchVel);
			HotDogThrow.Play();
		}
	}

	// Collides with other Collider2D
	void OnTriggerEnter2D(Collider2D other) {

		Debug.Log("OH I VENDOR COLLIDED WITH: " + other.name);

		if (other.gameObject.tag.Equals ("Coin")) {
			other.GetComponent<AudioSource>().Play();
			coins_collected++;
			other.gameObject.GetComponent<Coin>().Reset();
			TheCoinManager.AddNewCoin();

			//increase difficulty on ships, asteroids, and spawning rate
			GameObject.Find("Spawners").GetComponent<EnemySpawner>().IncreaseDifficulty();
		}

		// Collides with Enemy
		else if( other.gameObject.tag.Equals("Enemy") ){
		    Destroy(other.gameObject);
			Destroy(this.gameObject);
		}
	}
}
