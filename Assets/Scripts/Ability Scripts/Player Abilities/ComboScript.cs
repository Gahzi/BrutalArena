using UnityEngine;
using System.Collections;

public class ComboScript : AbilityScript {
	
	public ComboScript(CharacterScript attachedPlayer) : base(attachedPlayer) {
		abilityName = "Combo";
		tooltipText = "Attack a unit using a combo";
		staminaCost = 3;
	}
	
	// Use this for initialization
	public override void Selected() {
		//player has clicked move but hasn't selected where they wish to move to yet.
		
	}
	
	public override bool Execute(TileScript tile) {
		//player has selected a position to move to and we 
		return true;
	}
	
	public override bool ValidateMove(ref int expectedStamina, TileScript tile) {
		CharacterScript enemy = tile.GetTileInhabitant();
		if(enemy) {
			if(enemy.characterType != player.characterType && expectedStamina >= staminaCost) {
				return true;	
			}
		}
		return false;
	}

}