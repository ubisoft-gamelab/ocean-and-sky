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

	public PlaneBehaviour planeOne;
	public PlaneBehaviour planeTwo;

	//Transform[] allChildren;
	public GameObject levelDesignOne;
	public GameObject levelDesignTwo;
	//public GameObject levelDesignThree;


	public int stagePartIndex;
	public bool updateStage;

	public bool sectionOne;
	public bool sectionTwo;

	public float artefactForce;
	public float depletionRate;
	//public float sectionTwoVelocity;
	float firstThreshold;
	float secondThreshold;
	float thirdThreshold;
	float fourthThreshold;
	float fifthThreshold;
	float sixthThreshold;
	float fatiguedVelocity;


	// Use this for initialization
	void Start () {

		artefactForce = 1;
		depletionRate = 0;
		fatiguedVelocity = 900f;

		/** TODO Implement threshold values when sectionTwo is complete
		firstThreshold =;
		secondThreshold =;
		thirdThreshold =;
		fourthThreshold =;
		fifthThreshold =;
		sixthThreshold =;
		*/

		sectionOne = false;
		sectionTwo = true;

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

		//For testing
		updateStage = true;
		//---

		//stagePartIndex = 0;
		//levelDesignOne.transform.GetChild (stagePartIndex).gameObject.SetActive (true);
		//levelDesignOne.transform.GetChild (stagePartIndex).GetComponent<StageFormation> ().selectedPart = true;

	}
	
	// Update is called once per frame
	void Update () {

	

		/* TODO Implement when sectionTwo is designed
		 * if (planeOne.getMaxVelocity () > sectionTwoVelocity) 
		{
			sectionOne = false;
			sectionTwo = true;
		}*/

		// If the alert is received, load a new random stage formation
		if (updateStage) 
		{
			setStagePart ();
		}
	}

	// Get a new random stageFormation
	void setStagePart()
	{
		if (sectionOne)
		{
			//TODO Dynamically change range of stage parts to be selected from as velocity increases

			stagePartIndex = (int)Random.Range (0, levelDesignOne.transform.childCount);
			//stagePartIndex = 2;
			levelDesignOne.transform.GetChild (stagePartIndex).gameObject.SetActive (true);
			levelDesignOne.transform.GetChild (stagePartIndex).GetComponent<StageFormation> ().selectedPart = true;
			updateStage = false;
		}

		/* TODO: Implement when Section Two is Designed*/
		else if (sectionTwo)
		{
			//stagePartIndex = (int)Random.Range (0, levelDesignTwo.transform.childCount);
			stagePartIndex = 3;
			levelDesignTwo.transform.GetChild (stagePartIndex).gameObject.SetActive (true);
			levelDesignTwo.transform.GetChild (stagePartIndex).GetComponent<StageFormation> ().selectedPart = true;
			updateStage = false;
		}

		//TODO If maxVelocity has reached a threshold, increase difficulty slightly
		//difficultyCurve ();
	}

	// Alerts when it is time to load a new, random StageFormation to gameWorld
	public void update()
	{
		updateStage = true;
	}

	/* TODO Set of checks to dynamically increase difficulty of game.
	void difficultyCurve()
	{
		//Increase depletion rate. Set fatiguedVelocity to the firstThreshold
		if (planeOne.getMaxVelocity >= firstThreshold) {
			depletionRate += 0.5f;
			fatiguedVelocity = firstThreshold;
		}

		if (planeOne.getMaxVelocity >= secondThreshold) {
			depletionRate += 0.5f;
			fatiguedVelocity = secondThreshold;
		}

		if (planeOne.getMaxVelocity >= thirdThreshold) {
			depletionRate += 0.5f;
			fatiguedVelocity = thirdThreshold;
		}
		if (planeOne.getMaxVelocity >= fourthThreshold) {
			depletionRate += 0.5f;
			fatiguedVelocity = fourthThreshold;
		}
		if (planeOne.getMaxVelocity >= fifthThreshold) {
			depletionRate += 0.5f;
			fatiguedVelocity = fifthThreshold;
		}

		if (planeOne.getMaxVelocity >= sixthThreshold) {
			depletionRate += 0.5f;
			fatiguedVelocity = sixthThreshold;
		}
	}*/

	//Depletion rate of Player stamina. Steadily increases over time
	public float getDepletionRate()
	{
		return depletionRate;
	}

	//Called by Player when Stamina reaches 0
	public void burdenDropped()
	{
		planeOne.setMaxVelocity(fatiguedVelocity);
		planeTwo.setMaxVelocity(fatiguedVelocity);
	}

	/* TODO Implement Checkpoint by reseting values to previously saved values 
	public void activateCheckPoint()
	{
		planeOne.setMaxVelocity(fatiguedVelocity);
		planeTwo.setMaxVelocity(fatiguedVelocity);


	}*/

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
