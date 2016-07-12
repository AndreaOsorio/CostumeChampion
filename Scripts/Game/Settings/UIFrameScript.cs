using UnityEngine;
using System.Collections;

public class UIFrameScript : MonoBehaviour
{


	public static UIFrameScript instance;

	Transform[] kids = new Transform[3];

	void Start ()
	{
		UIFrameScript.instance = this;

		for (int i = 0; i < 3; i++) {
			kids [i] = gameObject.transform.GetChild (i);
		}

	}

	public void ActivateKidFace (int kid)
	{
		kids [kid].gameObject.SetActive (true);
	}

}
