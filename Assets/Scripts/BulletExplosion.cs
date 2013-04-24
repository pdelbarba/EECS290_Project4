using UnityEngine;
using System.Collections;

/**
 * A class that applies logic for when the bullet
 * actually hits an enemy
 * 
 * Jeff Einhaus
 */ 
public class BulletExplosion : MonoBehaviour {
	
	// the maximum duration of a bullets life time in the game space
	public float lifeTime = 3.0f;
	// damage the bullet does to an enemy
	public int bulletDamage = 10;
	// holds the fire particle system for bullet explosion
	public GameObject fire;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// ensures that bullets always disappear even if they dont hit something
		lifeTime -= Time.deltaTime;
		
		if(lifeTime <= 0)
			Destroy (gameObject);
	
	}
	
	void OnCollisionEnter(Collision collision){
		if(collision.gameObject.tag == "Enemy"){
			//MUST APPLY DAMAGE TO ENEMY HERE
			collision.gameObject.tag = "Untagged";
			Instantiate(fire, collision.transform.position, Quaternion.identity);	
		}
		Destroy (gameObject);
	}
	
	// increases the bullet damage by a fixed amount
	void increaseBulletDamage(int amt){
		bulletDamage += amt;
	}
		
}
