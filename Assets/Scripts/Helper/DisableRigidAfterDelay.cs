/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hagen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # DisableRigidAfterDelay.cs
*-----------------------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * rb - reference to rigidbody
 * delay - time before rigidbody is disabled
 * disable - bool set to true when disabling rigidbody
 * 
 * Creator: Myles Hagen, Tianqi Xiao
 */

public class DisableRigidAfterDelay : MonoBehaviour {

    Rigidbody rb;
    float delay = 10f;
    bool disable;

    // get reference to rigidbody and disable it with coroutine
    void Start () {
        disable = true;
        rb = GetComponent<Rigidbody>();
        StartCoroutine(DisableRigidbody(delay));
    }

	/*
	 * Function: DisableRigidBody
	 * parameters: delay
	 * Description: coroutine that disables rigidbody after a delay
	 */
    IEnumerator DisableRigidbody(float delay)
    {
        yield return new WaitForSeconds(delay);
        rb.isKinematic = true;


    }
}
