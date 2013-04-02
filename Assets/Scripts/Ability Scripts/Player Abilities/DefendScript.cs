using UnityEngine;
using System.Collections;

public class DefendScript : AbilityScript {
	
	public DefendScript(CharacterScript attachedPlayer) : base(attachedPlayer)  {
		abilityName = "Defend";
		tooltipText = "Prepare your unit to defend incoming damage";
		staminaCost = 2;
	}
	
	// Use this for initialization
	public override void Selected() {
		//player has clicked move but hasn't selected where they wish to move to yet.
		
	}
	
	public override bool Execute(TileScript tile) {
		//player has selected a position to move to and we 
		return true;
	}

}