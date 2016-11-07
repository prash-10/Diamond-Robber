using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour {
	public GameObject g;
	public GameObject g1;

	//public static GameObject StartGame;

	public void LoadScene(int level)
	{
		print ("Entered");
		if(g.tag== "Start Game"&&level == 2)
			{
			SceneManager.LoadScene ("Level 1");
		}

		else if(g1.tag=="Instructions"&&level == 1)
	{
			SceneManager.LoadScene ("Instructions");
	}
}
}