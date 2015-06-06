using UnityEngine;
using System.Collections;

public class CoinManager : MonoBehaviour {

	public readonly ObjectPool<Coin> coinPool = new ObjectPool<Coin>();

	public GameObject coinPrefab;
	public int sizeOfPool;

	// Use this for initialization
	void Awake () {
		coinPrefab.GetComponent<Coin> ().manager = this.gameObject;
		for (int i = 0; i < sizeOfPool; i++) {
			GameObject coinObj = GameObject.Instantiate(coinPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			coinObj.name = "Coin " + i;
			Coin coin = coinObj.GetComponent<Coin>();
			coinPool.AddObject( coin );
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
