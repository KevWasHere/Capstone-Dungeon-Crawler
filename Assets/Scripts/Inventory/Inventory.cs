/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone

*-----------------------------------------------------------------------*/

/*
 * Author: Yunzheng Zhou, Myles Hagen
 * Date: 2018-04-19
 */

/*
* space - size of inventory
* items - list of items in inventory
* uIItemDatabase - reference to ui item database
* gold - reference to gold gameobject
* currSize - current size of inventory
* itemDatabase - reference to item database
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public class Inventory : MonoBehaviour
{

    #region Singleton

    public static Inventory instance;

	// initialize onventory instance, and fill inventory with default items
    void Awake()
    {
        instance = this;
		//items.Clear();
        for (int i = 0; i < 40; i++)
        {
			items.Add(itemDatabase.GetItem(0));
        }
			
    }

    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    public const int space = 40;  // Amount of item spaces
    public List<Item> items = new List<Item>();
    public UIItemDatabase uIItemDatabase;
    public GameObject gold;
    //public Item[] items = new Item[space];
    int currSize = 0;
    public ItemDatabase itemDatabase;
    
    // please compile this time

    //[HideInInspector]
    public List<int> itemIDs = new List<int>();
   
	/*
	 * Function: OnBeforeSerialize
	 * Description: store itemIDs of all items in inventory
	 * 
	 * Creator: Myles Hagen
	 */
    public void OnBeforeSerialize()
    {

        itemIDs.Clear();
        foreach (Item i in items)
        {
            itemIDs.Add(i.itemID);
        }
        //Debug.Log("Before " + itemIDs.Count);
    }

	/*
	 * Function: OnAfterDeserialize
	 * Description: convert item IDs to scriptable objects that
	 * represent items in the inventory
	 * 
	 * Creator: Myles Hagen
	 */
    public void OnAfterDeserialize()
    {
        //Debug.Log("After " + itemIDs.Count);
		items.Clear();
		// add inventoryGUI clear function
		//InventoryGUI.instance.Clear();
        foreach (int i in itemIDs)
        {
            var item = itemDatabase.GetItem(i);
            items.Add(item);
			//InventoryGUI.instance.Add(i); refill the inventory gui with item ids
        }
    }


	/*
	 * Function: Add
	 * Parameters: item
	 * Description: add an item to the inventory if there is space
	 * 
	 */
    public void Add(Item item)
    {

        if (item.showInInventory)
        {
            if (items.Count >= space)
            {
                Debug.Log("Not enough room.");
                return;
            }
            items.Add(item);
        }

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();

    }
    // Remove an item
    public void Remove(Item item)
    {
        items.Remove(item);

        /* if (onItemChangedCallback != null)
             onItemChangedCallback.Invoke();*/
    }

    public void exchange(Item item)
    {
        for (int i = 0; i < space; i++)
        {
            if (items[i].itemID == 0)
            {
                items[i] = item;
                return;
            }
        }
    }

    private void LateUpdate()
    {
        UpdateInventory();
    }

    public void setdefault(int index)
    {
        items[index] = itemDatabase.GetItem(0); 
    }

    public void UpdateInventory()
    {
        for (int i = 0; i < 40; i++) {
            //Debug.Log (InventoryGUI.instance.items[i] + "a");
            if (InventoryGUI.instance != null)
                items[i] = itemDatabase.GetItem(InventoryGUI.instance.items[i]);
        }
    }

    public void DeleteQuestItem(int id)
    {
        for (int i = 0; i < space; i++)
        {
            if (items[i].itemID == id)
            {
                Equipment item = (itemDatabase.GetItem(0) as Equipment);
                items[i] = item;
                Debug.Log(items[i]);
            }
        }
    }
}
