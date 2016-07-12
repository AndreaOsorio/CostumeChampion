using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Friend : MonoBehaviour
{

	Animator[] anim = new Animator[3];
	AudioSource[] audioFriends = new AudioSource[3];
	public static Friend inst;

	Transform[] friends = new Transform[3];

	int currentScene;

	public string[] savedFriends = new string[3];

	void Start ()
	{
		audioFriends = GetComponentsInChildren<AudioSource> ();
		anim = GetComponentsInChildren<Animator> ();
		Friend.inst = this;
		//receives the childs into the friends array
		for (int i = 0; i < 3; i++) {
			friends [i] = this.gameObject.transform.GetChild (i);
		}
		currentScene = SceneManager.GetActiveScene ().buildIndex;
	}

	public void CelebrationAnim (string kidSaved)
	{
		StartCoroutine ("Celebrate", kidSaved);
	}

	void PlayFreedom (int friendSaved)
	{
		audioFriends [friendSaved].Play ();
	}

	IEnumerator Celebrate (string kS)
	{
		switch (kS) {
		case "FriendGirl":
			//anim celebration for girl
			anim [0].SetTrigger ("Freedom");
			PlayFreedom (0);
			//wait until animation is finished
			yield return new WaitForSeconds (1.0f);
			this.gameObject.transform.GetChild (0).gameObject.SetActive (false);
			KidsScore.instance.AddKids (currentScene, 0);
			ShowWinners.instance.ActiveInactive (0);
			UIFrameScript.instance.ActivateKidFace (0);
			break;
		case "Friend1Boy":
			//anim celebration for boy1
			anim [1].SetTrigger ("Freedom");
			PlayFreedom (1);
			//wait until animation is finished
			yield return new WaitForSeconds (1.0f);
			this.gameObject.transform.GetChild (1).gameObject.SetActive (false);
			KidsScore.instance.AddKids (currentScene, 1);
			ShowWinners.instance.ActiveInactive (1);
			UIFrameScript.instance.ActivateKidFace (1);
			break;
		case "Friend2Boy":
			//anim celebration for boy2
			anim [2].SetTrigger ("Freedom");
			PlayFreedom (2);
			//wait until animation is finished
			yield return new WaitForSeconds (1.0f);
			this.gameObject.transform.GetChild (2).gameObject.SetActive (false);
			KidsScore.instance.AddKids (currentScene, 2);
			ShowWinners.instance.ActiveInactive (2);
			UIFrameScript.instance.ActivateKidFace (2);
			break;
		}


	}




}
