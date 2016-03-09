using UnityEngine;
using System.Collections;

public class Enemies : MonoBehaviour {

	public float restoreVal;
	public float repulseVal;
	public bool isMoved;
	public bool isContact;
	public float epsilon;

	// Use this for initialization
	void Start () {
		isMoved = false;
		isContact = false;
	}
	
	/**
	 * Checks for if in contact. Then applies repulsion at impact time. Sets isContact to false
	 * Sets isMoved to true
	 * 
	 * Checks for if moved. Then, applies restoration until within epsilon
	 * Sets isMoved to false
	 **/
	void FixedUpdate () {
		if (isContact) {
			Launch (repulseVal--);
		}


		if (isMoved) {
			restorePos (restoreVal--);
		}

	}


	/**
	 * Launch repulses the object upwards away from the collision point
	 * @param pRepulse value is slowly decremented while applied to translation.
	 **/
	public void Launch(float pRepulse) {

		if (pRepulse <= epsilon) 
		{
			isContact = false;
			isMoved = true;
			return;
		}

		transform.Translate (Vector3.up *  Time.deltaTime * pRepulse);

	}


	/**
	 * restorePos brings the object down towards original location up until epsilon.
	 * @param pRestore value is slowly decremented while applied to translation
	 **/
	public void restorePos(float pRestore)
	{
		if (pRestore <= epsilon) 
		{
			isMoved = false;
			return;
		}

		transform.Translate (Vector3.down *  Time.deltaTime * pRestore);

	}
}
