using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour {

	public readonly ObjectPool<Coin> coinPool = new ObjectPool<Coin>();

	public GameObject coinPrefab;
	public int sizeOfPool;


	public GameObject eliotlol;
	public GameObject fool;


	private int totalCoins = 0;

	private Image foolImage;
	// Use this for initialization
	void Awake () {
		foolImage = fool.GetComponent<Image>();
		coinPrefab.GetComponent<Coin> ().manager = this.gameObject;
		for (int i = 0; i < sizeOfPool; i++) {
			GameObject coinObj = GameObject.Instantiate(coinPrefab, Vector3.zero, Quaternion.AngleAxis(90, Vector3.left)) as GameObject;
			coinObj.name = "Coin " + i;
			Coin coin = coinObj.GetComponent<Coin>();
			coinPool.AddObject( coin );
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(totalCoins >= 10){
			eliotlol.SetActive(true);
			GameObject.Find("Spawners").SetActive(false);
		}
	}

	public void AddNewCoin(){
		totalCoins ++;
		foolImage.fillAmount += 0.10f;
	}
}
