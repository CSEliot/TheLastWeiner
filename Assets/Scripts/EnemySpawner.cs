using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject asteroid1;
	public GameObject asteroid2;
	public GameObject asteroid3;
	public GameObject ship;

	// Use this for initialization
	void Start ()
	{
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
				Vector3 spawnPosition = new Vector3 (Random.Range ((float)-20.0, (float)25.0), (float)10.5, (float)0.0);
				if (random < 3)
				{
					Instantiate (asteroid1, spawnPosition, Quaternion.identity);
				}
				else if (random < 6)
				{
					Instantiate (asteroid2, spawnPosition, Quaternion.identity);
				}
				else
				{
					Instantiate (asteroid3, spawnPosition, Quaternion.identity);
				}
			}
			// Spawn ship the other 10% of the time
			else {
				Vector3 spawnPosition = new Vector3 ((float)25.0, Random.Range ((float)-5.0, (float)7.5), (float)0.0);
				Instantiate (ship, spawnPosition, Quaternion.identity);
			}

			yield return new WaitForSeconds((float)Random.Range((float)0.3, (float)0.6));
		}
	}
}
