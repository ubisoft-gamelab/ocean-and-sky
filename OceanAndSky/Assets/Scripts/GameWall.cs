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

	public GameObject levelDesignOne;
	//public GameObject levelDesignTwo;


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

		sectionOne = true;
		sectionTwo = false;

		//Start stageFormations at the first stageFormation of the first LevelDesign
		stagePartIndex = 0;
		levelDesignOne.transform.GetChild (stagePartIndex).GetComponent<StageFormation> ().selectedPart = true;
		updateStage = true;

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
			//stagePartIndex = (int)Random.Range (0, levelDesignOne.transform.childCount);
			stagePartIndex = 1;
			levelDesignOne.transform.GetChild (stagePartIndex).gameObject.SetActive (true);
			levelDesignOne.transform.GetChild (stagePartIndex).GetComponent<StageFormation> ().selectedPart = true;
			updateStage = false;
		}

		/* TODO: Implement when Section Two is Designed
		if (sectionTwo)
		{
			stagePartIndex = (int)Random.Range (0, levelDesignTwo.transform.childCount);
			levelDesignTwo.transform.GetChild (stagePartIndex).gameObject.SetActive (true);
			levelDesignTwo.transform.GetChild (stagePartIndex).GetComponent<StageFormation> ().selectedPart = true;
			updateStage = false;
		}*/

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

	public void setArtefactForce()
	{
		artefactForce = 7f;
	}

	public float getArtefactForce()
	{
		return artefactForce;
	}

	public void resetArtefactForce()
	{
		artefactForce = 1f;
	}
}
