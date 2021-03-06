using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # PlayerMotor2.cs
  # Motor functions for player character
*-----------------------------------------------------------------------*/

/*
Player motor. Contains motor functions that control the player such as moving,
interacting, and attacking. Also contains player focus to mov to.
Creator: Kevin Ho, Myles Hagen, Shane Weerasuriya, Tianqi Xiao
*/

/*
 * target - Focus target
 * agent - Player nav mesh agent
 * defaultAttackRate - Default attack skill rate
 * defaultAttack - Reference to defaultAttack game object
 * defaultAttackSpawn - Default attack skill spawn
 * nextDefaultAttack - Default attack next fire rate
 * defaultAttackRage - Rage cost of a default attack
 * dashAttackRate - Dash attack skill rate
 * dashAttack - Reference to dashAttack game object
 * dashAttackSpawn - Dash attack skill spawn
 * nextDashAttack - Dash attack next fire rate
 * dashAttackDestroy - Time to destory dash attack hitbox
 * dashAttackRage - Rage cost of a dash attack
 * dashObject - Copy of dash attack hitbox
 * destination - Dash attack destination
 * projectileAttackRate - Projectile attack skill rate
 * projectileAttack - Reference to projectileAttack game object
 * projectileAttackSpawn - Projectile attack skill spawn
 * nextProjectileAttack - Projectile attack next fire rate
 * projectileAttackDestroy - Time to destory projectile attack hitbox
 * projectileAttackRage - Rage cost of a projectile attack
 * AOEAttackRate - AOE attack skill rate
 * AOEAttack - Reference to AOEAttack game object
 * AOEAttackSpawn - AOE attack skill spawn
 * nextAOEAttack - AOE attack next fire rate
 * AOEAttackDestroy - Time to destory AOE attack hitbox
 * AOEAttackRage - Rage cost of a AOE attack
 * AOEObject - Copy of AOE attack hitbox
 * playerController - Reference to PlayerController script
 * testAnimator - Reference to animator
 * footsteps - Player footsteps SFX
 * projectileStart - Player projectile startup SFX
 * nextStep - Time of next footstep SFX called
 * stepRate - Rate of footstep SFX calls
 */

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(PlayerController2))]
public class PlayerMotor2 : MonoBehaviour {

	Transform target;
	NavMeshAgent agent;     // Reference to our NavMeshAgent
	
	//Default attack
	public float defaultAttackRate = 1.5f;
	public GameObject defaultAttack;
	public Transform defaultAttackSpawn;
	private float nextDefaultAttack;
	private float defaultAttackDestroy = 1.0f;
	private float defaultAttackRage = 20.0f;
	
	//Dash attack
	public float dashAttackRate = 1.0f;
	public GameObject dashAttack;
	public Transform dashAttackSpawn;
	public Transform dashAttackDestination;
	private float nextDashAttack;
	private float dashAttackDestroy = 1.0f;
	private float dashAttackRage = 20.0f;
	GameObject dashObject;
	private Vector3 destination;

	
	//Projectile attack
	public float projectileAttackRate = 1.5f;
	public GameObject projectileAttack;
	public Transform projectileAttackSpawn;
	private float nextProjectileAttack;
	private float projectileAttackDestroy = 1.5f;
	private float projectileAttackRage = 20.0f;
	
	
	//AOE attack
	public float AOEAttackRate = 4.0f;
	public GameObject AOEAttack;
	public Transform AOEAttackSpawn;
	private float nextAOEAttack;
	private float AOEAttackDestroy = 4.0f;
	private float AOEAttackRage = 20.0f;
	GameObject AOEObject;
	
	//Buff attack
	public float buffAttackRate = 5.0f;			//Make sure rate is ALWAYS lower then destory
	public GameObject buffAttack;
	public Transform buffAttackSpawn;
	private float nextBuffAttack;
	private float buffAttackDestroy = 5.0f;		//Otherwise permanent buff stack will occur 
	private float buffAttackRage = 20.0f;
	private int buffValue = 10;
	GameObject buffObject;
	private int originalBuffValue;
	
	//Debuff attack
	public float debuffAttackRate = 5.0f;			//Make sure rate is ALWAYS lower then destory
	public GameObject debuffAttack;
	private float nextDebuffAttack;
	[HideInInspector]
	public float debuffAttackDestroy = 5.0f;		//Otherwise permanent debuff stack will occur 
	private float debuffAttackRage = 20.0f;
	[HideInInspector]
	public int debuffValue = 10;

	//PlayerController reference
	PlayerController2 playerController;
	//Animator reference
	TestAnimator testAnimator;
	//PlayerStats reference
	PlayerStats playerStats;
	
	//Audio
	public AudioClip footsteps;
	public AudioClip projectileStart;
	
