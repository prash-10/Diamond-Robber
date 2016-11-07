using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Inst2 : MonoBehaviour {

	public GameObject p;

	public void LoadScene(int level)
	{
		if(p.tag== "Back" && level== 2.75)
		{
			SceneManager.LoadScene ("Level 2.75");
		}

	}

}
