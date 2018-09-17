using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone
  # BossChase.cs
  # ControllControl Chase states event in animator
*-----------------------------------------------------------------------*/

/*
 * Creator: Tianqi Xiao
 */
public class BossChase : BossFSM {

	//OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		base.OnStateEnter(animator, stateInfo, layerIndex);
	}

	/**
	 * Creator: Tianqi Xiao
	 * Face and chase player when in range
	 */
	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		var direction = opponent.transform.position - boss.transform.position;
		boss.transform.rotation = Quaternion.Slerp(boss.transform.rotation,
                                    Quaternion.LookRotation(direction),
                                    rotateSpeed * Time.deltaTime);
        boss.transform.Translate(0, 0, moveSpeed * Time.deltaTime);
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		
	}
}
