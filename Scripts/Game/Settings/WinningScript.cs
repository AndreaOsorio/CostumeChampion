using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WinningScript : MonoBehaviour
{
	public static WinningScript instance;
	AudioSource audioWin;
	public GameObject VictoryUI;
	private bool paused = false;

	// Use this for initialization
	void Start ()
	{
		instance = this;
		VictoryUI.GetComponent<Image> ().enabled = false;
		for (int i = 0; i < VictoryUI.GetComponentsInChildren<Image> ().Length; i++) {
			VictoryUI.GetComponentsInChildren<Image> () [i].enabled = false;
		}
		audioWin = GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update ()
	{

		if (paused) {
			VictoryUI.GetComponent<Image> ().enabled = true;
			for (int i = 0; i < VictoryUI.GetComponentsInChildren<Image> ().Length; i++) {
				if (!VictoryUI.GetComponentsInChildren<Image> () [i].gameObject.tag.Equals ("WinningAnims")) {
					VictoryUI.GetComponentsInChildren<Image> () [i].enabled = true;
				} else {
					if (VictoryUI.GetComponentsInChildren<Image> () [i].gameObject.name.Equals ("Aleah_Anim") && ShowWinners.instance.kidStates [0] == true) {
						VictoryUI.GetComponentsInChildren<Image> () [i].enabled = true;
					} else if (VictoryUI.GetComponentsInChildren<Image> () [i].gameObject.name.Equals ("Lazer_Anim") && ShowWinners.instance.kidStates [1] == true) {
						VictoryUI.GetComponentsInChildren<Image> () [i].enabled = true;
					} else if (VictoryUI.GetComponentsInChildren<Image> () [i].gameObject.name.Equals ("Mike_Anim") && ShowWinners.instance.kidStates [2] == true) {
						VictoryUI.GetComponentsInChildren<Image> () [i].enabled = true;
					}

				}
			}
			Time.timeScale = 0;
		}

		if (!paused) {
			VictoryUI.GetComponent<Image> ().enabled = false;
			for (int i = 0; i < VictoryUI.GetComponentsInChildren<Image> ().Length; i++) {
				VictoryUI.GetComponentsInChildren<Image> () [i].enabled = false;
			}
			Time.timeScale = 1;

		}
	}

	public void SetToPause ()
	{
		paused = !paused;
	}

	public void PlayWinSound ()
	{
		audioWin.Play ();
	}


}
