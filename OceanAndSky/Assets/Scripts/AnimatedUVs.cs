using UnityEngine;
using System.Collections;

public class AnimatedUVs : MonoBehaviour {
	public int materialIndex = 0;
	public Vector2 uvAnimateRate = new Vector2( 1.0f, 1.0f);
	public string textureName = "_MainTex";
	public string normalName = "_BumpMap";
	public Renderer rend;

	Vector2 uvOffset = Vector2.zero;


	void LateUpdate() {
		uvOffset += (uvAnimateRate * Time.deltaTime);
		if (rend.enabled) {
			rend.materials[materialIndex].SetTextureOffset(textureName, uvOffset);
			rend.materials[materialIndex].SetTextureOffset(normalName, uvOffset);
		}
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
