using UnityEngine;
using System.Collections;

public class StompScript : AbilityScript {
	
	public StompScript(CharacterScript attachedPlayer) : base(attachedPlayer){
		abilityName = "Stomp";
		tooltipText = "Smash the floor dealing n aoe damage";
		staminaCost = 2;
	}
	
	// Use this for initialization
	public override void Selected() {
		//player has clicked move but hasn't selected where they wish to move to yet.
	}
	
	public override void Execute(TileScript tile) {
		//player has selected a position to move to and we 
	}

}