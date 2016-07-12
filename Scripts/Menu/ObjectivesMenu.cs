using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

//SCRIPT WRITTEN BY: ANDREA AND KEVIN

public class ObjectivesMenu : MonoBehaviour
{
	//only for Android: The game will automatically start on Landscape and cannot be rotated
	public static ObjectivesMenu instance;

	void Awake ()
	{
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		Screen.autorotateToPortrait = false;
		Screen.autorotateToPortraitUpsideDown = false;
		Screen.autorotateToLandscapeRight = true;
		ObjectivesMenu.instance = this;

	}


	public void StartLevel1 ()
	{
		//loads first Level
		SceneManager.LoadScene (1);
	}


	public void StartLevel2 ()
	{
		//loads first Level
		SceneManager.LoadScene (2);
	}

	public void StartLevel3 ()
	{
		//loads first Level
		SceneManager.LoadScene (3);
	}

	public void StartLevel4 ()
	{
		//loads first Level
		SceneManager.LoadScene (4);
	}

	public void StartLevel5 ()
	{
		//loads first Level
		SceneManager.LoadScene (5);
	}

	public void StartLevel6 ()
	{
		//loads first Level
		SceneManager.LoadScene (6);
	}

	public void StartLevel7 ()
	{
		//loads first Level
		SceneManager.LoadScene (7);
	}



}
