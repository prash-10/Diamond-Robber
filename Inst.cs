using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Inst : MonoBehaviour {

	public GameObject i;
	
	public void LoadScene(int level)
	{
		if(i.tag== "Menu" && level== 0)
		{
			SceneManager.LoadScene ("Level 0");
		}

	}

}