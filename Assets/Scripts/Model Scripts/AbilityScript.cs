using UnityEngine;
using System.Collections;

public class AbilityScript {
	
	public string abilityName;
	public string tooltipText;
	public int staminaCost;
	public int damage;
	public int range;
	public CharacterScript player;
	
	public AbilityScript(CharacterScript attachedPlayer) {
		abilityName = "No Name";
		tooltipText = "No Tooltip Text";
		staminaCost = 0;
		player = attachedPlayer;
	}
	
	public AbilityScript(string newName, string newToolTipText, int newStmCost) {
		abilityName = newName;
		tooltipText = newToolTipText;
		staminaCost = newStmCost;
	}
	
	// Use this for initialization
	public virtual void Selected() {
		Debug.Log("Need to override base selected function");
	}
	
	// Update is called once per frame
	public virtual bool Execute(TileScript tile) {
		Debug.Log("Need to override base execute function");
		return true;
	}
	
	// Update is called once per frame
	public virtual bool ValidateMove(ref int expectedStamina,TileScript tile) {
		Debug.Log("Need to override base validate move function");
		return true;
	}
}
