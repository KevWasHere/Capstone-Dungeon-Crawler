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
 * Class that keeps track of items that were dropped in the scene so they can be 
 * instantiated when player moves back to that scene
 * Creator: Myles Hagen
 */
/*
 * sceneID - index of scene
 * itemDatabase - reference to item database
 */

public class ItemMaster : MonoBehaviour {

    #region Singleton

    public static ItemMaster instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public ItemDatabase itemDatabase;

    /*
     * search the scene item lists in global control and instantiate any items in there
     * correct location for the given scene.
     * 
     * Creator: Myles Hagen
     */
    void Start () {

        if (GlobalControl.Instance.SceneChangeItems)
        {
            Debug.Log("SCENE CHANGE"); 
            int sceneID = SceneManager.GetActiveScene().buildIndex;

            foreach (KeyValuePair<int, SaveDroppableList> itemList in GlobalControl.Instance.SceneItemsLists)
            {
                if (itemList.Key == sceneID && itemList.Value.SavedItems.Count > 0)
                {
                    foreach (SaveDroppableItem item in itemList.Value.SavedItems)
                    {
                        Item itemToInstantiate = itemDatabase.GetItem(item.itemID);
                        GameObject prefab = Instantiate(itemToInstantiate.itemPrefab);
                        prefab.transform.position = new Vector3(item.PositionX, item.PositionY, item.PositionZ);
                        //Physics.IgnoreCollision(prefab.GetComponent<Collider>(), GetComponent<Collider>());
                    }
                    itemList.Value.SavedItems.Clear();
                }
            }
            GlobalControl.Instance.SceneChangeItems = false;
        }
	}
	
}
