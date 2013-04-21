using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BAConstants;

public class Favor {
	ConstantsScript.TileFavorEffect effect;
	ConstantsScript.TileFavorDirection direction;
	GameManagerScript gm;
	TileScript currentTile;
	
	public Favor(GameManagerScript manager,TileScript startingTile,ConstantsScript.TileFavorEffect newEffect, ConstantsScript.TileFavorDirection startingDirection) {
		gm = manager;
		currentTile = startingTile;
		currentTile.AddFavor(this);
		effect = newEffect;
		direction = startingDirection;
	}
	
	public ConstantsScript.TileFavorEffect GetFavorEffect() {
		return effect;
	}
	
	public ConstantsScript.TileFavorDirection GetDirection() {
		return direction;
	}
	
	public TileScript GetCurrentTile() {
		return currentTile;	
	}
	
	//Move to next tile with the current direction
	public void MoveToNextTile() {
		TileMapScript map =  currentTile.GetTileMap();
		Hashtable tiles = map.GetTiles();
		Vector2 tileCoordinate = currentTile.GetTileCoordinate();
		List<int> rowCounts = map.GetRowCounts();
		int currentRowCount = rowCounts[(int)tileCoordinate.y];
		
		switch(direction) {
			case ConstantsScript.TileFavorDirection.Top: {
				if((int)tileCoordinate.y-2 >= 0) {
					int nextRowCount = rowCounts[(int)tileCoordinate.y-2];
					
					if(currentRowCount < nextRowCount ) {
						tileCoordinate.x += 1;
					}
					else if(currentRowCount > nextRowCount) {
						tileCoordinate.x -= 1;
					}
					tileCoordinate.y -= 2;
				}
				break;
			}
			case ConstantsScript.TileFavorDirection.TopRight: {
				if((int)tileCoordinate.y-1 >= 0) {
					int nextRowCount = rowCounts[(int)tileCoordinate.y-1];
					if(currentRowCount < nextRowCount ) {
						tileCoordinate.x += 2;
					}
					else if(currentRowCount > nextRowCount) {
						tileCoordinate.x += 1;
					}
	
					tileCoordinate.y -= 1;
				}
				break;
			}
			case ConstantsScript.TileFavorDirection.BottomRight: {
				if((int)tileCoordinate.y+1 <= rowCounts.Count-1) {
					int nextRowCount = rowCounts[(int)tileCoordinate.y+1];
					if(currentRowCount < nextRowCount ) {
						tileCoordinate.x += 2;
					}
					else if(currentRowCount > nextRowCount) {
						tileCoordinate.x += 1;
					}
	
					tileCoordinate.y += 1;
				}
				break;
			}
			case ConstantsScript.TileFavorDirection.Bottom: {
				if((int)tileCoordinate.y+2 <= rowCounts.Count-1) {
					int nextRowCount = rowCounts[(int)tileCoordinate.y+2];
					if(currentRowCount < nextRowCount ) {
						tileCoordinate.x += 1;
					}
					else if(currentRowCount > nextRowCount) {
						tileCoordinate.x -= 1;
					}
	
					tileCoordinate.y += 2;
				}
				break;
			}
			case ConstantsScript.TileFavorDirection.BottomLeft: {
				if((int)tileCoordinate.y+1 <= rowCounts.Count-1 ) {
					int nextRowCount = rowCounts[(int)tileCoordinate.y+1];
					if(currentRowCount < nextRowCount ) {
						tileCoordinate.x -= 1;
					}
					else if(currentRowCount > nextRowCount) {
						tileCoordinate.x -= 2;
					}
	
					tileCoordinate.y += 1;
				}
				break;
			}
			case ConstantsScript.TileFavorDirection.TopLeft: {
				if((int)tileCoordinate.y-1 >= 0) {
					int nextRowCount = rowCounts[(int)tileCoordinate.y-1];
					if(currentRowCount < nextRowCount ) {
						tileCoordinate.x -= 1;
					}
					else if(currentRowCount > nextRowCount) {
						tileCoordinate.x -= 2;
					}
	
					tileCoordinate.y -= 1;
				}
				break;
			}
		}
		
		currentTile.RemoveFavor(this);

		if(!(currentTile.tileCoordinate.Equals(tileCoordinate)) && tiles[tileCoordinate] != null) {
			TileScript nextTile = (TileScript)tiles[tileCoordinate];
			nextTile.AddFavor(this);
			currentTile = nextTile;
		}
		else {
			currentTile = null;	
		}
	}
	
	public Color getFavorColor() {
		switch(effect) {
			case ConstantsScript.TileFavorEffect.IncreaseDamage: return TileConstants.increaseDamageColor;
			case ConstantsScript.TileFavorEffect.DecreaseAbilityCost: return TileConstants.decreaseAbilityCostColor;
			case ConstantsScript.TileFavorEffect.IncreaseHitPercentage: return TileConstants.increaseHitPercentageColor;
			case ConstantsScript.TileFavorEffect.DecreaseEnemyDamage: return TileConstants.decreaseEnemyDamageColor;
			case ConstantsScript.TileFavorEffect.IncreaseEnemyAbilityCost: return TileConstants.increaseEnemyAbilityCost;
			case ConstantsScript.TileFavorEffect.DecreaseEnemyHitPercentage: return TileConstants.decreaseEnemyHitPercentage;
		}
		Debug.Log("Failed to find proper Favor Color");
		return new Color(1.0f,1.0f,1.0f,1.0f);
	}
}