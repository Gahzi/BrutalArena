using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ArcherScript : CharacterScript {
	
	private enum ArcherState {
		attacking,
		running
	}
	
	private ArcherState state;

	// Use this for initialization
	public override void Start () {
		base.Start();
		state = ArcherState.attacking;
		characterType = CharType.enemy;
		
		//set our gnoll's abilities
		abilityOne = new MoveScript(this);
		abilityTwo = new RockThrowScript(this);
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update();
		
		//If we havn't ended our turn yet
		if(!HasEndedTurn()) {
			//check my own health
			switch(state) {
				case ArcherState.attacking : {
					//TODO: Create a playerList in GameManager and use that instead of iterating each frame.
					List<GameObject> characterList = gm.GetCharacterList();
					CharacterScript targetPlayer = null;
					foreach(GameObject characterObject in characterList) {
						CharacterScript character = characterObject.GetComponent<CharacterScript>();
						if(character.characterType == CharType.player) {
							targetPlayer = character;
						}
					}
				
					if(targetPlayer) {
						int rangeToPlayer = map.GetAStar().GetRangeBetweenTwoTiles(currentTile,targetPlayer.currentTile);
						if(rangeToPlayer <= abilityTwo.range) {
							abilityTwo.Execute(targetPlayer.currentTile);
						}
						else {
							List<Vector2> movePath = map.GetAStar().GetPathBetweenTwoTiles(currentTile,targetPlayer.currentTile);
							TileScript targetTile = (TileScript)map.GetTiles()[movePath[1]];
						//TODO: Remove this
							if(!abilityOne.Execute(targetTile)) {
								EndTurn ();
								break;
							}
						}
					}
					else {
						Debug.Log("Couldn't find any player to target");
					}
				
					//check if we should end the turn
					if(stamina < abilityTwo.staminaCost) {
						EndTurn();
					}
					break;
				}
				
				case ArcherState.running: {
					//TODO: if less than max range
					//move to max range
					//attack
					break;
				}
			}
		}
		
		
		//TODO: check if we have enough stamina to move/attack again
		//otherwise end the turn
		
		//wait n number of seconds before next move
	}
	
	public override void StartTurn() {
		base.StartTurn();
		state = ArcherState.attacking;
		//TODO: if i'm under 25%, consider running away 
		//otherwise, move to max range and try to attack.
		/*
		if(health < 6) {
			if(Random.Range(0,100) < 25) {
				state = ArcherState.running;
			}		
		}
		else {
			state = ArcherState.attacking;	
		}
		*/
	}
	
	public override void EndTurn() {
		base.EndTurn();
	}
}