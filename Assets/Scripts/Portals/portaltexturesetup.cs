/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hagen, Shane Weerasuriya, 
  #                 Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # 
*-----------------------------------------------------------------------*/

/*-------------------------------------------------------------------------*
# this script sets the texture of the portal, it is live rendering so that 
# the player can see a live view of the other side through the portal
#
# Creator : shane weerasuriya
*-----------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portaltexturesetup : MonoBehaviour {

	public Camera camb;
	public Material cammatb;


	void Start () {
		
		if (camb.targetTexture != null)
		{
			camb.targetTexture.Release();
		}
		camb.targetTexture = new RenderTexture(Screen.width, Screen.height, 40);
		cammatb.mainTexture = camb.targetTexture;
	} 
}
