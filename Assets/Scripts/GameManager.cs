using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("r")){
			// reload the level.
			Application.LoadLevel(Application.loadedLevelName);
		}
	}
}
