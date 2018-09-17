/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # HorseAnimator.cs
  # Animator for horses in village
*-----------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 *  animator: reference to Animator
 *  navmeshAgent: reference to NavMeshAgent
 */
 
 /*
 Creator: Kevin Ho , Tianqi Xiao, Shane Weerasuriya
  */
  
public class HorseAnimator : MonoBehaviour {
	
	public Animator animator;

	NavMeshAgent navmeshAgent;

	/*
	 * initialize variables, subscribe OnAttack to CharacterCombat delegate 
	 */
	protected virtual void Start() {
		navmeshAgent = GetComponent<NavMeshAgent> ();
	}

	/*
	 * Update animator speed percent each frame
	 */
	protected virtual void Update () {
		animator.SetFloat ("Speed Percent", navmeshAgent.velocity.magnitude/navmeshAgent.speed,.1f,Time.deltaTime);
	}
}
