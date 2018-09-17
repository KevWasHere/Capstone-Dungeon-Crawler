using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone
  # switch camera
*-----------------------------------------------------------------------*/
/*
Creator: Yan Zhang, Kevin Ho, Shane Weerasuriya
Switch two camera
main: the main camera
puzzle: puzzle camera
*/
public class SwitchCamera : MonoBehaviour {
	public Camera main;
	public Camera puzzle;

	//switch camer on trigger enter
	void OnTriggerEnter (Collider other){
		
		main.enabled = false;
		puzzle.gameObject.SetActive (true);
	}
}
