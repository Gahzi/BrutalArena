using UnityEngine;
using System.Collections;

public class PlayerScript : CharacterScript {
	
	public AbilityScript abilityOne;
	public AbilityScript abilityTwo;
	public AbilityScript abilityThree;
	public AbilityScript abilityFour;
	public AbilityScript abilityFive;
	
	// Use this for initialization
	new void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	new void Update () {

	}
		
	new public void StartTurn() {
		base.StartTurn();
		
	}
}
