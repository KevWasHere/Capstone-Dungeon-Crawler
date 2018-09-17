using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # TestAnimator.cs
  # Animator for main character
*-----------------------------------------------------------------------*/

/*
 *  navmeshAgent: reference to NavMeshAgent
 *  combat: reference to CharacterCombat
 *  ac: reference to PlayerController
 * 
 */
 
  /*
 Creator: Myles Hangen, Yan Zhang, Yunzheng Zhou
  */
public class TestAnimator : MonoBehaviour {
	
	public Animator animator;

	NavMeshAgent navmeshAgent;
	CharacterCombat combat;
	PlayerController2 ac;
	Destructable destructable;
	PlayerStats stats;

	/*
	 * initialize variables, subscribe OnAttack to CharacterCombat delegate 
	 * 
	 */
	protected virtual void Start() {
		stats = Player.instance.playerStats;
		stats.OnDeath += OnDeath;
		//animator = GetComponent<Animator>();
		ac = GetComponentInChildren<PlayerController2>();
		navmeshAgent = GetComponent<NavMeshAgent> ();
		combat = GetComponent<CharacterCombat> ();
		combat.OnAttack += OnAttack;
		//destructable = GetComponent<Destructable>();
		//destructable.OnAttack += OnAttack;
	}

	/*
	 * Update animator speed percent each frame
	 * 
	 */
	protected virtual void Update () {
		animator.SetFloat ("Speed Percent", navmeshAgent.velocity.magnitude/navmeshAgent.speed,.1f,Time.deltaTime);
	}

	/*
	 * Function: OnAttack() 
	 * 
	 * Description: Change weapon index to display correct animation
	 * depending on the weaponState that is set in the character controller
	 * then set trigger Attack to run the attack animation
	 */
	protected virtual void OnAttack ()
	{
		if (ac.WeaponState == 0) 
			animator.SetFloat("Weapon Index", 0f);
		if (ac.WeaponState == 1)
			animator.SetFloat("Weapon Index", 1f);
		
		animator.SetTrigger ("Attack");
	}
	
	/*
	Function - Get speed percent of animation for playerController
	Return - Float of speed percent
	*/
	public float getSpeed() {
		return animator.GetFloat("Speed Percent");
	}

	void OnDeath() {
		animator.SetTrigger ("Death");
	}
}
