
/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # RotationSaver.cs
*-----------------------------------------------------------------------*/
using UnityEngine;

/*
 * Class: RotationSaver
 *          save the rotation of player when saved function is calling
 *          and save the data to local file
 */
public class RotationSaver : Saver
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
        saveData.Save(key, transformToSave.rotation);
    }

    // load the data according to the key
    protected override void Load()
    {
        Quaternion rotation = Quaternion.identity;

        if (saveData.Load(key, ref rotation))
            transformToSave.rotation = rotation;
    }
}
