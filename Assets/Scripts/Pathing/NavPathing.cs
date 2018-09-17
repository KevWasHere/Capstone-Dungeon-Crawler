/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hagen, Shane Weerasuriya, 
  #                 Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # 
*-----------------------------------------------------------------------*/


// This script is used to calculate the path every frame depending on an array of way points
// Creator : Shane Weerasuriya

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavPathing : MonoBehaviour {
    // array of way points 
    public Transform[] wayPt; //control points
    NavMeshAgent nav; // nav mesh
    float dist; // distance
    int i = 0; // random variable

    // initialize getting the nav mesh
    void Start () {
        nav = GetComponent<NavMeshAgent>();

	}
	
	// this will update every frame 
	void Update () {

        dist = Vector3.Distance(wayPt[i].position, transform.position);
        if (dist < nav.stoppingDistance)
        {
            nav.SetDestination(wayPt[i].position);
        }
        else if (i == 0)
            i = 1;
        else  
            i = 0;
    }
}
