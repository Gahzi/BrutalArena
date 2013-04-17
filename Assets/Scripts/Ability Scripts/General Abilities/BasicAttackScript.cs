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
						int abilityCostModifier = tile.GetNumOfFavorEffectsInTile(ConstantsScript.TileFavorEffect.DecreaseAbilityCost);
						if(player.stamina >= staminaCost - abilityCostModifier) {
							int damageModifier = tile.GetNumOfFavorEffectsInTile(ConstantsScript.TileFavorEffect.IncreaseDamage);
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
						int increaseAbilityCostModifier = tile.GetNumOfFavorEffectsInTile(ConstantsScript.TileFavorEffect.IncreaseEnemyAbilityCost);
						if(player.stamina >= staminaCost + increaseAbilityCostModifier) {
							int decreaseDamageModifier = tile.GetNumOfFavorEffectsInTile(ConstantsScript.TileFavorEffect.DecreaseEnemyDamage);
							enemy.health -= (damage - decreaseDamageModifier);
							player.stamina -= (staminaCost + increaseAbilityCostModifier);
							Debug.Log("Hitting Enemy for " + damage + " damage to " + enemy.health + " health");
							GameManagerScript gm = player.gm;
							AudioManagerScript am = gm.gameObject.GetComponent<AudioManagerScript>();
							am.PlayAudioClip(BAConstants.AudioConstants.AudioClipType.SwordHit1);
							return true;
						}
					}
					break;
				}
			}
			
			
		}
		return false;
		
		
		
	}

}
