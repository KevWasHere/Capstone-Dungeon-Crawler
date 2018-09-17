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
using UnityEngine.AI;
public class teleporter : MonoBehaviour
{

	public float x, y, z;
	// when player enters the collider it will trigger and teleport the player to the village
    void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			
			Player.instance.GetComponent<NavMeshAgent> ().enabled = false;
			Player.instance.transform.position = new Vector3 (x, y, z);
			Player.instance.GetComponent<NavMeshAgent> ().enabled = true;
        
		}
	}
}