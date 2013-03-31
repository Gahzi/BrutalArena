using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManagerScript : MonoBehaviour {
	
	List<GameObject> characterList = new List<GameObject>();
	List<GameObject> turnOrderList = new List<GameObject>();
	TileMapScript map;
	CreateGUIScript gui;
	
	CharacterScript currentCharacter;
	
	// Use this for initialization
	void Start () {
		map =  GameObject.Find(ConstantsScript.tileMapObjectName).GetComponent<TileMapScript>();
		gui =  GameObject.Find(ConstantsScript.guiManagerObjectName).GetComponent<CreateGUIScript>();
		
		//TODO: Change this or else characters won't show in characterlist.
		//GameObject.FindObjectsOfType(typeof(CharacterScript));
		GameObject[] chars = GameObject.FindGameObjectsWithTag(ConstantsScript.characterTag);
		
		foreach(GameObject character in chars) {
			characterList.Add(character);
		}
		
		turnOrderList.AddRange(characterList);
		SortTurnOrderListByStamina();
	}
	
	// Update is called once per frame
	void Update () {
		
		//handle mouse input
		HandleMouseInput();
		
		if(turnOrderList.Count > 0) {
			CharacterScript nextCharacter = turnOrderList[0].GetComponent<CharacterScript>();
			if(currentCharacter != nextCharacter) {
				currentCharacter = nextCharacter;
				currentCharacter.StartTurn();
				gui.SetAttachedCharacter(currentCharacter);
				//Set GUI elements to character 
			}
		}
		
		//TODO:
		//if there are no more enemies in the round or if there are no more players in the round.
		//round is over (round won or game over)
		
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
			//TODO: This can be refactored with more readability and elegance.
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
	
	public void EndTurn() {
		SortTurnOrderListByStamina();
	}
	
	void SortTurnOrderListByStamina() {
		turnOrderList.Sort(new CharacterListComparer());
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
}
		
		
