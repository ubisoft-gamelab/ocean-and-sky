using UnityEngine;
using System.Collections;

public class GameMusic : MonoBehaviour {

	public AudioClip startSound;

	private AudioSource source;
	private float volLowRange = 0.5f;
	private float volHighRange = 1.0f;

	void Awake(){
		source = GetComponent<AudioSource> ();
	}

	void Update(){
		source.PlayOneShot (startSound, .5f);
	}

}
