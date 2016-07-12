using UnityEngine;
using System.Collections;

//SCRIPT WRITTEN BY: ANDREA


[RequireComponent (typeof(BoxCollider2D))]
public class Costume : MonoBehaviour
{

	string costume;
	public static Costume instance;
	AudioSource audioCostume;

	void Start ()
	{
		costume = gameObject.tag;
		Costume.instance = this;
		audioCostume = GetComponent<AudioSource> ();
	}

	void CurrentAbility ()
	{
		switch (costume) {
		case "Benny":
			
			break;
		case "Ninja":
			
			break;
		case "Warrior":
			
			break;
		}
	}

	public void PlayCostumeSound ()
	{
		audioCostume.Play ();
	}





}
