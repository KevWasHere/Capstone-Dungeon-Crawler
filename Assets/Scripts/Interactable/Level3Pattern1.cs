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
 * Level3Pattern1 is only controlling the color of floors in both 2 pattern.
 * To fit the pattern of moving spike, the floor get material red if the spike is about
 * coming up, get green if the spike is about coming down.
 * 
 */
public class Level3Pattern1 : MonoBehaviour {


    #region Singleton

    public static Level3Pattern1 instance;

    void Awake()
    {
        instance = this;
    }

    #endregion
    public GameObject[] currentSpikesRow, nextSpikesRow;            // floors of current row and next row
    public Material red, green;                                     // mesh material red and green.
    public GameObject cube;                                         // the cube shaped collider to test ontrigger enter
    public bool lastrow, enter;                                     // enter check the ontrigger enter, lastrow if current row is lastrow
    public int pattern, lastIndex = 0, currentIndex = 0;            // check lastindex and currentindex of current row and next row

    // check if player enter the cube collider and call coroutine for becomegreenP1
    private void OnTriggerEnter(Collider other)
    {
        
        if (!enter && other.tag == "Player")
        {
            lastIndex = LevelRandomGenerator.instance.current;
            LevelRandomGenerator.instance.RowGenerator();           //  call a row generator from levelrandomgenerator instance
            currentIndex = LevelRandomGenerator.instance.current;
            StartCoroutine("BecomeGreenP1");
            enter = true;
        }
    }

    // become next row of floor to green and current row to red if the pattern is 1 
    // check multiply boolean value to determine the floor is getting green or red.
    IEnumerator BecomeGreenP1()
    {
        if (pattern == 1)
        {
            if (checkposition())
            {
                for (int i = 0; i < 4; i++)
                {
                    currentSpikesRow[i].GetComponent<MeshRenderer>().material = green;
                }
            }
        }
        else
        {
                if (lastIndex < currentIndex)
                {
                    for (int i = lastIndex; i <= currentIndex; i++)
                    {
                        currentSpikesRow[i].GetComponent<MeshRenderer>().material = green;
                    }
                }
                else
                {
                    for (int i = currentIndex; i <= lastIndex; i++)
                    {
                        currentSpikesRow[i].GetComponent<MeshRenderer>().material = green;
                    }
                }
            
        }
        yield return new WaitForSeconds(1f);
        if (!lastrow)
        {
            switch (pattern)
            {
                case 1:
                    for (int i = 0; i < 4; i++)
                    {
                        nextSpikesRow[i].GetComponent<MeshRenderer>().material = green;
                    }
                    break;
                case 2:
                    nextSpikesRow[currentIndex].GetComponent<MeshRenderer>().material = green;
                    break;
                default:
                    break;
            } 
        }
        StartCoroutine("BecomeRed");
    }

    //  if the player enter the cube collider then disable the used
    //  the cube collider
    IEnumerator BecomeRed()
    {
        yield return new WaitForSeconds(pattern);
        for (int i = 0; i < 4; i++)
        {
            currentSpikesRow[i].GetComponent<MeshRenderer>().material = red;
        }
        cube.SetActive(false);
    }

    // check the current player position if the player is on
    //  the floor that is green
    private bool checkposition()
    {
        bool greenbool = false;
        for (int i = 0; i < 4; i++)
        {
            if (currentSpikesRow[i].GetComponent<MeshRenderer>().material == green)
                greenbool = true;
        }
        return greenbool;
    }

}
