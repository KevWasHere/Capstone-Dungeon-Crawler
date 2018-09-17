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

/*
 * This class is currently not using
 */
public class ItemDataBase : MonoBehaviour {

    public List<Item> items = new List<Item>();
    
    /*void Start()
    {
        items.Add(new Item2("S_Sword01", 4, "It's a big big sword",2,0,Item2.ItemType.Weapon));
        items.Add(new Item2("A_Armor04", 1, "nice shirt", 2, 0, Item2.ItemType.Weapon));
        items.Add(new Item2("I_C_Meat", 2, "yummy fresh meat", 0, 0, Item2.ItemType.Consumable));
        items.Add(new Item2("S_Sword10", 3, "no ancestor", 0, 0, Item2.ItemType.Weapon));
    }

    public Item GetItem(int id)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemID == id)
            {
                return items[i];
            }
        }
        return null;

    }*/
}
