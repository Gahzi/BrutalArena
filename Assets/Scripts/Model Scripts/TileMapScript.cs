
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BAConstants;

public class TileMapScript : MonoBehaviour {
	
	//TODO: Use Dictionary instead.
	private Hashtable tiles = new Hashtable();
	private List<int> rowCount = new List<int>();
	
	public	GameManagerScript gm;
	private AStarScript aStar;
	private TileScript currentHoveredObject;
	
	void Awake() {
		int column = 0;
		int lastRowInColumn = 0;
		foreach(Transform rowTransform in transform) {
			foreach(Transform tileTransform in rowTransform) {	
				TileScript tile = tileTransform.gameObject.GetComponent<TileScript>();
				tiles.Add(tile.tileCoordinate,tile);

				column = (int)tile.tileCoordinate.y;
				if(lastRowInColumn < (int)tile.tileCoordinate.x) {
					lastRowInColumn = (int)tile.tileCoordinate.x;
				}
			}
			lastRowInColumn += 1;
			rowCount.Insert(column,lastRowInColumn);
			lastRowInColumn = 0;
		}
		
		aStar = new AStarScript(this);
		gm = GameObject.Find(ConstantsScript.gameManagerObjectName).GetComponent<GameManagerScript>();
	}
	
	public Hashtable GetTiles() {
		return tiles;	
	}

	public List<int> GetRowCountList() {
		return rowCount;
	}
	
	public AStarScript GetAStar() {
		return aStar;	
	}
	
	public TileScript GetCurrentHoveredTileObject() {
		return currentHoveredObject;
	}
	
	public void SetCurrentHoveredTileObject(TileScript tile) {
		currentHoveredObject = tile;	
		gm.SetAttachedTileInGUI(currentHoveredObject);
	}
	
	public Vector3 GetWorldPositionFromTileCoordinate(float x, float y) {
		Vector3 newPosition = new Vector3(0,0,0);
		Vector2 TargetTileKey = new Vector2(x,y);
		
		if(tiles.ContainsKey(TargetTileKey)) {
			newPosition = ((TileScript)(tiles[TargetTileKey])).gameObject.transform.position;
		}
		else {
			Debug.Log("Couldn't find world position from x and y coordinates given");
		}
		return newPosition;
	}
	
	public GameObject GetTileObjectFromTileCoordinate(float x, float y) {
		Vector2 TargetTileKey = new Vector2(x,y);
		GameObject tile = null;
		
		if(tiles.ContainsKey(TargetTileKey)) {
			tile = ((TileScript)tiles[TargetTileKey]).gameObject;
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
