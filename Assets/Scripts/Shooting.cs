using UnityEngine;
using System.Collections;

/**
 * A class that allows the user to shoot
 * enemies attacking it
 * 
 * Jeff Einhaus
 */ 
public class Shooting : MonoBehaviour {
	
	// actual bullet that is shot
	public GameObject bullet;
	// speed of the bullet
	public float bulletSpeed = 100f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// if shot button is clicked, finds gun and instantiates a bullet with force from it
		if(Input.GetButtonDown("Fire1")){
			GameObject gun = GameObject.FindGameObjectWithTag("Gun");
			GameObject shot = (GameObject) Instantiate (bullet, gun.transform.position + gun.transform.forward*2, gun.transform.rotation);
			shot.rigidbody.AddForce(gun.transform.forward * bulletSpeed, ForceMode.Impulse);
				
		}		
	}

			
}
