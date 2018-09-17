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
 * Level3MovingSpike is actually pattern3 of moving spike game.
 * It splits all spikes in the area to 2 groups, odd_spike and even_spike 
 * Those 2 groups of spike move up and down at reverse time.
 * This class also control the green and red color of the area. so red means spike coming up, green means safe to move on.
 */
public class Level3MovingSpike : MonoBehaviour {

    public GameObject[] odd_spike, even_spike, odd_floor, even_floor;               // 2 groups of spikes and 2 groups of floor to represent color
    //public GameObject[] hitbox1, pattern1spike, pattern2spike, pattern3spike;
    public bool done = false, turn = false;                                         // boolean value done to represent its time to change color. turn is for checking interact function.
    int check = 1;                                                                  // checking number for while loop
    int count = 0;                                                                  // count on how many times it would take
    public int pattern;                                                             // pattern value to determine pattern
    public Material red, green;                                                     // material red and green to set color of floor
    public Material defaultmaterialodd, defaultmaterialeven;                        // default material that floor has

    #region Singleton

    public static Level3MovingSpike instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    // set default material of floors and call run
    public void Start()
    {
        defaultmaterialodd = red;
        defaultmaterialeven = green;
        run();
    }

    // used to have a interact function to call run.
    // but instead, now it will run from beginning. and call startcoroutine
    public void run()
    {
        if (turn) return;
        turn = true;
        StartCoroutine("Moving");
    }

    // The coroutine moving change the color of floors every time count up to 25
    // and move even floor up and move odd floor down or in the opposite direction.
    IEnumerator Moving()
    {

        while(!done)
        {
            if (count == 25)
            {
                check *= (-1);
                count = 0;
                yield return new WaitForSeconds(1.7f);
                if (check > 0)
                {
                    defaultmaterialodd = red;
                    defaultmaterialeven = green;
                }
                else
                {
                    defaultmaterialeven = red;
                    defaultmaterialodd = green;
                }
                
            }
            yield return new WaitForSeconds(0.05f);
            for (int i = 0; i < 14; i++)
            {
                even_floor[i].GetComponent<MeshRenderer>().material = defaultmaterialeven;
                if (i < 11)
                    odd_floor[i].GetComponent<MeshRenderer>().material = defaultmaterialodd;
            }
            for (int i = 0; i < 14; i++)
            {   
                if (i < 11)
                    odd_spike[i].transform.position = new Vector3(odd_spike[i].transform.position.x, odd_spike[i].transform.position.y + 0.1f * check, odd_spike[i].transform.position.z);
                even_spike[i].transform.position = new Vector3(even_spike[i].transform.position.x, even_spike[i].transform.position.y + 0.1f * check * (-1), even_spike[i].transform.position.z);
            }
            count++;
        }
    }
}
