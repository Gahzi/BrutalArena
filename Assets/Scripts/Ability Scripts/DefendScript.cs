using UnityEngine;
using System.Collections;

public class DefendScript : AbilityScript {
	
	public DefendScript() {
		name = "Defend";
		tooltipText = "Prepare your unit to defend incoming damage";
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