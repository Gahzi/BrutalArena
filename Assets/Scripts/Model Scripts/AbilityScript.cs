using UnityEngine;
using System.Collections;

public class AbilityScript {
	
	public string name;
	public string tooltipText;
	public int stmCost;
	
	public AbilityScript() {
		name = "No Name";
		tooltipText = "No Tooltip Text";
		stmCost = 0;
	}
	
	public AbilityScript(string newName, string newToolTipText, int newStmCost) {
		name = newName;
		tooltipText = newToolTipText;
		stmCost = newStmCost;
	}
	
	// Use this for initialization
	public virtual void Selected() {
		Debug.Log("Need to override base selected function");
	}
	
	// Update is called once per frame
	public virtual void Execute(Vector2 tileCoordinate) {
		Debug.Log("Need to override base execute function");
		
	}
}
