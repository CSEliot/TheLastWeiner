using UnityEngine;
using System.Collections;

public class AsteroidMovement : MonoBehaviour {
	
	public float velocity;
	private Rigidbody2D _rigidbody;
	
	// Use this for initialization
	void Start ()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
		_rigidbody.angularVelocity = Random.Range ((float)100, (float)500);
	}

	void Update ()
	{
		_rigidbody.MovePosition(new Vector2(_rigidbody.position.x - velocity * Random.Range ((float)0.2, (float)1.0) * Time.deltaTime, _rigidbody.position.y - velocity * Time.deltaTime));
	}


	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.tag.Equals("Ground"))
		{
			Destroy(gameObject);
		}
	}
}