/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # StartingPosition.cs
*-----------------------------------------------------------------------*/
using System.Collections.Generic;
using UnityEngine;

/* Author: Myles Hagen, Yunzheng Zhou
 * 
 * StartingPosition set the starting position
 * of player and load the data from local file
 * 
 */
public class StartingPosition : MonoBehaviour
{
    public string startingPointName;                    // string of starting point name


    private static List<StartingPosition> allStartingPositions =  new List<StartingPosition> ();        // List of point name that contains all the position

    // add the start position
    private void OnEnable ()
    {
        allStartingPositions.Add (this);
    }

    // delete the start position from array
    private void OnDisable ()
    {
        allStartingPositions.Remove (this);
    }

    // find the starting position through the array
    public static Transform FindStartingPosition (string pointName)
    {
        for (int i = 0; i < allStartingPositions.Count; i++)
        {
            if (allStartingPositions[i].startingPointName == pointName)
                return allStartingPositions[i].transform;
        }

        return null;
    }
}
