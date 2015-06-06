using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour {

	public float rotateTime;

	private enum ActionState
	{
		IDLE,
		CARRYING_DOG,
		CARRYING_COIN,
		PERFORMING
	}

	private Transform _transform;
	private Rigidbody2D _rigidbody;
	private ActionState _state;

	private Quaternion _lastRotation;
	private Quaternion _targetRotation;
	private float _rotateDx;


	// Use this for initialization
	void Start () {
		_transform = transform;
		_rigidbody = GetComponent<Rigidbody2D> ();

		_state = ActionState.IDLE;

		_lastRotation = _transform.rotation;
		_targetRotation = _transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		if (_state == ActionState.PERFORMING) {
			_state = ActionState.IDLE;
		}

		_rotateDx = Mathf.Min (_rotateDx + Time.deltaTime, rotateTime);

		_transform.rotation = Quaternion.Lerp (_lastRotation, _targetRotation, _rotateDx / rotateTime);
	}

	public void Move(Vector3 direction) {
		float upangle = -(90f - (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));

		_lastRotation = _transform.rotation;
		_targetRotation = Quaternion.AngleAxis(upangle, Vector3.forward);
		_rotateDx = 0;

		_rigidbody.velocity = direction;
	}
	
	public void PerformAction() {
		if (_state == ActionState.CARRYING_COIN) {
			_state = ActionState.PERFORMING;
			// grab the coin from the pool and send the coin downwards
			//_coin.GetComponent<Rigidbody2D>().AddForce();
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.tag.Equals ("Hotdog")) {
			_state = ActionState.CARRYING_DOG;
			collider.gameObject.SetActive (false);
		} else if (collider.gameObject.tag.Equals ("Customer") && _state == ActionState.CARRYING_DOG) {
			_state = ActionState.CARRYING_COIN;
		}
	}
}
