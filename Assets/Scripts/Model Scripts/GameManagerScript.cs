using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using BAConstants;

public class GameManagerScript : MonoBehaviour {
	
	List<GameObject> characterList = new List<GameObject>();
	List<GameObject> turnOrderList = new List<GameObject>();
	List<Favor> favorList = new List<Favor>();

	TileMapScript map;
	CreateGUIScript gui;
	
	tk2dSpriteCollectionData scData;
	
	public int favor = 0;
	public int fWaveRequirement = 0;
	public int nextFavorWaveCost = 0;
	public float healthGainedPerFavor = 0.2f;

	public int enemySpawnCount = 1;
	public int enemySpawnRate = 1;

	CharacterScript currentCharacter;
	
	// Use this for initialization
	void Start () {
		map =  GameObject.Find(ConstantsScript.tileMapObjectName).GetComponent<TileMapScript>();
		gui =  GameObject.Find(ConstantsScript.guiManagerObjectName).GetComponent<CreateGUIScript>();
		
		//TODO: Change this to check for character class.
		GameObject[] chars = GameObject.FindGameObjectsWithTag(ConstantsScript.characterTag);
		
		foreach(GameObject character in chars) {
			characterList.Add(character);
		}
		
		favor = 0;

		if(fWaveRequirement == 0) {
			Debug.Log("Forgot to change Favor Wave Requirement to anything btu 0");
		}
		else {
			nextFavorWaveCost = fWaveRequirement;
		}
		
		turnOrderList.AddRange(characterList);
		SortTurnOrderListByStamina();
	}
	
	// Update is called once per frame
	void Update () {
		
		//handle mouse input
		HandleMouseInput();
		
		//if the current turn is finish, start the next turn
		if(turnOrderList.Count == 0) {
			List<Favor> favorsToDelete = new List<Favor>();
			foreach(Favor favor in favorList) {
				if(favor.GetCurrentTile() == null) {
					favorsToDelete.Add(favor);
				}
				else {
					favor.MoveToNextTile();	
				}
			}
			
			foreach(Favor favor in favorsToDelete) {
				favorList.Remove(favor);	
			}

			foreach(GameObject characterObject in characterList) {
				CharacterScript character = characterObject.GetComponent<CharacterScript>();
				character.stamina = character.staminaMax;
				turnOrderList.Add(characterObject);
				SortTurnOrderListByStamina();
			}
		}
		
		//if we aren't finished the current turn,
		//iterate through all characters and give them the chance to move.
		if(turnOrderList.Count > 0) {
			CharacterScript nextCharacter = turnOrderList[0].GetComponent<CharacterScript>();
			if(currentCharacter != nextCharacter) {
				currentCharacter = nextCharacter;
				currentCharacter.StartTurn();
				gui.SetAttachedCharacter(currentCharacter);
				//Set GUI elements to character 
			}
		}

		//End of round check
		
		bool enemyFound = false;
		CharacterScript player = null;
		foreach(GameObject characterObject in characterList) {
			CharacterScript character = characterObject.GetComponent<CharacterScript>();
			if(character.characterType == CharacterConstants.CharacterType.enemy) {
				enemyFound = true;
			}
			if(character.characterType == CharacterConstants.CharacterType.player) {
				player = character;
			}

			//TODO:Remove this hack
		}

		if(!enemyFound) {
			enemySpawnCount += enemySpawnRate;
			Hashtable tiles = map.GetTiles();

			if(player) {
				player.health += (int)(favor * healthGainedPerFavor);
			}

			for(int i = 1; i <= enemySpawnCount; i++) {
				//TODO:Figure out a better spawning algorithm.
				Vector2 spawnTileCoord = new Vector2(i,0);
				TileScript spawnTile = (TileScript)tiles[spawnTileCoord];
				CreateCharacter(CharacterConstants.CharacterClass.Gnoll,spawnTile);
			}
		}
		
	}
	
	void HandleMouseMoved() {
		//if we have clicked on an ability and are about to cast it
		//change currentlyhoveredtile state (color / whatever) or multiple tiles	
	}
	
