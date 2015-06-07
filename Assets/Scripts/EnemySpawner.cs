using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject asteroid1;
	public GameObject asteroid2;
	public GameObject asteroid3;
	public GameObject ship;
	private Vector3 SpawnPosition;

	// Use this for initialization
	void Start ()
	{
		StartCoroutine (spawnEnemy());
		SpawnPosition = new Vector3 (0,0,0);
	}

	// Update is called once per frame
	void Update()
	{

	}

	IEnumerator spawnEnemy()
	{
		// Change loop exit condition to whatever stops the game later
		while (true)
		{
			int random = Random.Range (0, 10);

			// Spawn asteroids 90% of the time
			if (random < 9) {
				SpawnPosition.Set(Random.Range (-10.0f, 15.0f), 10.5f, -3.83f);
				if (random < 3)
				{
					Destroy(Instantiate (asteroid1, SpawnPosition, Quaternion.identity), 3);
				}
				else if (random < 6)
				{
					Destroy(Instantiate (asteroid2, SpawnPosition, Quaternion.identity), 3);
				}
				else
				{
					Destroy(Instantiate (asteroid3, SpawnPosition, Quaternion.identity), 3);
				}
			}
			// Spawn ship the other 10% of the time
			else {
				Vector3 spawnPosition = new Vector3 (25.0f, Random.Range (-1.5f, 7.5f), 0.0f);
				Instantiate (ship, spawnPosition, Quaternion.identity);
			}

			yield return new WaitForSeconds((float)Random.Range((float)0.3, (float)0.6));
		}
	}
}
