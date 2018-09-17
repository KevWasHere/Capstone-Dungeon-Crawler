using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone
  # CharMenuControl.cs
  # Dialogue
*-----------------------------------------------------------------------*/
/*
Creator: Yan Zhang, Yunzheng Zhou
Item submit reaction, delete the quest item from the inventory
itemId: the item id that need to be deleted
*/
public class ItemSubmitReaction : Reaction {
	
	public int itemId;
	//imediate reaction 
	protected override void ImmediateReaction ()
	{        
		Inventory.instance.DeleteQuestItem(itemId);
        InventoryGUI.instance.deleteFromInventoryGUI(itemId);
	}
}
