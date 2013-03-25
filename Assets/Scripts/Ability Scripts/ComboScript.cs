using UnityEngine;
using System.Collections;

public class ComboScript : AbilityScript {
	
	public ComboScript() {
		name = "Combo";
		tooltipText = "Attack a unit using a combo";
		stmCost = 3;
	}
	
	// Use this for initialization
	public override void Selected() {
		//player has clicked move but hasn't selected where they wish to move to yet.
		
	}
	
	public override void Execute(Vector2 tileCoordinate) {
		//player has selected a position to move to and we 
	}

}