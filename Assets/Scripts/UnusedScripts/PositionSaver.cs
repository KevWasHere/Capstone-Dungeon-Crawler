
/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # PositionSaver.cs
*-----------------------------------------------------------------------*/
using UnityEngine;

/*
 * Class: PositionSaver
 * 
 * Description:
 *         Position saver save the position of player and put it in the save data.
  * Creator  : Yunzheng Zheng, Kevin Ho, Tianqi Xiao
 */
public class PositionSaver : Saver
{
    public Transform transformToSave;       // transform of player

    // set the name and type
    protected override string SetKey()
    {
        return transformToSave.name + transformToSave.GetType().FullName + uniqueIdentifier;
    }

    // save the rotation for the specific key
    protected override void Save()
    {
        saveData.Save(key, transformToSave.position);
    }

    // load the data according to the key
    protected override void Load()
    {
        Vector3 position = Vector3.zero;

        if (saveData.Load(key, ref position))
            transformToSave.position = position;
    }
}
