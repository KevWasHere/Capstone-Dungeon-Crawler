/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # DestoryByContact.cs
  # Destorys object on collision with another collider
*-----------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Destory by contact. Destorys game object if it hits a collider
Creator: Kevin Ho, Tianqi Xiao, Myles Hagen
*/

public class DestroyByContact : MonoBehaviour {
	void OnTriggerEnter(Collider other) {
		if ((this.tag == "PlayerAttack") && ((other.tag == "Enemy") || (other.tag == "EnemyArcher") || (other.tag == "DestructableObject"))) {
			Destroy(gameObject);
		}
		if ((this.tag == "EnemyArcherAttack") && (other.tag == "Player")) {
			Destroy(gameObject);
		}
		if ((this.tag == "EnemyMageAttack") && (other.tag == "Player")) {
			StartCoroutine(DestoryWait(1.0f));
		}
	}
	
	/*
	Coroutine to delay destory attack
	Parameters: delay - float of time delay of attack
	Creator: Kevin Ho, Tianqi Xiao, Myles Hagen
	*/
	IEnumerator DestoryWait(float delay) {
		yield return new WaitForSeconds(delay);
		Destroy(gameObject);
	}
}
