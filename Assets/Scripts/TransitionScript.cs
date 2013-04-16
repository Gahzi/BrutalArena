using UnityEngine;
using System.Collections;

public class TransitionScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnGUI () {
		if (GUI.Button(new Rect(100, 100, 100, 100), "Game Start")) {
			Application.LoadLevel(1);
		}
			
	
	}
}