	private float nextStep;
	private float stepRate = 1.0f;

	//Initialize components
	void Start ()
	{
		agent = GetComponent<NavMeshAgent>();
		playerController = GetComponent<PlayerController2>();
		playerStats = GetComponent<PlayerStats>();
		testAnimator = GetComponent<TestAnimator>();
		playerController.onFocusChangedCallback += OnFocusChanged;
	}

	//Update once per frame
	void Update ()
	{
		//Play footsteps SFX. Get speed of animation for timing
		if (agent.remainingDistance > 0.1f) {
			if (Time.time > nextStep) {
				nextStep = Time.time + stepRate - testAnimator.getSpeed()/1.25f;
				if (testAnimator.getSpeed() > 0.1) {
					AudioSource.PlayClipAtPoint(footsteps, transform.position, 0.25f);
				}
			}
		}
		if (target != null) {
			MoveToPoint (target.position);
			FaceTarget ();
		}
		
		//If dash attack was called, dash to destination
		if (dashObject != null) {

			transform.position = Vector3.Lerp(transform.position, destination, 5.0f * Time.deltaTime);
			
			//Calculate remaining distance till destination
			Vector3 sqrDestination = destination - transform.position;
			//Debug.Log("Square destination : " + sqrDestination.sqrMagnitude);
			if (sqrDestination.sqrMagnitude <= 0.07f) { //Square float
				transform.position = destination;
			}
		}
		//If AOE attack was called, update AOEObject position
		if (AOEObject != null) {
			AOEObject.transform.position = transform.position;
		}
		
		//If buff attack was called, update buffObject position
		if (buffObject != null) {
			buffObject.transform.position = transform.position;
		}
	}
	
	/*
	Move player to target
	*/
	public void MoveToPoint (Vector3 point) {
		agent.SetDestination(point);
	}

	/*
	 * Function: OnFocusChanged
	 * Parameters: new focus interactable
	 * 
	 * Description: if interactable exists set target to newfocus
	 * otherwise set target to null
	 * 
	 */
	void OnFocusChanged (Interactable newFocus)
	{
		if (newFocus != null)
		{
			agent.stoppingDistance = newFocus.radius*.8f;
			agent.updateRotation = false;

			target = newFocus.interactionTransform;
		}
		else
		{
			agent.stoppingDistance = 0f;
			agent.updateRotation = true;
			target = null;
		}
	}

	/*
	 * Function: FaceTarget
	 * 
	 * Description: rotate enemy to face target with some smoothing 
	 * 
	 */
	void FaceTarget()
	{
		
		Vector3 direction = (target.position - transform.position).normalized;
		//if (direction.x != 0.0f && direction.z != 0.0f) {
			Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
			transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
		//}
	}
	
	//Clean up attack code later. Code pretty much does the same thing each time
	/*
	Fires default skill attack
	Creator: Kevin Ho
	*/
	public void fireDefaultAttack() {
		//Spawn default attack hitbox
		GameObject hitBox = Instantiate(defaultAttack, defaultAttackSpawn.position, defaultAttackSpawn.rotation);
		Destroy(hitBox, defaultAttackDestroy);		
	
	}
	
	/*
	Fires default skill attack
	Creator: Kevin Ho, Myles Hagen
	*/
	/*
	public void fireDefaultAttack() {
		if (Time.time > nextDefaultAttack) {
			nextDefaultAttack = Time.time + defaultAttackRate;
			//Spawn default attack hitbox
			GameObject hitBox = Instantiate(defaultAttack, defaultAttackSpawn.position, defaultAttackSpawn.rotation);
			Destroy(hitBox, defaultAttackDestroy);		
		}
	}
	*/
	
	/*
	Fires dash skill attack
	Creator: Kevin Ho, Shane Weerasuriya
	*/
	public void fireDashAttack() {
        //Debug.Log("Time.time: " + Time.time + "nextDashAttack: " + nextDashAttack);
        //if (Time.time > nextDashAttack)
       // {
            
            destination = dashAttackDestination.position;
            //destination.z += some stats
            Player.instance.playerStats.currentRage += ((int)dashAttackRage + Player.instance.dashSkillStats.RageGeneration.GetValue());
            /*if (Player.instance.playerStats.currentRage > Player.instance.playerStats.maxRage)
                Player.instance.playerStats.currentRage = Player.instance.playerStats.maxRage;*/
            Player.instance.playerStats.currentRage = Mathf.Clamp(Player.instance.playerStats.currentRage, 0, Player.instance.playerStats.maxRage);

            //Debug.Log(Player.instance.dashSkillStats.RageGeneration.GetValue());
            //Player.instance.playerStats.currentRage += dashAttackRage;
            //Player.instance.playerStats.currentRage += Player.instance.dashSkillStats.RageCost.GetValue();

            MoveToPoint(destination);
            nextDashAttack = Time.time + dashAttackRate;

            //Spawn default attack hitbox
            dashObject = Instantiate(dashAttack, dashAttackSpawn.position, dashAttackSpawn.rotation);
            Destroy(dashObject, dashAttackDestroy);
        //}
           
	}
	
