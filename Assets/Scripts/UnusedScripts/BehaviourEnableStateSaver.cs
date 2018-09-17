/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hagen, Shane Weerasuriya, 
  #                 Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # 
*-----------------------------------------------------------------------*/

// Creators : Cloud, Myles, Shane
// this script enables saving game states 

using UnityEngine;

public class BehaviourEnableStateSaver : Saver
{   // instance of the behaviour object which will be the state to save
    public Behaviour behaviourToSave;

    // returns behaviour to save with name and unique identifier
    protected override string SetKey ()
    {
        return behaviourToSave.name + behaviourToSave.GetType ().FullName + uniqueIdentifier;
    }

    // calls the savedata's save function with key, behaviour to save and enabled
    protected override void Save ()
    {
        saveData.Save (key, behaviourToSave.enabled);
    }

    // loading a saved state
    protected override void Load ()
    {
        bool enabledState = false;

        if(saveData.Load (key, ref enabledState))
            behaviourToSave.enabled = enabledState;
    }
}
