using UnityEngine;
using System.Collections;

/*
 * Class which handles when Burden is Thrown by Player.
 * Keeps track of when Burden is - or is not - in possession
 * Burden becomes child of the Player which bears it.
 */
public class Burden : MonoBehaviour {

	public Player P1;
	public Player P2;

	public GameWall gameWall; 

	Vector3 parentPosition;
	Vector3 restPosition;

	float forwardForce;
	float upForce;

	bool isHeld;
	bool isThrown;
	bool isAtRest;

	int minHeight;
	int weight;


	// Use this for initialization
	void Start () {


		weight = 25;
		minHeight = 30;

		isHeld = false;
		isAtRest = true; //isAtRest when Burden returns to minHeight

		forwardForce = 250f;
		upForce = 150f;

	}

	// Update is called once per frame
	void Update () {
	
		gravity ();

		// TODO Implement cleaner, more reliable way to throw the Burden
		if (isThrown) 
		{
			thrown ();
			forwardForce -= 5;
			upForce -= 5;
		}
	}


	
	void gravity()
	{
		//Do not apply gravity if isHeld by Player
		if (isHeld) return;

		//If Position less than minHeight, restore to minHeight. Set isAtRest to true. Return
		if ( transform.position.y <= minHeight) 
		{
			transform.position = new Vector3(transform.position.x, minHeight, transform.position.z);
			isAtRest = true;

			//Make both Players isNeither again
			P1.resetPlayerType ();
			P2.resetPlayerType ();

			return;
		}

		//Slowly pull Burden downwards based on weight
		transform.Translate (Vector3.down * Time.deltaTime * weight);
	}

	/*
	 * If inPossesion, then set isHeld = true
	 * Make the Sphere trigger collider inactive
	 */
	public void inPossession()
	{
		isHeld = true;
		isAtRest = false;

		GetComponent<SphereCollider> ().enabled = false;
		parentPosition = gameObject.transform.parent.position;

		//Move Burden to slightly behind parent
		transform.position = new Vector3 (parentPosition.x-40, parentPosition.y, parentPosition.z);
	}

	/*
	 * If notInPossession, set isHeld to false
	 * Set sphere collider to true
	 * Set isAtRest to false
	 */
	public void notInPossession()
	{
		isHeld = false;
		GetComponent<SphereCollider> ().enabled = true;
		isAtRest = false;

	}

	// Force that propels Burden forwards when throwInput is pressed 
	public void thrown()
	{

		//Will not apply Throw force if Held, AtRest or Forward and Up forces have reached 0
		if (isHeld || isAtRest || ( forwardForce <= 0 && upForce <= 0)) 
		{
			forwardForce = 250f;
			upForce = 150f;
			isThrown = false;
			return;
		}


		//Simulates Parabola Arc of being thrown. Applies a weakening upward and forward force
		transform.Translate (Vector3.up * Time.deltaTime * upForce);
		transform.Translate(Vector3.right * Time.deltaTime * forwardForce);

	}

	void aiAgent()
	{
		//TODO Have Burden dynamically interact with Players depending on exactly which Stage Part is coming next
	}

	//Getter for isAtRest
	public bool isResting()
	{
		return isAtRest;
	}

	//Setter for isThrown
	public void setThrown()
	{
		isThrown = true;
	}
}
