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
	static float rotateRate;

	void Start () {
		//Located in Assets/Resources/Materials
		materials = Resources.LoadAll<Material>("Materials");
		rotateRate = 0.2f;
		skybox = GetComponent<Skybox>();

	}

	// Update is called once per frame
	void Update () {

		//Slowly rotate skybox
		gameObject.transform.Rotate (Vector3.right*Time.deltaTime*rotateRate);



	}


	public static void ChangeSkyboxNight()
	{
		for (int x=0; x<materials.Length; x++)
		{
			if (materials[x].name == "NightB")
			{
				skybox.material = materials[x];
				rotateRate = -0.5f;
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
				rotateRate = -0.9f;
			}
		}
	}

}