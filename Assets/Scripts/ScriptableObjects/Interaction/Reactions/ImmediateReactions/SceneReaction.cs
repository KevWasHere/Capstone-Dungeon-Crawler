using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone
  # CharMenuControl.cs
  # Dialogue
*-----------------------------------------------------------------------*/
/*
chaneg scene reaction  (unused right now)
*/
public class SceneReaction : Reaction
{
    public string sceneName;
    public string startingPointInLoadedScene;
    public SaveData playerSaveData;


    private SceneController sceneController;


    protected override void SpecificInit()
    {
        sceneController = FindObjectOfType<SceneController> ();
    }


    protected override void ImmediateReaction()
    {
       // playerSaveData.Save (PlayerMovement.startingPositionKey, startingPointInLoadedScene);
       sceneController.FadeAndLoadScene (this);

    }
}