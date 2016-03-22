using UnityEngine;
using System.Collections;

public class Flash : MonoBehaviour {

    GUITexture aGUITexture;
    //Makes sure that the flash is completed
    public bool isFinished;
    //An incrementing variable that increments every time update is called.
    int i;

    void Start () {

        aGUITexture = GetComponent<GUITexture>();
        aGUITexture.color = Color.clear;
        aGUITexture.enabled = true;
        isFinished = true;
        i = 0;
	}

	void Update () {

        if (!isFinished)
        {
            FlashToWhite();
        }
        //if isFinished is true, then the gameObject becomes inactive
        else
        {
            gameObject.SetActive(false);
        }

    }



    void FlashToWhite()
    {
        if(!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
        //The greater i is, the longer the screen will flash to white for
        if (i < 10)
        {
            //Fades to white
            aGUITexture.color = Color.Lerp(aGUITexture.color, Color.white, 7.5f * Time.deltaTime);
            i++;
            isFinished = false;
        }
        else if (i < 40)
        {
            //Fades to clear
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
