using UnityEngine;
using System.Collections;
using BAConstants;

public class AudioManagerScript : MonoBehaviour {
	
	public AudioClip[] Cheers;
	public AudioClip CrowdLoop;
	public AudioClip GameStartSound;
	public AudioClip MonsterGrowl1;
	public AudioClip MonsterGrowl2;
	public AudioClip SwordHit1;
	public AudioClip SwordPickup;
	public AudioClip EnemyAttack;
	public AudioClip EnemyWalk;
	public AudioClip EnemyDeath;
	public AudioClip PlayerDeath;
	public AudioClip GameOver;
	public AudioClip ShoutCheer;
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlayAudioClip(AudioConstants.AudioClipType type) {
		switch(type) {
			case AudioConstants.AudioClipType.Cheer1: {
				audio.clip = Cheers[0];
				break;
			}
			case AudioConstants.AudioClipType.Cheer2: {
				audio.clip = Cheers[1];
				break;
			}
			case AudioConstants.AudioClipType.Cheer3: {
				audio.clip = Cheers[2];
				break;
			}
			case AudioConstants.AudioClipType.CrowdLoop: {
				audio.clip = CrowdLoop;
				break;
			}
			case AudioConstants.AudioClipType.GameStartSound: {
				audio.clip = GameStartSound;
				break;
			}
			case AudioConstants.AudioClipType.MonsterGrowl1: {
				audio.clip = MonsterGrowl1;
				break;
			}
			case AudioConstants.AudioClipType.MonsterGrowl2: {
				audio.clip = MonsterGrowl2;
				break;
			}
			case AudioConstants.AudioClipType.SwordHit1: {
				audio.clip = SwordHit1;
				break;
			}
			case AudioConstants.AudioClipType.SwordPickup: {
				audio.clip = SwordPickup;
				break;
			}
			case AudioConstants.AudioClipType.EnemyAttack: {
				audio.clip = EnemyAttack;
				break;
			}
		
			case AudioConstants.AudioClipType.EnemyWalk: {
				audio.clip = EnemyWalk;
				break;	
			
			}
	
			case AudioConstants.AudioClipType.EnemyDeath: {
				audio.clip = EnemyDeath;
				break;	
			}
		
			case AudioConstants.AudioClipType.PlayerDeath: {
				audio.clip = PlayerDeath;
				break;	
			}	
			
			case AudioConstants.AudioClipType.GameOver: {
				audio.clip = GameOver;
				break;	
			}
			
			case AudioConstants.AudioClipType.ShoutCheer: {
				audio.clip = ShoutCheer;
				break;	
			}
			
			}
		if(audio.clip != null) {
			audio.Play();
		}
	}
}
