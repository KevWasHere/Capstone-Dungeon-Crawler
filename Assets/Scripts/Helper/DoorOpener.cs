/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hagen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # DoorOpener.cs
*-----------------------------------------------------------------------*/

using System.Collections;
using UnityEngine;

/*
Class that moves a gameobject between two positions as a constant speed
Creator: Myles Hagen
*/

/*
 * open: bool that is true when object is at point 1 and false when at point 0
 * pts: control points that the object will move between
 * speed: object speed
 *  
 */
public class DoorOpener : MonoBehaviour {

    bool open = false;
    public GameObject[] pts;
    public float speed = 2f;
    

    void Update () {

        //Debug.Log(transform.position.y + " " + pts[0].transform.position.y);
        float step = speed * Time.deltaTime;
        if (transform.position.y >= pts[1].transform.position.y)
        {
            open = true;
        }

        if (transform.position.y <= pts[0].transform.position.y)
        {
            open = false;           
        }



        if (!open)
        {
            transform.position = Vector3.MoveTowards(transform.position, pts[1].transform.position, step);
        }

        if (open)
        {
            transform.position = Vector3.MoveTowards(transform.position, pts[0].transform.position, step);
        }

    }


}
