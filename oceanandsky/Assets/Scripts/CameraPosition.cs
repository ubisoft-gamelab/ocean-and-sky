using UnityEngine;
using System.Collections;

public class CameraPosition : MonoBehaviour {

    public Player P1;
    public Player P2;

	
	void Update () {
        //The average of the two z positions
        float zPosition = (P1.transform.position.z + P2.transform.position.z) / 2;
        //The average of the two y positions, plus 55 to give more aerial view
        float yPosition = (P1.transform.position.y + P2.transform.position.y) / 2 + 55;
        transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(transform.position.x, yPosition, zPosition), Time.time * 500);

    }
}
