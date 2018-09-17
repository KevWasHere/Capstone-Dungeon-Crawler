using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone
  # CameraControllerUI.cs
*-----------------------------------------------------------------------*/

/*
 * Creator: Yan Zhang, Tianqi Xiao, Myles Hagen
 * Override the die function in the parent enemy script
*/
public class UncleEnemy : Enemy {

	//After the basic die function, oull up a credit screen
	public override void Die ()
	{
		base.Die ();
		SceneManager.LoadScene (11);
	}
}
