/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hagen, Shane Weerasuriya, 
  #                 Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # Level3FlyingSpike.cs
  # This script controls the flying spike in level 3
*-----------------------------------------------------------------------*/

/*
 * Creator: Yunzheng Zhou
 */
/*
 * spikes: an array of spikes game objects
 * done: an bool variable 
 * count: the count of loop set
 * index: the current index
 * turn: the number of turns
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3FlyingSpike : MonoBehaviour {

    public GameObject[] spikes;
    public bool done = true;
    int count = 0, index = 1, turn = 0;

	// Use this for initialization
	void Start () {
        Run();
	}

    /*
     * Creator: Yunzheng Zhou
     * Use StartCoroutine to call FlyingSpike() and shoot spikes
     */
    public void Run()
    {
        StartCoroutine("FlyingSpike");
    }

	/*
	 * Creator: Yunzheng Zhou
	 * Shoot spikes in cycle and add gap between waves
	 */
    IEnumerator FlyingSpike()
    {
        while (done)
        {
            if (count > 40)
            {
                count = 0;
                index *= -1;
                for (int i = 0; i < 4; i++)
                {
                    spikes[i].transform.Rotate(new Vector3(-180, 90*index, 7));
                }
            }
            yield return new WaitForSeconds(0.03f);
            for (int i = 0; i < 4; i++)
            {
                spikes[i].transform.position += new Vector3(0.3f * index, 0, 0);
                
            }
            count++;
        }
    }
}
