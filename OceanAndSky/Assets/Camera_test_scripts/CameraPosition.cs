using UnityEngine;
using System.Collections;

public class CameraPosition : MonoBehaviour {

    public GameObject player1;
    public GameObject player2;
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	
	}
	
	// Update is called once per frame
	void Update () {
        float yMovement = transform.position.y;
        transform.position = new Vector3((player1.transform.position.x + player2.transform.position.x) / 2, (player1.transform.position.y + player2.transform.position.y)/2, (player1.transform.position.z + player2.transform.position.z) / 2 - 15); //this is wrong but oh well
        yMovement = yMovement - transform.position.y;

        //Trying to figure out rotation
        //transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);


	
	}
}
