/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # CatmullRom.cs
  # CatmullRom pathing for NPCs
*-----------------------------------------------------------------------*/

// This script evaluates a list of control points for the catmul rom algorithm

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ctlPoint - list of control points
 * MINUS_ONE - length of control points minus 1
 */
 
 /*
 Creator: Kevin Ho
 */

public class CatmullRom : MonoBehaviour {
	private Transform[] ctlPoint;
	private int MINUS_ONE;
	
	public CatmullRom(Transform[] pt){
		ctlPoint = pt;
		MINUS_ONE = (ctlPoint.Length - 1);
	}
	
	//Evaluate target location of the control points
	public Vector3 Evaluate (float u) {
		int p1 = (int)(u);
		int p0 = (p1 + MINUS_ONE) % ctlPoint.Length;
		int p2 = (p1 + 1) % ctlPoint.Length;
		int p3 = (p1 + 2) % ctlPoint.Length;
	
		float t = u - p1;
		float t2 = t * t;
		float t3 = t2 * t;

		float b0 = 0.5f * (-t3 + 2f*t2 - t);
		float b1 = 0.5f * (3f*t3 - 5f*t2 + 2f);
		float b2 = 0.5f * (-3f*t3 + 4f*t2 + t);
		float b3 = 0.5f * (t3 - t2);

		return ctlPoint[p0].position * b0 + ctlPoint[p1].position * b1 + 
				ctlPoint[p2].position * b2 + ctlPoint[p3].position * b3;
	}
}
