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
Creator: Yan Zhang,Tianqi Xiao, Yan Zhang, Yunzheng Zhou
ItemPickUp reaction, pick up the item and show the quest item in inventory
item: the item scriptable oject
*/ 
public class ItemPickUpReaction : Reaction {
	public Item item;
	// Use this for initialization
	//Immediate the reaction of the pick up item
	protected override void ImmediateReaction ()
	{
		Inventory.instance.exchange(item);
		InventoryGUI.instance.addNewItem(item);
	}
}
