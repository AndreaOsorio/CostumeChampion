using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent (typeof(CircleCollider2D))]
public class PaperBall : MonoBehaviour
{
	

	//Speed the bullet travels once it's shot
	public float bulletSpeed;


	//Set the speed in case the speed is changed in Runtime in the Inspector
	public void SetBallSpeed (float nextSpeed)
	{
		bulletSpeed = nextSpeed;
	}

	void Update ()
	{
		transform.Translate (Vector3.left * Time.deltaTime * bulletSpeed);
	}

	//Method that determines what happens when a bullet collides with an object
	void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.gameObject.GetComponent<Benny> () != null) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
			Destroy (coll.gameObject);
		}
		if (coll.gameObject.tag == "Wall" || coll.gameObject.tag == "Floor") {
			Destroy (this.gameObject);
		}
	}
		

}
