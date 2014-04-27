﻿using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	//variables
	#region Variables 
	public Transform canonPos;
   
    public float maxHealthValue = 100;
    public float maxShieldValue = 30;
    public float speed = 7;//player speed    
    public float jumpSpeed = 15;//jump velocity
    public float gravity = 30;//player gravity
    public Rigidbody projectile;
    public int projectileSpeed = 20;

    public bool jumpRelease = false;//when jump is pressed

	public static Vector2 mov = new Vector2(0,0); //movement
	public static Vector3 movDirection = Vector3.zero;// movement direction
	public static float health; //character max health
	public static float maxHealth ; //character health
	public static float maxShield; //character max shield
	public static float shield; //character shield
	public static float fatalityDamage; //character fatality damage
	public static float meleeDamage; //character melee attack damage
	public static float commonRangeDamage; //character common range attack damage
	public static float specialRangeDamage;//character special range attac damage


	private CharacterController controller; //controller reference
	private GameObject[] enemyObj;//enemy objects
	private Vector2 mousePos;//mouse position

    private States states  =  States.Stand; // to set the machineSates

	
	
	#endregion

	//Use this for initialization
	void Start () {

        // stateMachine

		this.controller = GetComponent<CharacterController>();
        maxShield = maxShieldValue;
		shield = maxShield;
        maxHealth = maxHealthValue;
		health = maxHealth;
	}

	//Update is called once per frame
	void Update () {
		//move
		MovementControl();
		//combat control
		CombatControl();
		//shield control
		ShieldControl();
		//health control
		HealthControl();
		//shoot
		if(Input.GetMouseButtonDown (0) )
		{
			Rigidbody instantiatedProjectile  = Instantiate(projectile,canonPos.position, canonPos.rotation)as Rigidbody; 
			
			// get current distance from camera
			//float distFromCam = Camera.main.transform.InverseTransformPoint(instantiatedProjectile).z;
			
			// get target in world space, at same distance from camera:
			Vector3 targetScreenPos = Input.mousePosition;
			targetScreenPos.z = 10;
			Vector3 targetPosition = Camera.main.ScreenToWorldPoint(targetScreenPos);
			
			// calculate direction & velocity:
			Vector3 targetDelta = (transform.position - targetPosition);
			Vector3 launchVelocity = targetDelta.normalized * projectileSpeed;
			
			instantiatedProjectile.velocity = launchVelocity;
		}


		

	}

	//running
	public bool IsRunning(){
		return controller.isGrounded;
	}

	//movement control
	void MovementControl(){
		//Input movement
		mov.x = Input.GetAxis("Horizontal");
		//if not jumping
		if(!Input.GetButton("Jump")){
			jumpRelease = true;			
		}
		//jump
		if(controller.isGrounded){
			movDirection.y = 0f;
			if(Input.GetButton("Jump") ) {
				movDirection.y = jumpSpeed;
				jumpRelease = false;
			}
		}
		
		//movement
		mov.x *= speed;
		movDirection.y -= gravity * Time.deltaTime;
		movDirection.x = mov.x;
		controller.Move(movDirection * Time.deltaTime);
	}
		

	//combat control
	void CombatControl(){

		RangedAttack();

		//fatality
		if(Input.GetKeyDown("f")) 
        {
			Fatality();
		}
	}

	//ranged attack control
	void RangedAttack(){

	}
	
	//fatallity
	void Fatality(){
		print("Fatality");
			enemyObj =  GameObject.FindGameObjectsWithTag("Enemy");
			foreach(GameObject other in enemyObj)
			{
				if(Vector3.Distance(other.transform.position,this.transform.position) < 5){
					Destroy(other.gameObject);
				}
			}	
	}
	
	//shield control
	void ShieldControl()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if ( shield > 0.1f )
            {
                // set the player active the shield
                states = States.Guard;
                print(states);
            }else
            {
                // if the shield is destroyed player status return to normal
                states = States.Stand;
              
            }
        }
        else
        {
            // print(states);
        }
		if (shield < maxShield)
        {
            shield += 0.1f + Time.time ;

		}else if(shield > maxShield)
        {
			shield=maxShield;
		}
	}
	
	//health control
	void HealthControl(){

		//die
		if (health <=0)
        {
            this.gameObject.SetActive(false);
			Application.LoadLevel (Application.loadedLevel);
		}
	}

    public void TakeDamage(float damageAmount, float shieldDamageAmount)
    {
        // if the shield is active the shield value is subtracted
        if (states == States.Guard && shield > 0.1f)
        {
            shield -= shieldDamageAmount;
        }else
        {
            // if the shield is destroyed the health start subtract
            health -= damageAmount;
        }

      
        print(health + " " + shield);
    }

	//death
    void OnDisable() {
	}

}
