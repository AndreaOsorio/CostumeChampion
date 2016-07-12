using UnityEngine;
using System.Collections;

public class Prison : MonoBehaviour
{

	Animator[] anim = new Animator[3];
	AudioSource[] audioBreak = new AudioSource[3];

	public static Prison inst;

	Transform[] prisons = new Transform[3];

	void Start ()
	{
		anim = GetComponentsInChildren<Animator> ();
		audioBreak = GetComponentsInChildren<AudioSource> ();
		Prison.inst = this;
		//receives the childs into the friends array
		for (int i = 0; i < 3; i++) {
			prisons [i] = this.gameObject.transform.GetChild (i);
		}

	}

	void PlayDestroySound (int prisonNumber)
	{
		audioBreak [prisonNumber].Play ();
	}

	public void DestroyAnim (int prisonDestroyed)
	{
		StartCoroutine ("Destroyer", prisonDestroyed);
	}

	IEnumerator Destroyer (int pD)
	{
		switch (pD) {
		case 0:
			//put anim of destroying cage here
			anim [0].SetBool ("TouchCage", true);
			PlayDestroySound (0);
			yield return new WaitForSeconds (0.5f);
			this.gameObject.transform.GetChild (0).gameObject.SetActive (false);
			break;
		case 1:
			//put anim of destroying cage here
			anim [1].SetBool ("TouchCage", true);
			PlayDestroySound (1);
			yield return new WaitForSeconds (0.5f);
			this.gameObject.transform.GetChild (1).gameObject.SetActive (false);
			break;
		case 2:
			//put anim of destroying cage here
			anim [2].SetBool ("TouchCage", true);
			PlayDestroySound (2);
			yield return new WaitForSeconds (0.5f);
			this.gameObject.transform.GetChild (2).gameObject.SetActive (false);
			break;
		}


	}

}
