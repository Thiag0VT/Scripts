using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

    public float health = 100;
    public float shield = 50;
    public float distanceToShoot = 9f;
    public bool haveShield = false;
    public States states = States.Stand;
    public Transform canonPos;
    public Rigidbody projectile;
    public int projectileSpeed = 20;

	public static float enemyHit = 5;

    private Transform playerPos;
    private bool isNear = false;

	// Use this for initialization
	void Start () 
    {
        if (playerPos == null)
            playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        StartCoroutine("Shoot");
	}
	
    void Update()
    {
        // Control the Health
        HealthControl();

        // Get the distance of this enemy to player
        float playerDistance = Vector3.Distance(this.transform.position, playerPos.position);
      
        // able and disable the shoot based on player distance
        if (playerDistance < distanceToShoot && !isNear)
        {
            isNear = true;

        }else if (isNear && playerDistance > distanceToShoot)
        {
            isNear = false;
        }

        //print(playerDistance);
    }

    IEnumerator Shoot()
    {
        if (playerPos == null)
            yield break;

        // Only Shoot if the player is near
        if (isNear)
        {
            Rigidbody instantiatedProjectile = Instantiate(projectile, canonPos.position, canonPos.rotation)as Rigidbody; 

            // get target in world space
            Vector3 targetPosition = playerPos.position;
            // set Z neutral pos
            targetPosition.z = 0;
            // calculate direction & velocity:
            Vector3 targetDelta = (transform.position - targetPosition);
            //set the launch vel and direction
            Vector3 launchVelocity = transform.TransformDirection(-targetDelta.normalized * projectileSpeed);
            // launch the projectile
            instantiatedProjectile.velocity = launchVelocity;
        }

        yield return new WaitForSeconds(1f);

        StartCoroutine("Shoot");

        yield break;

    }

    //health control
    void HealthControl(){

        //die
        if (health <=0)
        {
            this.gameObject.SetActive(false);
            // Application.LoadLevel (Application.loadedLevel);
        }
    }

    public void TakeDamage(float damageAmount, float shieldDamageAmount)
    {
        // if the shield is active the shield value is subtracted
        if (states == States.Guard && shield > 10 && haveShield)
        {
            shield -= shieldDamageAmount;
        }else
        {
            // if the shield is destroyed the health start subtract
            health -= damageAmount;
        }


        // print(health + " " + shield);
    }


}
