using UnityEngine;
using System.Collections;

public class CameraPosition : MonoBehaviour
{

    public GameObject player1;
    public GameObject player2;
    public float minView = 20;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        player1LastPos = player1.transform.position;
        player2LastPos = player2.transform.position;
        zPos = Mathf.Min(player1.transform.position.z, player2.transform.position.z);
        float zRel = minView / 2;
    }



    Vector3 player1LastPos;
    Vector3 player2LastPos;
    float zRel;

    float Zoom(Vector3 p1RelPos, Vector3 p2RelPos, Vector3 camPos, Vector3 player1Pos, Vector3 player2Pos)
    {
        zRel = Mathf.Max(minView/2,zRel);
        if ((p1RelPos.x < 0.1 && p2RelPos.x > 0.9) || (p1RelPos.x > 0.9 && p2RelPos.x < 0.1)|| (p1RelPos.y < 0.1 && p2RelPos.y > 0.9) || (p1RelPos.y > 0.9 && p2RelPos.y < 0.1))
        {
            if (Mathf.Abs(player1Pos.x - player2Pos.x) > Mathf.Abs(player1LastPos.x - player2LastPos.x) || Mathf.Abs(player1Pos.y - player2Pos.y) > Mathf.Abs(player1LastPos.y - player2LastPos.y))
            {
                zRel = zRel + 0.5f;
            }
        }
		else if(camPos.z < zPos - minView/2)
        {
            if (Mathf.Abs(player1Pos.x - player2Pos.x) < Mathf.Abs(player1LastPos.x - player2LastPos.x) || Mathf.Abs(player1Pos.y - player2Pos.y) < Mathf.Abs(player1LastPos.y - player2LastPos.y))
            {
                zRel = zRel - 0.5f;
            }
        }
        Debug.Log(zPos-zRel + " " + zRel);
        return zPos - zRel;        
    }


    
    float zPos;
    void Update()
    {
        zPos = Mathf.Min(player1.transform.position.z, player2.transform.position.z);
        Vector3 lowerBound = cam.ViewportToWorldPoint(new Vector3(0, 0, Mathf.Abs(cam.transform.position.z - zPos)));
        Vector3 upperBound = cam.ViewportToWorldPoint(new Vector3(1, 1, Mathf.Abs(cam.transform.position.z - zPos)));
        float width = Mathf.Abs(upperBound.x - lowerBound.x);
        float height = Mathf.Abs(upperBound.y - lowerBound.y);
        float camRelposX = width / 2 - width * 0.1f;
        float camRelposY = height / 2 - height * 0.1f;
        Vector3 p1RelPos = cam.WorldToViewportPoint(player1.transform.position);
        Vector3 p2RelPos = cam.WorldToViewportPoint(player2.transform.position);
        float xMove = moveInDirection(p1RelPos.x, p2RelPos.x, transform.position.x, player1.transform.position.x, player2.transform.position.x, player1LastPos.x, player2LastPos.x, camRelposX);
        float yMove = moveInDirection(p1RelPos.y, p2RelPos.y, transform.position.y, player1.transform.position.y, player2.transform.position.y, player1LastPos.y, player2LastPos.y, camRelposY);
        float zMove = Zoom(p1RelPos, p2RelPos, transform.position, player1.transform.position, player2.transform.position);
        transform.position = Vector3.Lerp(transform.position, new Vector3(xMove, yMove, transform.position.z), Time.deltaTime * 20);
        player2LastPos = player2.transform.position;
        player1LastPos = player1.transform.position;
    }



	//if one of them is more than 1, then the camera's general position has to move 1 unit over

    float moveInDirection(float p1RelPos, float p2RelPos, float camPos, float player1Pos, float player2Pos, float player1LastPos, float player2LastPos, float camRelpos)
    {

        float moveValue = camPos;

        if ((p1RelPos < 0.1 && p2RelPos < 0.9) && p1RelPos < p2RelPos)
            {
            //Camera moves left with p1

            if (player1Pos > player1LastPos) //This means the player isn't moving left anymore so the camera should stop moving
            {
                return camPos;
            }
                return player1Pos + camRelpos;
            }
            else if (p1RelPos < 0.9 && p2RelPos < 0.1 && p1RelPos > p2RelPos)
            {
            //Camera moves left with p2

            if (player2Pos > player2LastPos)
            {
                return camPos;
            }

            return player2Pos + camRelpos;
            }
            else if (p1RelPos > 0.9 && p2RelPos > 0.1 && p1RelPos > p2RelPos)
            {
            //Camera moves right with p1
            if (player1Pos < player1LastPos)
            {
                return camPos;
            }
            return player1Pos - camRelpos;
            }

        else if (p1RelPos > 0.1 && p2RelPos > 0.9 && p1RelPos < p2RelPos)
        {
            //Camera moves right with p2

            if (player2Pos < player2LastPos)
            {
                //relpos = player2Pos - relpos;
                return camPos;
            }
                return player2Pos - camRelpos;
            }

        return moveValue;
    }


}
