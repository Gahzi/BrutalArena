using UnityEngine;
using System.Collections;

public class BasicAttackScript : AbilityScript {
	
	public BasicAttackScript(CharacterScript attachedPlayer) : base(attachedPlayer) {
		abilityName = "Basic Attack";
		tooltipText = "Attack a unit using your basic attack";
		staminaCost = 2;
		damage = 2;
		range = 1.4f;
	}
	
	// Use this for initialization
	public override void Selected() {
		//player has clicked move but hasn't selected where they wish to move to yet.
		
	}
	
	public override void Execute(TileScript tile) {
		CharacterScript enemy = tile.GetTileInhabitant();
		
		//if enemy isn't null
		//if the tile we selected contains an enemy,
			//if we meet the range requirement
			//if we meet the stamina requirement
				//apply damage
				//reduce stamina
		if(enemy) { 
			if(enemy.characterType == CharacterScript.CharType.enemy) {
				//TODO: Need to make same distance fix as move ability
				float distance = Vector2.Distance(tile.tileCoordinate,player.currentTile.tileCoordinate);
				if(distance <= range) {
					if(player.stamina >= staminaCost) {
						enemy.health -= damage;
						player.stamina -= staminaCost;
						Debug.Log("Hitting Enemy for " + damage + " damage to " + enemy.health + " health");
					}
				}
			}
		}
		
		
		
	}

}
