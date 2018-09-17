/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone
  # CameraControllerUI.cs
  # UI Camera
*-----------------------------------------------------------------------*/

/*
 Creator: Yan Zhang, Myles Hagen
 This script control the main camera in the game, ratation, zoom
target: Target to follow (player)
offset: Offset from the player
zoomSpeed : How quickly we zoom
minZoom: Min zoom amount
maxZoom: Max zoom amount
pitch: Pitch up the camera to look at head
yawSpeed: How quickly we rotate
currentZoom : initial zooming when game start
currentYaw: initial yaw angle when game start
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerUI : MonoBehaviour {

	public Transform target;

	public Vector3 offset;		
	public float zoomSpeed = 4f;	
	public float minZoom = 5f;	
	public float maxZoom = 15f;	

	public float pitch = 2f;	

	public float yawSpeed = 100f;	

	private float currentZoom = 10f;
	private float currentYaw = 0f;
	//Creator: Yan Zhang, Myles Hagen
	void Update ()
	{
		// Adjust our zoom based on the scrollwheel
		currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
		currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

		// Adjust our camera's rotation around the player
		currentYaw -= -(Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime);
	}
	//Creator: Yan Zhang, Myles Hagen
	void LateUpdate ()
	{
		// Set our cameras position based on offset and zoom
		transform.position = target.position - offset * currentZoom;
		// Look at the player's head
		transform.LookAt(target.position + Vector3.up * pitch);

		// Rotate around the player
		transform.RotateAround(target.position, Vector3.up, currentYaw);
	}

}
