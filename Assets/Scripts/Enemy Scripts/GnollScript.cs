using UnityEngine;
using System.Collections.Generic;

public class GnollScript : CharacterScript {
	
	private enum GnollState {
		attacking,
		runningthrowing
	}
	
	private GnollState state;

	// Use this for initialization
	public override void Start () {
		base.Start();
		state = GnollState.attacking;
		characterType = CharType.enemy;
		
		//set our gnoll's abilities
		abilityOne = new MoveScript(this);
		abilityTwo = new BasicAttackScript(this);
		abilityThree = new RockThrowScript(this);
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update();
		
		//If we havn't ended our turn yet
		if(!HasEndedTurn()) {
			//check my own health
			switch(state) {
				case GnollState.attacking : {
					//TODO: Create a playerList in GameManager and use that instead of iterating each frame.
					List<GameObject> characterList = gm.GetCharacterList();
					CharacterScript targetPlayer;
					foreach(GameObject characterObject in characterList) {
						CharacterScript character = characterObject.GetComponent<CharacterScript>();
						if(character.characterType == CharType.player) {
							targetPlayer = character;
						}
					}
				
					
				
					
					//get closest player tilecoordinates
					//if in range to basic attack, attack
					//otherwise move towards him
			
					break;
				}
				
				case GnollState.runningthrowing: {
					//if max throwing range away from player
					//throw,
					//otherwise move one tile coordinate away from player.
					break;
				}
			}
		
		}
		
		
		//check if we have enough stamina to move/attack again
		//otherwise end the turn
		
		//wait n number of seconds before next move
	}
	
	public override void StartTurn() {
		base.StartTurn();
		
		//if i'm under 25%, consider running away + throwing stone
		//otherwise, move forward and try to attack.
		if(health < 6) {
			if(Random.Range(0,100) < 25) {
				state = GnollState.runningthrowing;
			}		
		}
		else {
			state = GnollState.attacking;	
		}
	}
	
	public override void EndTurn() {
		base.EndTurn();
	}
}
