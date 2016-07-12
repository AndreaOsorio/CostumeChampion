using UnityEngine;
using System.Collections;

//SCRIPT WRITTEN BY: ANDREA OSORIO

public class BaseUnit : MonoBehaviour
{

	//Variables for movementSpeed, rigidbody, animators, and raycast distances
	public float movementSpeed;
	protected Rigidbody2D rb;
	protected Animator anim;
	private Vector3 scale;
	public float terrainCheckRaycastDistance = 0.15f;
	protected float raycastOffset = 0.5f;


	//state of Bennys feet
	private TerrainState myCurrentState;

	//Gets the Rigidbody component from the child object
	void Awake ()
	{
		rb = GetComponent<Rigidbody2D> ();
	}

	//Move method, takes the current direction and adds speed to the velocity
	protected void Move (float horizontal)
	{
		Vector2 vel = rb.velocity;
		vel.x = horizontal * movementSpeed;
		rb.velocity = vel;
		anim = GetComponentInChildren<Animator> ();
	}

	//method to shoot Raycasts from the pivot and receive a number of what the  raycast is hitting.
	protected TerrainState OnTerrain (float offsetX)
	{
		//The origin of our Raycast is the position, since the pivot is already at its feet
		Vector3 origin = transform.position;
		origin.x += offsetX;

		//shoots the raycast from the origin with a down direction a distance set with the terrainCheckRaycastDistance
		RaycastHit2D hitInfo = Physics2D.Raycast (origin, new Vector2 (0, -1), terrainCheckRaycastDistance);

		if (hitInfo.collider == null) {
			return TerrainState.None;
		} else {
			if (hitInfo.collider.gameObject.tag.Equals ("Floor")) {
				return TerrainState.Floor;
			} else if (hitInfo.collider.gameObject.tag.Equals ("Wall")) {
				return TerrainState.Wall;
			} else if (hitInfo.collider.gameObject.tag.Equals ("Corner")) {
				return TerrainState.Corner;
			} else if (hitInfo.collider.gameObject.tag.Equals ("Friend")) {
				return TerrainState.Friend;
			} else if (hitInfo.collider.gameObject.GetComponent<Teacher> () || hitInfo.collider.gameObject.GetComponent<Bully> ()) {
				return TerrainState.Enemy;
			} else if (hitInfo.collider.gameObject.tag.Equals ("CSM")) {
				return TerrainState.CSM;
			} else {
				return TerrainState.Other;
			}
		}
	}

	//Method to flip scale of the character based on the direction it is facing
	protected void Flip (float _horizontal)
	{
		scale = transform.localScale;
		if (_horizontal < 0) {
			scale.x = -Mathf.Abs (scale.x);
		} else if (_horizontal > 0) {
			scale.x = Mathf.Abs (scale.x);
		}
		transform.localScale = scale;	
	}

}

//enum to determine the different existing terrain states of Benny
public enum TerrainState
{
	None,
	Floor,
	Wall,
	Corner,
	Friend,
	Enemy,
	CSM,
	Other
}
