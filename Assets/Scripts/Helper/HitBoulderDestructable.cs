/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hagen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # HitBoulderDestructable.cs
  # Hit detection for boulder objects
*-----------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * de - Reference to destructable script
 * distanceFromGround - Distance between boulder and ground for contact
 */
 
 /*
 Creator: Myles Hagen, Kevin Ho, Tianqi Xiao
 */

 [RequireComponent(typeof(Destructable))]
public class HitBoulderDestructable : MonoBehaviour {
    private Destructable de;
	private float distanceFromGround = 2.8f;

    void Start() {
        de = GetComponent<Destructable>();
    }
	
	void Update() {
		GroundCheck();
	}
	
	/*
	Check if boulder has hit the ground or not. If has, destory
	Creator: Kevin Ho, Tianqi Xiao
	*/
	private void GroundCheck() {
		RaycastHit hit;
		if (Physics.Raycast (transform.position, -Vector3.up, out hit)) {
			float distanceToGround = hit.distance;
			string ground = LayerMask.LayerToName(hit.collider.gameObject.layer);
			//Debug.Log(ground + ": " + distanceToGround);
			if ((ground == "Terrain") && (distanceToGround < distanceFromGround)) {
				de.DestroyObject();	
			}
		}
	}

    void OnTriggerEnter(Collider other) { 
		//If hits player, do damage calculation and destroy, else just destory
        if (other.tag == "Player") {
			//Do damage to player here
			//Debug.Log("Hit player! Do damage");
			PlayerStats playerStats = Player.instance.playerStats;
            playerStats.TakeDamage(5);
            de.DestroyObject();			
        }
    }
}
