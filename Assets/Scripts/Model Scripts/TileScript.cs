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
	public bool isAttackable = false;

	private List<Favor> favorList;
	private List<tk2dSprite> favorArrowList;
	
	// Use this for initialization
	void Start () {
		map = transform.root.gameObject.GetComponent<TileMapScript>();

		if(!map) {
			Debug.Log("Error: Trouble retrieving map object from tile");	
		}

		favorList = new List<Favor>();
		favorArrowList = new List<tk2dSprite>();
	}
	
	// Update is called once per frame
	void Update () {
		tk2dSprite spriteScript = this.gameObject.GetComponent<tk2dSprite>();
		
		if(isAttackable) {
			spriteScript.color = TileConstants.highlightedAttackColor;	
		}
		else if(isHighlighted) {
			spriteScript.color = TileConstants.highlightedMovementColor;	
		}
		else {
			//Change color depending on the type of favor effects on the tile
			if(favorList.Count == 0) {
				spriteScript.color = Color.white;
				
				
				//Remove all arrows
			}
			else if(favorList.Count == 1) {
				spriteScript.color = favorList[0].getFavorColor();
				//Add the arrow for favorList 1	
			}
			else if(favorList.Count > 1) {
				Color finalColor = Color.white;
				foreach(Favor favor in favorList) {
					finalColor = Color.Lerp(finalColor,favor.getFavorColor(),0.5f);
					//Add the arrow for favorList n
				}
				
				spriteScript.color = finalColor;
			}		
		}
	}

	public int GetNumOfFavorEffectsInTile(ConstantsScript.TileFavorEffect effect) {
		IEnumerable<Favor> effectFavors = 
			from favor in favorList
			where favor.GetTileFavorEffect() == effect
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
		
		foreach(tk2dSprite sprite in favorArrowList) {
			
		}
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
