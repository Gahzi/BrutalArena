using UnityEngine;
using System.Collections;

public class GnollScript : CharacterScript {

	// Use this for initialization
	new void Start () {
		base.Start();
		
		characterType = CharType.enemy;
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update();
	}
}