	/*
	Fires projectile skill attack
	Creator: Kevin Ho, Tianqi
	*/
	public void fireProjectileAttack() {

        if (Time.time > nextProjectileAttack) {

            if (Player.instance.playerStats.currentRage >= projectileAttackRage) {
			    Player.instance.playerStats.currentRage -= (int)projectileAttackRage;
			    nextProjectileAttack = Time.time + projectileAttackRate;
			//Spawn default attack hitbox
			    GameObject hitBox = Instantiate(projectileAttack, projectileAttackSpawn.position, projectileAttackSpawn.rotation);
			    AudioSource.PlayClipAtPoint(projectileStart, transform.position, 1.0f);
			    Destroy(hitBox, projectileAttackDestroy);
		    }
		    else
			    Debug.Log("Not enough rage.");
		}
	}	
	
	/*
	Fires player AOE skill
	Creator: Kevin Ho
	*/
	public void fireAOEAttack() {
	if (Time.time > nextAOEAttack) {
		if (Player.instance.playerStats.currentRage >= AOEAttackRage) {
			float newAttackRate = AOEAttackRate;
			newAttackRate += Player.instance.aoeSkillStats.LastTime.GetValue();
			//nextAOEAttack = Time.time + AOEAttackRate;
			nextAOEAttack = Time.time + newAttackRate;
			//Spawn default attack hitbox

			AOEObject = Instantiate(AOEAttack, AOEAttackSpawn.position, Quaternion.Euler(new Vector3(0, 0, 0)));
			AOEObject.transform.localScale *= (1 + Player.instance.aoeSkillStats.Radius.GetValue());
			Player.instance.playerStats.currentRage -= (int)AOEAttackRage;
			//Destroy(AOEObject, AOEAttackDestroy);
			Destroy(AOEObject, AOEAttackDestroy);

			/*
			float attackRate = 0.0f;
			while (attackRate != AOEAttackRate)
				AOEObject = Instantiate(AOEAttack, AOEAttackSpawn.position, Quaternion.Euler(new Vector3(0, 0, 0)));
				//Destroy(AOEObject, AOEAttackRate);
				Destroy(AOEObject);
				attackRate = Time.time;
				Debug.Log(attackRate);
				*/
			//StartCoroutine(delayAOEBool(AOEAttackRate));
            }
		else
			Debug.Log("Not enough rage.");
        }
	}
	
	/*
	Fires buff skill attack
	Make sure rate of buff is ALWAYS less then destory to prevent permanent stacking
	Creator: Kevin Ho
	*/
	public void fireBuffAttack() {
		//Debug.Log("Buff Attack Fire!");
		if (Time.time > nextBuffAttack) {
			//Get player stats
			originalBuffValue = playerStats.damage.baseValue;		//Might change to better way in the future to prevent stacking
			playerStats.damage.baseValue += buffValue;
			buffObject = Instantiate(buffAttack, buffAttackSpawn.position, Quaternion.Euler(new Vector3(0, 0, 0)));
			Destroy(buffObject, buffAttackDestroy);
			
			nextBuffAttack = Time.time + buffAttackRate;
			
			//Coroutine to remove buff
			StartCoroutine(removeBuff(buffAttackDestroy));
		}
	}
	
	/*
	Fires debuff AOE skill attack
	Parameters: hit - Raycast of mouse position for spawn location
	Creator: Kevin Ho
	*/
	public void fireDebuffAOEAttack(RaycastHit hit) {
		if (Time.time > nextDebuffAttack) {
			//Get enemy stats
			
			GameObject hitBox = Instantiate(debuffAttack, hit.point + new Vector3(0f, 0.5f, 0f), Quaternion.Euler(new Vector3(0, 0, 0)));
			Destroy(hitBox, debuffAttackDestroy);	
			
			nextDebuffAttack = Time.time + debuffAttackRate;
		}
	}
	
	/*
	Coroutine to wait before removing buff effect
	Parameters: delay - float of time delay of attack
	Creator: Kevin Ho
	*/
	IEnumerator removeBuff(float delay) {
		yield return new WaitForSeconds(delay);
		playerStats.damage.baseValue = originalBuffValue;
	}
	
	/*
	Rotate player to attack direction
	Parameters: targetRotation - Rotate position of the player
	Creator: Kevin Ho
	*/
	public void rotatePlayer(Quaternion targetRotation) {
		this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, 250f * Time.deltaTime);
	}
}


