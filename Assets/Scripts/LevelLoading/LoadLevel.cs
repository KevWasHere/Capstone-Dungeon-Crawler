/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hagen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # LoadLevel.cs
*-----------------------------------------------------------------------*/
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

/*
 * Creator: Myles Hagen
 * 
 * LevelName - name of level
 * check - bool to stop Interact from triggering more than once
 * sceneIndex - index of scene
 * newTrans - bool determinging if a new transform is required for player in new level
 * posX, posY, posZ - floats representing the new position of the player
 */

public class LoadLevel : Interactable {

    public string LevelName;
	public int sceneIndex;
    private bool check = false;
    public bool newTrans = false;
    public float posX, posY, posZ;


	/*
	 * Function: Interact (overriden from interactable)
	 * Description: function used to load a new level, first the index of the
	 * new scene is passed to globalcontrol then the player data is saved and
	 * finally the loading screen is loaded.
	 * 
	 * Creator: Myles Hagen
	 */
    public override void Interact()
    {
        if (check)
            return;
        base.Interact();
        check = true;
        if (newTrans)
        {
            GlobalControl.Instance.newTransform = true;
            GlobalControl.Instance.newPosX = posX;
            GlobalControl.Instance.newPosY = posY;
            GlobalControl.Instance.newPosZ = posZ;
        }
		GlobalControl.Instance.newSceneID = sceneIndex;
        GlobalControl.Instance.SaveBetweenScenes();
        GlobalControl.Instance.IsSceneBeingLoaded = false;
        SceneManager.LoadScene(LevelName, LoadSceneMode.Single);
    }
}
