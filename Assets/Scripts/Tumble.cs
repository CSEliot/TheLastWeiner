using UnityEngine;
using System.Collections;

public class Tumble : MonoBehaviour {

	private Rigidbody2D _rigidbody;

	// Use this for initialization
	void Start () {
		_rigidbody = GetComponent<Rigidbody2D>();
		_rigidbody.angularVelocity = Random.Range (100f, 500f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
