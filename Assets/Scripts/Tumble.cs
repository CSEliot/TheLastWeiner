using UnityEngine;
using System.Collections;

public class Tumble : MonoBehaviour {

	private Rigidbody2D _rigidbody;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.left, Time.deltaTime * 180, Space.Self);
	}
}
