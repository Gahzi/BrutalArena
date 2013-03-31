using UnityEngine;
using System.Collections;

public class TileScript : MonoBehaviour {
	
	private TileMapScript map;
	private CharacterScript tileInhabitant = null;
	public Vector2 tileCoordinate;
	
	// Use this for initialization
	void Start () {
		map = transform.root.gameObject.GetComponent<TileMapScript>();

		if(!map) {
			Debug.Log("Error: Trouble retrieving map object from tile");	
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	//Set a character to a specific tile
	public void SetTileInhabitant(CharacterScript character) {
		tileInhabitant = character;	
	}
	
	//Get any characters on this tile
	public CharacterScript GetTileInhabitant() {
		return tileInhabitant;	
	}
	
	public Vector2 GetTileCoordinate() {
		return tileCoordinate;	
	}
	
	void OnMouseEnter() {
		//tell game manager that this tile currently hovered over
		map.SetCurrentHoveredTileObject(this);
	}
	
	void OnMouseExit() {
		//tell game manager that this tile is no longer currently hovered over
		map.SetCurrentHoveredTileObject(null);
	}
}