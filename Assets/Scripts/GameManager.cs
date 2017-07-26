using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	void Update() {
		// Press 'R' to reload the current scene and generate a new island:
		if ( Input.GetKeyDown( KeyCode.R )) {
			SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex );
			Pathmaker.globalTileCount = 0;
		}
	}
}
