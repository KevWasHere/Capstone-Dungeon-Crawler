/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # Mover.cs
  # Moves an object in a straight line
*-----------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Mover. Script for moving projectle attack and other moving objects
Taken from spaceshooter tutorial
* rb - referenc to rigid body
* speed - speed of the object to move
*/
//Creator: Kevin Ho, Shane Weerasuriya

public class Mover : MonoBehaviour {
	private Rigidbody rb;
	public float speed;
	
	void Start(){
		rb = GetComponent<Rigidbody>();
		//Shots constintly move forward at a set speed
		rb.velocity = (transform.forward * speed);
	}	
}
