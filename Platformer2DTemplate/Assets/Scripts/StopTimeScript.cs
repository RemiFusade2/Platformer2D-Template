using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class StopTimeScript : MonoBehaviour {

	public float currentSlowTimeRatio;
	public float changeSlowTimeRatioSpeed;

	float targetSlowTimeRatio;

	public float minSlowTimeRatio;
	public float defaultSlowTimeRatio;
	public float maxSlowTimeRatio;
	
	public Camera currentCamera;

	public float GetSlowTimeRatio()
	{
		return currentSlowTimeRatio;
	}

	public void ChangeSlowTimeRatio(float newSlowTimeRatio)
	{
		targetSlowTimeRatio = newSlowTimeRatio;
	}

	public void SlowTime()
	{
		ChangeSlowTimeRatio (minSlowTimeRatio);
	}
	public void UnslowTime()
	{
		ChangeSlowTimeRatio (defaultSlowTimeRatio);
	}

	public void UpdateCameraView()
	{
		currentCamera.GetComponent<SepiaTone>().enabled = (currentSlowTimeRatio < 0.5f);
	}

	// Use this for initialization
	void Start () 
	{
		currentSlowTimeRatio = defaultSlowTimeRatio;
		targetSlowTimeRatio = defaultSlowTimeRatio;
	}
	
	// Update is called once per frame
	void Update () 
	{
		UpdateCameraView ();
		if (currentSlowTimeRatio != targetSlowTimeRatio)
		{
			float dist = Mathf.Abs(targetSlowTimeRatio-currentSlowTimeRatio);
			if (dist < changeSlowTimeRatioSpeed)
			{
				currentSlowTimeRatio = targetSlowTimeRatio;
			}
			else
			{
				currentSlowTimeRatio += Mathf.Sign(targetSlowTimeRatio-currentSlowTimeRatio) * Time.deltaTime * changeSlowTimeRatioSpeed;
			}
		}
	}
}
