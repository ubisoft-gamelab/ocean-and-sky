using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;


/**
 * Class which controls behaviour of Player
 * Handles collisions, Bearer and Escort logic and Motion
 * Handles Throw and Catch mechanics, Stamina, SlipStream
 * Handles position Constraints in the Game World
 * Handles Push and Pull Artefact interactions
 * 
 */
public class Player : MonoBehaviour {

	public Player otherPlayer;
	public Burden burden;
	public GameWall gameWall;

	Rigidbody rigidBody;

	Transform slipStream;

	Vector3 dashDirection;

	public PlaneBehaviour planeOne;
	public PlaneBehaviour planeTwo;

	//TODO Implement dashInput and Dash Mechanic
	KeyCode upInput;
	KeyCode downInput;
	KeyCode leftInput;
	KeyCode rightInput;
	KeyCode catchInput;
	KeyCode throwInput;
	KeyCode dashInput;

	public bool isPlayer1;

	public bool isNeither; // Checks if neither Bearer nor Escort
	public bool isBearer;
	public bool isEscort;
	public bool canCatch;
	public bool canThrow;
	bool isFatigued;
	bool isDashing;
	bool inSlipStream;
	bool hitPushArtefactLeft;
	bool hitPushArtefactRight;
	bool isFlickering;

	float forwardPosition;
	float middlePosition;
	float rearPosition;
	float repulsionForce;
	float verticalMotion;
	float horizontalMotion;
	float dashMotion;
	float collisionCost;
	float dashCost;
	float eps;
	float bearerCoeff;

	public float stamina;
	float checkpointStamina;

	int maxHeight;
	int minHeight;
	int maxLeft;
	int maxRight;


	public int collisionPenalty;
	bool maxPenalty;


    // Use this for initialization
    void Start () {

		dashDirection = Vector3.zero;
		collisionPenalty = 1;

		// Set position constraints in the game world
		maxHeight = 600;
		minHeight = 35;
		maxLeft = 2100;
		maxRight = 5000;
		eps = 40f;

		// Positions where Player is sent to when Escort, Bearer or Neither
		forwardPosition = -4500f;
		middlePosition = -4600f;
		rearPosition = -4700f;

		//Used to repulse Player on contact with PushArtefact
		repulsionForce = 500f;

		//Stamina ranges from 0 to 50
		stamina = 50f;
		checkpointStamina = stamina;
		collisionCost = 0.1f;

		//Set stamina cost of dashing
		dashCost = 3f;

		// Gets the SlipStream and initially sets it to inactive
		slipStream = gameObject.transform.GetChild (0);
		//slipStream.GetComponent<BoxCollider> ().enabled = false;
		//transform.GetChild (1).gameObject.SetActive (false);

		// Gets the RigidBody of Player. clamp velocity
		rigidBody = GetComponent<Rigidbody> ();


		// Set all boolean values
		isNeither = true;
		maxPenalty = false;
		isBearer = false;
		isEscort = false;
		isFatigued = false;
		inSlipStream = false;
		hitPushArtefactLeft = false;
		hitPushArtefactRight = false;


		/**
		 * Set up controls
		 * Arrow keys for P1
		 * WASD for P2
		 */
		if (isPlayer1) 
		{
			upInput = KeyCode.UpArrow;
			downInput = KeyCode.DownArrow;
			leftInput = KeyCode.LeftArrow;
			rightInput = KeyCode.RightArrow;
			catchInput = KeyCode.RightShift;
			throwInput = KeyCode.P;
			dashInput = KeyCode.Space;
		} 

		else
		{
			upInput = KeyCode.W;
			downInput = KeyCode.S;
			leftInput = KeyCode.A;
			rightInput = KeyCode.D;
			catchInput = KeyCode.Tab;
			throwInput = KeyCode.C;
			dashInput = KeyCode.R;
		}

		 
		// Decrement the collisionPenalty every two seconds
		InvokeRepeating ("minusCollisionPenalty", 1f, 2f);

		// Decrement Stamina for every second the Player is Bearer
		InvokeRepeating ("updateStamina", 1f, 1f);
	}
	
