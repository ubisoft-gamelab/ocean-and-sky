using UnityEngine;
using System.Collections;

public class cameraMove : MonoBehaviour {
	GameObject depthPlane;
	public float defaultPos;

	// Use this for initialization
	void Start () {
		//depthPlane = this.transform.Find ("DepthHorizon").gameObject;
		defaultPos = this.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.forward * Time.deltaTime * 20);
		//depthPlane.transform.position.y;
		//defaultPos = transform.position.y;

		if (Input.GetKeyDown(KeyCode.C)) { defaultPos = -0.25f; }
		if (Input.GetKey(KeyCode.UpArrow)) { transform.Translate(Vector3.up * Time.deltaTime * 10); }
		if (Input.GetKey(KeyCode.DownArrow)) { transform.Translate(Vector3.down * Time.deltaTime * 10); }
		if (Input.GetKey(KeyCode.LeftArrow)) { transform.Translate(Vector3.left * Time.deltaTime * 10); }
		if (Input.GetKey(KeyCode.RightArrow)) { transform.Translate(Vector3.right * Time.deltaTime * 10); }
	}
}
