/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hagen, Shane Weerasuriya, 
  #                 Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # 
*-----------------------------------------------------------------------*/

/*-------------------------------------------------------------------------*
# This script checks if the player collides with the plane 
# Then it changes the camera to the portal view camera.
# when walking away from the camera it turns back to the main camera.
#
#
# Creator : shane weerasuriya
*-----------------------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portAcollider : MonoBehaviour {

	// current cam
	public Camera playercam;
    // camera that needs to be activated to be switched 
    public Camera toswitchto;

    // on awake setting the status of the cameras respective to the scene
	void Awake(){
		playercam.gameObject.SetActive (true);
		toswitchto.gameObject.SetActive (false);
	}

	// on trigger with collier change the camera
	void OnTriggerEnter (Collider other){
		if (other.tag == "Player") {
			playercam.gameObject.SetActive (false);
			toswitchto.gameObject.SetActive (true);
		}
	}

	// on exit on collider change it back to the main camera
	void OnTriggerExit(Collider other)
	{
		playercam.gameObject.SetActive (true);
		toswitchto.gameObject.SetActive (false);
	}
}
