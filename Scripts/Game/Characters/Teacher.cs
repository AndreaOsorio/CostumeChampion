using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

//SCRIPT WRITTEN BY: ANDREA

//Required componenets for the GameObject which are needed and cannot be deleted
[RequireComponent (typeof(BoxCollider2D))]
[RequireComponent (typeof(Rigidbody2D))]

//Child of BaseUnit for Move and OnTerrain methods
public class Teacher : BaseUnit
{
	//starts moving to the right
	public int direction = 1;
	//offset
	public float teacherOffset;

	private bool wallTouch;

	bool justFlipped = false;

	void Update ()
	{
		//When it is about to fall from floor or touches direction it changes direction
		if ((OnTerrain (-teacherOffset) != TerrainState.Floor || OnTerrain (teacherOffset) != TerrainState.Floor || wallTouch == true) && justFlipped != true) {
			StartCoroutine ("JustFlip");
			direction *= -1;
			wallTouch = false;

		}
		Flip (direction);
		Move (direction);
	}


	void OnTriggerEnter2D (Collider2D coll)
	{
		//When it collides with benny and benny is not dashing or hiding it destroys him and the level reloads.
		if (coll.gameObject.GetComponent<Benny> () != null) {
			bool hide = coll.gameObject.GetComponent<Benny> ().isHiding;
			bool dash = coll.gameObject.GetComponent<Benny> ().isDashing;
			if (hide == false && dash == false) {
				Destroy (coll.gameObject);
				SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
			}
		} 

		//if it collides with a wall wallTouch is true
		if (coll.gameObject.tag.Equals ("Wall")) {
			wallTouch = true;
		}
	}

	IEnumerator JustFlip ()
	{
		justFlipped = true;
		yield return new WaitForSeconds (0.8f);
		justFlipped = false;
	}
}
