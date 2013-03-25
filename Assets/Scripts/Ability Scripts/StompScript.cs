using UnityEngine;
using System.Collections;

public class StompScript : AbilityScript {
	
	public StompScript() {
		name = "Stomp";
		tooltipText = "Smash the floor dealing n aoe damage";
		stmCost = 2;
	}
	
	// Use this for initialization
	public override void Selected() {
		//player has clicked move but hasn't selected where they wish to move to yet.
		
	}
	
	public override void Execute(Vector2 tileCoordinate) {
		//player has selected a position to move to and we 
	}

}