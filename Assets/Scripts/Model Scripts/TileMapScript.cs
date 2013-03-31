using UnityEngine;
using System.Collections;

public class TileMapScript : MonoBehaviour {
	
	private Hashtable tiles = new Hashtable();
	//TODO: Perhaps store tileScript instead of raw transforms?
	private TileScript currentHoveredObject;
	
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
	
	public TileScript GetCurrentHoveredTileObject() {
		return currentHoveredObject;
	}
	
	public void SetCurrentHoveredTileObject(TileScript tile) {
		currentHoveredObject = tile;	
	}
	
	
	public Vector3 GetWorldPositionFromTileCoordinate(float x, float y) {
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
		//TODO: Need to figure this out.
		//I have the world position i'm checking
		//I have the world positions of all of the tiles
		//If the world position given is between a certain radius of any tile's world position
		//Return that tile.
	}
	
	//Helper function to move objects from tile to tile
	public void MoveCharacterToTileCoordinate(CharacterScript character, TileScript newTile) {
		character.currentTile.SetTileInhabitant(null);
		character.currentTile = newTile;
		newTile.SetTileInhabitant(character);
		Vector3 newPosition = GetWorldPositionFromTileCoordinate(newTile.tileCoordinate.x,newTile.tileCoordinate.y);
		newPosition.z = character.gameObject.transform.position.z;
		character.gameObject.transform.position = newPosition;
	}
}
