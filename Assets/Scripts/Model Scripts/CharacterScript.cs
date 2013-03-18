using UnityEngine;
using System.Collections;

public class CharacterScript : MonoBehaviour {

public int health;
public int stamina;
public Vector2 tilePosition;
public tk2dSprite sprite;
public TileMapScript map;
public GameManagerScript gm;
public bool isCurrentPlayer;
	
	// Use this for initialization
	public void Start () {
		isCurrentPlayer = false;
    	sprite = GetComponent<tk2dSprite>();
		if(map == null) Debug.Log("Forgot to drag tk2dTileMap into inspector");
		gm = GameObject.Find(ConstantsScript.gameManagerObjectName).GetComponent<GameManagerScript>();
		if(gm == null) Debug.Log("Could not find Game Manager in Scene");
		
		this.gameObject.transform.position = map.GetWorldPositionFromCoordinate(tilePosition.x,tilePosition.y);
	}
	
	public void Update() {
		if(isCurrentPlayer) {
			
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