using UnityEngine;
using System.Collections;

public class AnimationScript : MonoBehaviour {
	
	private tk2dAnimatedSprite animatedSprite;
	
	// Use this for initialization
	void Start () {
		animatedSprite = GetComponent<tk2dAnimatedSprite>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if(!animatedSprite.IsPlaying(BAConstants.AnimationConstants.ATTACK_ANIMATION_NAME)) {
			Destroy(this.gameObject);	
		}
	
	}
}
