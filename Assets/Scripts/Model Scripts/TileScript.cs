using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BAConstants;

public class TileScript : MonoBehaviour {
	
	private TileMapScript map;
	private CharacterScript tileInhabitant = null;
	public Vector2 tileCoordinate;

	private List<Favor> favorList = new List<Favor>();
	
	// Use this for initialization
	void Start () {
		map = transform.root.gameObject.GetComponent<TileMapScript>();

		if(!map) {
			Debug.Log("Error: Trouble retrieving map object from tile");	
		}

		favorList = new List<Favor>();
	}
	
	// Update is called once per frame
	void Update () {
		tk2dSprite spriteScript = this.gameObject.GetComponent<tk2dSprite>();
		if(favorList.Count == 1) {
			spriteScript.color = Color.red;
		}
		else if(favorList.Count > 1) {
			spriteScript.color = Color.green;
		}
		else {
			spriteScript.color = Color.white;
		}
		//Change color depending on the type of favor effects on the tile
	}

	public List<Favor> GetFavorList() {
		return favorList;
	}

	public void AddFavor(Favor newFavor) {
		favorList.Add(newFavor);
	}
	
	public void RemoveFavor(Favor favor) {
		favorList.Remove(favor);
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

	public TileMapScript GetTileMap() {
		return map;
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
