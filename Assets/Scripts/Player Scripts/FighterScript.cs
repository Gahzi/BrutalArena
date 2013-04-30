using UnityEngine;
using System.Collections;
using BAConstants;

public class FighterScript : CharacterScript {

	// Use this for initialization
	public override void Start () {
		base.Start();
		
		//set the character type
		characterType = CharacterConstants.CharacterType.player;
		
		//set our fighter's abilities
		abilityOne = new MoveScript(this);
		abilityTwo = new BasicAttackScript(this);
		//abilityThree = new ComboScript(this);
		//abilityFour = new CircleAttack(this);
		//abilityFive = new StompScript(this);
	}
	
	public override void Update () {
		base.Update();	
		
	}
}
