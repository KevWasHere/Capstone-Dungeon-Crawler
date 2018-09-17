/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Myles Hagen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # CharacterCombat.cs
  # Combat scripts for player and enemy
*-----------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: Myles Hagen, Tianqi Xiao, Shane Weerasuriya, Kevin Ho
 * attackSpeed - Speed of attack
 * attackCooldown - Length of attack cooldowns
 * attackDelay - Delay between attack
 * OnAttack - Calls animator on attacks
 * health - Health of object
 * myStats - Stats of object
 * enemyStats - Stats of enemy object
 * archerAttack - Reference to ArcherAttack script
 * playerController - Reference to playerController script
 * playerMotor - Reference to playerMotor script
 * defaultAttack - Reference to sword swing default attack hitbox
*/

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour {
	
	public float attackSpeed = 1f;
	private float attackCooldown = 0f;
	public float attackDelay = 0.5f;

	public event System.Action OnAttack;
	//public Transform health;

	CharacterStats myStats;
	CharacterStats enemyStats;
	
	ArcherAttack archerAttack;
	PlayerController2 playerController;
	PlayerMotor2 playerMotor;
	
	//Sword attack effect/hitbox
	//Default attack
	public GameObject defaultAttack;

	//Initialize components
	void Start ()
	{
		myStats = GetComponent<CharacterStats>();
		archerAttack = GetComponent<ArcherAttack>();
        playerController = Player.instance.playerController;
        playerMotor = GetComponent<PlayerMotor2>();;
	}

	//Update once per frame
	void Update ()
	{
		attackCooldown -= Time.deltaTime;
	}
	
	/*
	 * Object calls attack on a target
	 * Parameters: targetStats - Target object to attack
	 * Creator: Tianqi Xiao, Shane Weerasuriya
	 */
	public void Attack (CharacterStats targetStats)
	{
        //Cooldown is preventing multiple hits for skills
		if (attackCooldown <= 0f) {
			StartCoroutine(DoDamage(targetStats, attackDelay));
			
			//Prob wrong place. Move later
			//Player is using a sword to attack
			if (playerController.GetWeaponState() == 1 && tag == "Player") {
				StartCoroutine(DefaultSwordEffect(attackDelay));
                //GameObject swordEffect = Instantiate(defaultAttack, transform);
				//Destroy(swordEffect, 1.0f);	
			}

			if (OnAttack != null)
				OnAttack();

			Player.instance.playerStats.currentRage += 10;
			Player.instance.playerStats.currentRage = Mathf.Clamp(Player.instance.playerStats.currentRage, 0, Player.instance.playerStats.maxRage);
			attackCooldown = 1f / attackSpeed;
		}
		
	}
	
	/*
	Function: Coroutine to instantiate default sword attack
	Creator: Kevin Ho
	*/
	IEnumerator DefaultSwordEffect (float delay) {
        yield return new WaitForSeconds(delay);
		playerMotor.fireDefaultAttack();
		//GameObject swordEffect = Instantiate(defaultAttack, transform);
		//Destroy(swordEffect, 1.0f);	
	}
	
	/*
	Function: Call attack animation without calculating damage. For Destructable Objects
	Creator: Kevin Ho
	*/
	public void noDamageAttack() {
		if (attackCooldown <= 0f) {
			//Player is using a sword to attack
			if (playerController.GetWeaponState() == 1 && tag == "Player") {
				StartCoroutine(DefaultSwordEffect(attackDelay));
				//GameObject swordEffect = Instantiate(defaultAttack, transform);
				//Destroy(swordEffect, 1.0f);	
			}	
			if (OnAttack != null)
				OnAttack();
			attackCooldown = 1f / attackSpeed;
		}
	}

	//NEED TO REMOVE DELAY AND MOVE TO ONDEATH INSTEAD
	/*
	Coroutine to calculate damage and play animation
	Parameters: stats - object stats, delay - Length of coroutine delay
	Return: Return after delay
	Creator: Tianqi Xiao, Shane Weerasuriya
	*/
	IEnumerator DoDamage (CharacterStats stats, float delay)
	{
        yield return new WaitForSeconds(delay);
		stats.TakeDamage(myStats.damage.GetValue());
	}
	
	//Change/remove later. Temp function for multihitting skills
	/*
	Object calls skill attack on a target
	Parameters: targetStats - Target object to attack
	Creator: Kevin Ho , Myles Hagen
	*/
	public void ProjSkillAttack (CharacterStats targetStats) {

        targetStats.TakeDamage(Player.instance.projSkillStats.Damage.GetValue());
	}

    public void AOESkillAttack(CharacterStats targetStats)
    {

        targetStats.TakeDamage(Player.instance.aoeSkillStats.Damage.GetValue());
    }

    public void DashSkillAttack(CharacterStats targetStats)
    {

        targetStats.TakeDamage(Player.instance.dashSkillStats.Damage.GetValue());
    }

    /*
	Object calls enemy archer attack on a target
	Parameters: targetStats - Target object to attack
	Creator: Kevin Ho, Myles Hagen
	*/
    public void ArcherAttack (CharacterStats targetStats)
	{
		//Cooldown is preventing multiple hits for skills
		if (attackCooldown <= 0f) {
			//this.enemyStats = enemyStats;
			//targetStats.TakeDamage(myStats.damage.GetValue());
			//Debug.Log (transform.name + " short for " + targetStats.damage.GetValue () + " damage");
			
			//StartCoroutine(DoDamage(targetStats, punchAttackDelay));
			//archerAttack.fireArcherAttack();
			
			//Prob wrong place. Move later
			//Player is using a sword to attack
			if (playerController.GetWeaponState() == 1 && this.tag == "Player") {
				GameObject swordEffect = Instantiate(defaultAttack, this.transform);
			}

			if (OnAttack != null)
				OnAttack();

			//Debug.Log (transform.name + " shot it's arrow");
			
			attackCooldown = 1.5f / attackSpeed;
		}
		
	}
}