	void HandleMouseInput() {
		//if an ability is currently selected and we have clicked on a tile we are hovering over, 
		//if we have clicked on a tile and we have already clicked on an ability, then cast spell
		
		//otherwise if we havn't then do nothing
		
		if(Input.GetMouseButtonUp(0)) {
			CharacterScript player = gui.GetAttachedCharacter();
			TileScript currentSelectedTile = map.GetCurrentHoveredTileObject();
			
			if(player != null) {
				if(gui.selected == 0 && currentSelectedTile != null) {
					//Debug.Log("Ability One Executed");
					player.abilityOne.Execute(currentSelectedTile);
					gui.selected = -1;
				}
				else if(gui.selected == 1 && currentSelectedTile != null) {
					//Debug.Log("Ability Two Executed");
					player.abilityTwo.Execute(currentSelectedTile);
					gui.selected = -1;
				}
				else if(gui.selected == 2 && currentSelectedTile != null) {
					//Debug.Log("Ability Three Executed");
					player.abilityThree.Execute(currentSelectedTile);
					gui.selected = -1;
				}
				else if(gui.selected == 3 && currentSelectedTile != null) {
					//Debug.Log("Ability Four Executed");
					player.abilityFour.Execute(currentSelectedTile);
					gui.selected = -1;
				}
				else if(gui.selected == 4 && currentSelectedTile != null) {
					//Debug.Log("Ability Five Executed");
					player.abilityFive.Execute(currentSelectedTile);
					gui.selected = -1;
				}
				else if(gui.selected != -1 && map.GetCurrentHoveredTileObject() == null && !gui.isNewButtonSelected){
					gui.selected = -1;	
					//Debug.Log("Unclicking");
				}
			}
			
		}
	}

	public void CreateCharacter(BAConstants.CharacterConstants.CharacterClass type, TileScript startingTile) {
		
		switch(type) {
			case BAConstants.CharacterConstants.CharacterClass.Gnoll: {
				//TODO: Turn "Enemy" into a constant that is referenced from ConstantsScript
				GameObject newObject = (GameObject)Instantiate(Resources.Load(CharacterConstants.GNOLL_PREFAB_NAME),Vector3.zero,Quaternion.identity);
				GnollScript newGnollScript = newObject.GetComponent<GnollScript>();
				newGnollScript.currentTile = startingTile;
				newGnollScript.gm = this;
				newGnollScript.map = map;
				characterList.Add(newObject);
				break;
			}
		}
	}

	public void KillCharacter(CharacterScript character) {
		Debug.Log(character.gameObject.name + " has died");
		turnOrderList.Remove(character.gameObject);
		characterList.Remove(character.gameObject);

		if(character.characterType == CharacterConstants.CharacterType.enemy) {
			favor += character.favorAwarded;
			if(favor >= fWaveRequirement) {
				GenerateNewFavorWave();
				nextFavorWaveCost += fWaveRequirement;
			}
		}
	}
	
	public void EndTurn() {
		turnOrderList.Remove(currentCharacter.gameObject);
		SortTurnOrderListByStamina();
	}
	
	public List<GameObject> GetTurnOrderList() {
		return turnOrderList;
	}
	
	void SortTurnOrderListByStamina() {
		turnOrderList.Sort(new CharacterListComparer());
	}
	
	public List<GameObject> GetCharacterList() {
		return characterList;
	}
	
	private class CharacterListComparer : IComparer<GameObject> {
		
		public int Compare(GameObject a, GameObject b)
         {
            CharacterScript charA=a.GetComponent<CharacterScript>();
            CharacterScript charB=b.GetComponent<CharacterScript>();
			
			if(charA.stamina < charB.stamina) {
				return 1;
			}
			
			if(charA.stamina > charB.stamina) {
				return -1;	
			}
			
			else { return 0; }
         }
	}

	public int GetFavor() {
		return favor;
	}

	public void SetFavor(int newFavor) {
		favor = newFavor;
	}

	public void GenerateNewFavorWave() {
		System.Random rand = new System.Random();
		ConstantsScript.TileFavorDirection side = (ConstantsScript.TileFavorDirection)rand.Next(1,6);
		ConstantsScript.TileFavorEffect effect = (ConstantsScript.TileFavorEffect)rand.Next(1,6);
		ReadOnlyCollection<Vector2> startingTiles = ConstantsScript.GetFavorWaveStartingTiles(side);
		Hashtable tiles = map.GetTiles();
		foreach(Vector2 tileCoordinate in startingTiles) {
			Favor favor = new Favor(this,((TileScript)tiles[tileCoordinate]),effect,side);
			favorList.Add(favor);
		}
	}

	public List<Favor> GetFavorList() {
		return favorList;
	}
}
		
		
