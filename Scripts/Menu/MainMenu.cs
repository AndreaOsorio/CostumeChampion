using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

//SCRIPT WRITTEN BY: ANDREA AND KEVIN

public class MainMenu : MonoBehaviour
{
	//only for Android: The game will automatically start on Landscape and cannot be rotated

	void Start ()
	{
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		Screen.autorotateToPortrait = false;
		Screen.autorotateToPortraitUpsideDown = false;
		Screen.autorotateToLandscapeRight = true;
	}


	public void StartGame ()
	{
		//load objectives menu
		SceneManager.LoadScene (8);
	}
}