	// Update is called once per frame
	void Update () {

		//Make motion of player scale by maxVelocity 
		verticalMotion = 0.10f * planeOne.getMaxVelocity();
		horizontalMotion = 0.10f * planeOne.getMaxVelocity ();

		dashMotion = 7f * verticalMotion;

		//TODO: Clamp Positoin of Player to never go too low

		//Slows the Beaerer's movements if not in slipstream
		if (isBearer && !inSlipStream)
			bearerCoeff = 0.4f;
		else if (isBearer && inSlipStream) bearerCoeff = 1f;
		else bearerCoeff = 1f;


		// Clamp Player velocity to prevent launching off screen from collision force
		rigidBody.velocity = Vector3.ClampMagnitude(rigidBody.velocity, 1f);

		// Clamp Player position to within camera ViewPort
		cameraConstrain ();

		// If collisionPenalty exceeds 10 set maxPenalty to true
		if (collisionPenalty >= 10) maxPenalty = true;

		//If Player hits a PushArtefact on the left. Repulse Player towards the right
		if (hitPushArtefactLeft) repulseRight();

		//If Player hits PushArtefact on the right. Repulse the Player towards the left
		if (hitPushArtefactRight) repulseLeft ();

		// Handle Player input
		handleInput ();

		//Handle Player position based on isBearer, isEscort or isNeither
		handlePosition ();
	}
		

	// Increment collisionPenalty when colliding with object
	public void addCollisionPenalty()
	{
		//If at maxPenalty, do not increment penaly counter any more.
		if (maxPenalty) return;
	
		collisionPenalty += 1;
	}


	//Called every two seconds. Decrements penalty if greater than zero and less than ten
	public void minusCollisionPenalty()
	{
		collisionPenalty --;
	
		if (collisionPenalty <= 0) 
		{
			maxPenalty = false;
			collisionPenalty = 0;
		}
	}

	public int getCollisionPenalty()
	{
		return collisionPenalty;
	}

	public bool getMaxPenalty()
	{
		return maxPenalty;
	}

	void cameraConstrain()
	{
		/** 
		 * Clamp the position of the Player to CameraViewPort and .1 buffer zone
		 * Escort has a tighter clamp because it is further up in the screen
		 */
		if (isEscort) 
		{
			Vector3 pos = Camera.main.WorldToViewportPoint (transform.position);
			pos.x = Mathf.Clamp (pos.x, 0.24f, 0.76f);
			pos.y = Mathf.Clamp (pos.y, 0.24f, 0.76f);
			transform.position = Camera.main.ViewportToWorldPoint (pos);
		}

		if (isBearer || isNeither) 
		{
			Vector3 pos = Camera.main.WorldToViewportPoint (transform.position);
			pos.x = Mathf.Clamp (pos.x, 0.1f, 0.9f);
			pos.y = Mathf.Clamp (pos.y, 0.1f, 0.9f);
			transform.position = Camera.main.ViewportToWorldPoint (pos);
		}
	}


	void handleInput()
	{
		/* TODO Have movement controls be a percentage of maxVelocity, so controls feel more responsive */

		// Can only throw if this player is the bearer, and is in slipstream
		if (isBearer && inSlipStream) canThrow = true;

		/// UPInput Handler
		if (Input.GetKey(upInput)) 
		{ 
			
			if ((transform.position.y <= maxHeight-eps) && Input.GetKeyDown(dashInput) && !isDashing)
			{
				stamina -= dashCost;
				StartCoroutine("PlayerDash", new object[]{Time.time, upInput});
			}



			else if (transform.position.y <= maxHeight)
			{
				transform.Translate (Vector3.up * Time.deltaTime * verticalMotion * bearerCoeff);
			}
		}

		// DOWNInput Handler
		if (Input.GetKey(downInput)) 
		{
			if ((transform.position.y >= minHeight+eps) && Input.GetKeyDown(dashInput) && !isDashing)
			{
				stamina -= dashCost;
				StartCoroutine("PlayerDash", new object[] { Time.time, downInput });
			}


			if ( transform.position.y >= minHeight)
			{
				transform.Translate (Vector3.down * Time.deltaTime * verticalMotion * bearerCoeff);
			}
		}

		// LEFTInput Handler
		if (Input.GetKey(leftInput)) 
		{

			if ((transform.position.x >= maxLeft+eps) && Input.GetKeyDown(dashInput) && !isDashing)
			{
				stamina -= dashCost;
				StartCoroutine("PlayerDash", new object[] { Time.time, leftInput});
			}


			if (transform.position.x >= maxLeft) 
			{
				transform.Translate (Vector3.left * Time.deltaTime * horizontalMotion * bearerCoeff);
			}
		}

		// RIGHTInput Handler 
		if (Input.GetKey(rightInput)) 
		{
			if ((transform.position.x <= maxRight-eps) && Input.GetKeyDown(dashInput) && !isDashing)
			{
				stamina -= dashCost;
				StartCoroutine("PlayerDash", new object[] { Time.time, rightInput });
			}


			if (transform.position.x <= maxRight) 
			{
				transform.Translate (Vector3.right * Time.deltaTime * horizontalMotion * bearerCoeff); 
			}
		}

		// CATCHInput Handler 
		if (Input.GetKeyDown(catchInput) && canCatch && !isFatigued)
		{
			catchBurden();
			isNeither = false;
			otherPlayer.isNeither = false;
		}

		/** 
		 * THROWInput Handler 
		 * Check when throwInput is pressed, and if the Player is currently in possession of the Burden 
		**/
		if (Input.GetKeyDown(throwInput) && isBearer && otherPlayer.isEscort && canThrow)
		{
			throwBurden();
		}

	}

