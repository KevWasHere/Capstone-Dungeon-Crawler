/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hagen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # PortalInteractable.cs
  # Portal interaction for scene switching
*-----------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

/*
* portalInterface - reference to portal interaction game object
*/

/*
Creators:  Myles Hagen, Yan Zhang
*/

public class PortalInteractable : Interactable {

	public GameObject portalInterface;
    
   
    public override void Interact()
	{
		
		base.Interact();
		if (portalInterface!= null) {
			portalInterface.SetActive (true);
		}
		//int num = SceneManager.GetActiveScene().buildIndex;
		//ManageScene.instance.SetUpPortal (num);
		//Debug.Log ("Choose a level");
		

	}

	void OnTriggerExit(Collider other) {

		if (portalInterface != null && other.tag == "Player")
        {
            portalInterface.SetActive (false);
        }
    }
}
