using UnityEngine;
using System.Collections;

public class CameraRot : MonoBehaviour
{

	public Transform player;
	float distance = 20;
	float distance3 = 3;

	void Update ()
	{

		if (player == null) {
			return;
		}

		Vector3 position = transform.position;
		position.z = player.position.z - distance;
		position.y = player.position.y + distance3;
		position.x = player.position.x;

		transform.position = position;

	}
}
