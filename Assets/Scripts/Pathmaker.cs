using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pathmaker : MonoBehaviour {

	public static int globalTileMax = 500;
	public static int globalTileCount;

	private int counter = 0; //tracks how many land tiles have been instantiated
	public int counterMax = 50;

	GameObject landPrefab; //used to store the type of land tile being placed
	public GameObject landPrefab_sand;
	public GameObject landPrefab_grass;
	public GameObject landPrefab_mountain;
	public GameObject pathmakerPrefab;


	/* 
	 *	NOTE: 5f IS USED FREQUENTLY, AS THE "GRID" SPACES ARE 5 UNITS APART.
	 */

	void Update () {

		// While less than 50 tiles have been created, spawn tiles:
		if ( counter < counterMax ) {

			// Turns randomly:
			int randRot = Random.Range( 0, 9 );
			transform.Rotate( Vector3.up * (float)randRot * 45f );

			// 5% chance to spawn new pathmaker:
			float randNum = Random.Range( 0.00f, 1.00f );
			if ( randNum <= 0.05f ) {
				SpawnObject( pathmakerPrefab, "PATHMAKERS", "Pathmaker" );
			}

			// Spawns random land tile:
			float tileType = Random.Range( 0.00f, 1.00f );
			if ( tileType <= 0.50f ) {
				SpawnObject( landPrefab_sand, "SAND TILES", "Sand Tile" );
				counter++;
				globalTileCount++;
			} else if ( tileType <= 0.92f ) {
				SpawnObject( landPrefab_grass, "GRASS TILES", "Grass Tile" );
				counter++;
				globalTileCount++;
			} else if ( tileType <= 0.95f ) {
				SpawnObject( landPrefab_mountain, "MOUNTAIN TILES", "Mountain Tile" );
				counter++;
				globalTileCount++;
			} //5% chance the pathmaker won't spawn a land tile, allowing new islands

			// Move:
			transform.Translate( Vector3.forward * 5f );
			
		//Self-destruct if enough tiles have been creeated:
		} else {
			Destroy( gameObject );
		}

		if ( globalTileCount >= globalTileMax ) {
			Destroy( gameObject );
		}
	}

	void SpawnObject( GameObject prefab, string parent, string name ) {
		GameObject newInstance = Instantiate( prefab, transform.position, Quaternion.identity );
		newInstance.transform.parent = GameObject.Find( parent ).transform;
		newInstance.name = name;
	}
}