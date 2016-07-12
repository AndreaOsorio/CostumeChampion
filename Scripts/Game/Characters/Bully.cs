using UnityEngine;
using System.Collections;

[RequireComponent (typeof(BoxCollider2D))]
public class Bully : MonoBehaviour
{

	public Transform muzzle;
	public PaperBall paper;
	public float throwTimeMS;
	public float muzzleLevel;

	float nextShotTime;





	public void Shoot ()
	{
		if (Time.time > nextShotTime) {
			nextShotTime = Time.time + throwTimeMS / 1000;
			PaperBall newBall = Instantiate (paper, muzzle.position, muzzle.rotation) as PaperBall;
			newBall.SetBallSpeed (muzzleLevel);
		}

	}


}
