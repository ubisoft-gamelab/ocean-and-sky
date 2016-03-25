using UnityEngine;
using System.Collections;

/*
 * Class which controls most meta reasoning
 * Handles Checkpoints
 * Contains knowledge of which stageFormation will next be released
 * Applies artefactForce to stageFormations, when Player collides with Push or Pull Artefact
 * Handles evolutionary progression curve in difficulty
 */
public class GameWall : MonoBehaviour {


	Player P1;
	Player P2;

	public Burden burden;
	public PlaneBehaviour planeOne;
	public PlaneBehaviour planeTwo;

	public GameObject levelDesignOne;
	public GameObject levelDesignTwo;
	public GameObject levelDesignThree;

	public GameObject maskingPlane;
	public GameObject secondWaterLayer;

	KeyCode checkpointInput;

	Vector3 hiddenPosition1;
	Vector3 hiddenPosition2;

	public int stagePartIndex;
	public int stagePart3Index;
	public bool updateStage;

	public bool sectionOne;
	public bool sectionTwo;
	public bool sectionThree;
	public bool transitSection;
	bool hasReachedCheckpointOne;
	bool hasReachedCheckpointTwo;
	bool hasReachedCheckpointThree;
	bool hasReachedCheckpointFour;
	bool hasReachedCheckpointFive;

	//For testing:
	public float planeVelocity;

	public float artefactForce;
	public float depletionRate;

	public float transitVelocity;
	public float sectionTwoVelocity;
	public float sectionThreeVelocity;

	float firstThreshold;
	float secondThreshold;
	float thirdThreshold;
	float fourthThreshold;
	float fifthThreshold;
	float sixthThreshold;
	float speedOfSound;

	float levelCapOne;
	float levelCapTwo;

	public float fatiguedVelocity;
	public float checkPointVelocity;


	// Use this for initialization
	void Start () {


		P1 = GameObject.Find ("FirstPlayer").GetComponent<Player> ();
		P2 = GameObject.Find ("SecondPlayer").GetComponent<Player> ();

		artefactForce = 1;
		depletionRate = 0;

		//Set fatiguedVelocity and checkPointVelocity equal to initial maxVelocity
		fatiguedVelocity = 1500f;
		checkPointVelocity = 1500f;

		/** TODO Implement threshold values when sectionTwo is complete
		 * TODO If either player is fatigued, set maxVelocity to the current checkPointVelocity */
		firstThreshold = 1750;
		secondThreshold = 2250;
		thirdThreshold = 3000;
		fourthThreshold = 3750;
		fifthThreshold = 4250;
		sixthThreshold = 4500;
		speedOfSound = 4750;

		levelCapOne = 8;
		levelCapTwo = 8;


		transitVelocity = 2800f;
		sectionTwoVelocity = 3000f;
		sectionThreeVelocity = 4500f;

		//Set sectionOne to true initially
		sectionOne = true;
		sectionTwo = false;
		transitSection = false;

		hasReachedCheckpointOne = false;
		hasReachedCheckpointTwo = false;
		hasReachedCheckpointThree = false;
		hasReachedCheckpointFour = false;
		hasReachedCheckpointFive = false;


		checkpointInput = KeyCode.Escape;

		//Sets levelDesignOne stageFormations to inactive
		Transform allChildren1 = levelDesignOne.gameObject.GetComponentInChildren <Transform>();
		foreach (Transform child in allChildren1) 
		{
			child.gameObject.SetActive (false);
			child.GetComponent<StageFormation> ().selectedPart = false;
		}

		/* TODO: Sets levelDesignTwo stageFormations to inactive. Implement when sectionTwo is designed*/
		Transform allChildren2 = levelDesignTwo.gameObject.GetComponentInChildren <Transform>();
		foreach (Transform child in allChildren2) 
		{
			child.gameObject.SetActive (false);
			child.GetComponent<StageFormation> ().selectedPart = false;
		} 

		levelDesignThree.gameObject.transform.GetChild (0).gameObject.SetActive (false);

		//TODO: Loop and set all audio sources to inactive. Begin new tracks as velocity increases

		stagePart3Index = 0;
		updateStage = true;
	}

