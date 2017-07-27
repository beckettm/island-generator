using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Generator : MonoBehaviour {

	public Pathmaker pathmakerPrefab;

	/* UI ELEMENTS */
	public Slider maxTilesSlider;
	public Slider pathmakerLifespanSlider;
	public Slider mountainHeightVarianceSlider;
	public Slider peninsulaProbabilitySlider;
	public Toggle tileShapeToggle;


	/* CUSTOMIZABLE PARAMETERS */
	public static int globalTileMax;
	public static int pathmakerLimit;
	public static float mountainYVariance;
	public static float dontTurnProbability;
	public static bool circularTiles;


	/* INTERNAL PARAMETERS */
	public static int globalTileCount;

	public GameObject focalpoint;
	Vector3 focalpointPos = Vector3.zero;
	public static float zMax, zMin, xMax, xMin = 0f; //tracks island bounds (edited in Pathfinder.cs)


	//===========================================//


	void Start() {
		ResetParameters();
	}

	void OnDrawGizmos() {
		if ( focalpoint != null ) {
			Gizmos.color = Color.white;
			Gizmos.DrawLine( focalpointPos, focalpointPos + ( Vector3.up * 100f ));
		}
	}

	void Update() {
		UIParameterDisplay();

		// Centers focalpoint:
		focalpointPos.x = ( xMin + xMax ) / 2f;
		focalpointPos.z = ( zMin + zMax ) / 2f;

		focalpoint.transform.position = focalpointPos;
	}


	//===========================================//


	public void GenerateNewIsland() {
		DestroyIsland();

		zMax = 0f; zMin = 0f; xMax = 0f; xMin = 0f; //resets island bounds
		focalpointPos = Vector3.zero; //resets focalpoint
		globalTileCount = 0;

		Instantiate( pathmakerPrefab, transform.position, Quaternion.identity );
	}

	public void DestroyIsland() {
		GameObject[] allTiles = GameObject.FindGameObjectsWithTag( "Land Tile" );

		for ( int i = 0; i < allTiles.Length; i++ ) {
			Destroy( allTiles[ i ] );
		}
	}

	public void ResetParameters() {
		globalTileMax = 1000;
		pathmakerLimit = 50;
		mountainYVariance = 3f;
		dontTurnProbability = 0.13f;
		circularTiles = false;
	}

	public void UIParameterDisplay() {
		maxTilesSlider.value = (float)globalTileMax;
		pathmakerLifespanSlider.value = (float)pathmakerLimit;
		mountainHeightVarianceSlider.value = (float)mountainYVariance;
		peninsulaProbabilitySlider.value = (float)dontTurnProbability;
		tileShapeToggle.isOn = (bool)circularTiles;
	}


	/* UI CONTROLLER FUNCTIONS */

	public void Slider_MaxTiles( float newValue ) {
		globalTileMax = (int)newValue;

		// Find the "Handle Label" Text component:
		Transform handleLabel = maxTilesSlider.transform.Find( "Handle Slide Area" );
		handleLabel = handleLabel.transform.Find( "Handle" );
		handleLabel = handleLabel.transform.Find( "Handle Label" );

		// Display value under slider handle:
		Text valueText = handleLabel.GetComponent<Text>();
		valueText.text = maxTilesSlider.value.ToString();
	}

	public void Slider_PathmakerLifespan( float newValue ) {
		pathmakerLimit = (int)newValue;

		// Find the "Handle Label" Text component:
		Transform handleLabel = pathmakerLifespanSlider.transform.Find( "Handle Slide Area" );
		handleLabel = handleLabel.transform.Find( "Handle" );
		handleLabel = handleLabel.transform.Find( "Handle Label" );

		// Display value under slider handle:
		Text valueText = handleLabel.GetComponent<Text>();
		valueText.text = pathmakerLifespanSlider.value.ToString();
	}

	public void Slider_MountainHeightVariance( float newValue ) {
		mountainYVariance = newValue;

		// Find the "Handle Label" Text component:
		Transform handleLabel = mountainHeightVarianceSlider.transform.Find( "Handle Slide Area" );
		handleLabel = handleLabel.transform.Find( "Handle" );
		handleLabel = handleLabel.transform.Find( "Handle Label" );

		// Display value under slider handle:
		Text valueText = handleLabel.GetComponent<Text>();
		valueText.text = mountainHeightVarianceSlider.value.ToString( "0.##" );
	}

	public void Slider_PeninsulaProbability( float newValue ) {
		dontTurnProbability = newValue;

		// Find the "Handle Label" Text component:
		Transform handleLabel = peninsulaProbabilitySlider.transform.Find( "Handle Slide Area" );
		handleLabel = handleLabel.transform.Find( "Handle" );
		handleLabel = handleLabel.transform.Find( "Handle Label" );

		// Display value under slider handle:
		Text valueText = handleLabel.GetComponent<Text>();
		valueText.text = peninsulaProbabilitySlider.value.ToString( "0.##" );
	}

	public void Toggle_TileShape( bool newValue ) {
		circularTiles = newValue;
	}

}