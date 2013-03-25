using UnityEngine;
using System.Collections;

public class TileMapScript : MonoBehaviour {
	
	Hashtable tiles = new Hashtable();
	
	void Awake() {
		string tileKey = "";
		string rowSymbol = "";
		string columnSymbol = "";
		
		foreach(Transform row in transform) {
			rowSymbol = row.gameObject.name;
			foreach(Transform tile in row) {	
				columnSymbol = tile.gameObject.name;
				tileKey = rowSymbol + " " + columnSymbol;
				tiles.Add(tileKey,tile);
			}
			
		}
	}
	
	public Vector3 GetWorldPositionFromCoordinate(float x, float y) {
		Vector3 newPosition = new Vector3(0,0,0);
		string targetRowCol = "Row " + y + " Column " + x;
		
		if(tiles.ContainsKey(targetRowCol)) {
			newPosition = ((Transform)(tiles[targetRowCol])).position;
		}
		else {
			Debug.Log("Couldn't find world position from x and y coordinates given");
			newPosition = new Vector3(0,0,0);
		}
		return newPosition;
	}
	
	public GameObject GetTileFromCoordinate(float x, float y) {
		string targetRowCol = "Row " + x + " Column " + y;
		GameObject tile = null;
		
		if(tiles.ContainsKey(targetRowCol)) {
			tile = ((Transform)tiles[targetRowCol]).gameObject;
		}
		else {
			Debug.Log("Couldn't find a proper tile from x and y coordinates given");
		}
		
		return tile;
	}
	
	public void GetTileFromWorldPosition(Vector3 worldPos) {
		/*
		Ray newRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit newHit;
		GameObject tile;
		
		if(Physics.Raycast(newRay,out newHit,200)) {
			GameObject hitObject = newHit.collider.gameObject;	
		}
		
		
		Debug.DrawLine (Camera.main.transform.position, newHit.point, Color.cyan,10.0f);
		/*
		
		/*
		GameObject tile = GetTileFromCoordinate(0,1);
		Bounds tileBound = tile.renderer.bounds;
		
		Debug.Log("X: " + worldPos.x.ToString() + " Y: " + worldPos.y.ToString());
		
		return null;
		*/
		//I have the world position i'm checking
		//I have the world positions of all of the tiles
		//If the world position given is between a certain radius of any tile's world position
		//Return that tile.
	}
}
