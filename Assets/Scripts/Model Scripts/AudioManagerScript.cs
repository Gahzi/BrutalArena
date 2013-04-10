using UnityEngine;
using System.Collections;

public class AudioManagerScript : MonoBehaviour {
	
	public AudioClip[] Cheers;
	public AudioClip CrowdLoop;
	public AudioClip GameStartSound;
	public AudioClip MonsterGrowl1;
	public AudioClip MonsterGrowl2;
	public AudioClip SwordHit1;
	public AudioClip SwordPickup;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void SwordHit() {
		audio.clip = SwordHit1;
		audio.Play();
		Debug.Log ("Played!");
	}
}
