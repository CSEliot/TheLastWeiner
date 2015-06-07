using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour {

	public float actionDelay = 0.5f;
	public float rotateTime = 0.2f;
	public int maxDogs = 1;
	public Transform manager;

	public enum ActionState
	{
		IDLE,
		CARRYING,
		PERFORMING
	}
	
	private CoinManager _coinManager;

	private Transform _transform;
	private Rigidbody2D _rigidbody;
	public ActionState _state = ActionState.IDLE;

	private Quaternion _lastRotation;
	private Quaternion _targetRotation;
	private float _rotateDx;

	public int _numDogs;
	public int _numCoins;

	private float _lastAction;


	// Use this for initialization
	void Start () {
		_transform = transform;
		_rigidbody = GetComponent<Rigidbody2D> ();

		_lastRotation = _transform.rotation;
		_targetRotation = _transform.rotation;

		_coinManager = manager.GetComponent<CoinManager> ();
	}
	
	void FixedUpdate () {
		if (_state == ActionState.PERFORMING) {
			if (_numCoins == 0) {
				_state = ActionState.IDLE;
			} else {
				if (Time.time - _lastAction > actionDelay) { 
					_state = ActionState.CARRYING;
				}
			}
		}

		_rotateDx = Mathf.Min (_rotateDx + Time.deltaTime, rotateTime);

		_transform.rotation = Quaternion.Lerp (_lastRotation, _targetRotation, _rotateDx / rotateTime);
	}

	public void Move(Vector3 direction) {
		float upangle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

		_lastRotation = _transform.rotation;
		_targetRotation = Quaternion.AngleAxis(upangle, Vector3.forward);
		_rotateDx = 0;

		_rigidbody.velocity = direction;
	}
	
	public void PerformAction() {
		if (_state == ActionState.CARRYING && _numCoins > 0) {
			_state = ActionState.PERFORMING;

			// grab the coin from the pool and send the coin downwards
			Coin coin = _coinManager.coinPool.RequestObject();
			if (coin != null) {
				print("ACTION PERFORMED");
				coin.gameObject.SetActive(true);				
				coin.transform.position = new Vector3(_transform.position.x, _transform.position.y, _transform.position.z);
				_numCoins--;
				_lastAction = Time.time;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.tag.Equals ("Hotdog") && _numDogs < maxDogs) {
			_state = ActionState.CARRYING;
			_numDogs++;
			collider.gameObject.SetActive (false);
		} else if (collider.gameObject.tag.Equals ("Customer") && _numDogs > 0) {
			_numDogs--;
			_numCoins++;
			Customer cust = collider.gameObject.GetComponent<Customer>();
			if (cust != null) {
				cust.MoveRandom();
			}
		} else if (collider.gameObject.tag.Equals ("Enemy")) {
			this.gameObject.SetActive(false);
		}
	}
}
