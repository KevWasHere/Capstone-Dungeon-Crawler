/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hagen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # CharacterAnimator.cs
  # Animator for enemeies
*-----------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 *  animator: reference to Animator
 *  navmeshAgent: reference to NavMeshAgent
 *  combat: reference to CharacterCombat
 *  stats: reference to CharacterStats
 */
 
 /*
 Creator: Myles Hagen, Shane Weerasuriya
  */
public class CharacterAnimator : MonoBehaviour {
	
	public Animator animator;

	NavMeshAgent navmeshAgent;
	CharacterCombat combat;
	EnemyStats stats;

	/*
	 * initialize variables, subscribe OnAttack to CharacterCombat delegate 
	 */
	protected virtual void Start() {
		navmeshAgent = GetComponent<NavMeshAgent> ();
		combat = GetComponent<CharacterCombat> ();
		//stats = GetComponent<CharacterStats> ();
        stats = GetComponent<EnemyStats>();
		combat.OnAttack += OnAttack;
		stats.OnDeath += OnDeath;
	}

	/*
	 * Update animator speed percent each frame
	 */
	protected virtual void Update () {
		animator.SetFloat ("Speed Percent", navmeshAgent.velocity.magnitude/navmeshAgent.speed,.1f,Time.deltaTime);
	}

	/*
	 * Function: OnAttack() 
	 * 
	 * Description: set trigger "Attack" calling the attack animation 
	 */
	public virtual void OnAttack() {
		animator.SetTrigger ("Attack");
	}

	/*
	 * Function: OnDeath() 
	 * 
	 * Description: set trigger "Death" calling the death animation 
	 */
	void OnDeath() {
		animator.SetTrigger ("Death");
	}

}
