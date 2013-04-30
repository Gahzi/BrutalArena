using UnityEngine;
using System.Collections;

public class PushScript : AbilityScript {
	
	public PushScript(CharacterScript attachedPlayer) : base(attachedPlayer) {
		abilityName = "Push";
		tooltipText = "Push target character";
		staminaCost = 4;
		damage = 0;
		range = 1;
	//	TODO: Use this -> int pushDistance = 1;
		//TODO: Fix range so that it represents the amount of tile coordinates away instead of float
	}
	
	public override void Selected() {
		//player has clicked move but hasn't selected where they wish to move to yet.
	}
	
	public override bool Execute(TileScript tile) {
		CharacterScript enemy = tile.GetTileInhabitant();
		
		if(enemy) { 
			int distance = player.map.GetAStar().GetRangeBetweenTwoTiles(player.currentTile,tile);
			if( distance <= range) {
				if(player.stamina >= staminaCost) {
					enemy.health -= damage;
					player.stamina -= staminaCost;
					//TODO: push enemy 1 hex away from player
					Debug.Log("Pushing player");
					return true;
				}
			}
		}
		return false;
	}
	
	//we wish to see if the player can execute their ability on the given tile coordinate
	public override bool ValidateMove(ref int expectedStamina, TileScript tile) {
		CharacterScript enemy = tile.GetTileInhabitant();
		if(enemy) {
			if(enemy.characterType != player.characterType && expectedStamina >= staminaCost) {
				expectedStamina -= staminaCost;
				return true;	
			}
		}
		return false;
	}
}
