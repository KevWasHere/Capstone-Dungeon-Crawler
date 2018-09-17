/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hagen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # SpawnExplosionOnContact.cs
*-----------------------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * collisionPrefab - collision prefab to be instantiated on collision with player
 * 
 * Creator: Myles Hagen,Tianqi Xiao, Yan Zhang, Yunzheng Zhou
 */

public class SpawnExplosionOnContact : MonoBehaviour {

	public GameObject collisionPrefab;

	// when mage attack collides with player, instantiate the explosion effect
	void OnTriggerEnter(Collider other) {
		if ((this.tag == "EnemyMageAttack") && (other.tag == "Player")) {
			GameObject collision = Instantiate (collisionPrefab, transform.position, Quaternion.identity);
			// add damage
			PlayerStats playerStats = Player.instance.playerStats;
			playerStats.TakeDamage(20);
			Destroy (gameObject);
			Destroy (collision, 1.5f);
		}
	}
}
