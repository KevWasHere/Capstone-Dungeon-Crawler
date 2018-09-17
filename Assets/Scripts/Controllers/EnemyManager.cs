using UnityEngine;
using System.Collections;

/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone
  # EnemyManager.cs
  # Manage enemies spawn wave
*-----------------------------------------------------------------------*/

/*
 * Creator: Tianqi Xiao, Yan Zhang, Yunzheng Zhou
 * 
 * enemy: The enemy prefab to be spawned.
 * spawPoints:An array of the spawn points this enemy can spawn from.
 * enmyCount: the number of enemy need to spaw
 * spawwait: the seconds to wait before spaw next enemy
 */
public class EnemyManager : MonoBehaviour
{
	public GameObject enemy;
	public Transform[] spawnPoints;
	public int enemyCount;
	public float spawwait;


	void Start ()
	{
		// Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
		StartCoroutine(Spawn());
	}

	/* 
	 * Author: Tianqi Xiao, Yan Zhang, Yunzheng Zhou
	 * Spawn enemies from spawnPoint
	 */
	IEnumerator Spawn ()
	{
	    for (int j = 0; j < enemyCount; j++) {

	    // Find a random index between zero and one less than the number of spawn points.
		int spawnPointIndex = Random.Range (0, spawnPoints.Length);

	    // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
		Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
		yield return new WaitForSeconds (spawwait);
	    }

	}
}
