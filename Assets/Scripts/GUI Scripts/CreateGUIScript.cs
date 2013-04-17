using UnityEngine;
using System.Collections;


public class CreateGUIScript : MonoBehaviour {
	
	CharacterScript attachedCharacter;
	public int selected;
	private int previousSelected = -1;
	public GUIContent[] buttons = new GUIContent[5];
	public bool isNewButtonSelected = false;
	
	// Use this for initialization
	void Start () {
		selected = -1;
	}
	
	void OnGUI() {
		
		if(attachedCharacter != null) {
			GUI.Label(new Rect(25,25,200,30), "Character: " + attachedCharacter.characterName);
			GUI.Label(new Rect(25,45,100,30), "Health Points: " + attachedCharacter.health.ToString());	
			GUI.Label(new Rect(25,65,100,30), "Stamina: " + attachedCharacter.stamina.ToString());
			
			if(attachedCharacter.characterType == CharacterScript.CharType.player) {

				if(attachedCharacter.abilityOne != null) buttons[0] = new GUIContent(attachedCharacter.abilityOne.abilityName,attachedCharacter.abilityOne.tooltipText);
				if(attachedCharacter.abilityTwo != null) buttons[1] = new GUIContent(attachedCharacter.abilityTwo.abilityName,attachedCharacter.abilityTwo.tooltipText);
				if(attachedCharacter.abilityThree != null) buttons[2] = new GUIContent(attachedCharacter.abilityThree.abilityName,attachedCharacter.abilityThree.tooltipText);
				if(attachedCharacter.abilityFour != null) buttons[3] = new GUIContent(attachedCharacter.abilityFour.abilityName,attachedCharacter.abilityFour.tooltipText);
				if(attachedCharacter.abilityFive != null) buttons[4] = new GUIContent(attachedCharacter.abilityFive.abilityName,attachedCharacter.abilityFive.tooltipText);
				
				selected = GUI.SelectionGrid(new Rect(25,95,110,150),selected,buttons,1);
				
				if(GUI.Button(new Rect(25,260,110,30),"End Turn")) {
					attachedCharacter.EndTurn();		
				}
				
				if(previousSelected != selected) {
					isNewButtonSelected = true;	
					previousSelected = selected;
				}
				else {
					isNewButtonSelected = false;	
				}
			}
			else {
				// Must be an enemy so don't show move Buttons
			}		
		}
	}
	
	public void SetAttachedCharacter(CharacterScript character) {
		attachedCharacter = character;
	}
	
	public CharacterScript GetAttachedCharacter() {
		return attachedCharacter;		
	}
}
