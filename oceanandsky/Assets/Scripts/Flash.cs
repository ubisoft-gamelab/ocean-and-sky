using UnityEngine;
using System.Collections;

public class Flash : MonoBehaviour {

    GUITexture aGUITexture;

	void Start () {

        aGUITexture = GetComponent<GUITexture>();
        aGUITexture.color = Color.clear;
        aGUITexture.enabled = true;	
	}


    int i = 0;
	void Update () {

        if (!isFinished)
        {
            FlashToWhite();
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    bool isFinished = false;


    void FlashToWhite()
    {
        
        if (i < 10)
        {
            aGUITexture.color = Color.Lerp(aGUITexture.color, Color.white, 7.5f * Time.deltaTime);
            i++;
            isFinished = false;
        }
        else if (i < 40)
        {
            aGUITexture.color = Color.Lerp(aGUITexture.color, Color.clear, 16.5f * Time.deltaTime);
            i++;
            isFinished = false;
        }
        else
        {
            i = 0;
            isFinished = true;
        }
    }

}
