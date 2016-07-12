using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

	public GameObject PauseUI;
	private bool paused = false;

	// Use this for initialization
	void Start ()
	{
		PauseUI.SetActive (false);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetButtonDown ("Pause")) {
			paused = !paused;
		}

		if (paused) {

			PauseUI.SetActive (true);
			Time.timeScale = 0;
		}

		if (!paused) {

			PauseUI.SetActive (false);
			Time.timeScale = 1;
		
		}
	}

	public void Pause ()
	{

		paused = true;

	}

	public void Resume ()
	{
	
		paused = false;

	}

	public void Restart ()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}

	public void Next ()
	{
		SceneManager.LoadScene ((SceneManager.GetActiveScene ().buildIndex) + 1);
	}

	public void UseAbility ()
	{
		Abilities equippedAbility = Benny.instance.currentAbility;

		switch (equippedAbility) {
		case Abilities.Hide:
			Benny.instance.ForceHide ();
			break;

		case Abilities.Dash:
			Benny.instance.ForceDash ();
			break;
		}

	}
}
