using UnityEngine;
using System.Collections;

/**
 * Renders the Skybox of the game
 * TODO Implement means of swapping which Skybox material is in use
 * based on the maxVelocity.
 */
public class SkyBoxCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//Slowly rotate skybox
		gameObject.transform.Rotate (Vector3.right*Time.deltaTime*0.2f);

	}
}
