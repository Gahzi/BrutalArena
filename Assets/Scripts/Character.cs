using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

public int health;
public int stamina;
public Vector3 tilePosition;
public tk2dSprite sprite;
	
	// Use this for initialization
	public void Start () {
    	sprite = GetComponent<tk2dSprite>();
	}
}