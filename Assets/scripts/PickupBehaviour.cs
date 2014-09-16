using UnityEngine;
using System.Collections;

public class PickupBehaviour : MonoBehaviour {

	ClientGetAsync request;

	public static int totalPickups=0;
	// Use this for initialization
	void Start () {
		request= new ClientGetAsync(resp);
		++totalPickups;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void doGet()
	{
		//Debug.Log("do get");
		request.getRequest("http://www.codeproject.com/Articles/4773/Events-and-Delegates-Simplified");
	}

	void resp(string s) {
		Debug.Log("TEST: " + s);
	}
}
