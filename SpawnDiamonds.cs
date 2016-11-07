using UnityEngine;
using System.Collections;

public class SpawnDiamonds : MonoBehaviour {

	public Transform[] diamondSpawns;
	public GameObject diamond;

	// Use this for initialization
	void Start () {
		Spawn ();
	}
	
	void Spawn()
	{
		for (int i = 0; i < diamondSpawns.Length; i++) {
			int diamondFlip = Random.Range (0,2);
			if (diamondFlip > 0)
				Instantiate (diamond, diamondSpawns[i].position, Quaternion.identity);
		}
	}
}