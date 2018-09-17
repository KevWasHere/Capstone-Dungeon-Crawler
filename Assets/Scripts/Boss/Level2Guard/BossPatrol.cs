using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone
  # BossPatrol.cs
  # ControllControl Patrol states event in animator
*-----------------------------------------------------------------------*/

/*
 * Creator: Tianqi Xiao
 */
/*
 * rangePoint: an array of points to patrol
 * current: the current patrol point
 */
public class BossPatrol : BossFSM {

	GameObject[] rangePoint;
	int current;

	//calls when awake
    void Awake(){
		rangePoint = GameObject.FindGameObjectsWithTag("rangePoint");
	}

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        current = 0;
	}

	/**
	 * Creator: Tianqi Xiao
	 * Patrol through all range points one by one
	 */
	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (rangePoint.Length == 0) 
			return;
		if (Vector3.Distance(rangePoint[current].transform.position, boss.transform.position) < accu) {
				current++;
		}
        if (current >= rangePoint.Length)
        {
            current = 0;
        }
		var direction = rangePoint[current].transform.position - boss.transform.position;
		boss.transform.rotation = Quaternion.Slerp(boss.transform.rotation, 
									Quaternion.LookRotation(direction), 
									rotateSpeed * Time.deltaTime);
		boss.transform.Translate(0, 0, moveSpeed * Time.deltaTime);
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	
	}

}
