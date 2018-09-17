using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hagen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # aiGroundSwarm.cs
*-----------------------------------------------------------------------*/

/*
 class that controls an object through its navmesh to follow the player around
 and constantly spawn enemies, always maintaining a specified number of enemies.
 the enemies also use navmesh to chase player.
 Creator: Myles Hagen
*/
/*
 * prefab - object to be instantiated
 * numObject - number of objects to be spawned at a time
 * allObjects - array of instantiated objects
 * agent - reference to navmeshagent
 * delay - time between spawning new objects
 * deathCounter - keep track of number of death to maintain numObjects
 * instance - reference to instance of aiGroundSwarm
 * 
 */

public class aiGroundSwarm : MonoBehaviour {

	public GameObject prefab;
    static int numObjects = 10;
    public static GameObject[] allObjects = new GameObject[numObjects];
    NavMeshAgent agent;
    public float delay = 2f;
    [HideInInspector]
    public int deathCounter = 0;

    public static aiGroundSwarm instance;

	// initialize instance and get refrence to navmesh
    private void Awake()
    {
        instance = this;
        agent = GetComponent<NavMeshAgent>();
    }

	// instantiate all prefabs
    void Start()
    {
        for (int i = 0; i < numObjects; i++)
        {
            allObjects[i] = Instantiate(prefab, transform.position, Quaternion.identity);
            //Debug.Log("num objects " + i);
        }
    }

	// spawn new enemies when deathcounter is greater than 0
    private void Update()
    {
        Debug.Log("death counter: " + deathCounter);
        
       
       while (deathCounter > 0)
        {
            deathCounter--;
            StartCoroutine(DelaySpawn(delay));
            
        }
			
        inRange();
    }

	// keep moving towards player until reaching stopping distance
    public void inRange()
    {
        float dist = Vector3.Distance(Player.instance.transform.position, transform.position);
        if (dist > agent.stoppingDistance)
            agent.SetDestination(Player.instance.transform.position);
    }
		
	// coroutine to delay the spawning of enemies
    IEnumerator DelaySpawn(float delay)
    {
        yield return new WaitForSeconds(delay);
        Instantiate(prefab, transform.position, Quaternion.identity);
        
    }






}
