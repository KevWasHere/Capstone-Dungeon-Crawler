/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # VillagerAnimator.cs
  # Animator for villagers
*-----------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 *  animator: reference to Animator
 *  navmeshAgent: reference to NavMeshAgent
 *  aiController: reference to AIController
 */

 /*
 Creator: Kevin Ho, Tianqi Xiao, Shane Weerasuriya
  */
  
public class VillagerAnimator : MonoBehaviour {
	
	public Animator animator;

	NavMeshAgent navmeshAgent;
	AIController aiController;

	/*
	 * initialize variables, subscribe OnAttack to CharacterCombat delegate 
	 */
	protected virtual void Start() {
		navmeshAgent = GetComponent<NavMeshAgent> ();
		aiController = GetComponent<AIController> ();
		
		aiController.OnWave += OnWave;
	}

	/*
	 * Update animator speed percent each frame
	 */
	protected virtual void Update () {
		animator.SetFloat ("Speed Percent", navmeshAgent.velocity.magnitude/navmeshAgent.speed,.1f,Time.deltaTime);
	}
	
	/*
	Wave animation
	*/
	public virtual void OnWave() {
		animator.SetTrigger("Wave");
	}
}
