using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pathmaker : MonoBehaviour {

	public GameObject sandPrefab_OCT, grassPrefab_OCT, mountainPrefab_OCT; //octagonal tiles
	public GameObject sandPrefab_CIRC, grassPrefab_CIRC, mountainPrefab_CIRC; //circular tiles
	GameObject sandTile, grassTile, mountainTile; //used to track which type of tile is being used
	public GameObject pathmakerPrefab;

	private int counter = 0; //tracks how many land tiles have been instantiated


	//===========================================//


	void Update () {

		// Only spawn up to [ maxTilesPerPathmaker ] tiles:
		if ( counter < Generator.pathmakerLimit ) {
			RandTurn( Generator.dontTurnProbability );
			RandSpawnPathmaker( 0.05f );
			RandSpawnTile();

			transform.Translate( Vector3.forward * 5f ); //move forward

			EditIslandBounds();
			
		//Self-destruct if enough tiles have been creeated:
		} else {
			Destroy( gameObject );
		}

		if ( Generator.globalTileCount >= Generator.globalTileMax ) {
			Destroy( gameObject );
		}
	}


	//===========================================//


	void RandTurn( float goStraightProb ) {
		float randNum = Random.Range( 0.00f, 1.00f );
		int randRot;

		if ( randNum <= goStraightProb ) { //if randNum <= goStraightProbability, then go straight
			randRot = 0;
		} else {
			randRot = Random.Range( 1, 7 );
		}

		transform.Rotate( Vector3.up * (float)randRot * 45f );
	}


	void RandSpawnPathmaker( float spawnProb ) {
		float randNum = Random.Range( 0.00f, 1.00f );

		if ( randNum <= spawnProb ) {
			SpawnObject( pathmakerPrefab, "PATHMAKERS", "Pathmaker", 0f );
		}
	}


	void RandSpawnTile() {
		// If circular tiles selected, set the respective prefabs:
		if ( Generator.circularTiles == true ) {
			sandTile = sandPrefab_CIRC;
			grassTile = grassPrefab_CIRC;
			mountainTile = mountainPrefab_CIRC;
		} else {
			sandTile = sandPrefab_OCT;
			grassTile = grassPrefab_OCT;
			mountainTile = mountainPrefab_OCT;
		}

		// Spawns random land tile:
		float tileType = Random.Range( 0.00f, 1.00f );
		if ( tileType <= 0.50f ) {
			SpawnObject( sandTile, "SAND TILES", "Sand Tile", 0f );
			counter++;
			Generator.globalTileCount++;
		} else if ( tileType <= 0.94f ) {
			SpawnObject( grassTile, "GRASS TILES", "Grass Tile", 0f );
			counter++;
			Generator.globalTileCount++;
		} else if ( tileType <= 0.95f ) {
			SpawnObject( mountainTile, "MOUNTAIN TILES", "Mountain Tile", Generator.mountainYVariance );
			counter++;
			Generator.globalTileCount++;
		} //5% chance the pathmaker won't spawn a land tile, allowing new islands
	}


	void SpawnObject( GameObject prefab, string parent, string name, float yVariance ) {
		//Instantiates new instance of prefab with random height:
		GameObject newInstance = Instantiate(
			prefab,
			transform.position + ( Vector3.up * Random.Range(-yVariance, yVariance)),
			Quaternion.identity
		);

		newInstance.transform.parent = GameObject.Find( parent ).transform;
		newInstance.name = name;
	}


	// Checks and sets island bounds:
	void EditIslandBounds() {
		if ( transform.position.x > Generator.xMax ) {
			Generator.xMax = transform.position.x;
		}
		if ( transform.position.x < Generator.xMin ) {
			Generator.xMin = transform.position.x;
		}
		if ( transform.position.z > Generator.zMax ) {
			Generator.zMax = transform.position.z;
		}
		if ( transform.position.z < Generator.zMin ) {
			Generator.zMin = transform.position.z;
		}
	}
	
}