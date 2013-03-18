using UnityEngine;
using System.Collections;

public class CreateGUIScript : MonoBehaviour {
	
	void OnGUI() {
		GUI.Label(new Rect(25,25,100,30), "Character: ");
		GUI.Label(new Rect(25,65,100,30), "HP: ");
		GUI.Label(new Rect(25,95,100,30), "STM: ");
		GUI.Button(new Rect(25,135,110,30), "Move");
		GUI.Button(new Rect(25,175,110,30), "Basic Attack");
		GUI.Button(new Rect(25,215,110,30), "Combo");
		GUI.Button(new Rect(25,255,110,30), "Crippling Cleave");
		GUI.Button(new Rect(25,295,110,30), "Counterstrike");
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
