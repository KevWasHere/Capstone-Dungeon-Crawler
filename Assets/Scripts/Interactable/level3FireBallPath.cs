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
 * Just simply setup a simple AI moving from p1 to p2 to p3 to p4
 * this CatmuRoomSpline is handle the postion of 4 sphere object
 * 
 */
public class level3FireBallPath : MonoBehaviour
{

    public Transform[] ctlPoint;     //public transform array to store 4 sphere objects
    private int MINUS_ONE;           // maxium index of array

    /*
     * set ctlPoint to pt and MINUS_ONE as legnth of array -1
     * 
     */
    public level3FireBallPath(Transform[] pt)
    {
        ctlPoint = pt;
        MINUS_ONE = ctlPoint.Length - 1;
    }

    /*
     * get position of sphere object
     * 
     */
    public Vector3 Evaluate(float u)
    {
        int p1 = (int)(u);
        int p0 = (p1 + MINUS_ONE) % ctlPoint.Length;
        int p2 = (p1 + 1) % ctlPoint.Length;
        int p3 = (p1 + 2) % ctlPoint.Length;
        float t = u - p1;
        float t2 = t * t;
        float t3 = t2 * t;
        float b0 = 0.5f * (-t3 + 2f * t2 - t);
        float b1 = 0.5f * (3f * t3 - 5f * t2 + 2f);
        float b2 = 0.5f * (-3f * t3 + 4f * t2 + t);
        float b3 = 0.5f * (t3 - t2);

        return ctlPoint[p0].position * b0 + ctlPoint[p1].position * b1 + ctlPoint[p2].position * b2 + ctlPoint[p3].position * b3;
    }




}
