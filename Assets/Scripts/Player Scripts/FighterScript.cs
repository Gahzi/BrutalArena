using UnityEngine;
using System.Collections;

public class FighterScript : CharacterScript {

	// Use this for initialization
	public override void Start () {
		base.Start();
		
		//set the character type
		characterType = CharType.player;
		
		//set our fighter's abilities
		abilityOne = new MoveScript(this);
		abilityTwo = new BasicAttackScript(this);
		//abilityThree = new ComboScript(this);
		//abilityFour = new StompScript(this);
		//abilityFive = new DefendScript(this);
	}
	
	public override void Update () {
		base.Update();	
		
		if(!HasEndedTurn()) {
			
		}
	}
}
