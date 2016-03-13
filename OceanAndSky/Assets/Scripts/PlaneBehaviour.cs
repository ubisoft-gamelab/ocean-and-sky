using UnityEngine;
using System.Collections;

/**
 * Class which moves Planes forwards to give the impression of motion in the game state.
 * When a plane moves out of viewing - behind the player camera - it will be snapped back into
 * a position in front of the camera to be looped again.
 * 
 * MaxVelocity exists in this class, because 'velocity' here is simply the rate at which the planes loop.
 */
public class PlaneBehaviour : MonoBehaviour {

	public Player P1;
	public Player P2;

	/** Translation Values **/
	bool hasExited;
	Vector3 currentPosition;
	float planeLength;

	/** Motion Values **/
	float maxVelocity;
    float currentVelocity;
    public float startingVelocity;
    public float increaseAmount;
	bool isMaxVelocity;

	/** Game Logic Variables **/
	bool hasCollided;

    /** Timing Variables **/
    float lastIncreaseTime;

	/** All Methods in file **/
	//public void leavePlane ()
	//void popForward()
	//public void move()
	//public void accelerate()
	//public void decelerate();
	//public float getSpeed();
	//public void setSpeedChanged
	//public void increaseMaxVelocity();

	void Start () {

		/** Set hasLeft = false in the beginning. Will be modified onTriggerExit **/
		hasExited = false;

		/** Get Length of Plane to be used in moving the Plane forwards **/
		planeLength = transform.localScale.x;

		/** Set maxVelocity **/
		maxVelocity = startingVelocity;
        currentVelocity = startingVelocity;

		/** Set isMaxVelocity = false **/
		isMaxVelocity = false;

		/** Set hasCollided = false **/
		hasCollided = false;
	}
	


	void Update () {
		/** Constantly move the plane forward **/
		move ();

		/** If we have exited the Box Collider, then pop the Plane Forward **/
		if (hasExited) { popForward (); }


	}

	

	/** Called when the Player has left the plane's box collider **/
	public void leavePlane()
	{
		currentPosition = transform.position;
		Debug.Log("planeLength is: " + planeLength);
		hasExited = true;
	}

	/** 
	 * Pops the Plane forward by 755 units on the X-axis, after Player has left the 
	 * Plane's box collider
	 */ 
	void popForward()
	{
		transform.position = new Vector3(2030, currentPosition.y, currentPosition.z);
		hasExited = false;
	}

	/**
	 * Moves the Plane forward incrementally by every frame
	 * This is to create the illusion of movement
	 */
	public void move()
	{
		transform.Translate (Vector3.left * Time.deltaTime * currentVelocity);

	}
	

	/** 
	 * increments maxVelocity every 5 seconds if Player has not collided in recent time.
     * If P1 has a collision penalty, we check if 5 seconds plus the penalty time has passed
     * since the last time the maxVelocity was increased. If yes, then we increase the maxVelocity
     * by increaseAmount, set lastIncreaseTime to Time.time, and reset P1.collisionPenalty to 0.
     * We assume here that all collision penalties are accumulated under the P1 object.
	 */
	public void increaseMaxVelocity() {
        if (Time.time > lastIncreaseTime + 5.0f + P1.collisionPenalty) {
            maxVelocity += increaseAmount;
            lastIncreaseTime = Time.time;
            P1.collisionPenalty = 0;
            Debug.Log("increaseMaxVelocity : maxVelocity Increased");
        }
    }

}
