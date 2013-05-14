using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BAConstants;

public class BasicAttackScript : AbilityScript {
	
	
	public BasicAttackScript(CharacterScript attachedPlayer) : base(attachedPlayer) {
		abilityName = "Basic Attack";
		tooltipText = "Attack a unit using your basic attack";
		staminaCost = 2;
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
			switch(player.characterType) {
				case CharacterConstants.CharacterType.player: {
					int distance = player.map.GetAStar().GetRangeBetweenTwoTiles(player.currentTile,tile);
					if(distance <= range) {
						int abilityCostModifier = player.currentTile.GetNumOfFavorEffectsInTile(ConstantsScript.TileFavorEffect.DecreaseAbilityCost);
						if(player.stamina >= staminaCost - abilityCostModifier) {
							int damageModifier = player.currentTile.GetNumOfFavorEffectsInTile(ConstantsScript.TileFavorEffect.IncreaseDamage);
							enemy.health -= (damage + damageModifier);
							player.stamina -= (staminaCost - abilityCostModifier);
							Debug.Log("Hitting Enemy for " + damage + " damage to " + enemy.health + " health");
							GameManagerScript gm = player.gm;
							AudioManagerScript am = gm.gameObject.GetComponent<AudioManagerScript>();
							am.PlayAudioClip(BAConstants.AudioConstants.AudioClipType.SwordHit1);							
							return true;
						}
					}
					break;
				}
				case CharacterConstants.CharacterType.enemy: {
					int distance = player.map.GetAStar().GetRangeBetweenTwoTiles(player.currentTile,tile);
					if(distance <= range) {
						int increaseAbilityCostModifier = player.currentTile.GetNumOfFavorEffectsInTile(ConstantsScript.TileFavorEffect.IncreaseEnemyAbilityCost);
						if(player.stamina >= staminaCost + increaseAbilityCostModifier) {
							int decreaseDamageModifier = player.currentTile.GetNumOfFavorEffectsInTile(ConstantsScript.TileFavorEffect.DecreaseEnemyDamage);
							enemy.health -= (damage - decreaseDamageModifier);
							player.stamina -= (staminaCost + increaseAbilityCostModifier);
							Debug.Log("Hitting Player for " + damage + " damage to " + enemy.health + " health");
							GameManagerScript gm = player.gm;
							AudioManagerScript am = gm.gameObject.GetComponent<AudioManagerScript>();
							am.PlayAudioClip(BAConstants.AudioConstants.AudioClipType.EnemyAttack);
							//JONATHAN: ENEMY HITS PLAYER HERE.
							return true;
						}
					}
					break;
				}
			}
			
			
		}
		return false;
	}
	
	//we wish to see if the player can execute their ability on the given tile coordinate
	public override bool ValidateMove(ref int expectedStamina, TileScript expectedTilePosition, TileScript targetTile) {
		int distance = 99;
		CharacterScript enemy = targetTile.GetTileInhabitant();
		
		switch(player.characterType) {
			case BAConstants.CharacterConstants.CharacterType.player: {
				if(enemy.characterType == CharacterConstants.CharacterType.enemy) {
					distance = player.map.GetAStar().GetRangeBetweenTwoTiles(expectedTilePosition,targetTile);
					if(distance <= range) {
						int abilityCostModifier = player.currentTile.GetNumOfFavorEffectsInTile(ConstantsScript.TileFavorEffect.DecreaseAbilityCost);
						if(player.stamina >= staminaCost - abilityCostModifier) {
							return true;
						}
					}
				}
				break;
			}
			case BAConstants.CharacterConstants.CharacterType.enemy: {
				distance = player.map.GetAStar().GetRangeBetweenTwoTiles(expectedTilePosition,targetTile);
				if(distance <= range) {
					if(enemy.characterType == CharacterConstants.CharacterType.player) {
						int increaseAbilityCostModifier = player.currentTile.GetNumOfFavorEffectsInTile(ConstantsScript.TileFavorEffect.IncreaseEnemyAbilityCost);
						if(player.stamina >= staminaCost + increaseAbilityCostModifier) {
							return true;
						}
					}
				}
				break;
			}
		}
		return false;
	}

}
