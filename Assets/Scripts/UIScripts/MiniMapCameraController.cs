/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone
  # CharMenuControl.cs
  # MiniMapCameraController
*-----------------------------------------------------------------------*/

/*
Creator: Yan Zhang, Kevin Ho
Control the minimap Camera
player: the player object
offset: the offset between player and camera
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCameraController : MonoBehaviour {

	public GameObject player;

	private Vector3 offset;

	/*set up the position at beginning
	 * creator Yan
	 */
	void Start ()
	{
		//make minimap camera follow player at begining
		transform.position = new Vector3 (player.transform.position.x, player.transform.position.y + 50f, player.transform.position.z);
		offset = transform.position - player.transform.position;
	}

	/*update camera's position
	 * creator: yan
	 */
	void LateUpdate ()
	{
		transform.position = player.transform.position + offset;
	}
}
