using UnityEngine;
using System.Collections;

public class Hotdog : MonoBehaviour {
	// Collides with other Collider2D
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag.Equals ("Ground")) {
			Destroy(gameObject);
		}
	}
}
