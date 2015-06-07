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
		SpawnPosition = new Vector3 (0,0,0);
		StartCoroutine (spawnEnemy());
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
					Destroy(Instantiate (asteroid1, SpawnPosition, Quaternion.identity), 10);
				}
				else if (random < 6)
				{
					Destroy(Instantiate (asteroid2, SpawnPosition, Quaternion.identity), 10);
				}
				else
				{
					Destroy(Instantiate (asteroid3, SpawnPosition, Quaternion.identity), 10);
				}
			}
			// Spawn ship the other 10% of the time
			else {
				Debug.Log("Spawning Ship");
				Vector3 spawnPosition = new Vector3 (25.0f, Random.Range (-4.5f, 7.5f), -3.83f);
				Destroy(Instantiate (ship, spawnPosition, Quaternion.identity), 10);
			}

			yield return new WaitForSeconds(Random.Range(0.3f, 0.6f)*3f);
		}
	}
}
