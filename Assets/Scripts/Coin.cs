using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

	public GameObject manager;

	private CoinManager _coinManager;

	// Use this for initialization
	void Start () {
		_coinManager = manager.GetComponent<CoinManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.tag.Equals("Ground")) {
			_coinManager.coinPool.ReturnObject(this);
			this.gameObject.SetActive(false);
		}
	}
}
