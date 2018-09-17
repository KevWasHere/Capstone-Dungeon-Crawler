using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # CharacterAnimator.cs
  # Animator for enemeies
*-----------------------------------------------------------------------*/

/*
*  animator: reference to Animator Controller
*  combat: reference to CharacterCombat
*  stats: reference to CharacterStats
*/

/*
Creator: Tianqi Xiao, Yunzheng Zhou, Kevin Ho
*/
public class UncleAnim : MonoBehaviour
{

	public Animator animator;
	CharacterCombat combat;
	EnemyStats stats;

	/**
	 * Creator: Tianqi Xiao, Yunzheng Zhou, Kevin Ho
	 * initialize variables, subscribe OnAttack to CharacterCombat delegate 
	 */
	protected virtual void Start()
	{
		stats = GetComponent<EnemyStats>();
		stats.OnDeath += OnDeath;
	}
		
	/**
	 * Creator: Tianqi Xiao, Yunzheng Zhou, Kevin Ho
	 * Function: OnDeath() 
	 * Description: set trigger "Death" calling the death animation 
	 */
	void OnDeath()
	{
		animator.SetTrigger("Die");
	}

}
