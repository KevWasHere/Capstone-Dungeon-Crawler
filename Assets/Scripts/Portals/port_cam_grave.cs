/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hagen, Shane Weerasuriya, 
  #                 Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # 
*-----------------------------------------------------------------------*/

/*-------------------------------------------------------------------------*
# This script will teleport the player back to the village's portal front
#
#
# Creator : shane weerasuriya
*-----------------------------------------------------------------------*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class port_cam_grave : MonoBehaviour {

	public Transform playerCamera;
	public Transform portal;
	public Transform otherPortal;

	// Update is called once per frame
	void Update () {
		Vector3 playerOffsetFromPortal = playerCamera.position - otherPortal.position;
		transform.position = portal.position + playerOffsetFromPortal;

	}
}
