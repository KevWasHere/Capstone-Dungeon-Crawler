/*-------------------------------------------------------------------------*
 # INTR Group 2
 # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya,
 #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
 # CMPT 498 Final Project
 # crator : Shane Weerasuriya
 *-----------------------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class scene_change : MonoBehaviour {

	void OnTriggerEnter (Collider other)
 {    
     
     
		SceneManager.LoadScene("dungen", LoadSceneMode.Single); 
     
 }
}
