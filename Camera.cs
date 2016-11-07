using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

	private Vector3 offset;
	public GameObject player;

	// Use this for initialization
	void Start () {

			offset = transform.position - player.transform.position;
	}


	void LateUpdate () {

			transform.position = player.transform.position + offset;

	}
}