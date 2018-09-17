
/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hagen, Shane Weerasuriya, 
  #                 Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # 
*-----------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLeadPosition : MonoBehaviour {

    public Rigidbody target;
    public float projectileVelocity;

	
	void FixedUpdate () {

        float distance = (transform.position - target.position).magnitude;
        float timeToTarget = distance / projectileVelocity;
        transform.position = target.position + target.velocity * timeToTarget;
	}
}
