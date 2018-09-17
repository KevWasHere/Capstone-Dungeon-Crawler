/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hagen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # HitDestructible.cs
  # Hit detection for destructable objects
*-----------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * de - Reference to destructable script
 */
/*
 * Creator: Myles Hagen, Kevin Ho
 */
[RequireComponent(typeof(Destructable))]
public class HitDestructible : MonoBehaviour {
    Destructable de;

    void Start() {
        de = GetComponent<Destructable>();
    }

	// destroy object when it collides with player
    void OnTriggerEnter(Collider other) {     
        if (other.tag == "PlayerAttack") {
            de.DestroyObject();
            
        }
    }
}
