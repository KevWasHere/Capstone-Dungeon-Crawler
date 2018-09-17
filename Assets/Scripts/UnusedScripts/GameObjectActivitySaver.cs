/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hagen, Shane Weerasuriya, 
  #                 Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # 
*-----------------------------------------------------------------------*/

// Creator : Shane , Myles 
// This script enables saving the activity states
using UnityEngine;

public class GameObjectActivitySaver : Saver
{
    public GameObject gameObjectToSave;


    protected override string SetKey()
    {
        return gameObjectToSave.name + gameObjectToSave.GetType().FullName + uniqueIdentifier;
    }


    protected override void Save()
    {
        saveData.Save(key, gameObjectToSave.activeSelf);
    }


    protected override void Load()
    {
        bool activeState = false;

        if (saveData.Load(key, ref activeState))
            gameObjectToSave.SetActive (activeState);
    }
}
