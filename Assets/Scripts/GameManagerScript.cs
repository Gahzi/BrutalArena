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
		
		//handle input on menu's
		handleInput();
		
		if(turnOrderList.Count > 0) {
			CharacterScript nextCharacter = turnOrderList[0].GetComponent<CharacterScript>();
			if(currentCharacter != nextCharacter) {
				currentCharacter = nextCharacter;
				currentCharacter.StartTurn();
				//Set GUI elements to character 
			}
		}
		
		//TODO:
		//if there are no more enemies in the round or if there are no more players in the round.
		//round is over (round won or game over)
		
	}
	
	void handleInput() {
		if(Input.GetKeyDown(KeyCode.W)) {
			
		}
		if(Input.GetKeyDown(KeyCode.S)) {
				
		}
		if(Input.GetKeyDown(KeyCode.A)) {
				
		}
		if(Input.GetKeyDown(KeyCode.D)) {
			
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
		
		
