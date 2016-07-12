using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VictoryMenu : MonoBehaviour
{
	public static VictoryMenu instance;
	public GameObject VictoryUI;
	private bool paused = false;

	// Use this for initialization
	void Start ()
	{
		instance = this;
		VictoryUI.SetActive (false);
	}

	// Update is called once per frame
	void Update ()
	{
		
		if (paused) {

			VictoryUI.SetActive (true);
			Time.timeScale = 0;
		}

		if (!paused) {

			VictoryUI.SetActive (false);
			Time.timeScale = 1;

		}
	}

	public void SetToPause ()
	{
		paused = !paused;
	}


}
