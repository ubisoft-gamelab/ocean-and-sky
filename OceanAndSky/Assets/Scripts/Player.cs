using UnityEngine;
using System.Collections;


/**
 * Class which controls behaviour of Player
 * Handles collisions, Bearer and Escort logic and motion
 */
public class Player : MonoBehaviour {

	/*
	 * Checks that neither player is Bearer or Escort
	 * True only in the beginning or if the Burden is thrown, and no one catches it.
	 */
	public bool isNeither;
    public float speed;

	public Player otherPlayer;
	public Burden burden;

	Transform slipStream;

	public PlaneBehaviour planeOne;
	public PlaneBehaviour planeTwo;

	public KeyCode upInput;
	public KeyCode downInput;
	public KeyCode leftInput;
	public KeyCode rightInput;
	public KeyCode accelerateInput;
	public KeyCode catchInput;
	public KeyCode throwInput;

	public bool isPlayer1;

	public bool isBearer;
	public bool isEscort;
	public bool canCatch;
	public bool canThrow;

	bool inSlipStream;

	/** Implement Stamina **/
	/** If I am bearer, stamina goes down
	 * Lose 1 stamina for every second holding burden
	 * If 0 stamina, set burden parent = null
	 * Player can not catch Burden until stamina back at 50
	 */
	public int Stamina;

	int weight;

	public int minHeight;


	/** Collision Attributes **/
	public int collisionPenalty;
	bool maxPenalty;

	// Use this for initialization
	void Start () {
	
		weight = 20;
		minHeight = 30;

		isNeither = true; 

		slipStream = gameObject.transform.GetChild (0);
		slipStream.GetComponent<BoxCollider> ().enabled = false;

		collisionPenalty = 0;
		maxPenalty = false;

		isBearer = false;
		isEscort = false;
		inSlipStream = false;

		/**
		 * Set up controls
		 * Arrow keys used for P1
		 * WASD used for P2
		 */
		if (isPlayer1) {
			upInput = KeyCode.UpArrow;
			downInput = KeyCode.DownArrow;
			leftInput = KeyCode.LeftArrow;
			rightInput = KeyCode.RightArrow;
			catchInput = KeyCode.RightShift;
			throwInput = KeyCode.LeftShift;
		} else {
			upInput = KeyCode.W;
			downInput = KeyCode.S;
			leftInput = KeyCode.A;
			rightInput = KeyCode.D;
			catchInput = KeyCode.Tab;
			throwInput = KeyCode.C;
		}

		/** 
		 * If collisionPenalty is higher than 0
		 * Then decrement the collisionPenalty every two seconds
		 */

		InvokeRepeating ("minusCollisionPenalty", 1f, 2f);
	}
	
	// Update is called once per frame
	void Update () {
	
	
		/** Apply gravity if heigher than the minHeight */
		if (transform.position.y > minHeight) 
		{
			gravity ();
		}

		/**
		 * If collisionPenalty is >= 10
		 * Then set maxPenalty to true
		 * While maxPenalty is true, any further collisions will not call addCollisionPenalty
		 * minusPenalty will be continuously called until collisionPenalty is <=0
		 * If maxPenalty is true - it will be set to false as well
		 */
		if (collisionPenalty >= 10) 
		{
			maxPenalty = true;
		}


		/* Can only throw if this player is the bearer, and is in Tier 0 */
		if (isBearer && inSlipStream)
		{
			canThrow = true;
		}

		/** Check when upInput is pressed **/
		if (Input.GetKey(upInput)) 
		{ 
			transform.Translate(Vector3.right * Time.deltaTime * speed); 
		}
		
		/** Check when downInput is pressed **/
		if (Input.GetKey(downInput)) 
		{ 
			if ( !(transform.position.y < minHeight))
			{
				transform.Translate(Vector3.left * Time.deltaTime * speed);
			}
		}
		
		/** Check when leftInput is pressed **/
		if (Input.GetKey(leftInput)) 
		{ 
			transform.Translate(Vector3.back * Time.deltaTime * speed);
		}
		

		/** Check when rightInput is pressed **/
		if (Input.GetKey(rightInput)) { 
			transform.Translate(Vector3.forward * Time.deltaTime * speed); 
		}

		/** Check when catchInput is pressed, and if the Player is within range of Burden **/
		if (Input.GetKeyDown(catchInput) && canCatch)
		{
			catchBurden();
			isNeither = false;
			otherPlayer.isNeither = false;
		}

        if (Input.GetKeyDown(accelerateInput)) {
            GameObject[] planes = GameObject.FindGameObjectsWithTag("Plane");
            foreach (GameObject plane in planes) {
                plane.GetComponent<PlaneBehaviour>().accelerate();
            }
        }

		/** Check when throwInput is pressed, and if the Player is currently in possession of the Burden **/
		if (Input.GetKeyDown(throwInput) && isBearer && otherPlayer.isEscort && canThrow)
		{
			Debug.Log("Received Throw Input");
			throwBurden();
		}

		/* 
		 * If this player is the Escort, and the other player is the bearer
		 * Then move this player to the forward tier
		 */
		if (isEscort && otherPlayer.isBearer) 
		{
			moveForward();
		}

		/*
		 * If this player is the Bearer, and the other player is the bearer
		 * And this player is not in SlipStream, then move this player to the back tier
		 */
		if (isBearer && otherPlayer.isEscort && !inSlipStream) 
		{
			moveBackward();
		}

		/* If this player is the bearer, and the other player is the bearer
		 * And this playaer is in SlipStream, then move this player to the Middle tier
		 */
		if (isBearer && otherPlayer.isEscort && inSlipStream)
		{
			moveMiddle();
		}

		/* If this player is neither Bearer nor Escort, then move to middle */
		if (isNeither) 
		{
			moveMiddle();
		}

	}


