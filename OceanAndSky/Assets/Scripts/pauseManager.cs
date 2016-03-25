using UnityEngine;
using System.Collections;

public class pauseManager : MonoBehaviour {

	//pause Audio
	public AudioClip pause;
	public AudioClip resume;
	private AudioSource source;

	//Game pause UI
	public GameObject pausePanel;
	bool isPaused;

	// Use this for initialization
	void Start () {
		isPaused = false;
	}

	void Awake(){
		source = GetComponent<AudioSource> ();
		source.PlayOneShot (resume, .5f);
	}

	// Update is called once per frame
	void Update () {

		if (isPaused) {
			pauseGame (true);
		} else {
			pauseGame (false);
		}
		if(Input.GetKeyDown("p")){
			SwitchPause ();
		}

	}

	//pause funtionality
	void pauseGame(bool state){
		//Paused
		if (state) {
			pausePanel.SetActive (true);
			Time.timeScale = 0.0f;
		} 
		//Unpause
		else {
			Time.timeScale = 1f;
			pausePanel.SetActive (false);
		}
	}
	public void SwitchPause(){
		if (isPaused) {
			isPaused = false;
		} else {
			isPaused = true;
		}
	}
}
