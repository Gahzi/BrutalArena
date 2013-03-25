using UnityEngine;
using System.Collections;

public class MoveScript : AbilityScript {
	
	public MoveScript() {
		name = "Move";
		tooltipText = "Move a unit";
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
