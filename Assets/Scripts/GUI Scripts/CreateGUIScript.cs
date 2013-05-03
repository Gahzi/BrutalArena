using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using BAConstants;

public class CreateGUIScript : MonoBehaviour {
	
	public CharacterScript attachedCharacter;
	public TileScript attachedTile;
	public int selected;
	private int previousSelected = -1;
	public GUIContent[] buttons = new GUIContent[5];
	public bool isNewButtonSelected = false;
	
	// Use this for initialization
	void Start () {
		selected = -1;
	}
	
	void OnGUI() {
		//Set label text color to be black
		GUI.contentColor = Color.black;
		if(attachedCharacter != null) {
			GUI.Label(new Rect(100,125,200,30), "Character: " + attachedCharacter.characterName);
			GUI.Label(new Rect(100,145,100,30), "Health Points: " + attachedCharacter.health.ToString());	
			GUI.Label(new Rect(100,165,100,30), "Stamina: " + attachedCharacter.stamina.ToString());
			
			if(attachedCharacter.characterType == CharacterConstants.CharacterType.player) {
				GUI.contentColor = Color.white;
				/*
				if(attachedCharacter.abilityOne != null) buttons[0] = new GUIContent(attachedCharacter.abilityOne.abilityName,attachedCharacter.abilityOne.tooltipText);
				if(attachedCharacter.abilityTwo != null) buttons[1] = new GUIContent(attachedCharacter.abilityTwo.abilityName,attachedCharacter.abilityTwo.tooltipText);
				if(attachedCharacter.abilityThree != null) buttons[2] = new GUIContent(attachedCharacter.abilityThree.abilityName,attachedCharacter.abilityThree.tooltipText);
				if(attachedCharacter.abilityFour != null) buttons[3] = new GUIContent(attachedCharacter.abilityFour.abilityName,attachedCharacter.abilityFour.tooltipText);
				if(attachedCharacter.abilityFive != null) buttons[4] = new GUIContent(attachedCharacter.abilityFive.abilityName,attachedCharacter.abilityFive.tooltipText);
				
				selected = GUI.SelectionGrid(new Rect(25,95,110,150),selected,buttons,1);
				*/
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
		
		if(attachedTile) {
			GUI.contentColor = Color.black;
			//GUI.Label(new Rect(Screen.width - 225, 25, 200, 30), "Tile Location: " + attachedTile.tileCoordinate.ToString());
			string favorText = "";
			
			if(attachedTile.GetFavorList().Count > 0) {
				int increaseDamageCount = attachedTile.GetNumOfFavorEffectsInTile(ConstantsScript.TileFavorEffect.IncreaseDamage);
				int decreaseAbilityCostCount = attachedTile.GetNumOfFavorEffectsInTile(ConstantsScript.TileFavorEffect.DecreaseAbilityCost);
				int decreaseEnemyDamageCount = attachedTile.GetNumOfFavorEffectsInTile(ConstantsScript.TileFavorEffect.DecreaseEnemyDamage);
				int increaseEnemyAbilityCostCount = attachedTile.GetNumOfFavorEffectsInTile(ConstantsScript.TileFavorEffect.IncreaseEnemyAbilityCost);

				if(increaseDamageCount > 0) favorText += "Increases player damage by " + increaseDamageCount + "." + Environment.NewLine;
				if(decreaseAbilityCostCount > 0) favorText += "Decreases player ability cost by " + decreaseAbilityCostCount + " Stamina." + Environment.NewLine;
				if(decreaseEnemyDamageCount > 0) favorText += "Decreases enemy damage done by " + decreaseEnemyDamageCount + Environment.NewLine;
				if(increaseEnemyAbilityCostCount > 0) favorText += "Increases enemy ability cost by " + increaseEnemyAbilityCostCount + " Stamina." +  Environment.NewLine;
			}
			
			if(!favorText.Equals("")) {
				GUI.contentColor = Color.black;
				GUI.Label(new Rect(Screen.width - 425,55,300,120),favorText);
			}
			
			CharacterScript tileInhabitant = attachedTile.GetTileInhabitant();
			if(tileInhabitant) {
				GUI.contentColor = Color.black;
				
				GUI.Label(new Rect(Screen.width - 125,195,200,30),tileInhabitant.name);
				GUI.Label(new Rect(Screen.width - 125,235,200,30),"Health: " + tileInhabitant.health.ToString());
				GUI.Label(new Rect(Screen.width - 125,215,200,30),"Stamina: " + tileInhabitant.staminaMax.ToString());
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
