using UnityEngine;
using System.Collections;

//SCRIPT WRITTEN BY: ANDREA

public class GameManager : MonoBehaviour
{
	//Creates an array with the 3 friends to save the level
	public Friend[] friends = new Friend[3];

	//creates an array for the position where the friends will spawn
	public Transform[] friendSpawner = new Transform[3];

	//Creates an array with the number of enemies in the level
	public BaseUnit[] enemies;

	//creates an array for the position where the friends will spawn
	public Transform[] enemySpawner;

	//Creates an object for the csm
	public CSM csm;

	//creates an spawner for the position where the csm will spawn
	public Transform csmSpawner;


	//when the level loads, it spawns the 3 friends
	void Awake ()
	{
		for (int i = 0; i < friends.Length; i++) {
			Spawn (i, TypeOfPrefab.Friend);
		}
		for (int i = 0; i < enemies.Length; i++) {
			Spawn (i, TypeOfPrefab.Enemy);
		}
		Spawn (0, TypeOfPrefab.CSM);
	}
		
	//method that instantiates the gameObjects in their respective spawner location

	void Spawn (int n, TypeOfPrefab prefab)
	{
		if (prefab.ToString ().Equals ("Friend")) {
			Instantiate (friends [n], friendSpawner [n].transform.position, friendSpawner [n].transform.rotation);
		} else if (prefab.ToString ().Equals ("Enemy")) {
			Instantiate (enemies [n], enemySpawner [n].transform.position, enemySpawner [n].transform.rotation);
		} else if (prefab.ToString ().Equals ("CSM")) {
			Instantiate (csm, csmSpawner.transform.position, csmSpawner.transform.rotation);
		}

	}

	public enum TypeOfPrefab
	{

		Friend,
		Enemy,
		CSM

	}
}
