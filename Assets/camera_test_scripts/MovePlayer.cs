using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour
{
    public enum PlayerType { Sky, Ocean};

    public PlayerType playerType;


    public string xMove;
    public string zMove;
    public KeyCode up;
    public KeyCode down;
    public float bounds = 7;
    public GameObject otherPlayer;
    public GameObject plane;
    public float speed = 15;


    private float distance;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        distance = Vector3.Distance(transform.position, otherPlayer.transform.position);
    }


    //A function that finds the speed of the players based on how far they are from each other (The closer they are, the faster)
    //I just put in a random function but we can change it later
    void findSpeed()
    {
        distance = Vector3.Distance(transform.position, otherPlayer.transform.position);
        speed = 1000/(Mathf.Pow(distance,1.5f));
    }

    void Update()
    {

        findSpeed();
        if (distance < 60)
        {
            transform.Translate(Vector3.forward * Input.GetAxis(zMove) * Time.deltaTime * speed);
            transform.Translate(new Vector3(1, 0, 0) * Input.GetAxis(xMove) * Time.deltaTime * speed);


            if (Input.GetKey(up))
            {
                transform.Translate(Vector3.up * Time.deltaTime * speed);
            }

            if (Input.GetKey(down))
            {
                transform.Translate(Vector3.down * Time.deltaTime * speed);
            }
        }
        else
        {
            dead();
        }



        //the water bounds
        if(playerType == PlayerType.Ocean)
        {
            rb.position = new Vector3
            (
              rb.position.x,
              Mathf.Clamp(rb.position.y, -5000, plane.transform.position.y - 3),
              rb.position.z
            );
        }
        else
        {
            rb.position = new Vector3
            (
              rb.position.x,
              Mathf.Clamp(rb.position.y, plane.transform.position.y + 3, 5000),
              rb.position.z
            );
        }
    
}
    void dead()
    {
        Debug.Log("This is where the player dies");
    }
} 