	IEnumerator PlayerDash(object[] parameters)
	{
		isDashing = true;

		var timeKeyPressed = (float)parameters[0];
		var key = (KeyCode)parameters[1];
		int accelerationReduction = 1;

		if (key == upInput) dashDirection = Vector3.up * Time.deltaTime;
		else if (key == downInput) dashDirection = Vector3.down * Time.deltaTime;
		else if (key == leftInput) dashDirection = Vector3.left * Time.deltaTime;
		else if (key == rightInput) dashDirection = Vector3.right * Time.deltaTime;



		while (Time.time - timeKeyPressed < 0.5f)
		{
			accelerationReduction += 1;
			if (Input.GetKey(key)) transform.Translate(dashDirection * (dashMotion / accelerationReduction));
			yield return new WaitForEndOfFrame();
		}

		isDashing = false;
	}

	void handlePosition()
	{
		// Move forward if isEscort and other player isBearer
		if (isEscort && otherPlayer.isBearer) 
		{
			moveForward();
		}

		// Move back if isBearer and not inSlipStream and if other player isEscort.
		if (isBearer && otherPlayer.isEscort && !inSlipStream) 
		{
			moveBackward();
		}

		// Move to middle if isBearer and inSlipStream and if other player isEscort.
		if (isBearer && otherPlayer.isEscort && inSlipStream)
		{
			moveMiddle();
		}

		// Move to middle if isNeither a Bearer nor an Escort
		if (isNeither) 
		{
			moveMiddle();
		}
	}

	// Moves Player forward
	void moveForward()
	{
		transform.position = new Vector3 (transform.position.x, transform.position.y, forwardPosition);
	}


	// Moves Player to middle
	void moveMiddle()
	{
		transform.position = new Vector3 (transform.position.x, transform.position.y, middlePosition);
	}

	// Moves Player backwards
	void moveBackward()
	{
		transform.position = new Vector3 (transform.position.x, transform.position.y, rearPosition);
	}

	// Catches the Burden if in range to catch
	void catchBurden()
	{
		// Make the Burden's parent whichever Player catches it
		burden.gameObject.transform.parent = this.transform;
		burden.inPossession ();

		// Make sure Player is now Bearer, is no longer Escort and can no longer catch
		isBearer = true;
		isEscort = false;
		canCatch = false;

		// Other Player becomes  Escort and other Player's slipStream is activated
		otherPlayer.isEscort = true;
		otherPlayer.activateSlipStream ();

		//Disable my SLipstream
		deactivateSlipStream();

	}

	void throwBurden()
	{
		// Burden is no longer the child of either Player
		burden.gameObject.transform.parent = null;
		burden.notInPossession ();

		// Player is not Bearer, Player isNeither Bearer nor Escort, Player cannot Throw or Catch
		isBearer = false;
		isNeither = true;
		canThrow = false;
		canCatch = false;

		//Set burden.isThrown to true;
		burden.setThrown();

	}

	void updateStamina()
	{
		//If Player runs out of Stamina completely
		if (stamina < 0) 
		{
			stamina = 0;
			isFatigued = true;

			// Burden is no longer the child of either Player
			burden.gameObject.transform.parent = null;
			burden.notInPossession ();

			//Player becomes isNeither
			isBearer = false;
			isEscort = false;
			isNeither = true;

			//OtherPlayer becomes isNeither
			otherPlayer.isBearer = false;
			otherPlayer.isEscort = false;
			otherPlayer.isNeither = true;

			gameWall.fatigueReached ();
			return;
		}

		//If fully replenished, bound stamina to 50
		if (stamina > 50) 
		{
			stamina = 50;
			isFatigued = false;
			return;
		}

		//If a Bearer, deplete stamina
		if (isBearer) stamina -= 0.5f + (gameWall.getDepletionRate());

		//If not a Bearer, increase stamina
		else stamina++;
	}

	public float getStamina()
	{
		return stamina;	
	}

	//Sets the checkPointStamina to the Player's current stamina at the time
	public void setCheckpointStamina(float newVal)
	{
		checkpointStamina = newVal;
		isFatigued = false;
	}

	public void staminaToCheckpointVal()
	{
		stamina = checkpointStamina;
	}

	public bool getFatigue()
	{
		return isFatigued;
	}
	
	// Sets the SlipStream (Player's first child), to active 
	public void activateSlipStream()
	{
		slipStream.GetComponent<BoxCollider> ().enabled = true;
		transform.GetChild (1).gameObject.SetActive (true);
	}

