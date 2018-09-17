using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # Uncle.cs
  # Controller for the final boss - uncle
*-----------------------------------------------------------------------*/

/*
 * Creator: Tianqi Xiao, Yunzheng Zhou
 */

/*
*  animator: reference to Animator Controller
*  isSkill: check if the enemy is using skill attack
*  moveSpeed: the magnitude of uncle's speed
*  rotateSpeed: the angle of uncle's turn
*  player: player's position 
*  nav: the nav mesh agent
*  combat: reference to CharacterCombat
*  stats: reference to EnemyStats
*  rangeSkill1: reference to skill attack 1 game object
*  rangeSkill2: reference to skill attack 2 game object
*  rangeSkill3: reference to skill attack 3 game object
*  firePoint: reference to empty game object for shooting projectile
*  dist: distance between uncle and player
*  lowHealth: check if uncle is in low health state
*  spawn: check whether the spawn has started
*  skillType: random choose skill index
*  spawnPoint: reference to spawnPoint GameObject
*  attackgap1: bool check to make sure the wait time between skills is finished
*  attackgap2: bool check to make sure the wait time between skills is finished
*  attackgap3: bool check to make sure the wait time between skills is finished
*  tmp: update time
*/

public class Uncle : MonoBehaviour {

	private Animator animator;

	[HideInInspector]
	private bool isSkill;
	private float moveSpeed = 3f;
	private float rotateSpeed = 1f;

	[HideInInspector]
	public Transform player;
	NavMeshAgent nav;
	CharacterCombat combat;
	private EnemyStats stats;
	public GameObject rangeSkill1, rangeSkill2, rangeSkill3;
	public GameObject firePoint;
	public float dist;
	public bool lowHealth = false;
	public bool spawn = false;
	public int skillType = 1;
	public GameObject spawnPoint;
    private bool attackgap1, attackgap2, attackgap3;
    private float tmp;

	// Initialization
	void Start () {
		animator = GetComponent<Animator>();
		player = Player.instance.transform;
		nav = GetComponent<NavMeshAgent>();
		combat = GetComponent<CharacterCombat>();
		stats = GetComponent<EnemyStats>();
        tmp = Time.deltaTime;
	}

	/**
	 * Creator: Tianqi Xiao, Yunzheng Zhou
	 * Make dicision of boss's next action based on the distance between the boss and the player 
	 * and boss's current health. The skills will be randomly selected.
	 */
	void FixedUpdate () {
		lowHealth = checkLowHealth ();
		dist = Vector3.Distance(player.position, transform.position);

		if (dist < 3) {
			attackPlayer ();
		} else if (dist < 7) {
            //if (lowHealth) {
            if (Time.time > tmp + 0.8)
            {
                skillType = UnityEngine.Random.Range(1, 4);

                StartFiring(skillType);
                attackPlayer();
                /*} else {
                    skillType = 1;
                    StartFiring (skillType);
                    attackPlayer ();
                }*/
                isSkill = true;
            }
            else
            {
                FacePlayer();
                return;
            }
            
		} else if (dist < 14) {
			chasePlayer ();
		}

		if (isSkill && dist >= 7) {
			StopFiring (skillType);
			isSkill = false;
		}

		if (lowHealth && !spawn) {
			Spawn ();
			spawn = true;
		}
        FacePlayer();
        UpdateAnimator();
	}
	
	/**
	 * Creator: Tianqi Xiao
	 * Retrive player's position, face and chase player using transform and Quaternion functions.
	 */
	void chasePlayer ()
	{
		var direction =  player.transform.position - transform.position;
		transform.rotation = Quaternion.Slerp(transform.rotation,
			Quaternion.LookRotation(direction),
			rotateSpeed * Time.deltaTime);
		transform.Translate(0, 0, moveSpeed * Time.deltaTime);
	}

	/**
	 * Creator: Tianqi Xiao
	 * Retrive player's position, face player using transform and Quaternion functions.
	 */
	void FacePlayer()
	{
		Vector3 direction = (player.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
	}
	
	/**
	 * Creator: Tianqi Xiao
	 * Attack and deal damages to player by calling CharacterStats script.
	 */
	void attackPlayer()
	{
		CharacterStats playerStats = player.GetComponent<CharacterStats>();
		combat.Attack(playerStats);
		if (playerStats != null)
		{
			combat.Attack(playerStats);
		}
		FacePlayer();
	}
	
	/**
	 * Creator: Tianqi Xiao, Yunzheng Zhou
	 * Function used to instantiate projectile skill 1. 
	 */
	void Fire1() {
		GameObject fp = Instantiate(rangeSkill1, firePoint.transform.position, firePoint.transform.rotation);
        StopFiring(1);
		//fp.GetComponent<Rigidbody>().AddForce(firePoint.transform.forward * 500);
	}

	/**
	 * Creator: Tianqi Xiao, Yunzheng Zhou
	 * Function used to instantiate projectile skill 2. 
	 */
	void Fire2() {
		GameObject fp = Instantiate(rangeSkill2, firePoint.transform.position, firePoint.transform.rotation);
        StopFiring(2);
        //fp.GetComponent<Rigidbody>().AddForce(firePoint.transform.forward * 500);
    }

	/**
	 * Creator: Tianqi Xiao, Yunzheng Zhou
	 * Function used to instantiate projectile skill 1. 
	 */
	void Fire3() {
		GameObject fp = Instantiate(rangeSkill3, firePoint.transform.position, firePoint.transform.rotation);
        StopFiring(3);
        //fp.GetComponent<Rigidbody>().AddForce(firePoint.transform.forward * 500);
    }
	
	/**
	 * Creator: Tianqi Xiao
	 * Call different Fire functions based on the skilltype index selected and repeatly
	 * instantiate projectile skill.
	 * Parameter: skillType - and int variable indicating selected skill
	 */
	public void StartFiring(int skillType) {
        
		switch(skillType){
		case(1): 
            InvokeRepeating("Fire1", 0.7f, 2.0f);
			break;
		case(2):
            InvokeRepeating("Fire2", 0.7f, 2.0f);
			break;
		case(3):
            InvokeRepeating("Fire3", 0.7f, 2.0f);
            break;
		}
        tmp = Time.time;
	}
	
	/**
	 * Creator: Tianqi Xiao
	 * Call different Fire functions based on the skilltype index selected to 
	 * terminate projectile skill.
	 * Parameter: skillType - and int variable indicating selected skill
	 */
	public void StopFiring(int skillType) {

		switch(skillType){
		case(1):
                CancelInvoke("Fire1");
			break;
		case(2):
                CancelInvoke("Fire2");
			break;
		case(3):
                CancelInvoke("Fire3");
			break;
		}
	}
	
	/**
	 * Creator: Tianqi Xiao
	 * Call different Fire functions based on the skilltype index selected to 
	 * terminate projectile skill.
	 * Return: a bool variable indicating if the boss is in low health state
	 */
	bool checkLowHealth(){
		if (stats.currentHealth > 50f) {
			return false;
		} else {
			return true;
		}
	}
	
	/**
	 * Creator: Tianqi Xiao
	 * Update Animator of the boss by setting the parameters
	 */
	void UpdateAnimator() {
		animator.SetFloat("distance", dist);
		animator.SetInteger("skillType", skillType);
	}

	/**
	 * Creator: Tianqi Xiao
	 * Move the spawnPoint to boss's position and enable to spawn enemies
	 */
	void Spawn () 
	{
		if (stats.currentHealth > 0f) {
			spawnPoint.transform.position = transform.position;
			spawnPoint.SetActive (true);
		} else {
			spawnPoint.SetActive (false);
		}
	}


}
