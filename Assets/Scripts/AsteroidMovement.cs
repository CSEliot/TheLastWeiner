using UnityEngine;
using System.Collections;

public class AsteroidMovement : MonoBehaviour {
	
	public float velocity;
	private float random;
	private Rigidbody2D _rigidbody;
	
	// Use this for initialization
	void Start ()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
		random = Random.Range ((float)0.2, (float)1.0);
	}
	
	// Update is called once per frame
	void Update ()
	{
		_rigidbody.MovePosition(new Vector2(_rigidbody.position.x - velocity * random, _rigidbody.position.y - velocity));
	}
}