using UnityEngine;
using System.Collections;

public class MoveScript : AbilityScript {
	
	public MoveScript(CharacterScript attachedPlayer) : base(attachedPlayer) {
		abilityName = "Move";
		tooltipText = "Move to the selected tile";
		staminaCost = 2;
	}
	
	
	
	// Use this for initialization
	public override void Selected() {
		//player has clicked move but hasn't selected where they wish to move to yet.
		
	}
	
	public override bool Execute(TileScript tile) {
		if(tile.GetTileInhabitant() == null) {
			//TODO: Rewrite distance algorithm to be more simple with Math.Abs,
			//This should fix double move bug.
			float distance = Vector2.Distance(tile.tileCoordinate,player.currentTile.tileCoordinate);
			Debug.Log(distance.ToString());
			if(distance <= 1.5f) {
				if(player.stamina >= staminaCost) {
					player.map.MoveCharacterToTileCoordinate(player,tile);
					player.stamina -= staminaCost;
					return true;
				}
			}
		}
		return false;
	}
}
