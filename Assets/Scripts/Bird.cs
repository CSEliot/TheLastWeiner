using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour {

	public float actionDelay = 0.5f;
	public float rotateTime = 0.2f;
	public float scaleTime = 0.2f;
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

	private Vector3 _scaleVector;
	private float _lastScale;
	private float _targetScale;
	private float _scaleDx;
	
	public int _numDogs;
	public int _numCoins;

	private float _lastAction;


	// Use this for initialization
	void Start () {
		_transform = transform;
		_rigidbody = GetComponent<Rigidbody2D> ();

		_lastRotation = _transform.rotation;
		_targetRotation = _transform.rotation;

		_lastScale = _transform.localScale.x;
		_targetScale = _transform.localScale.x;
		_scaleVector = new Vector3 ();

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

		_scaleDx = Mathf.Min (_scaleDx + Time.deltaTime, scaleTime);
		_scaleVector.x = Mathf.Lerp(_lastScale, _targetScale, _scaleDx / scaleTime);
		_scaleVector.y = _transform.localScale.y;
		_scaleVector.z = _transform.localScale.z;
		_transform.localScale = _scaleVector;
	}

	public void Move(Vector3 direction) {
		float upangle = Mathf.Atan2(direction.y, 0) * Mathf.Rad2Deg;

		_lastRotation = _transform.rotation;
		_lastScale = _transform.localScale.x;

		if (direction.x < 0) {
			_targetScale = -2.3f;
			_targetRotation = Quaternion.AngleAxis(-upangle, Vector3.forward);
		}
		else if (direction.x >= 0) {
			_targetScale = 2.3f;
			_targetRotation = Quaternion.AngleAxis(upangle, Vector3.forward);
		}

		_rotateDx = 0;
		_scaleDx = 0;

		_rigidbody.velocity = direction;
	}
	
	public void PerformAction() {
		if (_state == ActionState.CARRYING && _numCoins > 0) {
			_state = ActionState.PERFORMING;

			// grab the coin from the pool and send the coin downwards
			Coin coin = _coinManager.coinPool.RequestObject();
			if (coin != null) {
				coin.gameObject.SetActive(true);				
				coin.transform.position = new Vector3(_transform.position.x, _transform.position.y, _transform.position.z);
				_numCoins--;
				_lastAction = Time.time;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.tag.Equals ("Hot Dog") ) {
			if (_numDogs < maxDogs) {
				_state = ActionState.CARRYING;
				_numDogs++;
				collider.gameObject.SetActive (false);
			}
		} else if (collider.gameObject.tag.Equals ("Customer") && _numDogs > 0) {
			_numDogs--;
			_numCoins++;
			_state = ActionState.CARRYING;
			Customer cust = collider.gameObject.GetComponent<Customer>();
			if (cust != null) {
				bool CalledFromStart = false;
				cust.MoveRandom(CalledFromStart);
			}
		} else if (collider.gameObject.tag.Equals ("Enemy")) {
			GetComponent<AudioSource>().Play();
			Destroy(this.gameObject, 1);
		}
	}
}
