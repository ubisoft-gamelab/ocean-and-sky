using UnityEngine;
using System.Collections;

public class Burden : MonoBehaviour {

	Player P1;
	Player P2;
	
	Vector3 parentPosition;
	Vector3 restPosition;

	public float forwardForce;
	public float upForce;

	bool isGrounded;
	public bool isHeld;
	public bool isThrown;
	public bool isAtRest;

	public int minHeight;

	int weight;

	Vector3 currentPosition;

	// Use this for initialization
	void Start () {

		currentPosition = transform.position;
		isAtRest = true;

		weight = 25;
		minHeight = 30;
	
		isHeld = false;


		forwardForce = 290f;
		upForce = 150f;

	}

	// Update is called once per frame
	void Update () {
	
		gravity ();

		if (isThrown) 
		{
			throwBurden ();
			forwardForce -= 5;
			upForce -= 5;
		}
	}


	
	void gravity()
	{
		if (isHeld) 
		{
			return;
		}


		if ( transform.position.y <= minHeight) 
		{
			transform.position = new Vector3(transform.position.x, minHeight, transform.position.z);
			isAtRest = true;
			return;
		}

		transform.Translate (Vector3.down * Time.deltaTime * weight);
	}

	/*
	 * If inPossesion, then set isHeld = true
	 * and also make the Sphere trigger collider inactive
	 */
	public void inPossession()
	{
		isHeld = true;
		isAtRest = false;

		GetComponent<SphereCollider> ().enabled = false;
		parentPosition = gameObject.transform.parent.position;
		transform.position = new Vector3 (parentPosition.x-40, parentPosition.y, parentPosition.z);
	}

	public void notInPossession()
	{
		isHeld = false;
		GetComponent<SphereCollider> ().enabled = true;
		isThrown = true;
		isAtRest = false;

	}

	/* Force which propels Burden forwards when the throwInput is pressed */
	void throwBurden()
	{
		if (isHeld || ( forwardForce <= 0 && upForce <= 0)) 
		{
			forwardForce = 290f;
			upForce = 150f;
			isThrown = false;
			return;
		}

		transform.Translate (Vector3.up * Time.deltaTime * upForce);
		transform.Translate(Vector3.right * Time.deltaTime * forwardForce);
	}


}
