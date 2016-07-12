using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class KidsScore : MonoBehaviour
{
	
	public static KidsScore instance;


	bool[,] lvl = new bool[7, 3];

	public Transform[] kidsFaces;

	void Awake ()
	{
		if (instance == null) {
			DontDestroyOnLoad (gameObject);
			instance = this;	
		} else if (instance != this) {
			Destroy (gameObject);
		}


	}

	void Update ()
	{
		if (SceneManager.GetActiveScene ().buildIndex == 8) {
			for (int i = 0; i < 21; i++) {
				kidsFaces [i] = CanvasChildren.instance.gameObject.transform.GetChild (i);
			}
			for (int i = 0; i < 7; i++) {
				for (int j = 0; j < 3; j++) {
					if (lvl [i, j] == true) {
						//setActive faces on the objectives scene
						switch (i) {
						//lvl1
						case 0:
							if (j == 0) {
								kidsFaces [0].gameObject.SetActive (true);
							} else if (j == 1) {
								kidsFaces [1].gameObject.SetActive (true);
							} else if (j == 2) {
								kidsFaces [2].gameObject.SetActive (true);
							}
							break;
						//lvl2
						case 1:
							if (j == 0) {
								kidsFaces [3].gameObject.SetActive (true);
							} else if (j == 1) {
								kidsFaces [4].gameObject.SetActive (true);
							} else if (j == 2) {
								kidsFaces [5].gameObject.SetActive (true);
							}
							break;
						//lvl3
						case 2:
							if (j == 0) {
								kidsFaces [6].gameObject.SetActive (true);
							} else if (j == 1) {
								kidsFaces [7].gameObject.SetActive (true);
							} else if (j == 2) {
								kidsFaces [8].gameObject.SetActive (true);
							}
							break;
						//lvl4
						case 3:
							if (j == 0) {
								kidsFaces [9].gameObject.SetActive (true);
							} else if (j == 1) {
								kidsFaces [10].gameObject.SetActive (true);
							} else if (j == 2) {
								kidsFaces [11].gameObject.SetActive (true);
							}
							break;
						//lvl5
						case 4:
							if (j == 0) {
								kidsFaces [12].gameObject.SetActive (true);
							} else if (j == 1) {
								kidsFaces [13].gameObject.SetActive (true);
							} else if (j == 2) {
								kidsFaces [14].gameObject.SetActive (true);
							}
							break;
						//lvl6
						case 5:
							if (j == 0) {
								kidsFaces [15].gameObject.SetActive (true);
							} else if (j == 1) {
								kidsFaces [16].gameObject.SetActive (true);
							} else if (j == 2) {
								kidsFaces [17].gameObject.SetActive (true);
							}
							break;
						//lvl7
						case 6:
							if (j == 0) {
								kidsFaces [18].gameObject.SetActive (true);
							} else if (j == 1) {
								kidsFaces [19].gameObject.SetActive (true);
							} else if (j == 2) {
								kidsFaces [20].gameObject.SetActive (true);
							}
							break;
						}
					}
				}
			}
		} else {
			
		}

	}
	//ORDER OF KIDS
	//0==girl1
	//1==boy1
	//2==boy2
	public void AddKids (int currentLevel, int savedKid)
	{
		lvl [currentLevel - 1, savedKid] = true;
	}
}
