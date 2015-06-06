using UnityEngine;
using System.Collections;

public class BirdControls : MonoBehaviour {

	public bool keyboard = true;

	private const string HORIZONTAL_NAME = "BirdHorizontal";
	private const string VERTICAL_NAME = "BirdVertical";
	private const string HORIZONTAL_BUTTON_NAME = "BirdHorizontalButton";
	private const string VERTICAL_BUTTON_NAME = "BirdVerticalButton";
	private const string ACTION_NAME = "BirdAction";
	
	public float speed;

	private Vector3 _direction;
	private Bird _bird;

	// Use this for initialization
	void Awake () {
		this._direction = new Vector3 ();
		this._bird = this.gameObject.GetComponent<Bird>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!keyboard) {
			_direction.x = Input.GetAxis (HORIZONTAL_NAME);
			_direction.y = Input.GetAxis (VERTICAL_NAME);
		} else {
			_direction.x = Input.GetAxisRaw (HORIZONTAL_BUTTON_NAME);
			_direction.y = Input.GetAxisRaw (VERTICAL_BUTTON_NAME);	
		}

		_direction.z = 0.0f;
		_direction.Normalize ();

		print (_direction.ToString());

		_bird.Move (_direction * speed * Time.deltaTime);

		if (Input.GetAxis (ACTION_NAME) > 0) {
			_bird.PerformAction();
		}
	}
}
