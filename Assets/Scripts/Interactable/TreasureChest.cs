/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hagen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # TreasureChest.cs
  # 
*-----------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * animator - Reference to animator
 * isOpen - Bool if chest is opened or not
 * items - Array of items
 * table - Droppable items
*/

/*
Creator: Myles Hagen, Shane Weerasuriya
*/

public class TreasureChest : Interactable {

	Animator animator;

	bool isOpen;
	public Item[] items;
	private DropTable table;

	//Initalize components
	void Start() {
		animator = GetComponent<Animator> ();
		table = GetComponent<DropTable>();
	}

	/*
	Checks open state
	*/
	public override void Interact ()
	{
		base.Interact ();
		if (!isOpen) {
			animator.SetTrigger ("Open");
			StartCoroutine (DropTreasure ());
		}
	}

	/*
	Chest Drops
	*/
	IEnumerator DropTreasure() {

		isOpen = true;

		yield return new WaitForSeconds (1f);
		print ("Chest opened");
		table.DropItem();
	}
}
