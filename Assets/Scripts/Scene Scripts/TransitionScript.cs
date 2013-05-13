using UnityEngine;
using System.Collections;

public class TransitionScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnGUI () {
		Event e = Event.current;
		if (e.button == 0 && e.isMouse) {
			Application.LoadLevel(1);
		}
			
	
	}
}
