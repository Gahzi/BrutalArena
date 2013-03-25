using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManagerScript : MonoBehaviour {
	
	public List<GameObject> characterList = new List<GameObject>();
	public List<GameObject> turnOrderList = new List<GameObject>();
	TileMapScript map;
	CreateGUIScript gui;
	
	CharacterScript currentCharacter;
	
	// Use this for initialization
	void Start () {
		map =  GameObject.Find(ConstantsScript.tileMapObjectName).GetComponent<TileMapScript>();
		gui =  GameObject.Find(ConstantsScript.guiManagerObjectName).GetComponent<CreateGUIScript>();
		
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
				gui.SetGUIToCharacter(currentCharacter);
				//Set GUI elements to character 
			}
		}
		
		//TODO:
		//if there are no more enemies in the round or if there are no more players in the round.
		//round is over (round won or game over)
		
	}
	
	void HandleMouseInput() {
		if(Input.GetMouseButtonUp(0)) {
			
			map.GetTileFromWorldPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
			
			
			if(gui.selected == 0) {
				Debug.Log("Ability One Selected");
				//gui.selected = -1;
			}
			
			if(gui.selected == 1) {
				Debug.Log("Ability Two Selected");
				//gui.selected = -1;
			}
			
			if(gui.selected == 2) {
				Debug.Log("Ability Three Selected");
				//gui.selected = -1;
			}
			
			if(gui.selected == 3) {
				Debug.Log("Ability Four Selected");
				//gui.selected = -1;
			}
			
			if(gui.selected == 4) {
				Debug.Log("Ability Five Selected");
				//gui.selected = -1;
			}
			
			//lmb has been clicked
			// check if a button has been selected
			// if a button has selected, call that abilities execute method.
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
		
		
