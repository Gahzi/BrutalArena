using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	
int health;
int stamina;
int tilePositionX;
int tilePositionY;
tk2dSprite sprite;
public tk2dTileMap map;
		
	
// Use this for initialization
void Start () {
	sprite = GetComponent<tk2dSprite>();	
	if(map == null) Debug.Log("Forgot to drag tk2dTileMap into inspector");
	
	map.GetTileAtPosition(transform.position,out tilePositionX,out tilePositionY);
		
	Debug.Log("tilePositionX: " + tilePositionX);
	Debug.Log("tilePositionY: " + tilePositionY);
		
	sprite.color = Color.blue;
}

// Update is called once per frame
void Update () {
		if(Input.GetKeyDown(KeyCode.W)) {
			Vector3 newPosition = map.GetTilePosition(tilePositionX,tilePositionY+1);
			newPosition.z = transform.position.z;
			
			transform.position = newPosition;
			tilePositionY += 1;
			
			Debug.Log("tilePositionX: " + tilePositionX);
			Debug.Log("tilePositionY: " + tilePositionY);
		}
		if(Input.GetKeyDown(KeyCode.S)) {
			Vector3 newPosition = map.GetTilePosition(tilePositionX,tilePositionY-1);
			newPosition.z = transform.position.z;
			
			transform.position = newPosition;
			tilePositionY -= 1;
			
			Debug.Log("tilePositionX: " + tilePositionX);
			Debug.Log("tilePositionY: " + tilePositionY);
		}
		if(Input.GetKeyDown(KeyCode.A)) {
			Vector3 newPosition = map.GetTilePosition(tilePositionX-1,tilePositionY);
			newPosition.z = transform.position.z;
			
			transform.position = newPosition;
			tilePositionX -= 1;
			
			Debug.Log("tilePositionX: " + tilePositionX);
			Debug.Log("tilePositionY: " + tilePositionY);
		}
		if(Input.GetKeyDown(KeyCode.D)) {
			Vector3 newPosition = map.GetTilePosition(tilePositionX+1,tilePositionY);
			newPosition.z = transform.position.z;
			
			transform.position = newPosition;
			tilePositionX += 1;
			
			Debug.Log("tilePositionX: " + tilePositionX);
			Debug.Log("tilePositionY: " + tilePositionY);
		}
    }
}
