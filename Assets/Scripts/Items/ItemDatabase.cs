/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone

*-----------------------------------------------------------------------*/

/*
 * Author: Myles Hagen,Tianqi Xiao, Yan Zhang
 * Date: 2018-04-19
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/ItemDatabase")]
public class ItemDatabase : ScriptableObject
{

	
    public Item[] items;

	/*
	 * Function: GetItem
	 * Parameter: itemID
	 * Returns: item scriptable object if it exists, or null if not
	 * Description: search item database for item with given ID
	 *
	 * Author: Myles Hagen,Tianqi Xiao, Yan Zhang
 	 * Date: 2018-04-19
	 */
    public Item GetItem(int itemID)
    {
        foreach (var item in items)
        {
            if (item != null && item.itemID == itemID)
                return item;
        }
        return null;
    }
}
