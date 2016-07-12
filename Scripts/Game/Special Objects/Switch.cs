using UnityEngine;
using System.Collections;

//CODE WRITTEN BY: ANDREA

public class Switch : BaseUnit
{
	//Create static variable as an instance to be used from other classes
	public static Switch inst;
	AudioSource alarmSound;

	//get the components for the instance and the animator
	void Start ()
	{
		Switch.inst = this;
		anim = GetComponent<Animator> ();
		alarmSound = GetComponent<AudioSource> ();
	}

	//code to use the animation of the switch
	public void SwitchTrigger ()
	{
		anim.SetBool ("TurnAlarmOn", true);
	}

	public void PlayAlarmSound ()
	{
		alarmSound.Play ();
	}
}
