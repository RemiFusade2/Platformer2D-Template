using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LevelSelectionUIFlow : MonoBehaviour 
{
	public GameObject LevelSelectionScreen;
	public GameObject LoadingScreen;
	
	public List<Text> TitleTexts;

	public string titleMenuLevelName;

	// Use this for initialization
	void Start () 
	{		
		OpenLevelSelectionScreen();
		
		foreach (Text titleText in TitleTexts)
		{
			titleText.text = GameInformation.Title;
		}
	}
	
	private void HideAllSubscreens()
	{
		LoadingScreen.SetActive(false);
	}
	
	public void OpenLevelSelectionScreen()
	{
		HideAllSubscreens ();
	}
	
	public void OpenLoadingScreen()
	{
		HideAllSubscreens ();
		LoadingScreen.SetActive(true);
	}
	
	public void BackToTitleMenu()
	{
		Application.LoadLevelAsync (titleMenuLevelName);
	}
	
	public void OpenLevel(string levelName)
	{
		OpenLoadingScreen ();
		Application.LoadLevelAsync (levelName);
	}
}
