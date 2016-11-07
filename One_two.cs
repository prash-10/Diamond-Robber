using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class One_two : MonoBehaviour {
	public GameObject z;

	public void LoadScene(int level)
	{
		if(z.tag == "OneTwo" && level == 4)
		{
			SceneManager.LoadScene ("Level 2");
		}
	}
}
