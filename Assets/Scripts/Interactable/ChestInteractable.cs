/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 330 Final Project
  # ChestInteractable.cs
  # //////////ADD REAL COMMENTS HERE LATER!!!
*-----------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * table - Droppable items
 * opened - Bool to check if chest if opened or not
*/

/*
Creator: Myles Hangen,Tianqi Xiao
*/

public class ChestInteractable : Interactable {

	DropTable table;
	bool opened;

	//Initialize components
	void Start ()
	{
		table = GetComponent<DropTable>();
	}

	public override void Interact ()
	{
		
		base.Interact ();
		if (!opened) {
			table.DropItem ();
			opened = true;
		} else {
			Debug.Log("Chest already looted");
		}
	}
}
