using UnityEngine;
using System.Collections;
using BAConstants;

public class CharacterScript : MonoBehaviour {

	public int health;
	public int healthMax;
	public string characterName;
	public int stamina;
	public int staminaMax;
	public TileScript currentTile;
	public tk2dSprite sprite;
	public TileMapScript map;
	public GameManagerScript gm;
	
	private bool hasEndedTurn;
	
	public CharacterConstants.CharacterType characterType;

	public int favorAwarded;
	
	public AbilityScript abilityOne;
	public AbilityScript abilityTwo;
	public AbilityScript abilityThree;
	public AbilityScript abilityFour;
	public AbilityScript abilityFive;

	// Use this for initialization
	public virtual void Start () {
		characterType = CharacterConstants.CharacterType.npc;
		hasEndedTurn = true;
    	sprite = GetComponent<tk2dSprite>();
		if(map == null) Debug.Log("Forgot to drag tk2dTileMap into inspector");
		gm = GameObject.Find(ConstantsScript.gameManagerObjectName).GetComponent<GameManagerScript>();
		if(gm == null) Debug.Log("Could not find Game Manager in Scene");
		
		if(currentTile == null) Debug.Log("Could not find tile object associated with");
		else {
			map.MoveCharacterToTileCoordinate(this,currentTile);
		}
		
	}
	
	public virtual void Update() {
		//check for death
		if(health <= 0) {
			Debug.Log(this.gameObject.name + " has died");
			gm.KillCharacter(this);
			this.gameObject.SetActive(false);
			Destroy(this.gameObject);
			
			//run death animation
			//delete yourself

		}
	}
	
	public bool HasEndedTurn() {
		return hasEndedTurn;	
	}
	
	public virtual void StartTurn() {
		hasEndedTurn = false;
	}
	
	public virtual void EndTurn() {
		hasEndedTurn = true;
		gm.EndTurn();
	}
	
	
}