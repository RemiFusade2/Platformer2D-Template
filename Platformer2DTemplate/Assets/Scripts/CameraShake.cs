using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour 
{
	public Vector3 strength;

	Vector3 currentStrength;

	Vector3 cameraPositionWithoutShake;

	public float durationFadeIn;
	public float durationFullShake;
	public float durationFadeOut;

	float timeShakeStart;

	// Use this for initialization
	void Start () 
	{
		timeShakeStart = float.MinValue;
	}
	
	// Update is called once per frame
	void Update () 
	{
		float shakeRatio = 0.0f;
		if (Time.time - timeShakeStart < durationFadeIn)
		{
			shakeRatio = (Time.time-timeShakeStart)/durationFadeIn;
		}
		else if (Time.time - timeShakeStart - durationFadeIn < durationFullShake)
		{
			shakeRatio = 1.0f;
		}
		else if (Time.time - timeShakeStart - durationFadeIn - durationFullShake < durationFadeOut)
		{
			shakeRatio = -(Time.time - timeShakeStart - durationFadeIn - durationFullShake - durationFadeOut)/durationFadeOut;
		}
		currentStrength = strength * shakeRatio;
	}

	public void TriggerShake()
	{
		timeShakeStart = Time.time;
	}

	public void UpdateCameraPositionDuringShake()
	{
		cameraPositionWithoutShake = this.transform.position;
		Vector3 currentDeltaPosition = new Vector3 (-currentStrength.x / 2 + Random.Range (0.0f, currentStrength.x), 
		                                            -currentStrength.y / 2 + Random.Range (0.0f, currentStrength.y),  
		                                            -currentStrength.z / 2 + Random.Range (0.0f, currentStrength.z));
		this.transform.position = cameraPositionWithoutShake + currentDeltaPosition;
	}
}
