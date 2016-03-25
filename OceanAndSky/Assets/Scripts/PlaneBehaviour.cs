using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/**
 * Class which moves Planes forwards to give the impression of motion in the game state.
 * When a plane moves out of viewing behind the player camera, it will be snapped back into
 * a position in front of the camera to be looped again.
 * 
 * MaxVelocity exists in this class. MaxVelocity increases incrementally throughout gameplay.
 * MaxVelocity never goes down.
 */
public class PlaneBehaviour : MonoBehaviour {

	//Velocity UI
	public Slider velocityDisplay;
	public Text velocityValue;

	public PlaneBehaviour otherPlane;

	public Player P1;
	public Player P2;

	public Burden burden;

	bool hasExited;
	bool hasCollided;
	bool isMaxVelocity;

	float planeLength;
	float maxVelocity;
	float resetPosition;


	void Start () {

		maxVelocity = 1500f;
		resetPosition = otherPlane.transform.position.z;// + 10000f;

		// Call increaseMaxVelocity() on the first second of gameplay, and every five seconds after.
		InvokeRepeating ("increaseMaxVelocity", 1f, 5f);

	}

	void Update () {
		resetPosition = otherPlane.transform.position.z + 4500f;
		/** Constantly move the plane forward **/
		move ();
	}

	/** 
	 * Pops the Plane forward on the X-axis to the resetPosition, after Player has left the 
	 * Plane's box collider
	 */ 
	void popForward()
	{
		transform.position = new Vector3 (transform.position.x, transform.position.y, resetPosition);
	}

	// Moves the Plane forward incrementally by every frame
	public void move()
	{
		transform.Translate (Vector3.back * Time.deltaTime * maxVelocity);
	}

	/** 
	 * increments maxVelocity every 5 seconds if Player has not collided in recent time
	 * And if the Burden is being held by a Player
	 */
	public void increaseMaxVelocity()
	{
		//If Burden is at rest or if either Player has collided recently, do not increment maxVelocity
		if((P1.getCollisionPenalty() > 0) || P2.getCollisionPenalty() > 0 || burden.isResting())
		{
			return;
		}

		//If either player is fatigued do not incremente maxVelocity
		if (P1.getFatigue() || P2.getFatigue()) 
		{
			return;
		}


		maxVelocity += 15;
		//slider
		velocityValue.text = "Velocity:" + maxVelocity.ToString();
		velocityDisplay.value = maxVelocity;
	}

	public float getMaxVelocity()
	{
		return maxVelocity;
	}

	public void setMaxVelocity (float newValue)
	{
		maxVelocity = newValue;
	}

	//If in contact with gameWall, pop forward
	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.name == "GameWall") 
		{
			popForward ();
		}
	}

}