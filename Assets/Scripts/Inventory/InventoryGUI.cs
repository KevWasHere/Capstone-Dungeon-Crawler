/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone

*-----------------------------------------------------------------------*/

/*
 * Author: Yunzheng Zhou
 * Date: 2018-04-19
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * InventoryGui is a class about front-end GUI inventory
 * It has functions to implement the item in items array to change or add or delete item from array.
 * The GUI inventory will directly read the items array and show the icon of item and put iteminfo
 * into the tooltip panel if player user mouse over the icon of item on gui.
 * 
 */
public class InventoryGUI : MonoBehaviour {

    #region Singleton

    public static InventoryGUI instance;

    void Awake()
    {
        instance = this;
        //UpdateOnLoad();
    }

    #endregion
    public GameObject[] gameslot;               // gameobject array contain the gui slots of inventory
    public UIItemDatabase itemDatabase;         // the database of gui inventory to control the item type, id and icon of items
    public int[] items;                         // items array contain only the id of item that put in items array. if the slot contain empty item, the id is 0

    /*
     * UpdateOnload works for load function when player press f7 or click on load button when the panel shows up
     * it will read all data and fill in the necssary array that GUI inventory need
     */
    public void UpdateOnLoad()
    {
        for (int i = 0; i < 40; i++)
        {
            if (items[i] == 0)
            {
                continue;
            }
            Test_UIItemSlot_Assign item = gameslot[i].GetComponent<Test_UIItemSlot_Assign>();
            if (item == null)
            {
                gameslot[i].AddComponent<Test_UIItemSlot_Assign>();
                Test_UIItemSlot_Assign newitem = gameslot[i].GetComponent<Test_UIItemSlot_Assign>();
                newitem.slot = gameslot[i].GetComponent<UIItemSlot>();
                newitem.itemDatabase = itemDatabase;
                newitem.assignItem = items[i]; 
            }
            else {
                item.assignItem = items[i];
                UIItemSlot slot = gameslot[i].GetComponent<UIItemSlot>();
                slot.Assign(itemDatabase.GetByID(item.assignItem = items[i]));
            }
        }
    }


    // add new item to the items array. if the item id is 0 which means its empty,
    //  fill in the additem into that slot
    public bool addNewItem(Item additem)
    {
        for (int i = 0; i < 40; i++)
        {
            Test_UIItemSlot_Assign item = gameslot[i].GetComponent<Test_UIItemSlot_Assign>();
            if (items[i] == 0)
            {
                //gameslot[i].AddComponent<Test_UIItemSlot_Assign>();
                //Test_UIItemSlot_Assign newitem = gameslot[i].GetComponent<Test_UIItemSlot_Assign>();
                //newitem.slot = gameslot[i].GetComponent<UIItemSlot>();
                //newitem.itemDatabase = itemDatabase;
                //newitem.assignItem = additem.itemID;
                items[i] = additem.itemID;
                UIItemSlot slot = gameslot[i].GetComponent<UIItemSlot>();
                slot.Assign(itemDatabase.GetByID(item.assignItem = items[i]));
                return true;
            }
        }
        return false;
    }

    // update the item by array of item ids and also update the potion slots
    void Update()
    {
        for (int i = 0; i < 40; i++)
        {
            Test_UIItemSlot_Assign item = gameslot[i].GetComponent<Test_UIItemSlot_Assign>();
            
            items[i] = item.assignItem;
        }
        for (int i = 0; i < 5; i++) {
            if (EquipmentManager.instance.potionSlot[i] != null)
                EquipmentManager.instance.Potion_IDs[i] = EquipmentManager.instance.potionSlot[i].GetComponent<Test_UIEquipSlot_Assign>().assignItem;
            EquipmentManager.instance.Potions[i] = (EquipmentManager.instance.itemDatabase.GetItem(EquipmentManager.instance.Potion_IDs[i]) as Consumable);
        }
    }

    // deletefromInventoryGUI will delete a item according to the given id
    public void deleteFromInventoryGUI(int id)
    {
        for (int i = 0; i < 40; i++)
        {
            if (items[i] == id)
            {
                gameslot[i].GetComponent<Test_UIItemSlot_Assign>().assignItem = 0;
                gameslot[i].GetComponent<UIItemSlot>().Assign(itemDatabase.GetByID(0));
                items[i] = 0;
            }
        }
    }

    //  updateslot()    update the gui items from backend array and use the item id in backend array to update the gui item
    public void updateSlot()
    {
         for (int i = 0; i < 40; i++)
        {
            //Debug.Log("FIRST ROW");
            Test_UIItemSlot_Assign item = gameslot[i].GetComponent<Test_UIItemSlot_Assign>();
            item.assignItem = Inventory.instance.items[i].itemID;
            UIItemSlot slot = gameslot[i].GetComponent<UIItemSlot>();
            slot.Assign(itemDatabase.GetByID(item.assignItem));
            //Debug.Log("update slot " + Inventory.instance.items[i].itemID);
        }
    }
	
	
}
