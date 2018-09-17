/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hagen, Shane Weerasuriya, 
  #                 Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # 
*-----------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/*
 Class that keeps track of all items in a scene allowing them
 to be deactivated when the correct conditions are met.
 Creator: Myles Hagen , Shane 
*/
/*
 * sceneID - index of current scene
 * ItemsDictionary - dictionary of items 
 */

public class ItemRegistry : MonoBehaviour {

    #region Singleton

    public static ItemRegistry instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public Dictionary<string, GameObject> ItemsDictionary = new Dictionary<string, GameObject>();
    int sceneID;
    /*
     * Start function where all game objects tagged as item are
     *  added too a dictionary, the objects will then be deactivated
     *  if they are contained in the global dictionary for that scene.
     */
    void Start () {

        sceneID = SceneManager.GetActiveScene().buildIndex;
        
        var respawns = GameObject.FindGameObjectsWithTag("Item");
        foreach (var item in respawns)
        {
			if (item.GetComponent<ItemPickup> () != null) {
				if (item.GetComponent<ItemPickup> ().isStatic)
					ItemsDictionary.Add (item.name, item);
			}

			if (item.GetComponent<GoldPickup> () != null) {
				if (item.GetComponent<GoldPickup> ().isStatic)
					ItemsDictionary.Add (item.name, item);
			}
        }
        if (GlobalControl.Instance.SceneItemNames[sceneID].Count > 0)
        {
            foreach (string name in GlobalControl.Instance.SceneItemNames[sceneID])
            {
                ItemsDictionary[name].SetActive(false);
            }
        }

     }

}
