using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public enum UIState
{
	TITLE,
	SETTINGS,
	CREDITS,
	LOADING,
	INGAME
}

public class MainUIFlow : MonoBehaviour {

	public GameObject TitleScreen;
	public GameObject SettingsScreen;
	public GameObject CreditsScreen;
	public GameObject LoadingScreen;

	public List<Text> TitleTexts;

	UIState currentUIState;

	void Start()
	{
		OpenTitleScreen();

		foreach (Text titleText in TitleTexts)
		{
			titleText.text = GameInformation.Title;
		}
	}

	private void HideAllSubscreens()
	{
		SettingsScreen.SetActive(false);
		CreditsScreen.SetActive(false);
		LoadingScreen.SetActive(false);
	}

	public void OpenTitleScreen()
	{
		HideAllSubscreens ();
		currentUIState = UIState.TITLE;
	}

	public void OpenSettingsScreen()
	{
		HideAllSubscreens ();
		SettingsScreen.SetActive(true);
		currentUIState = UIState.SETTINGS;
	}

	public void OpenCreditsScreen()
	{
		HideAllSubscreens ();
		CreditsScreen.SetActive(true);
		currentUIState = UIState.CREDITS;
	}

	public void OpenLoadingScreen()
	{
		HideAllSubscreens ();
		LoadingScreen.SetActive(true);
		currentUIState = UIState.LOADING;
	}

	public void Back()
	{
		if (currentUIState == UIState.CREDITS || currentUIState == UIState.SETTINGS)
		{
			OpenTitleScreen();
		}
	}

	public Slider MusicSlider;
	public Slider SFXSlider;

	public void SetMusicVolume()
	{
		GameInformation.MusicVolume = MusicSlider.value;
	}
	public void SetSFXVolume()
	{
		GameInformation.SFXVolume = SFXSlider.value;
	}
	public void SetLanguage(string language)
	{
		GameInformation.Language = language;
	}
}
