/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hagen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # EnemyRegistry.cs
*-----------------------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * E - dictionary of enemies with key: name and value: GameObject
 * respawnTimer - float representing the time before a dead enemy is respawned
 * setEnemies - 
 * enemyToRemove - stack representing the enemies to be removed from the enemy dictionary
 * respawns - array of gameobjects with tag "Enemy"
 * repawns2 - array of gameobjects wieth tag "EnemyArcher"
 * sceneID - index of scene
 * 
 * Creator: Myles HagenYan Zhang, SHane Weerasuriya
 */

public class EnemyRegistry : MonoBehaviour {

    public Dictionary<string, GameObject> E = new Dictionary<string, GameObject>();
    public float respawnTimer = 30f;
    bool setEnemies = true;
    Stack<EnemyDeathTime> enemyToRemove = new Stack<EnemyDeathTime>();
    public GameObject[] respawns;
	private int sceneID;
   

	/*
	 * fill arrays with gameobjects tagged as "Enemy" and "EnemyArcher"
	 * add the enemies to the enemy dictionary. if the enemies are contained
	 * in the global scenedeathlists then they are set to be inactive.
	 */ 
    void Start () {
        E.Clear();
        var respawns = GameObject.FindGameObjectsWithTag("Enemy");
        var respawns2 = GameObject.FindGameObjectsWithTag("EnemyArcher");
		int numEnemies = respawns.Length + respawns2.Length;
		Debug.Log ("Number of enemies: " + numEnemies);
        foreach (GameObject enemy in respawns)
        {
            // Debug.Log(enemy.name);
            if (!E.ContainsKey(enemy.name))
                E.Add(enemy.name, enemy);
        }
        foreach (GameObject enemy in respawns2)
        {
			if (!E.ContainsKey(enemy.name))
            	E.Add(enemy.name, enemy);
        }


        sceneID = SceneManager.GetActiveScene().buildIndex;

		foreach (KeyValuePair<int, List<EnemyDeathTime>> deathList in GlobalControl.Instance.SceneDeathLists) {
			if (deathList.Key == sceneID && deathList.Value.Count > 0) {
				foreach (EnemyDeathTime dt in deathList.Value) {
					Debug.Log("name: " + dt.name + " time of death: " + dt.deathTime);
					if (Time.time - dt.deathTime < respawnTimer) {
						if (E.ContainsKey (dt.name)) {
							GameObject enemy = E [dt.name];
							enemy.SetActive (false);
						}
					} else {
						enemyToRemove.Push (dt);
                    
					}
				}
				while (enemyToRemove.Count > 0) {
					deathList.Value.Remove (enemyToRemove.Pop ());
				}

			}
		}

		if (AllEventList.returnStatus("villageInitial", 0)) {
            if (E.ContainsKey("Enemies"))
			    E ["Enemies"].SetActive (false);
		}

    }

}