	// Sets the SlipStream (Player's first child), to inactive 
	public void deactivateSlipStream()
	{
		slipStream.GetComponent<BoxCollider> ().enabled = false;
		transform.GetChild (1).gameObject.SetActive (false);
	}
		
	// Repulse Player towards the right.
	void repulseRight()
	{
		transform.Translate(Vector3.forward * Time.deltaTime * repulsionForce ); 
		Camera.main.transform.Translate (Vector3.right * Time.deltaTime * repulsionForce * 1.1f);
	}

	// Repulse Player towards the left.
	void repulseLeft()
	{
		transform.Translate(Vector3.back * Time.deltaTime * repulsionForce); 
		Camera.main.transform.Translate (Vector3.left * Time.deltaTime * repulsionForce * 1.1f);
	}

	// Reset Artefact Properties after a stageFormation has collided with gameWall
	public void resetArtefactProperties()
	{
		hitPushArtefactLeft = false;
		hitPushArtefactRight = false;
	}

	//Reset Player back to isNeither
	public void resetPlayerType()
	{
		isBearer = false;
		isEscort = false;
		isNeither = true;
	}

	void OnCollisionEnter(Collision other)
	{	
		// Check if collided with 'Obstacle'. Adds collisionPenalty
		if (other.gameObject.tag == "Obstacle") {

			isFlickering = true;
			StartCoroutine ("FlickerPlayer");

			//Decrease stamina on collision
			stamina -= collisionCost;
			addCollisionPenalty ();
		}
	}


	IEnumerator FlickerPlayer()
	{
		var mat = transform.GetChild (3).GetComponent<SkinnedMeshRenderer> ().material;
		//var mat = gameObject.GetComponent<Renderer>().material;
		var color = mat.color;

		while (isFlickering)
		{
			canThrow = false;
			var vignette = Camera.main.GetComponent<VignetteAndChromaticAberration>();
			vignette.intensity = 0.35f;
			mat.EnableKeyword("_ALPHATEST_ON");
			mat.EnableKeyword("_ALPHABLEND_ON");
			color.a = 0;
			mat.color = color;
		
			yield return new WaitForSeconds(0.15f);
			vignette.intensity = 0.23f;
			mat.DisableKeyword("_ALPHATEST_ON");
			mat.DisableKeyword("_ALPHABLEND_ON");
			color.a = 1;
			mat.color = color;
		
			yield return new WaitForSeconds(0.15f);
		}


	}

	void OnCollisionExit(Collision other)
	{
		StartCoroutine("FlickerExtension", Time.time);
	}

	IEnumerator FlickerExtension(float timeHit)
	{
		while (Time.time - timeHit < 0.5f)
		{
			yield return null;
		}

		isFlickering = false;
		canThrow = true;
	}

	void OnTriggerEnter(Collider other)
	{
	
		// Check if there is collision with a PushArtefact on the left side of the screen
		if (other.gameObject.tag == "PushArtefactLeft") 
		{
			// Check if Player 1 is the Bearer and is inSlipStream
			if (isPlayer1 && isBearer && inSlipStream) 
			{	
				gameWall.setArtefactForce ();
				hitPushArtefactLeft = true;
				otherPlayer.hitPushArtefactLeft = true;
			}
		}

		// Check if there is a collision with a PushArtefact on the right side of the screen
		if (other.gameObject.tag == "PushArtefactRight") 
		{
			// Check if Player 1 is the Bearer and is inSlipStream
			if (isPlayer1 && isBearer && inSlipStream) 
			{
				gameWall.setArtefactForce ();
				hitPushArtefactRight = true;
				otherPlayer.hitPushArtefactRight = true;
			}
		}

		// Check if colliding with Pull Artefact
		if (other.gameObject.tag == "PullArtefact") 
		{
			// Check if Player 2 is the Bearer and is inSlipStream
			if (!isPlayer1 && isBearer && inSlipStream) 
			{
				gameWall.setArtefactForce ();
			}
		}

	}

	void OnTriggerStay(Collider other)
	{
		//If inSlipStream, set inSlipStream to true
		if (other.gameObject.name == "SlipStream") inSlipStream = true;

		//If in Burden's collider, set canCatch to true
		if (other.gameObject.name == "Burden") canCatch = true;
		

	}

	void OnTriggerExit(Collider other)
	{
		// If Player leaves Burden collider, can no longer catch
		if (other.gameObject.name == "Burden") canCatch = false;
		

		// If Player leaves Slipstream, can no longer throw
		if (other.gameObject.name == "SlipStream") 
		{
			canThrow = false;
			inSlipStream = false;
		}

	}
}
