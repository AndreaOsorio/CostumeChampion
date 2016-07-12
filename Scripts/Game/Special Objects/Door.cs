using UnityEngine;
using System.Collections;

//Code written by: Andrea

[RequireComponent (typeof(BoxCollider2D))]

public class Door : BaseUnit
{
	//Create static variable as an instance to be used from other classes
	public static Door inst;


	//box collider for the door
	BoxCollider2D doorCollider;

	void Start ()
	{
		//when scene is loaded, intance, animator and boxcollider2d are set
		Door.inst = this;
		anim = GetComponent<Animator> ();
		doorCollider = GetComponent<BoxCollider2D> ();
	}

	//method to activate the animation of the door and open it, also it sets the isTrigger of the boxCollider2D to check
	public void OpenDoor ()
	{
		anim.SetBool ("DoorOpen", true);
		doorCollider.isTrigger = true;
	}

}
