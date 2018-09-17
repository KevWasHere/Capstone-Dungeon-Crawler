using UnityEngine;

/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hagen, Shane Weerasuriya, 
  #                 Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # CameraController.cs
  # The controller for the camera to follow the player
*-----------------------------------------------------------------------*/

/*
* Creator: Tianqi Xiao, Shane Weerasuriya
* 
* target: reference to the target Transform
* offset: the smooth follow speed
* currentZoom: the current zoom rate
* maxZoom: the max zoom rate
* minZoom: the min zoom rate
* yawSpeed: the yaw rotation rate
* zoomSensitivity: the sensitivity of zoom rate changing
* dst: the distance between camera and player
* zoomSmoothV: reference value for the smooth follow speed
* targetZoom: the target zoom rate
*/

public class CameraController : MonoBehaviour {

	public Transform target;

	public Vector3 offset;
	public float smoothSpeed = 2f;

	public float currentZoom = 1f;
	public float maxZoom = 3f;
	public float minZoom = .3f;
	public float yawSpeed = 70;
	public float zoomSensitivity = .7f;
	float dst;

	float zoomSmoothV;
	float targetZoom;

	//Initialization
	void Start() {
		dst = offset.magnitude;
		transform.LookAt (target);
		targetZoom = currentZoom;
	}

	/**
	 * Creator: Tianqi Xiao, Shane Weerasuriya
	 * Receive input from scroll and change the zoom rate accordingly.
	 */
	void Update ()
	{
		float scroll = Input.GetAxisRaw("Mouse ScrollWheel") * zoomSensitivity;

		if (scroll != 0f)
		{
			targetZoom = Mathf.Clamp(targetZoom - scroll, minZoom, maxZoom);
		}
		currentZoom = Mathf.SmoothDamp (currentZoom, targetZoom, ref zoomSmoothV, .15f);
	}

	/**
	 * Creator: Tianqi Xiao, Shane Weerasuriya
	 * Update the yaw input and rotate the camera.
	 */
	void LateUpdate () {
		transform.position = target.position - transform.forward * dst * currentZoom;
		transform.LookAt(target.position);

		float yawInput = Input.GetAxisRaw ("Horizontal");
		transform.RotateAround (target.position, Vector3.up, yawInput * yawSpeed * Time.deltaTime);
	}

}
