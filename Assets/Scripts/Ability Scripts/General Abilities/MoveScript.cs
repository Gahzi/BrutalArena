using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BAConstants;

public class MoveScript : AbilityScript {
	
	public MoveScript(CharacterScript attachedPlayer) : base(attachedPlayer) {
		abilityName = "Move";
		tooltipText = "Move to the selected tile";
		staminaCost = 2;
		range = 1;
	}
	
	
	
	// Use this for initialization
	public override void Selected() {
		//player has clicked move but hasn't selected where they wish to move to yet.
		
	}
	
	public override bool Execute(TileScript tile) {
		switch(player.characterType) {
			case CharacterConstants.CharacterType.player: {
				List<Vector2> path = player.map.GetAStar().GetPathBetweenTwoTiles(player.currentTile,tile);
				
				if(path.Count > 0) {
					//Reduce the count cost by since it includes the movers current position.
					int totalStaminaCost = staminaCost * (path.Count-1);
					int totalAbilityCostModifier = 0;
					
					//remove target location tile since we don't need to check
					//for it's ability cost modifiers
					path.RemoveAt(path.Count-1);
				
					foreach(Vector2 tileCoordinate in path) {
						TileScript currentTileInPath = (TileScript)player.map.GetTiles()[tileCoordinate];
						totalAbilityCostModifier += currentTileInPath.GetNumOfFavorEffectsInTile(ConstantsScript.TileFavorEffect.DecreaseAbilityCost);
					}
				
					if(player.stamina >= (totalStaminaCost - totalAbilityCostModifier)) {
						player.map.MoveCharacterToTileCoordinate(player,tile);
						player.stamina -= (totalStaminaCost - totalAbilityCostModifier);
						//JONATHAN: PLAYER MOVES HERE
						return true;
					}
				}
				break;
			}
			
			case CharacterConstants.CharacterType.enemy: {
				int distance = player.map.GetAStar().GetRangeBetweenTwoTiles(player.currentTile,tile);
				if( distance - 1 <= range) {
					int abilityCostModifier = player.currentTile.GetNumOfFavorEffectsInTile(ConstantsScript.TileFavorEffect.IncreaseEnemyAbilityCost);
					if(player.stamina >= (staminaCost + abilityCostModifier)) {
						player.map.MoveCharacterToTileCoordinate(player,tile);
						player.stamina -= (staminaCost + abilityCostModifier);
						//JONATHAN: ENEMY MOVES HERE
						return true;
					}
				}
				break;
			}
		}
		return false;
	}
	
	public override bool ValidateMove(ref int expectedStamina, TileScript expectedTilePosition, TileScript targetTile) {
		CharacterScript enemy = targetTile.GetTileInhabitant();
		if(enemy == null) {
			List<Vector2> pathToTile = player.map.GetAStar().GetPathBetweenTwoTiles(expectedTilePosition,targetTile);
			if(pathToTile.Count > 0) {
				//Reduce the count cost by since it includes the movers current position.
				int totalStaminaCost = staminaCost * (pathToTile.Count-1);
				int totalAbilityCostModifier = 0;
					
				//remove target location tile since we don't need to check
				//for it's ability cost modifiers
				pathToTile.RemoveAt(pathToTile.Count-1);
				
				foreach(Vector2 tileCoordinate in pathToTile) {
					TileScript currentTileInPath = (TileScript)player.map.GetTiles()[tileCoordinate];
					totalAbilityCostModifier += currentTileInPath.GetNumOfFavorEffectsInTile(ConstantsScript.TileFavorEffect.DecreaseAbilityCost);
				}
				
				if(player.stamina >= (totalStaminaCost - totalAbilityCostModifier)) {
					return true;
				}	
			}
		}
		return false;
	}
}
