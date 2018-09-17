
/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # EnemyManagerController.cs
*-----------------------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * enemyManager: Reference to trigger if enemy enter the collider and set it active
 */
 
 /*
 Creator: Yan Zhang, Yunzheng Zhou
 */
public class EnemyMnagerController : MonoBehaviour {

	public GameObject enemyManager;
	// Use this for initialization
	
	/*
	 * Function: OnTriggerEnter
	 * Description: trigger the enemy manager when the collider is entered
	*/
	void OnTriggerEnter(Collider other){
		enemyManager.SetActive (true);

	}
}
