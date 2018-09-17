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
 * Level3RandomGenerator will generate a number from range 0 to 3
 * Because its a singleton so that all other class at same scene could call it
 * and gain a random number.
 */
public class LevelRandomGenerator : MonoBehaviour {

    #region Singleton

    public static LevelRandomGenerator instance;

    void Awake()
    {
        instance = this;
    }

    #endregion
    public int last, current;               //  last and current will be the integer value that would be used

    /*
     * generate a number from beginning
     */
    private void Start()
    {
        current = Random.Range(0, 3);
    }

    // generate a row number in range of 0 to 3
    public void RowGenerator()
    {
        last = current;
        current = Random.Range(0, 3);
    }
}
