using UnityEngine;
using System.Collections;

public class FocalPoint : MonoBehaviour {

	public Player P1;
	public Player P2;

	Transform pOneTrans;
	Transform pTwoTrans;
	Vector3 endPos;

	// Use this for initialization
	void Start () {
		//pOneTrans = P1.gameObject.transform

		endPos.x = Mathf.Abs(P1.gameObject.transform.position.x + P2.gameObject.transform.position.x) * 0.5f;
		endPos.y = Mathf.Abs(P1.gameObject.transform.position.y + P2.gameObject.transform.position.y) * 0.5f;
		//endPos.z = Mathf.Abs(P1.gameObject.transform.position.z - P2.gameObject.transform.position.z) * 0.5f;
		//endPos.y = 61f;
		endPos.z = -4600f;
		transform.position = endPos;	}

	// Update is called once per frame
	void Update () {
		endPos.x = Mathf.Abs(P1.gameObject.transform.position.x + P2.gameObject.transform.position.x) * 0.5f;
		endPos.y = Mathf.Abs(P1.gameObject.transform.position.y + P2.gameObject.transform.position.y) * 0.5f;
		//endPos.z = Mathf.Abs(P1.gameObject.transform.position.z - P2.gameObject.transform.position.z) * 0.5f;

		transform.position = endPos;
		//Debug.Log("Position : " + transform.Po
	}
}