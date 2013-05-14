using UnityEngine;
using System.Collections;

public class RestartScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnGUI () {
		if (GUI.Button(new Rect(280, 500, 270, 160), "",GUIStyle.none)) {
			Application.LoadLevel(1);
		}
		
		if (GUI.Button(new Rect(600, 500, 300, 130), "",GUIStyle.none)) {
			Application.Quit();
		}
			
	}
}
