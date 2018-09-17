using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone
  # BossFSM.cs
  # Controller of Boss Finite State Machine
*-----------------------------------------------------------------------*/

/*
 * Creator: Tianqi Xiao
 */
/*
 * boss: the boss game object
 * opponent: the target game object
 * moveSpeed: the speed of movement
 * rotateSpeed: the speed of rotation
 * accu: the accuracy scale
 */
public class BossFSM : StateMachineBehaviour {

	public GameObject boss;
	public GameObject opponent;
	public float moveSpeed = 2.0f;
	public float rotateSpeed = 1.0f;
	public float accu = 2.0f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.gameObject;
        opponent = Player.instance.gameObject;
    }
}
