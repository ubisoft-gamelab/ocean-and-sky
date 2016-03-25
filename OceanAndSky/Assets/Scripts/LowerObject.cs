using UnityEngine;
using System.Collections;

public class LowerObject : MonoBehaviour {

	public static float fallRate;
	// Use this for initialization
	void Start () {
	
		fallRate = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {

		transform.Translate (Vector3.down * Time.deltaTime * fallRate);
	
	}

	public static void fall()
	{
		fallRate = 250f;
	}

	public static void stopFall()
	{
		fallRate = 0;
	}
}
