using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BAConstants;

public class TileScript : MonoBehaviour {
	
	private TileMapScript map;
	private CharacterScript tileInhabitant = null;
	public Vector2 tileCoordinate;
	public bool isHighlighted = false;

	private List<Favor> favorList;
	
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
		
		if(favorList.Count == 0) {
			spriteScript.color = Color.white;
		}
		else if(favorList.Count == 1) {
			spriteScript.color = favorList[0].getFavorColor();	
		}
		else if(favorList.Count > 1) {
			Color finalColor = Color.white;
			foreach(Favor favor in favorList) {
				finalColor = Color.Lerp(finalColor,favor.getFavorColor(),0.5f);
			}
			spriteScript.color = finalColor;
		}
		//Change color depending on the type of favor effects on the tile
	}

	public int GetNumOfFavorEffectsInTile(ConstantsScript.TileFavorEffect effect) {
		IEnumerable<Favor> effectFavors = 
			from favor in favorList
			where favor.GetFavorEffect() == effect
			select favor;
		
		return effectFavors.Count();
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
