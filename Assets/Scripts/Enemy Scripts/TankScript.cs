using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class TankScript : CharacterScript {
	
	private enum TankState {
		attacking,
		pushing
	}
	
	private TankState state;

	// Use this for initialization
	public override void Start () {
		base.Start();
		state = TankState.attacking;
		characterType = CharType.enemy;
		
		//set our tank's abilities
		abilityOne = new MoveScript(this);
		abilityTwo = new BasicAttackScript(this);
		abilityThree = new PushScript(this);
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update();
		
		//If we havn't ended our turn yet
		if(!HasEndedTurn()) {
			//check my own health
			switch(state) {
				case TankState.attacking : {
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
				
				case TankState.pushing: {
					//TODO: if starting a turn next to player
					//push
					//otherwise attack
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
		state = TankState.pushing;
		//TODO: if tank is next to enemy
		//push.
		//otherwise, move and attack
		/*
		if(rangeToPlayer <= abilityTwo.range) {
			state = TankState.pushing;
			}		
		}
		else {
			state = TankState.attacking;	
		}
		*/
	}
	
	public override void EndTurn() {
		base.EndTurn();
	}
}