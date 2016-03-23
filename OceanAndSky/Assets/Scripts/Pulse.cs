using UnityEngine;
using System.Collections;

public class Pulse : MonoBehaviour {

    bool canGrow;
    Vector3 originalScale;
    // Use this for initialization
    void Start () {

        originalScale = transform.localScale;
	}

	// Update is called once per frame
	void Update () {

        grow();
    }


    void grow()
    {
        transform.localScale = new Vector3(originalScale.x + Mathf.PingPong(Time.time * 0.1f, 0.3f), originalScale.y + Mathf.PingPong(Time.time * 0.1f, 0.3f), originalScale.z);
    }

}
