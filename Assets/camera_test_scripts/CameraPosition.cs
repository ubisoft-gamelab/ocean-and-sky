using UnityEngine;
using System.Collections;

public class CameraPosition : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public float minView;
    public float maxView; //Will decrease when moving onto section 2 and 3
    public float maxX;
    public float minX;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        player1LastPos = player1.transform.position;
        player2LastPos = player2.transform.position;
        zPos = Mathf.Min(player1.transform.position.z, player2.transform.position.z);
        float zRel = minView;
        nextZoomIn = 0f;
        nextZoomOut = 0f;
    }


    //Finds the bounds of where the camera can see
    void findBounds(GameObject player)
    {
        Vector3 lowerBound = cam.ViewportToWorldPoint(new Vector3(0, 0, Mathf.Abs(cam.transform.position.z - player.transform.position.z)));
        Vector3 upperBound = cam.ViewportToWorldPoint(new Vector3(1, 1, Mathf.Abs(cam.transform.position.z - player.transform.position.z)));
        width = Mathf.Abs(upperBound.x - lowerBound.x);
        height = Mathf.Abs(upperBound.y - lowerBound.y);
    }

    //binds the player by the camera's bounds
    void bindPlayer()
    {
        float room;
        if (transform.position.z < zPos - maxView + 1)//if the camera is zoomed out to the max
        {
            room = width / 10;
        }
        else
        {
            room = -width / 10; //the bounds of the player are within the camera view, allows the player to move to the edges to move the camera
        }
        findBounds(player1);
        float clampMin = Mathf.Max(minX, cam.transform.position.x - width / 2 + room); //Finds the max value between the zoom and the regular bounds
        float clampMax = Mathf.Min(maxX, cam.transform.position.x + width / 2 - room);
        player1.transform.position = new Vector3
            (
            Mathf.Clamp(player1.transform.position.x, clampMin, clampMax), //This is what the camera sees            
            Mathf.Clamp(player1.transform.position.y, cam.transform.position.y - height / 2 + room, cam.transform.position.y + height / 2 - room),
            player1.transform.position.z
            );
        findBounds(player2);
        clampMin = Mathf.Max(minX, cam.transform.position.x - width / 2 + room); //Finds the max value between the zoom and the regular bounds
        clampMax = Mathf.Min(maxX, cam.transform.position.x + width / 2 - room);
        player2.transform.position = new Vector3
        (
          Mathf.Clamp(player2.transform.position.x, clampMin, clampMax), //This is what the camera sees
          Mathf.Clamp(player2.transform.position.y, cam.transform.position.y - height / 2 + room, cam.transform.position.y + height / 2 - room),
          player2.transform.position.z
        );

    }

    Vector3 player1LastPos;
    Vector3 player2LastPos;
    float zRel;
    public float playerSpeed;
    float nextZoomIn;
    float nextZoomOut;
    int section = 1;

    void findSection() //redo this
    {
        if (section == 1)
        {
            minView = Mathf.Max(7, minView); //MinView has to be at least 7
            maxView = 10;
        }
        else if (section == 2)
        {
            maxView = 7;
        }

        else if (section == 3)
        {
            maxView = 5.5f;
        }
    }




    float zPos;
    float width;
    float height;
    float zMove;
    void Update()
    {
        findSection();
        zMove = transform.position.z;
        zPos = Mathf.Min(player1.transform.position.z, player2.transform.position.z);
        Vector3 lowerBound = cam.ViewportToWorldPoint(new Vector3(0, 0, Mathf.Abs(cam.transform.position.z - zPos)));
        Vector3 upperBound = cam.ViewportToWorldPoint(new Vector3(1, 1, Mathf.Abs(cam.transform.position.z - zPos)));
        width = Mathf.Abs(upperBound.x - lowerBound.x);
        height = Mathf.Abs(upperBound.y - lowerBound.y);
        float camRelposX = width / 2 - width * 0.1f; //the position of the camera's viewing x, has the same z as the players
        Vector3 p1RelPos = cam.WorldToViewportPoint(player1.transform.position);
        Vector3 p2RelPos = cam.WorldToViewportPoint(player2.transform.position);
        bindPlayer();
        float xMove = moveInDirection(p1RelPos.x, p2RelPos.x, cam.transform.position.x, player1.transform.position.x, player2.transform.position.x, player1LastPos.x, player2LastPos.x, camRelposX);
        if (!Zoom(p1RelPos.x, p2RelPos.x, player1.transform.position.x, player2.transform.position.x))
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(xMove, transform.position.y, transform.position.z), Time.deltaTime * 500);
        }
        player2LastPos = player2.transform.position;
        player1LastPos = player1.transform.position;
    }

    bool zoom = false;
    float playerDistance = 0;
    bool Zoom(float p1RelPos, float p2RelPos, float player1Pos, float player2Pos)
    {
        zRel = Mathf.Max(minView, zRel);
        //either zoom or do x to make it smoother not both
        if (Mathf.Abs(player1.transform.position.x - player2.transform.position.x) < playerDistance - 0.3 && nextZoomIn < Time.time && (p1RelPos > 0.1 || p1RelPos < 0.9 || p2RelPos > 0.1 || p2RelPos < 0.9) && transform.position.z < zPos - minView + 0.5)
        {
            //cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 100, Time.deltaTime);
            if (Mathf.Abs(player1Pos - player2Pos) < Mathf.Abs(player1LastPos.x - player2LastPos.x))
            {
                /*zRel = zRel - 0.1f;
                zMove = zPos - zRel;
                nextZoomOut = Time.time + 0.5f;
                */
                //transform change     
                zoom = false;
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, zPos - minX), Time.deltaTime);
                return true;
            }
        }
        else if (Mathf.Abs(player1.transform.position.x - player2.transform.position.x) > 1 * playerDistance && nextZoomOut < Time.time && (p1RelPos < 0.1 && p2RelPos > 0.9 || p2RelPos < 0.1 && p1RelPos > 0.9))
        {
            //cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 120, 5* Time.deltaTime );
            if (Mathf.Abs(player1Pos - player2Pos) > Mathf.Abs(player1LastPos.x - player2LastPos.x))
            {
                if (!zoom)
                {
                    zoom = true;
                    playerDistance = Mathf.Abs(player1.transform.position.x - player2.transform.position.x);
                }
                /*zRel = zRel + 0.5f;
                zMove = zPos - zRel;
                nextZoomIn = Time.time + 0.5f;                
                */
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, zPos - maxX), Time.deltaTime);
                return true;
            }
        }
        return false;
    }

    float moveInDirection(float p1RelPos, float p2RelPos, float camPos, float player1Pos, float player2Pos, float player1LastPos, float player2LastPos, float camRelpos)
    {

        float moveValue = camPos;
        if ((p1RelPos < 0.1 && p2RelPos < 0.9) && p1RelPos < p2RelPos)
        {
            //Camera moves left with p1
            if (player1Pos > player1LastPos) //This means the player isn't moving left anymore so the camera should stop moving. Equals?
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
