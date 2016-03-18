using UnityEngine;
using System.Collections;

/**
 * Class which controls camera follow
 */
public class CameraBehaviour : MonoBehaviour {

	public Player P1;
	public Player P2;

	public PlaneBehaviour planeOne;
	public PlaneBehaviour planeTwo;

	Vector3 pos1;
	Vector3 pos2;

	float turnForce;
	float riseForce;
	float maxHeight;
	float maxLeft;
	float maxRight;


	// Use this for initialization
	void Start () {

		turnForce = 100f;
		riseForce = 50f;
	}
	
	// LateUpdate to smoothen movement 
	void LateUpdate () {

		pos1 = Camera.main.WorldToViewportPoint (P1.transform.position);
		pos2 = Camera.main.WorldToViewportPoint (P2.transform.position);

		//Handle camera motion in relation to player position in viewport
		handleFollow ();

	}
		

	void handleFollow()
	{
		// Viewport goes from (0,0) in bottom left corner to (1,1) in top right corner

		//--- P1 Constraints

		//LEFT SIDE  
		if (pos1.x <= 0.45f) 
		{
			Camera.main.transform.Translate (Vector3.left*Time.deltaTime*turnForce);
		}
			
		//RIGHT SIDE 
		if (pos1.x >= 0.55f) 
		{
			Camera.main.transform.Translate (Vector3.right*Time.deltaTime*turnForce);
		}
			
		//UP SIDE
		if (pos1.y >= 0.55f) 
		{
			Camera.main.transform.Translate (Vector3.up*Time.deltaTime*riseForce);
		}
			
		//DOWN SIDE
		if (pos1.y <= 0.45f) 
		{
			Camera.main.transform.Translate (Vector3.down * Time.deltaTime * riseForce);
		}
			
		//--- P2

		//LEFT SIDE
		if (pos2.x <= 0.45f) 
		{
			Camera.main.transform.Translate (Vector3.left*Time.deltaTime*turnForce);
		}
			
		//RIGHT SIDE
		if (pos2.x >= 0.55f) 
		{
			Camera.main.transform.Translate (Vector3.right*Time.deltaTime*turnForce);
		}
			
		//UP SIDE
		if (pos2.y >= 0.55f) 
		{
			Camera.main.transform.Translate (Vector3.up*Time.deltaTime*riseForce);
		}

		//DOWN SIDE
		if (pos2.y <= 0.45f) 
		{
			Camera.main.transform.Translate (Vector3.down*Time.deltaTime*riseForce);
		}

	}
		
}
