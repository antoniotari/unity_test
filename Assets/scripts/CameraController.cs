using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;
	private Vector3 offset;

	// Use this for initialization
	void Start () 
	{
		//camera current offset position
		offset = transform.position;
	}

	// for following camera, procedual animation and gathering
	// states is best to use LateUpdate
	void LateUpdate () 
	{
		transform.position = player.transform.position + offset;
	}
}