	void gravity()
	{
		transform.Translate (Vector3.left * Time.deltaTime * weight); 
	}

	//-------------------------TODO: Implement Collisions ------------------------------//
	/** 
	 * Called when Player collides with an obstacle
	 */
	public void addCollisionPenalty()
	{
		collisionPenalty++;
	}


	/** 
	 * Called every two seconds that the Player does not collide with another obstacle
	 * after having already collided at least once and collisionPenalty > 0
	 */
	public void minusCollisionPenalty()
	{
		if (collisionPenalty <= 0) 
		{
			maxPenalty = false;
			collisionPenalty = 0;
			return;
		}
		collisionPenalty --;
		otherPlayer.collisionPenalty --;
	}
	
	public bool getMaxPenalty()
	{
		return maxPenalty;
	}

	//-------------------------------------------------------------------------



	/*
	 * If this player is the Escort AND the other player is the Bearer
	 * Move this player forwards
	 */
	void moveForward()
	{
		transform.position = new Vector3 (-620, transform.position.y, transform.position.z);
	}

	/*
	 * If this player is the Bearer AND the other player is the Escort
	 * And this player is in Slipstream
	 * Move this player to the middle
	 */
	void moveMiddle()
	{
		transform.position = new Vector3 (-730, transform.position.y, transform.position.z);
	}

	/*
	 * If this player is the bearer AND the other player is the Escort
	 * And this player is not in Slipstream
	 * Move this player backwards
	 */
	void moveBackward()
	{
		transform.position = new Vector3 (-820, transform.position.y, transform.position.z);
	}

	void catchBurden()
	{
		burden.gameObject.transform.parent = this.transform;
		burden.inPossession ();
		isBearer = true;
		isEscort = false;
		canCatch = false;
		otherPlayer.isEscort = true;
		otherPlayer.activateChild ();
	}

	void throwBurden()
	{
		burden.gameObject.transform.parent = null;
		Debug.Log ("throwBurden");
		burden.notInPossession ();
		isBearer = false;
		//otherPlayer.isEscort = false;
		isNeither = true;
		canThrow = false;
		canCatch = false;
		//otherPlayer.isNeither = true;
	}

	/* Sets the SlipStream (Player's first child), to active */
	void activateChild()
	{
		slipStream.GetComponent<BoxCollider> ().enabled = true;
	}

	/* Sets the SlipStream (Player's first child), to inactive */
	void deactivateChild()
	{
		slipStream.GetComponent<BoxCollider> ().enabled = false;
	}

	void OnTriggerEnter(Collider other)
	{
		/** 
		 * Checks that gameObject collided with is an Obstacle
		 * and that maxPenalty is false at the time of collision
		 */
		if (other.gameObject.name == "Obstacle" && !maxPenalty)
		{
			addCollisionPenalty();
			otherPlayer.addCollisionPenalty();
			Debug.Log("Obstacle enter");
		}

	
		//other.gameObject.GetComponent<PullArtefact>();
		//other.gameObject.GetComponent<PushArtefact>();


	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.name == "SlipStream") 
		{
			inSlipStream = true;
		}

		if (other.gameObject.name == "Burden") 
		{
			canCatch = true;
			Debug.Log("Burden enter");
		}

	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Plane")
		{
			if ( isEscort || isNeither)
			{
				other.gameObject.GetComponent<PlaneBehaviour> ().leavePlane();
				Debug.Log("Exit Plane");
			}
		}

		if (other.gameObject.name == "Burden")
		{
			canCatch = false;
			Debug.Log("Burden exit");
		}

		if (other.gameObject.name == "SlipStream") 
		{
			canThrow = false;
			inSlipStream = false;
		}

	}
}
