/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # SaveDroppableList.cs
*-----------------------------------------------------------------------*/
using System.Collections.Generic;
using System;

/*
 * class that stores a list of SaveDroppableItem for the given scene index
 * 
 * sceneID - scene index
 * SavedItems - list of SaveDroppableItem
 * 
 * Creator: Myles Hagen
 */

[Serializable]
public class SaveDroppableList {

    public int SceneID;
    public List<SaveDroppableItem> SavedItems;

    public SaveDroppableList(int newSceneID)
    {
        SceneID = newSceneID;
        SavedItems = new List<SaveDroppableItem>();
        
    }

}
