using UnityEngine;
using System.Collections;

public class LoadOnClick : MonoBehaviour {

	public AudioClip startSound;

	private AudioSource source;
	private float volLowRange = 0.5f;
	private float volHighRange = 1.0f;

	void Awake(){
		source = GetComponent<AudioSource> ();
		source.PlayOneShot (startSound, .5f);
	}
		
	public void LoadScene(int level)
	{
		Application.LoadLevel (level);
		source.Stop ();
	}

}