	// Update is called once per frame
	void Update () {

		planeVelocity = planeOne.getMaxVelocity ();

		if (Input.GetKey(checkpointInput)) activateCheckPoint ();

		if (planeOne.getMaxVelocity () > transitVelocity && planeOne.getMaxVelocity() < sectionTwoVelocity)
		{
			sectionOne = false;
			sectionTwo = false;
			sectionThree = false;
			transitSection = true;
		}
		//If sectionTwo velocity is reached, make sectionTwo = true
		if (planeOne.getMaxVelocity () > sectionTwoVelocity) 
		{
			sectionOne = false;
			sectionTwo = true;
			sectionThree = false;
			transitSection = false;
		}

		//If sectionThree velocity is reached, make sectionThree = true
		if (planeOne.getMaxVelocity () > sectionThreeVelocity) 
		{
			sectionOne = false;
			sectionTwo = false;
			sectionThree = true;
			transitSection = false;
		}


		//Ends the game if greater or equal to max velocity
		if (planeOne.getMaxVelocity () > speedOfSound) 
		{
			//TODO: End the game.

			//Kill all audio
			//Something happens to players
			//Something happens to Burden
		}


		// If the alert is received, load a new random stage formation
		if (updateStage) 
		{
			setStagePart ();
		}


	}



	// Get a new random stageFormation
	void setStagePart()
	{
		if (sectionOne) {
			stagePartIndex = (int)Random.Range (0, levelCapOne);
			//stagePartIndex = 0;
			levelDesignOne.transform.GetChild (stagePartIndex).gameObject.SetActive (true);
			levelDesignOne.transform.GetChild (stagePartIndex).GetComponent<StageFormation> ().selectedPart = true;
			updateStage = false;

			//burden.aiAgent (1, stagePartIndex);
		} 

		else if (transitSection) 
		{
			levelDesignTwo.transform.GetChild (0).gameObject.SetActive (true);
			levelDesignTwo.transform.GetChild (0).GetComponent<StageFormation> ().selectedPart = true;
			updateStage = false;
			LowerObject.fall ();
			SkyBoxCamera.ChangeSkyboxNight ();

		}


		else if (sectionTwo) {

			stagePartIndex = (int)Random.Range (1, levelCapTwo);

			//stagePartIndex = 0;
			levelDesignTwo.transform.GetChild (stagePartIndex).gameObject.SetActive (true);
			levelDesignTwo.transform.GetChild (stagePartIndex).GetComponent<StageFormation> ().selectedPart = true;
			updateStage = false;

			//burden.aiAgent (2, stagePartIndex);
		} 

		else if (sectionThree) 
		{

			levelDesignThree.transform.GetChild (stagePart3Index).gameObject.SetActive (true);
			levelDesignThree.transform.GetChild (stagePart3Index).GetComponent<StageFormation> ().selectedPart = true;
			updateStage = false;
			planeOne.setMaxVelocity (planeOne.getMaxVelocity () + 50);
			//stagePart3Index = 1;
			SkyBoxCamera.ChangeSkyboxDSBWP ();

			//burden.aiAgent (3, stagePartIndex);
		}

		//TODO If maxVelocity has reached a threshold, increase difficulty slightly. Store checkpoint
		handleCheckpoint ();
	}

	// Alerts when it is time to load a new, random StageFormation to gameWorld
	public void updateGame()
	{
		updateStage = true;
	}

	public void updateGame(int flag)
	{
		hiddenPosition1 = new Vector3 (planeOne.transform.position.x, -50000, planeOne.transform.position.z);
		hiddenPosition2 = new Vector3 (planeTwo.transform.position.x, -50000, planeTwo.transform.position.z);

		updateStage = true;
		maskingPlane.gameObject.SetActive (false);
		secondWaterLayer.gameObject.SetActive (false);
		planeOne.transform.position = hiddenPosition1;
		planeTwo.transform.position = hiddenPosition2;
	}
	//Depletion rate of Player stamina. Steadily increases over time
	public float getDepletionRate()
	{
		return depletionRate;
	}

	//Called by Player when Stamina reaches 0
	public void fatigueReached()
	{
		planeOne.setMaxVelocity(fatiguedVelocity);
		planeTwo.setMaxVelocity(fatiguedVelocity);
	}


