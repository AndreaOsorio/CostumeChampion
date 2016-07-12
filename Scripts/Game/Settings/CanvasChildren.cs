using UnityEngine;
using System.Collections;

public class CanvasChildren : MonoBehaviour
{
	public static CanvasChildren instance;
	// Use this for initialization
	void Awake ()
	{
		CanvasChildren.instance = this;
	}
}
