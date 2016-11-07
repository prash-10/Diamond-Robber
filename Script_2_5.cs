using UnityEngine;
using System.Collections;

public class Script1 : MonoBehaviour {

	public AudioClip win;
	public AudioSource audioSource;

	// Use this for initialization
	void Start () {

		audioSource.clip = win;
		audioSource.Play ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
