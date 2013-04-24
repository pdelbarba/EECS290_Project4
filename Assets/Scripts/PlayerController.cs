using UnityEngine;
using System.Collections;

/**
 * A class that controls movement from a first 
 * person perspective of the player
 * 
 * Jeff Einhaus
 */ 
[RequireComponent (typeof(CharacterController))]
public class PlayerController : MonoBehaviour {
	
	// the move speed of the player
	public float maxSpeed = 5.0f;
	// the mouse look speed of the player
	public float maxMouseSpeed = 5.0f;
	// the maximum tilt angle upward of the player
	public float tiltRange = 60.0f;
	// current vertical rotation for mouse look of the player
	public float vertRotation = 0.0f;
	// gravity acting in the world
	public float vertVel = 0.0f;
	// jump speed of the player
	public float jumpSpeed = 20.0f;
	// player's health
	public int health = 100;
	// number of credits the player has earned
	public int credits = 0;
	// credit total needed for weapon upgrade
	public int upgradeScore = 50;
	// holds whether or not player has been upgraded
	private bool upgraded = false;
	// stores the player CharacterController
	private CharacterController player;

	// Use this for initialization
	void Start () {
		// makes sure cursor is not on screen
		Screen.lockCursor = true;
		player = GetComponent<CharacterController>();
	
	}
	
	// Update is called once per frame
	void Update () {
		// upgrade player weapon by increasing damage
		if(credits >= upgradeScore){
			player.SendMessage ("increaseBulletDamage", 10);
			upgraded = true;
		}
		// mouse look
		float rotateLR =  Input.GetAxis ("Mouse X") * maxMouseSpeed;
		transform.Rotate(0,rotateLR,0);
		
		vertRotation -= Input.GetAxis ("Mouse Y") * maxMouseSpeed;
		vertRotation = Mathf.Clamp (vertRotation, -tiltRange, tiltRange);
		Camera.main.transform.localRotation = Quaternion.Euler(vertRotation,0,0);
		
		// player movement
		float vertSpeed = Input.GetAxis("Vertical") * maxSpeed;
		float horSpeed = Input.GetAxis("Horizontal") * maxSpeed;
		vertVel += Physics.gravity.y * Time.deltaTime;
			
		if(player.isGrounded && Input.GetButtonDown("Jump"))
			vertVel = jumpSpeed;
			
			
		Vector3 speed = new Vector3(horSpeed,vertVel,vertSpeed);
		speed = transform.rotation * speed;
		player.Move(speed * Time.deltaTime);
		
	}
	
	void OnGUI(){
		// displays an upgrade message if you've been upgraded
		if(upgraded){
			GUI.Label (new Rect(Screen.width*.5f,Screen.height*.9f, 200, 200),"You've been upgraded!");
			delayUpgrade();
		}
		
		// always displays current health and credits
		GUI.Label (new Rect(Screen.width*.9f,Screen.height*.1f, 200, 200), "Health: " + health +"\nCredits: " + credits);
	}
	
	// adjusts health when hurt
	void damaged(int amt){
		health -= amt;
	}
	
	// used to increase credits when they kill an enemy
	void increaseCredits(int amt){
		credits += amt;
	}
	
	// puts in a 5 second delay to display upgrade message
	IEnumerator delayUpgrade(){
		yield return new WaitForSeconds(5f);
		upgraded = false;
	}
}
