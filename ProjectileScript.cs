using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {

    //private Transform playerReferenceTag;

    public float damageAmount = 30; // set the damage this bullet can cause to health
    public float shieldDamageAmount = 10; // set the damage this bullet can cause to shield
    public int timeToDestroy = 3; // set the time to destroy this bullet
    public bool enemyBullet = false;

	// Use this for initialization
	void Start ()
    {
        // Destroy the bullet after time 
        Destroy(this.gameObject,timeToDestroy);
	}
	

    void OnTriggerEnter(Collider col)
    {
        if (enemyBullet)
        {
            // To send to player the damage hit amount shield and health
            if (col.CompareTag("Player"))
            {
                // get the playerscript component
                PlayerScript player = col.gameObject.GetComponent<PlayerScript>();
                // set the damage to player health and shield variable
                player.TakeDamage(damageAmount, shieldDamageAmount);
            }
        }else
        {

        }
    }

}
