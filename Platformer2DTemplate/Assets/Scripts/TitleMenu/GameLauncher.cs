using UnityEngine;
using System.Collections;

public class GameLauncher : MonoBehaviour {

	public string levelToLaunchName;

	public void ExitApplication()
	{
		Application.Quit ();
	}

	public void LaunchGame()
	{
		Application.LoadLevelAsync (levelToLaunchName);
	}
}
