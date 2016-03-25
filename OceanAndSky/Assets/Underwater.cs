using UnityEngine;
using System.Collections;



public class Underwater : MonoBehaviour {

	public float waterLevel = 0;
	private bool isUnderwater;
	private Color normalColor;
	private Color underwaterColor;

	// Use this for initialization
	void Start () {
		//RenderSettings.fog = true;
		normalColor = new Color (0.5f, 0.5f, 0.5f, 0.05f);
		underwaterColor = new Color (0.22f, 0.65f, 0.77f, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		isUnderwater = transform.position.y <= waterLevel;
		if (isUnderwater) { setUnderwater(); }
		if (!isUnderwater) { setNormal(); }
		/*
		if (transform.position.y <= waterLevel) 
		{ 
			//isUnderwater = transform.position.y < waterLevel;
			if (isUnderwater) { setUnderwater(); }
			if (!isUnderwater) { setNormal(); }
		}*/
	}

	
	void setNormal()
	{
		RenderSettings.fogColor = normalColor;
		RenderSettings.fogDensity = 0.002f;
	}
	
	void setUnderwater()
	{
		RenderSettings.fogColor = underwaterColor;
		RenderSettings.fogDensity = 0.03f;
	}
}

