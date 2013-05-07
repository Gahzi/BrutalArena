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

	List<Vector2> hoveredPath = new List<Vector2>();

	TileMapScript map;
	CreateGUIScript gui;
	
	tk2dSpriteCollectionData scData;
	
	public int favor = 0;
	public int fWaveRequirement = 0;
	public int nextFavorWaveCost = 0;
	public float healthGainedPerFavor = 0.1f;

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

			StartNewFullTurn();
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
			//TODO:Increment enemy base damage every n waves
		}

		if(!enemyFound) {
			enemySpawnCount += enemySpawnRate;
			
			//JONATHAN: NEW ROUND STARTS HERE
			
			if(enemySpawnCount == 5) {
				//JONATHAN: ROUND 5 STARTS HERE	
			}
			
			if(enemySpawnCount == 10) {
				//JONATHAN: ROUND 10 STARTS HERE
			}

			if(player) {
				if(player.health + ((int)(favor * healthGainedPerFavor)) >= player.healthMax) {
					player.health = player.healthMax;
				}
				else {
					player.health += (int)(favor * healthGainedPerFavor);
				}
			}
			System.Random rand = new System.Random(); 
			for(int i = 1; i <= enemySpawnCount; i++) {
				//TODO:Figure out a better spawning algorithm.
				Vector2 spawnTileCoord = new Vector2(rand.Next(0,12),rand.Next(0,12));
				
				bool hasNewSpawnFound = false;
				
				while(!hasNewSpawnFound) {
					spawnTileCoord = new Vector2(rand.Next(0,12),rand.Next(0,12));	
					if(map.GetTiles()[spawnTileCoord] != null) {
						TileScript spawnTile = (TileScript)map.GetTiles()[spawnTileCoord];
						if(spawnTile.GetTileInhabitant() == null) {
							hasNewSpawnFound = true;	
						}
					}				
				}
				GameObject newCharacterObject = CreateCharacter(CharacterConstants.GNOLL_PREFAB_NAME + " " + i.ToString() ,CharacterConstants.CharacterClass.Gnoll,(TileScript)map.GetTiles()[spawnTileCoord]);
				turnOrderList.Add(newCharacterObject);
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
			TileScript currentHoveredTile = map.GetCurrentHoveredTileObject();
			
			if(player != null && currentHoveredTile != null) {
				CharacterScript currentHoveredTileInhabitant = currentHoveredTile.GetTileInhabitant();
				if(currentHoveredTileInhabitant == null) {
					player.abilityOne.Execute(currentHoveredTile);
					if(hoveredPath.Count > 0) {
						foreach(Vector2 tileCoordinate in hoveredPath) {
							TileScript tile = (TileScript)map.GetTiles()[tileCoordinate];
							tile.isHighlighted = false;
						}
					}
				}
				else if(currentHoveredTileInhabitant != null) {
					if(currentHoveredTileInhabitant.characterType == CharacterConstants.CharacterType.enemy) {
						//MOVE and ATTACK
						//bring up attack dialogue
						Vector2 adjacentTile = Vector2.zero;
						if(hoveredPath.Count == 2) {
							adjacentTile = hoveredPath[(hoveredPath.Count - 1)];
							int tempStamina = currentCharacter.stamina;
							bool canAttackAtTile = player.abilityTwo.ValidateMove(ref tempStamina,player.currentTile,currentHoveredTile);
						
							if(canAttackAtTile) {
								player.abilityTwo.Execute(currentHoveredTile);
								foreach(Vector2 tileCoordinate in hoveredPath) {
									TileScript hoveredTile = (TileScript)map.GetTiles()[tileCoordinate];
									hoveredTile.isHighlighted = false;
									hoveredTile.isAttackable = false;
								}
							}
						}
						else if(hoveredPath.Count > 2) {
							adjacentTile = hoveredPath[(hoveredPath.Count - 2)];
							TileScript tile = (TileScript)player.map.GetTiles()[adjacentTile];
							int tempStamina = currentCharacter.stamina;
							bool canMovetoTile = player.abilityOne.ValidateMove(ref tempStamina,player.currentTile,tile);
							
							if(canMovetoTile) {
								bool canAttackAtTile = player.abilityTwo.ValidateMove(ref tempStamina,tile,currentHoveredTile);
								if(canAttackAtTile) {
									player.abilityOne.Execute(tile);
									player.abilityTwo.Execute(currentHoveredTile);
									foreach(Vector2 tileCoordinate in hoveredPath) {
										TileScript hoveredTile = (TileScript)map.GetTiles()[tileCoordinate];
										hoveredTile.isHighlighted = false;
										hoveredTile.isAttackable = false;
									}
								}	
							}
						}
					}
				}
			}
			
			
		}
	}

	public GameObject CreateCharacter(string charName, BAConstants.CharacterConstants.CharacterClass type, TileScript startingTile) {
		GameObject newCharacterObject = null;
		
		switch(type) {
			case BAConstants.CharacterConstants.CharacterClass.Gnoll: {
				//TODO: Turn "Enemy" into a constant that is referenced from ConstantsScript
				newCharacterObject = (GameObject)Instantiate(Resources.Load(CharacterConstants.GNOLL_PREFAB_NAME),Vector3.zero,Quaternion.identity);
				GnollScript newGnollScript = newCharacterObject.GetComponent<GnollScript>();
				newCharacterObject.name = charName;
				newGnollScript.characterName = charName;
				newGnollScript.currentTile = startingTile;
				newGnollScript.gm = this;
				newGnollScript.map = map;
				characterList.Add(newCharacterObject);
				break;
			}
		}
		return newCharacterObject;
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
			//JONATHAN: ENEMY DIES HERE	
		} else if (character.characterType == CharacterConstants.CharacterType.player) {
			//TODO: Delay for short time
			//JONATHAN: PLAYER DIES HERE
			Application.LoadLevel(2);
		}
	}
	
	public void StartNewFullTurn() {
		foreach(GameObject characterObject in characterList) {
				CharacterScript character = characterObject.GetComponent<CharacterScript>();
				character.stamina = character.staminaMax;
				turnOrderList.Add(characterObject);
		}
		SortTurnOrderListByStamina();
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
		//JONATHAN: NEW FAVOR WAVE IS CREATED HERE
		System.Random rand = new System.Random();
		ConstantsScript.TileFavorDirection side = (ConstantsScript.TileFavorDirection)rand.Next(1,6);
		ConstantsScript.TileFavorEffect effect = (ConstantsScript.TileFavorEffect)rand.Next(1,4);
		
		if(effect == ConstantsScript.TileFavorEffect.DecreaseEnemyDamage || effect == ConstantsScript.TileFavorEffect.IncreaseEnemyAbilityCost) {
			//JONATHAN: NEW JEER IS CREATED HERE	
		}
		
		ReadOnlyCollection<Vector2> startingTiles = ConstantsScript.GetFavorWaveStartingTiles(side);
		Vector2 startingTileCoordinate = startingTiles[rand.Next(0,startingTiles.Count - 1)];
		List<TileScript> adjacentTiles = map.GetAllAdjacentTiles(startingTileCoordinate);
		Hashtable tiles = map.GetTiles();

		Favor centerFavor = new Favor(this,(TileScript)tiles[startingTileCoordinate],effect,side);
		favorList.Add(centerFavor);
		
		foreach(TileScript tile in adjacentTiles) {
			Favor favor = new Favor(this,tile,effect,side);
			favorList.Add(favor);
		}
	}

	public List<Favor> GetFavorList() {
		return favorList;
	}
	
	public void SetAttachedTileInGUI(TileScript hoveredTile) {
		//does tile have an inhabitant
		int tempStamina = currentCharacter.stamina;
		foreach(Vector2 tileCoordinate in hoveredPath) {
			TileScript tile = (TileScript)map.GetTiles()[tileCoordinate];
			tile.isHighlighted = false;
			tile.isAttackable = false;
		}
		
		if(currentCharacter.characterType == CharacterConstants.CharacterType.player && hoveredTile != null) {
			CharacterScript hoveredInhabitant = hoveredTile.GetTileInhabitant();
			
			if(hoveredInhabitant != null) {
				if(hoveredInhabitant.characterType == CharacterConstants.CharacterType.enemy) {
					hoveredPath = map.GetAStar().GetPathBetweenTwoTiles(currentCharacter.currentTile,hoveredTile);
					if(hoveredPath.Count == 2) {
						bool canAttackAtTile = currentCharacter.abilityTwo.ValidateMove(ref tempStamina,currentCharacter.currentTile,hoveredTile);
						if(canAttackAtTile) {
							foreach(Vector2 tileCoordInPath in hoveredPath) {
								TileScript tileInPath = (TileScript)map.GetTiles()[tileCoordInPath];
								if(hoveredPath.IndexOf(tileCoordInPath) == (hoveredPath.Count - 1)) {
									tileInPath.isAttackable = true;
								}
								else {
									tileInPath.isHighlighted = true;
								}
							}
						}
					}
					else if(hoveredPath.Count > 2) {
						Vector2 adjacentTile = hoveredPath[(hoveredPath.Count - 2)];
						TileScript tile = (TileScript)map.GetTiles()[adjacentTile];
						//tempStamina = currentCharacter.stamina;
						bool canMovetoTile = currentCharacter.abilityOne.ValidateMove(ref tempStamina,currentCharacter.currentTile,tile);
						if(canMovetoTile) {
							bool canAttackAtTile = currentCharacter.abilityTwo.ValidateMove(ref tempStamina,tile,hoveredTile);
							if(canAttackAtTile) {
								foreach(Vector2 tileCoordInPath in hoveredPath) {
									TileScript tileInPath = (TileScript)map.GetTiles()[tileCoordInPath];
									
									if(hoveredPath.IndexOf(tileCoordInPath) == (hoveredPath.Count - 1)) {
										tileInPath.isAttackable = true;
									}
									else {
										tileInPath.isHighlighted = true;
									}
								}
							}
						}
					}	
				}	
			}
			else {
				bool canMoveToHoveredTile = currentCharacter.abilityOne.ValidateMove(ref tempStamina,currentCharacter.currentTile,hoveredTile);
				
				if(canMoveToHoveredTile) {
					hoveredPath = map.GetAStar().GetPathBetweenTwoTiles(currentCharacter.currentTile,hoveredTile);
					if(hoveredPath.Count > 0) {
						foreach(Vector2 tileCoordinate in hoveredPath) {
							TileScript tile = (TileScript)map.GetTiles()[tileCoordinate];
							tile.isHighlighted = true;
						}	
					}
				}
			}	
		}
		gui.attachedTile = hoveredTile;
	}
}
		
		
