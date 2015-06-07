using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour {

	public float velocity;
	private Rigidbody2D _rigidbody;

	// Use this for initialization
	void Start ()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
		_rigidbody.MoveRotation (180);
	}
	
	// Update is called once per frame
	void Update ()
	{
		_rigidbody.MovePosition(new Vector2(_rigidbody.position.x - velocity * Time.deltaTime, _rigidbody.position.y));
	}

}
