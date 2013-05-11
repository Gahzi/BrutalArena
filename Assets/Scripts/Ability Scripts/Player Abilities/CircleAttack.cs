using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BAConstants;

public class CircleAttack : AbilityScript {
	
	
	public CircleAttack(CharacterScript attachedPlayer) : base(attachedPlayer) {
		abilityName = "Circle Attack";
		tooltipText = "Attack all enemies around you";
		staminaCost = 4;
		damage = 2;
		range = 1;
	}
	
	
	
	// Use this for initialization
	public override void Selected() {
		//player has clicked move but hasn't selected where they wish to move to yet.
		
	}
	
	public override bool Execute(TileScript tile) {
		CharacterScript enemy = tile.GetTileInhabitant();
		
		if(enemy) { 
			int distance = player.map.GetAStar().GetRangeBetweenTwoTiles(player.currentTile,tile);
			if(distance <= range) {

				//int abilityCostModifier = tile.GetNumOfFavorEffectsInTile(ConstantsScript.TileFavorEffect.ReduceAbilityCost);
				//TODO:add these back in.
				//- abilityCostModifier;
				if(player.stamina >= staminaCost ) {

					int damageModifier = tile.GetNumOfFavorEffectsInTile(ConstantsScript.TileFavorEffect.IncreaseDamage);
					enemy.health -= damage + damageModifier; 
					//+ damageModifier;
					//- abilityCostModifier
					player.stamina -= (staminaCost);
					Debug.Log("Hit Enemies for " + damage + " damage to " + enemy.health + " health");
					GameManagerScript gm = player.gm;
					AudioManagerScript am = gm.gameObject.GetComponent<AudioManagerScript>();
					am.PlayAudioClip(BAConstants.AudioConstants.AudioClipType.SwordHit1);
					return true;
				}
			}
		}
		return false;
	}
	
	public override bool ValidateMove(ref int expectedStamina, TileScript expectedTilePosition, TileScript targetTile) {
		CharacterScript enemy = targetTile.GetTileInhabitant();
		if(enemy) {
			if(enemy.characterType != player.characterType && expectedStamina >= staminaCost) {
				return true;	
			}
		}
		return false;
	}

}
