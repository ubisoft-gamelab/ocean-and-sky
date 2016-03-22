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

	public FocalPoint focalPoint;

	Vector3 focalPosition;
	Vector3 focalViewportPosition;
	Vector3 camPos;

    //The players starting positions
    Vector3 originalPos;



    
	// Use this for initialization
	void Start () {

		focalPosition = focalPoint.gameObject.transform.position;
		camPos = Camera.main.transform.position;
        originalPos = camPos;
	}
	
	// LateUpdate to smoothen movement 
	void LateUpdate () {

		focalPosition = focalPoint.gameObject.transform.position;
		focalViewportPosition = Camera.main.WorldToViewportPoint (focalPosition);
		camPos.x = Camera.main.transform.position.x;

		//Have camera position update based on positon of the focal point
		camPos.x = Mathf.Lerp (Camera.main.transform.position.x, focalPosition.x, Time.deltaTime * 10);
		camPos.y = Mathf.Lerp (Camera.main.transform.position.y, focalPosition.y+50f, Time.deltaTime * 10);

		Camera.main.transform.position = camPos;

		//Handle camera motion in relation to player position in viewport
		handleFollow ();

	}


	void handleFollow()
	{
        //TODO : Handle fluid follow by zooming in and out based on location of focalPoint in viewPort
        //Assuming players start on the surface of the ocean
        float zoom = 80 - (transform.position.y - originalPos.y) * 0.1f;
        if(zoom < 50)
        {
            zoom = 50;
        }
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, zoom, 0.1f);

	}



}
