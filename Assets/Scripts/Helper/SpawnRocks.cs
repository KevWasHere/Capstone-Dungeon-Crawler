/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hagen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # SpawnRocks.cs
*-----------------------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * hazard - object to instantiate
 * hazards - array of objects to instantiate
 * spawnPoint - spawn point of hazards
 * hazardCount - number of hazards to spawn in each wave
 * spawnWait - time between each hazard is spawned
 * startWait - time before hazards start spawning 
 * waveWait - time between each wave of hazards
 * hazardNum - used as index to hazards array
 * numWaves - number of waves to spawn
 * 
 * Creator: Myles Hagen,Tianqi Xiao, Yan Zhang, Yunzheng Zhou
 */

public class SpawnRocks : MonoBehaviour {

    public GameObject hazard;
    public GameObject[] hazards;
    public GameObject spawnPoint;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    int hazardNum;
    int numWaves = 6;

    void Start () {
        StartCoroutine(SpawnWaves());
	}

	/*
	 * Function: SpawnWaves
	 * Description: spawn a set number of waves, with a set amount of hazards per wave
	 * delay between spawns, and wave spawns.
	 * 
	 * 
	 */
    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (numWaves > 0)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                hazardNum = Random.Range(0, hazards.Length - 1);
                float xPos = spawnPoint.transform.position.x;
                Vector3 spawnPosition = new Vector3(Random.Range(xPos, xPos - 0.5f), spawnPoint.transform.position.y, spawnPoint.transform.position.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazards[hazardNum], spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
            numWaves--;
        }
    }
}
