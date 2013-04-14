using UnityEngine;
using System.Collections;

public class RockThrowScript : AbilityScript {
	
	public RockThrowScript(CharacterScript attachedPlayer) : base(attachedPlayer) {
		abilityName = "Rock Throw";
		tooltipText = "Throw a rock at target character";
		staminaCost = 2;
		damage = 1;
		range = 1;
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
					Debug.Log("Throwing Rock at Player for " + damage + " damage to " + enemy.health + " health");
					return true;
				}
			}
		}
		return false;
	}
}