using UnityEngine;
using System.Collections;

public class characterBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Translate (Vector3.up * Time.deltaTime * 10);

	}
}
