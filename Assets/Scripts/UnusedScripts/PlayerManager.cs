using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hagen, Shane Weerasuriya,
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # PlayerManager.cs
  # This script manages the player, it is a single instance of a object
  # so other objects can reference this.
*-----------------------------------------------------------------------*/

/*
 * instance - Instance of object
 * player - Reference of object
*/

/*
 Creator : Myles Hagen, Tianqi Xiao
 */

public class PlayerManager : MonoBehaviour {

	#region Singleton

	public static PlayerManager instance;
	void Awake ()
	{
		instance = this;
	}
	#endregion

	public GameObject player;

	/*
	Kill and reload scene on player death
	Creator: Myles Hangen, Tianqi Xiao
	*/
	public void KillPlayer ()
	{
		Debug.Log ("Kill Player");
		PlayerManager.instance.player.GetComponent<PlayerStats>().enabled = false;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);	
		//SceneManager.LoadScene("CharacterMenuTestScene", LoadSceneMode.Single);

	}
}
