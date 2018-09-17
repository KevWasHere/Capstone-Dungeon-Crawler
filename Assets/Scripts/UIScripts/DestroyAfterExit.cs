using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone
  # CharMenuControl.cs
  # desytroyafterExit
*-----------------------------------------------------------------------*/

/*Creator: Yan Zhang
* destroy the object if player exit the collider
*/
public class DestroyAfterExit : MonoBehaviour {
	//destroy the game object after exit the collider
	void OnTriggerExit(Collider other) {
		Destroy (this.gameObject);
	}
}
