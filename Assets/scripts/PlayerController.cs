using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	//private string VERTICAL = "Vertical";

	//when declaring variables as public they show in the unity editor
	public float speed;

	private int count;
	public GUIText countText;
	public GUIText winText;

	// Use this for initialization
	void Start () 
	{
		count = 0;
		setCountText ();
		winText.text = "";
	}
	
	// Update is called before updating a frame
	// most of the game code here
	void Update () 
	{
		// record the input horizontal or vertical axis controlled by keys
		// player object uses a rigid body and interacts with the physics engine
		// we will use this input to add forces to the player and move it
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		//moltiply the value to make the movement be faster
		//to make the movement smooth and framerate independent moltiply by Time.deltaTime
		rigidbody.AddForce(movement*speed*Time.deltaTime);


		//from official documentation
		/*float translation = Input.GetAxis("Vertical") * speed;
		float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
		translation *= Time.deltaTime;
		rotation *= Time.deltaTime;
		transform.Translate(0, 0, translation);
		transform.Rotate(0, rotation, 0);*/
	}

	// when a game object (this is the player) touches a trigger collider
	// argument the object touched
	void OnTriggerEnter(Collider other) 
	{
		// with this code destroy the game object touched

		//this will destroy the object
		//Destroy(other.gameObject);

		if (other.gameObject.tag == "pickup") 
		{
			other.gameObject.SetActive (false);
			var pickupBehaviour = other.gameObject.GetComponent<PickupBehaviour>();
			pickupBehaviour.doGet();
			++count;
			setCountText();
		}
	}

	void setCountText()
	{
		countText.text = "Count: " + count.ToString ();
		if (count >= PickupBehaviour.totalPickups) 
		{
			winText.text="YOU WIN!";
		}
	}



	//called before any physics update
	//physics code will code here
	void FixedUpdate()
	{
	}
}
