/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hagen, Shane Weerasuriya, 
  #                 Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # 
*-----------------------------------------------------------------------*/

// Creator : Shane , Kevin Ho
// POsition transform

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionBasedOnTargetVelocity : MonoBehaviour {

    public Rigidbody target;
    public float time = 1.0f;
    // updates every frame and changes the position 
    void FixedUpdate () {

        transform.position = target.position + target.velocity;
	}

    
}
