using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # LevelBossAnim.cs
  # Animator for enemeies
*-----------------------------------------------------------------------*/

/*
 * Creator: Tianqi Xiao, Myles Hagen, Kevin Ho
 */
/*
 *  navmeshAgent: reference to NavMeshAgent
 *  combat: reference to CharacterCombat
 *  stats: reference to CharacterStats
 */
public class LevelBossAnim : MonoBehaviour
{

	public Animator animator;
	//NavMeshAgent navmeshAgent;
	CharacterCombat combat;
	EnemyStats stats;

	/**
	 * Creator: Tianqi Xiao, Myles Hagen, Kevin Ho
	 * initialize variables, subscribe OnAttack to CharacterCombat delegate 
	 */
	protected virtual void Start()
	{
		//navmeshAgent = GetComponent<NavMeshAgent>();
		combat = GetComponent<CharacterCombat>();
		//stats = GetComponent<CharacterStats> ();
		stats = GetComponent<EnemyStats>();
		combat.OnAttack += OnAttack;
		stats.OnDeath += OnDeath;
	}

	/**
	 * Creator: Tianqi Xiao, Myles Hagen, Kevin Ho
	 * Function: OnAttack() 
	 * Description: set trigger "Attack" calling the attack animation 
	 */
	public virtual void OnAttack()
	{
		animator.SetTrigger("Attack");
	}

	/**
	 * Creator: Tianqi Xiao, Myles Hagen, Kevin Ho
	 * Function: OnDeath() 
	 * Description: set trigger "Death" calling the death animation 
	 */
	void OnDeath()
	{
		animator.SetTrigger("Die");
	}

}
