using UnityEngine;
using System.Collections;

public class TileScript : MonoBehaviour {
	
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseEnter() {
		tk2dSprite script =  this.gameObject.GetComponent<tk2dSprite>();
		script.color = Color.red;
		Debug.Log("Entered on " + gameObject.name);	
	}
	
	void OnMouseExit() {
		tk2dSprite script =  this.gameObject.GetComponent<tk2dSprite>();
		script.color = Color.white;
		Debug.Log("Exited on " + gameObject.name);	
	}
	
	void OnMouseDown() {
		Debug.Log("Clicked on " + gameObject.name);	
	}
}
