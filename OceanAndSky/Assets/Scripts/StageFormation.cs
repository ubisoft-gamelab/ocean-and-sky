using UnityEngine;
using System.Collections;

/*
 * Class which contains stageFormation information
 */
public class StageFormation : MonoBehaviour {

	public Player P1;
	public Player P2;

	public GameWall gameWall;

	public PlaneBehaviour firstPlane;
	public PlaneBehaviour secondPlane;

	float maxVelocity;
	float resetPosition;

	public bool selectedPart;



	// Use this for initialization
	void Start () {

		resetPosition = 3500f;



	}
	
	// Update is called once per frame
	void Update () {
	
		// Moves stageFormation if it has been selected by gameWall
		if (selectedPart) 
		{
			move ();	
		}

		// Else, deactivate the stageFormation
			//else gameObject.SetActive (false);


	}

	 
	//Pops the Plane forward to the resetPosition after colliding with gameWall
	void popForward()
	{
		transform.position = new Vector3(transform.position.x, transform.position.y, resetPosition);
	}


	// Moves the stageFormation forwards constantly
	public void move()
	{
		transform.Translate (Vector3.left * Time.deltaTime * firstPlane.getMaxVelocity()*0.9f*gameWall.getArtefactForce());
	}
		

	void OnTriggerStay(Collider other)
	{
		/** 
		 * On collision with GameWall, pop back to resetPosition to begin looping again
		 * Set the current stageFormation to false
		 * Tell GameWall to pick another random StageFormation
		 */
		if (other.gameObject.name == "GameWall") {
			Debug.Log ("GameWall hit");

			//Reset any changes to ArtefactForces
			gameWall.resetArtefactForce();
			P1.resetArtefactProperties ();
			P2.resetArtefactProperties ();

			//Move the stageFormation fowards
			popForward ();

			//Increase maxVelocity
			firstPlane.increaseMaxVelocity ();
			secondPlane.increaseMaxVelocity ();

			//Tell gameWall to find a new, random, stageFormation
			gameWall.update ();
			selectedPart = false;
			gameObject.SetActive (false);
		}
	}

}
