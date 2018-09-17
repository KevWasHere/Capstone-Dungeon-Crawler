/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hagen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # ActivatePortal.cs
*-----------------------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * rpc - reference to runeportalcontroller
 * enterTrigger = bool to determine if player enter or exit trigger
 * delay - delay before portal animation is activated or deactivated
 * 
 * Creator: Myles Hagen, Tianqi
 */

public class ActivatePortal : MonoBehaviour {

	RunePortalController rpc;
	bool enterTrigger = false;
    
    float delay = 5f;

	// get reference to RunePortalController
	void Start () {
	
		rpc = GetComponent<RunePortalController>();
	}

	// toggle portal animation when animation is finished playing or set coroutine
	// to toggle the animation after a delay
    void OnTriggerEnter(Collider other) {
        //Debug.Log("SUP");
		if (other.tag == "Player") {
            
            enterTrigger = true;
            if (rpc.effectsAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0)
                rpc.TogglePortal();
            else
                StartCoroutine(TurnOffPortal(delay));
		}
	}

	// toggle portal animation when animation is finished playing or set coroutine
	// to toggle the animation after a delay
	void OnTriggerExit(Collider other) {
		if (other.tag == "Player") {
            enterTrigger = false;
            if (rpc.effectsAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                rpc.TogglePortal();
            else
                StartCoroutine(TurnOffPortal(delay));
        }
	}

	/*
	 * Function: TurnOffPortal
	 * parameters: delay
	 * description: toggle the animation for the portal after a delay, this is because
	 * the animation cannot be toggled while it is still running.
	 */
    IEnumerator TurnOffPortal(float delay)
    {
        yield return new WaitForSeconds(delay);
        rpc.TogglePortal();


    }
}
