using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CSM : MonoBehaviour
{

	Animator anim;
	public static CSM instance;

	void Start ()
	{
		anim = GetComponent<Animator> ();
		CSM.instance = this;
	}

	public void ActivateDestruction ()
	{
		StartCoroutine ("DestroyCSM");
	}

	IEnumerator DestroyCSM ()
	{
		//Play anim here of Destroying CSM
		anim.SetTrigger ("TouchCSM");
		yield return new WaitForSeconds (0.9f);
		//go to Level Selection screen or winning screen
		WinningScript.instance.SetToPause ();
		WinningScript.instance.PlayWinSound ();
	}

}
