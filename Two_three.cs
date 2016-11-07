using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Two_three : MonoBehaviour {

	public GameObject l;

	public void LoadScene(int level)
	{
		if(l.tag == "TwoThree" && level == 5)
		{
			SceneManager.LoadScene ("Level 3");
		}
	}
}