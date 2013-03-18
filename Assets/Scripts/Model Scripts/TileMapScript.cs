using UnityEngine;
using System.Collections;

public class TileMapScript : MonoBehaviour {
	
	ArrayList tiles = new ArrayList();
	
	void Awake() {
		foreach(Transform row in transform) {
		  tiles.Add(row);
		}
	}

	// Use this for initialization
	void Start() {
	}
	
	public Vector3 GetWorldPositionFromCoordinate(float x, float y) {
		Vector3 newPosition = new Vector3(0,0,0);
		bool isTileFound = false;
		string targetRow = "Row " + y;
		string targetCol = "Column " + x;
		
		foreach(Transform row in tiles) {
		  	if(row.gameObject.name.Equals(targetRow)) {
				foreach(Transform tile in row) {
					if(tile.gameObject.name.Equals(targetCol)) {
						newPosition = tile.transform.position;	
						isTileFound = true;
						break;
					}
				}
			}
		}
		
		if(!isTileFound) {
			Debug.Log("Couldn't find world position from x and y coordinates given");
			newPosition = new Vector3(0,0,0);
		}
		return newPosition;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
