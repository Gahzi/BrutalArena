using UnityEngine;
using System.Collections;
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
			int distance = player.map.GetAStar().GetRangeBetweenTwoTiles(player.currentTile,tile);
			if(distance <= range) {
				if(player.stamina >= staminaCost) {
					enemy.health -= damage;
					player.stamina -= staminaCost;
					Debug.Log("Hitting Enemy for " + damage + " damage to " + enemy.health + " health");
					GameManagerScript gm = player.gm;
					AudioManagerScript am = gm.gameObject.GetComponent<AudioManagerScript>();
					am.PlayAudioClip(BAConstants.AudioConstants.AudioClipType.SwordHit1);
					return true;
				}
			}
		}
		return false;
		
		
		
	}

}
