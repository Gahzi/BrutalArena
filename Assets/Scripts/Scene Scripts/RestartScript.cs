using UnityEngine;
using System.Collections;

public class RestartScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnGUI () {
		if (GUI.Button(new Rect(100, 100, 100, 100), "Game Reset")) {
			Application.LoadLevel(1);
		}
			
	}
}
