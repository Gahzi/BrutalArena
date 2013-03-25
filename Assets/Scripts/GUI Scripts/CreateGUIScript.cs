using UnityEngine;
using System.Collections;


public class CreateGUIScript : MonoBehaviour {
	
	CharacterScript attachedCharacter;
	public bool isAbilityOneSelected = false;
	public bool isAbilityTwoSelected = false;
	public bool isAbilityThreeSelected = false;
	public bool isAbilityFourSelected = false;
	public bool isAbilityFiveSelected = false;
	
	public int selected;
	public GUIContent[] buttons = new GUIContent[5];
	
	// Use this for initialization
	void Start () {
		selected = -1;
	}
	
	void OnGUI() {
		
		if(attachedCharacter != null) {
			GUI.Label(new Rect(25,25,200,30), "Character: " + attachedCharacter.characterName);
			GUI.Label(new Rect(25,45,100,30), "Health Points: " + attachedCharacter.health.ToString());	
			GUI.Label(new Rect(25,65,100,30), "Stamina: " + attachedCharacter.stamina.ToString());
			
			if(attachedCharacter is PlayerScript) {
				PlayerScript attachedPlayer = (PlayerScript)attachedCharacter;

				buttons[0] = new GUIContent(attachedPlayer.abilityOne.name,attachedPlayer.abilityOne.tooltipText);
				buttons[1] = new GUIContent(attachedPlayer.abilityTwo.name,attachedPlayer.abilityTwo.tooltipText);
				buttons[2] = new GUIContent(attachedPlayer.abilityThree.name,attachedPlayer.abilityThree.tooltipText);
				buttons[3] = new GUIContent(attachedPlayer.abilityFour.name,attachedPlayer.abilityFour.tooltipText);
				buttons[4] = new GUIContent(attachedPlayer.abilityFive.name,attachedPlayer.abilityFive.tooltipText);
				selected = GUI.SelectionGrid(new Rect(25,95,110,150),selected,buttons,1);
				
				if(selected == 0) {
					attachedPlayer.abilityOne.Selected();
				}
				if(selected == 1) {
					attachedPlayer.abilityTwo.Selected();
				}
				
				if(selected == 2) {
					attachedPlayer.abilityThree.Selected();
				}
				
				if(selected == 3) {
					attachedPlayer.abilityFour.Selected();
				}
				
				if(selected == 4) {
					attachedPlayer.abilityFive.Selected();
				}
				
				/*
				isAbilityOneSelected = GUI.Button(new Rect(25,95,110,30), attachedPlayer.abilityOne.name);
				isAbilityTwoSelected = GUI.Button(new Rect(25,135,110,30), attachedPlayer.abilityTwo.name);
				isAbilityThreeSelected = GUI.Button(new Rect(25,175,110,30), attachedPlayer.abilityThree.name);
				isAbilityFourSelected = GUI.Button(new Rect(25,215,110,30), attachedPlayer.abilityFour.name);
				isAbilityFiveSelected = GUI.Button(new Rect(25,255,110,30), attachedPlayer.abilityFive.name);

				if(isAbilityOneSelected) {
					attachedPlayer.abilityOne.Selected();
				}
				if(isAbilityTwoSelected) {
					attachedPlayer.abilityTwo.Selected();
				}
				
				if(isAbilityThreeSelected) {
					attachedPlayer.abilityThree.Selected();
				}
				
				if(isAbilityFourSelected) {
					attachedPlayer.abilityFour.Selected();
				}
				
				if(isAbilityFiveSelected) {
					attachedPlayer.abilityFive.Selected();
				}
				*/
			}
			else {
				// Must be an enemy so don't show move Buttons
			}		
		}
	}
	
	public void SetGUIToCharacter(CharacterScript character) {
			attachedCharacter = character;
	}
}
