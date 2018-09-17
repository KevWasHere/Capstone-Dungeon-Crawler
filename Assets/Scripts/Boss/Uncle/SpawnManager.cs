using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hagen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # SpawnManager.cs
  # Manage the spawnpoint for the final boss
*-----------------------------------------------------------------------*/

/*
*  Creator: Tianqi Xiao,Yan Zhang,Kevin Ho
*
* enemy: the enemy prefab to be spawned
* spawnTime: gap between each spawn
* spawnPoints: an array of the spawn points
* nav: reference to enemy's NavMeshAgent
*/
public class SpawnManager : MonoBehaviour {

	public GameObject enemy;
	public float spawnTime = 3f;
	public Transform[] spawnPoints;
	NavMeshAgent nav;

	/** 
	 * Creator: Tianqi Xiao,Yan Zhang, Kevin Ho
	 * Initialization. Call the Spawn function after a delay of the spawnTime and then continue 
	 * to call after the same amount of time.
	 */
	void Start ()
	{
		// Repeatly call Spawn() function with delay between spawns
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
		nav = enemy.GetComponent<NavMeshAgent>();
	}

	/** 
	 * Creator: Tianqi Xiao,Yan Zhang, Kevin Ho
	 * If the payer is alive, instantiate enemy from spawn point. 
	 */
	void Spawn ()
	{
		CharacterStats playerStats = Player.instance.playerStats;
		// If the player has no health left, do nothing
		if(playerStats.currentHealth <= 0f) {
			return;
		}
		int spawnPointIndex = Random.Range (0, spawnPoints.Length);

		//enable and disable NavMeshAgent on enemy for better interpretation
		nav.enabled = false;
		GameObject g = Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
		g.GetComponent<NavMeshAgent>().enabled = true;
	}
}
