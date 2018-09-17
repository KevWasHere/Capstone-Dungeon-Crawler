using UnityEngine;
using System.Collections;
/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone
  # CameraControllerUI.cs
  # camera facing
*-----------------------------------------------------------------------*/

/*
Creator: Yan Zhang, shane weerasuriya
Keep the Object facing the camera all the time
m_Camera: the camera that need to be facing

*/
public class CameraFacingBillboard : MonoBehaviour
{
    Camera m_Camera;
	//set the camera to be the main camera
    private void Start()
    {
        m_Camera = Camera.main;
    }
	//facing the object to the camera each frame
    void Update()
	{
		//change the transform of the object to face camera
		transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward,
			m_Camera.transform.rotation * Vector3.up);
	}
}
