/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # saver.cs
*-----------------------------------------------------------------------*/
using UnityEngine;

/*
 * Class: Saver
 * 
 * Saver define unique identifier and key and scene id to check the data on the specific scene
 * It could only be awaked once each game at run time
 * 
 * Variables:
 *          uniqueIdentifier - the unique string to identify the data
 *          saveData - the data that need to save
 *          key - the name and type of transform plus the unique identifier
 *          sceneController - controller of scene
 */
public abstract class Saver : MonoBehaviour
{
    public string uniqueIdentifier;
    public SaveData saveData;


    protected string key;


    private SceneController sceneController;


    private void Awake()
    {
        sceneController = FindObjectOfType<SceneController>();

        if(!sceneController)
            throw new UnityException("Scene Controller could not be found, ensure that it exists in the Persistent scene.");
        
        key = SetKey ();
    }

    // enable the scene controller to add up the save and load data
    private void OnEnable()
    {
        sceneController.BeforeSceneUnload += Save;
        sceneController.AfterSceneLoad += Load;
    }

    // disable the sceneController
    private void OnDisable()
    {
        sceneController.BeforeSceneUnload -= Save;
        sceneController.AfterSceneLoad -= Load;
    }

    // set key abstract for children class
    protected abstract string SetKey ();

    // set save abstract for children class
    protected abstract void Save ();

    // set load abstract for children class
    protected abstract void Load ();
}
