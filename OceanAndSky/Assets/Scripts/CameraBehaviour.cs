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
    Vector3 posPlane1;
    Vector3 posPlane2;


    float turnForce;
	float riseForce;
	float maxHeight;
	float maxLeft;
	float maxRight;
    Vector3 P1last;
    Vector3 P2last;


	// Use this for initialization
	void Start () {


        turnForce = 200f; //changed from 100 to 200, might need to be faster
		riseForce = 110f;  //changed from 50 to 110
        P1last = P1.transform.position;
        P2last = P2.transform.position;
        posPlane1 = Camera.main.WorldToViewportPoint(planeOne.transform.position);
        posPlane2 = Camera.main.WorldToViewportPoint(planeTwo.transform.position);

    }


    GameObject disappearObj;
    float disapearTime = 0;

    void RenderObstacle()
    {
        GameObject obj;
        if(disappearObj!=null && Time.time > disapearTime)
        {
            disappearObj.GetComponent<Renderer>().enabled = true;
            disappearObj = null;          
        }
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(0.5f,0.5f,0.5f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 40))
        {
            if(hit.collider!=null)
            {
                obj = hit.collider.gameObject;
                if (obj.tag == "Obstacle")
                {
                    if (disappearObj != null && disappearObj != obj)
                    {
                        disappearObj.GetComponent<Renderer>().enabled = true;
                        Debug.Log(Time.time);
                    }
                    disapearTime = Time.time + 1;
                    obj.GetComponent<Renderer>().enabled=false;
                    disappearObj = obj;
                }
            }
        }

    }



	// LateUpdate to smoothen movement 
	void LateUpdate () {

		pos1 = Camera.main.WorldToViewportPoint (P1.transform.position);
		pos2 = Camera.main.WorldToViewportPoint (P2.transform.position);
        posPlane1 = Camera.main.WorldToViewportPoint(planeOne.transform.position);
        posPlane2 = Camera.main.WorldToViewportPoint(planeTwo.transform.position);
        //Handle camera motion in relation to player position in viewport
        handleFollow ();
        RenderObstacle();

    }




    void handleFollow()
	{
        // Viewport goes from (0,0) in bottom left corner to (1,1) in top right corner

        //--- P1 Constraints

        //LEFT SIDE  
        //increasing z goes left
        
		if (pos1.x <= 0.49f && pos2.x <= 0.49f)
		{
            Camera.main.transform.Translate (Vector3.left*Time.deltaTime*turnForce);
		}
			
		//RIGHT SIDE 
		else if (pos1.x >= 0.51f && pos2.x >= 0.51f) 
		{
            Camera.main.transform.Translate (Vector3.right*Time.deltaTime*turnForce);
		}
        
        //UP SIDE
        if (pos1.y >= 0.51f && pos2.y >= 0.51f) 
		{
            Camera.main.transform.Translate (Vector3.up*Time.deltaTime*riseForce);
		}
			
		//DOWN SIDE
		if (pos1.y <= 0.49f && pos2.y <= 0.49f) 
		{
			Camera.main.transform.Translate (Vector3.down * Time.deltaTime * riseForce);
		}

    }
    

}
