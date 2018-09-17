/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hagen, Shane Weerasuriya, 
  #                 Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # 
*-----------------------------------------------------------------------*/
// This script is used to create a linear path to calculate pathing for A * algorithm
// Creator : Shane Weerasuriya

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearPathing : MonoBehaviour
{
    public Transform[] wayPt; //control points

    public float v;           //Speed of body
    public bool constantVelocity;

    private Transform current, target; //brackets
    private int endT;            //index of target
    private float t, dT; //current t(parameter)

    void Start()
    {
        endT = 1;
        target = wayPt[1];
        current = wayPt[0];

        //current.gameObject.renderer.material.color = Color.red;
        //target.gameObject.renderer.material.color = Color.green;

        transform.position = current.position;

        if (constantVelocity)
            dT = v / Vector3.Distance(current.position, target.position);
        else
            dT = v;
    }

    void Update()
    {
        t += Time.deltaTime * dT;

        if (t >= endT)
        { // Switch to next pair of points
            //current.gameObject.renderer.material.color = Color.white;

            current = target;
            endT = (int)Mathf.Floor(t) + 1;
            target = wayPt[endT % wayPt.Length];

            //current.gameObject.renderer.material.color = Color.red;
            //target.gameObject.renderer.material.color = Color.green;

            if (constantVelocity)
                dT = v / Vector3.Distance(
                    current.position, target.position);
        }

        float left = t - endT + 1;
        transform.position = Vector3.Lerp(
            current.position, target.position, left);
    }



}
