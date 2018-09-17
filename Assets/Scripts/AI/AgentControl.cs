using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hagen, Shane Weerasuriya, 
  #                 Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # AgentControl.cs
  # The controller for AI agent
*-----------------------------------------------------------------------*/

/*
* Creator: Myles Hagen, Yan Zang
 * 
 * home: reference to home Transform
 * agent: reference to NavMeshAgent
 */

public class AgentControl : MonoBehaviour {

    public Transform home;
    NavMeshAgent agent;

	void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(home.position);
	}
	
}
