using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParallaxManager : MonoBehaviour {

	public Camera currentCamera;

	public List<GameObject> decorationLayers;

	Vector3 lastCameraPosition;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 cameraMovement = currentCamera.transform.position - lastCameraPosition;

		foreach (GameObject decorationLayer in decorationLayers)
		{
			float zDelta = decorationLayer.transform.position.z;
		}
	}

	void LateUpdate()
	{
		lastCameraPosition = currentCamera.transform.position;
	}
}
