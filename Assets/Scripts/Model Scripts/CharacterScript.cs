using UnityEngine;
using System.Collections;

public class CharacterScript : MonoBehaviour {

	public int health;
	public string characterName;
	public int stamina;
	public TileScript currentTile;
	public tk2dSprite sprite;
	public TileMapScript map;
	public GameManagerScript gm;
	private bool isCurrentPlayer; //is it the character's turn?
	
	//Character Types
	public enum CharType {
		player,
		enemy,
		npc
	}
	
	public CharType characterType;
	
	public AbilityScript abilityOne;
	public AbilityScript abilityTwo;
	public AbilityScript abilityThree;
	public AbilityScript abilityFour;
	public AbilityScript abilityFive;
	
	// Use this for initialization
	public void Start () {
		characterType = CharType.npc;
		isCurrentPlayer = false;
    	sprite = GetComponent<tk2dSprite>();
		if(map == null) Debug.Log("Forgot to drag tk2dTileMap into inspector");
		gm = GameObject.Find(ConstantsScript.gameManagerObjectName).GetComponent<GameManagerScript>();
		if(gm == null) Debug.Log("Could not find Game Manager in Scene");
		
		if(currentTile == null) Debug.Log("Could not find tile object associated with");
		else {
			map.MoveCharacterToTileCoordinate(this,currentTile);
		}		
	}
	
	public void Update() {
		
		if(isCurrentPlayer) {
			
		}
		
		//check for death
		if(health <= 0) {
			Debug.Log(this.gameObject.name + " has died");
			//run death animation
			//delete yourself

		}
	}
	
	public void StartTurn() {
		isCurrentPlayer = true;
	}
	
	public void EndTurn() {
		isCurrentPlayer = false;
		gm.EndTurn();
	}
	
	
}