using UnityEngine;
using System;
using UnityEngine.SceneManagement;

/*-------------------------------------------------------------------------*
 # INTR Group 2
 # Student's Name: Kevin Ho, Myles Hagen, Shane Weerasuriya,
 #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
 # CMPT 498 Final Project
 # ButtonFunctions.cs
 # Add button to functions
 *-----------------------------------------------------------------------*/

/*
 * Creator: Shane Weerasuriya, Myles Hagen
 * LevelName: get the namespace for different level
 */

public class ButtonFunctions : MonoBehaviour {


	/*
	 * Creator: Shane Weerasuriya, Myles Hangen
	 * Initiate a new game and load the first level
	 */
    public void StartNewGame()
    {
		AllConditions.Instance.Reset ();
		AllEventList.changeStatus("villageInitial", 0, false);
		GlobalControl.Instance.newSceneID = 0;
		SceneManager.LoadSceneAsync(9, LoadSceneMode.Single);
    }

	/*
	 * Creator: Shane Weerasuriya, Myles Hangen
	 * Load level data by calling global control.
	 */
    public void LoadData()
    {
		Debug.Log("Loading Data...");
		GlobalControl.Instance.LoadData();
		GlobalControl.Instance.IsSceneBeingLoaded = true;

		int whichScene = GlobalControl.Instance.LocalCopyOfData.SceneID;
		GlobalControl.Instance.newSceneID = whichScene;
		SceneManager.LoadSceneAsync(9, LoadSceneMode.Single);
		/*if (Player.instance.deathpanel.activeSelf)
			Player.instance.deathpanel.SetActive(false);*/
    }
}
