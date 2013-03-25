using UnityEngine;
using System.Collections;

public class FighterScript : PlayerScript {

	// Use this for initialization
	new void Start () {
		base.Start();
		
		//set our fighter's abilities
		abilityOne = new MoveScript();
		abilityTwo = new BasicAttackScript();
		abilityThree = new ComboScript();
		abilityFour = new StompScript();
		abilityFive = new DefendScript();
	}
}
