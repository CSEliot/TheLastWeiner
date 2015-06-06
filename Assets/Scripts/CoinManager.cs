using UnityEngine;
using System.Collections;

public class CoinManager : MonoBehaviour {

	public readonly ObjectPool<Coin> coinPool = new ObjectPool<Coin>();

	public GameObject coinPrefab;
	public int sizeOfPool;

	// Use this for initialization
	void Awake () {
		for (int i = 0; i < sizeOfPool; i++) {
			GameObject coin = GameObject.Instantiate(coinPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			coinPool.AddObject( coin.GetComponent<Coin>() );
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