	//Stores updates CheckpointVelocity and checkpointStamina
	public void handleCheckpoint()
	{
		//If within First and Second Threshold
		if (planeOne.getMaxVelocity () >= firstThreshold && planeOne.getMaxVelocity () < secondThreshold)
			checkpointOne ();
		//If within Second and Third threshold
		else if ((planeOne.getMaxVelocity () >= secondThreshold) && (planeOne.getMaxVelocity () < thirdThreshold))
			checkpointTwo ();
		//If within third and fourth threshold
		else if ((planeOne.getMaxVelocity () >= thirdThreshold) && planeOne.getMaxVelocity () < fourthThreshold)
			checkpointThree ();
		//If within fourth and fifth threshold
		else if ((planeOne.getMaxVelocity () >= fourthThreshold) && planeOne.getMaxVelocity () < fifthThreshold)
			checkpointFour ();
		//if within fifth and sixth threshold
		else if (planeOne.getMaxVelocity () >= fifthThreshold && planeOne.getMaxVelocity () < sixthThreshold)
			checkpointFive ();

	}

	public void checkpointOne()
	{
		if (hasReachedCheckpointOne)
			return;
		else 
		{
			depletionRate += 0.2f;
			checkPointVelocity = firstThreshold;
			fatiguedVelocity = checkPointVelocity / 2;
			P1.setCheckpointStamina (45);
			P2.setCheckpointStamina (45);

			levelCapOne = 8;
			hasReachedCheckpointOne = true;;
		}
	}

	public void checkpointTwo()
	{
		if (hasReachedCheckpointTwo)
			return;
		else 
		{
			depletionRate += 0.2f;
			checkPointVelocity = secondThreshold;
			fatiguedVelocity = checkPointVelocity / 2;
			P1.setCheckpointStamina (40);
			P2.setCheckpointStamina (40);
			levelCapOne = levelDesignOne.transform.childCount;
			hasReachedCheckpointTwo = true;;
		}
	}

	public void checkpointThree()
	{
		if (hasReachedCheckpointThree)
			return;
		else 
		{
			depletionRate += 0.2f;
			checkPointVelocity = thirdThreshold;
			fatiguedVelocity = checkPointVelocity / 2;
			P1.setCheckpointStamina (35);
			P2.setCheckpointStamina (35);

			levelCapTwo = 6;
			hasReachedCheckpointThree = true;
		}
	}

	public void checkpointFour()
	{
		if (hasReachedCheckpointFour)
			return;
		else 
		{
			depletionRate += 0.2f;
			checkPointVelocity = fourthThreshold;
			fatiguedVelocity = checkPointVelocity / 2;
			P1.setCheckpointStamina (30);
			P2.setCheckpointStamina (30);

			levelCapTwo = 10;
			hasReachedCheckpointFour = true;
		}
	}

	public void checkpointFive()
	{
		if (hasReachedCheckpointFive)
			return;
		else 
		{
			depletionRate += 0.2f;
			checkPointVelocity = fifthThreshold;
			fatiguedVelocity = checkPointVelocity / 2;
			P1.setCheckpointStamina (20);
			P2.setCheckpointStamina (20);
			levelCapTwo = levelDesignTwo.transform.childCount;
			hasReachedCheckpointFive = true;
		}
	}



	/* When checkPoint is selected */
	public void activateCheckPoint()
	{
		//Set Plane Values to checkPointVelocity
		planeOne.setMaxVelocity(checkPointVelocity);
		planeTwo.setMaxVelocity(checkPointVelocity);

		//Set Stamina to checkpoint Stamina
		P1.staminaToCheckpointVal ();
		P2.staminaToCheckpointVal ();


		burden.gameObject.transform.parent = null;
		burden.notInPossession ();

		//Restore the Burden to restPosition
		burden.restorePosition ();

	}

	//Scalar value that modifies maxVelocity in StageFormation
	public void setArtefactForce()
	{
		artefactForce = 5f;
	}

	//Returns the artefact force
	public float getArtefactForce()
	{
		return artefactForce;
	}

	//Resets the artefact force
	public void resetArtefactForce()
	{
		artefactForce = 1f;
	}
}