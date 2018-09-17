/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone

*-----------------------------------------------------------------------*/

/*
 * Author: Yunzheng Zhou
 * Date: 2018-04-19
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * level3hitrow set 2 rows of spikes move upward or downward by coroutine if player 
 * hit the collider. there are 2 patterns that player need to figure out.
 * Once a group of spikes move upward, the area of the spike will be red so that player could easily 
 * see its dangerous. otherwise the area will be green.
 */
public class level3HitRow : MonoBehaviour {

    public GameObject[] currentrow, nextrow;                        // two rows of spikies in arrays
    public bool firstrow = false, lastrow = false, enter = false;   // boolean value to check if current row is first row or last row. 
    public int pattern, currentIndex, nextIndex;                    // patten number refer to different of 2 patterns. current Index represent the index of current spike and next index represent next chosen spike in pattern 2
    public bool green;                                              // check if the current area is green

    // if player trigger the colider then check pattern and start coroutine
    private void OnTriggerEnter(Collider other)
    {
        if (!enter && other.tag == "Player")
        {
            if (pattern == 1)
                StartCoroutine("MovedownP1");
            else
                StartCoroutine("MovedownP2");
            enter = true;
        }
    }

    // coroutine for pattern 1 just simply move up the current row of spikes and move downward of next row of spikes.
    IEnumerator MovedownP1()
    {
        yield return new WaitForSeconds(1f);
        for (int k = 0; k < 100; k++)
        {
            yield return new WaitForSeconds(0.03f);
            for (int i = 0; i < currentrow.Length; i++)
            {
                currentrow[i].transform.position = new Vector3(currentrow[i].transform.position.x, currentrow[i].transform.position.y + 0.2f, currentrow[i].transform.position.z);
                if (!lastrow)
                    nextrow[i].transform.position = new Vector3(nextrow[i].transform.position.x, nextrow[i].transform.position.y + 0.2f * (-1), nextrow[i].transform.position.z);
            }
        }
    }

    // corotine 2 for pattern 2 will only move up the current selected spike at current row and 
    // make next chosen spike in next row to be green and move it downward
    IEnumerator MovedownP2()
    {
        int j = LevelRandomGenerator.instance.current, m = LevelRandomGenerator.instance.last;
        yield return new WaitForSeconds(1f);
        for (int k = 0; k < 40; k++)
        {
            green = true;
            yield return new WaitForSeconds(0.03f);
            if (firstrow)
            {
                for (int i = 0; i < currentrow.Length; i++)
                {
                    currentrow[i].transform.position = new Vector3(currentrow[i].transform.position.x, currentrow[i].transform.position.y + 0.2f, currentrow[i].transform.position.z);
                    if (!lastrow)
                    {
                        nextrow[j].transform.position = new Vector3(nextrow[i].transform.position.x, nextrow[i].transform.position.y + 0.2f * (-1), nextrow[i].transform.position.z);
                        green = false;
                    }
                }
            }
            else
            {
                if (j > m)
                {
                    for (int i = m; i <= j; i++)
                    {
                        currentrow[i].transform.position = new Vector3(currentrow[i].transform.position.x, currentrow[i].transform.position.y + 0.2f * (-1), currentrow[i].transform.position.z);
                    }
                }
                else
                {
                    for (int i = j; i <= m; i++)
                    {
                        currentrow[i].transform.position = new Vector3(currentrow[i].transform.position.x, currentrow[i].transform.position.y + 0.2f * (-1), currentrow[i].transform.position.z);
                    }
                }
                if (!lastrow)
                    nextrow[j].transform.position = new Vector3(nextrow[j].transform.position.x, nextrow[j].transform.position.y + 0.2f * (-1), nextrow[j].transform.position.z);
                green = false;
            }
        }
        
        for (int k = 0; k < 40; k++)
        {
            yield return new WaitForSeconds(0.02f);
            if (j > m)
            {
                for (int i = m; i <= j; i++)
                {
                    currentrow[i].transform.position = new Vector3(currentrow[i].transform.position.x, currentrow[i].transform.position.y + 0.2f * (1), currentrow[i].transform.position.z);
                }
            }
            else
            {
                for (int i = j; i <= m; i++)
                {
                    currentrow[i].transform.position = new Vector3(currentrow[i].transform.position.x, currentrow[i].transform.position.y + 0.2f * (1), currentrow[i].transform.position.z);
                }
            }
            
        }
    }
}
