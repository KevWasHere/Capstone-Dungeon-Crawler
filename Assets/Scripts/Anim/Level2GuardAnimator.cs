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
 *  navmeshAgent: reference to NavMeshAgent
 *  combat: reference to CharacterCombat
 *  stats: reference to CharacterStats
 * 
 */

/*
Creator: Tianqi Xiao, Shane Weerasuriya, Yunzhen 
 */
public class Level2GuardAnimator : MonoBehaviour
{

    public Animator animator;

    //NavMeshAgent navmeshAgent;
    CharacterCombat combat;
    EnemyStats stats;

    /*
	 * initialize variables, subscribe OnAttack to CharacterCombat delegate 
	 * 
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

    /*
	 * Function: OnAttack() 
	 * 
	 * Description: set trigger "Attack" calling the attack animation 
	 */
    public virtual void OnAttack()
    {
        animator.SetTrigger("Attack");
    }

    /*
	 * Function: OnDeath() 
	 * 
	 * Description: set trigger "Death" calling the death animation 
	 */
    void OnDeath()
    {
        animator.SetTrigger("Die");
    }

}
