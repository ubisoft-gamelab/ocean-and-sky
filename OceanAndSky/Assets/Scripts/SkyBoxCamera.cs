using UnityEngine;
using System.Collections;

/**
 * Renders the Skybox of the game
 * TODO Implement means of swapping which Skybox material is in use
 * based on the maxVelocity.
 */
public class SkyBoxCamera : MonoBehaviour {

	// Use this for initialization

	private static Material[] materials;
	private static Skybox skybox;

	void Start () {
		//Located in Assets/Resources/Materials
		materials = Resources.LoadAll<Material>("Materials");

		skybox = GetComponent<Skybox>();

	}

	// Update is called once per frame
	void Update () {

		//Slowly rotate skybox
		gameObject.transform.Rotate (Vector3.right*Time.deltaTime*0.2f);



	}


	public static void ChangeSkyboxNight()
	{
		for (int x=0; x<materials.Length; x++)
		{
			if (materials[x].name == "NightB")
			{
				skybox.material = materials[x];
				Debug.Log("Changing skybox");
			}
		}


	}

	public static void ChangeSkyboxDSBWP()
	{
		for (int x = 0; x < materials.Length; x++)
		{
			if (materials[x].name == "DSBWP")
			{
				skybox.material = materials[x];
			}
		}
	}

}
