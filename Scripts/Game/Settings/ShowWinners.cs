using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowWinners : MonoBehaviour
{

	public static ShowWinners instance;

	public bool[] kidStates = new bool[3];

	Transform[] kidsAnims = new Transform[6];

	void Start ()
	{
		ShowWinners.instance = this;

		for (int i = 1; i < 7; i++) {
			kidsAnims [i - 1] = gameObject.transform.GetChild (i);
		}
		for (int i = 0; i < 3; i++) {
			kidStates [i] = false;
		}

	}

	public void ActiveInactive (int kid)
	{
		kidStates [kid] = true;
	}

}
