using UnityEngine;
using System.Collections;

/*
 * Class which contains stageFormation information
 */
public class StageFormation : MonoBehaviour {

	Player P1;
	Player P2;

	public GameWall gameWall;

	public PlaneBehaviour firstPlane;
	public PlaneBehaviour secondPlane;

	float maxVelocity;
	float resetPosition;

	public bool selectedPart;


	// Use this for initialization
	void Start () {

		resetPosition = 3500f;
		P1 = GameObject.Find ("FirstPlayer").GetComponent<Player> ();
		P2 = GameObject.Find ("SecondPlayer").GetComponent<Player> ();

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
		if (gameObject.name == "Transition : Clouds") {
			transform.Translate (Vector3.up * Time.deltaTime * firstPlane.getMaxVelocity () * 0.9f * gameWall.getArtefactForce ());
		} else {
			transform.Translate (Vector3.left * Time.deltaTime * firstPlane.getMaxVelocity () * 0.9f * gameWall.getArtefactForce ());
		}
	}


	void OnTriggerStay(Collider other)
	{
		/** 
		 * On collision with GameWall, pop back to resetPosition to begin looping again
		 * Set the current stageFormation to false
		 * Tell GameWall to pick another random StageFormation
		 */
		if (other.gameObject.name == "GameWall") {

			//Reset any changes to ArtefactForces
			gameWall.resetArtefactForce ();
			P1.resetArtefactProperties ();
			P2.resetArtefactProperties ();

			//Move the stageFormation fowards
			popForward ();

			//Increase maxVelocity
			firstPlane.increaseMaxVelocity ();
			secondPlane.increaseMaxVelocity ();

			if (gameObject.name == "Transition : Clouds") {
				gameWall.updateGame (0);
				LowerObject.stopFall ();
			} else {
				gameWall.updateGame ();
			}


			selectedPart = false;
			gameObject.SetActive (false);
		}
	}
}