/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone

*-----------------------------------------------------------------------*/

/*
 * Creator: Yunzheng Zhou, Tianqi Xiao, Yan Zhang
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Level3FireBallMoving let a gameobject fireball move forward in a update function.
 * Every few seconds as fireball hit the expected position, change the rotation of fireball
 * 
 * The expected position is from Catmun rom in level3fireballpath class
 */
public class level3FireBallMoving : MonoBehaviour {

    public level3FireBallPath path;             // Catmun rom path to decide where should fireball go
    public GameObject fireball;                 // The gameobject fireball
    public int index = 0;                       // initial index to determine rotation

    // get component of level3fireballpath from gameobject itself
    private void Start()
    {
        path = GetComponent<level3FireBallPath>();
    }

    // update position of fireball and change rotation of fire if it hit the expected position.
    private void Update()
    {
        if (index > 3)
        {
            index = 0;
        }
        float step = 20 * Time.deltaTime;
        fireball.transform.position = Vector3.MoveTowards(fireball.transform.position, path.Evaluate(index), step);
        if (fireball.transform.position == path.Evaluate(index))
        {
            fireball.transform.Rotate(new Vector3(0f, -90f, 0f));
            index++;
        }

    }
}
