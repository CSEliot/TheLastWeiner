using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Customer : MonoBehaviour {

	public List<Transform> windows;

	private Transform _transform;
	private int _lastWindow;

	// Use this for initialization
	void Start () {
		_transform = this.transform;
		_lastWindow = -1;
		MoveRandom ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void MoveRandom() {
		int choice = PickRandomElement (windows);

		_transform.position = windows[choice].position;

		_lastWindow = choice;
	}

	private int PickRandomElement(List<Transform> list) {
		for (int i = 0; i < list.Count; i++) {
			if (i != _lastWindow) {
				int rng = Random.Range (i, list.Count - 1);
				if (rng == i) {
					return i;
				}
			}
		}

		return 0;
	}
	
}
