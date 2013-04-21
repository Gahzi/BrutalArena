using UnityEngine;
using System.Collections;
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
		if(tile.GetTileInhabitant() == null) {
			int distance = player.map.GetAStar().GetRangeBetweenTwoTiles(player.currentTile,tile);
			if( distance <= range) {
				int abilityCostModifier = player.currentTile.GetNumOfFavorEffectsInTile(ConstantsScript.TileFavorEffect.DecreaseAbilityCost);
				if(player.stamina >= staminaCost) {
					player.map.MoveCharacterToTileCoordinate(player,tile);
					player.stamina -= (staminaCost - abilityCostModifier);
					return true;
				}
			}
		}
		return false;
	}
}
