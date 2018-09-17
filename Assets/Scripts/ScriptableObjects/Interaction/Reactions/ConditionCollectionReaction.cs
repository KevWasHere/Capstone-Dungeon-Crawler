using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone
  # CharMenuControl.cs
  # ConditionCollection
*-----------------------------------------------------------------------*/
/*
Creator: Yan Zhang, Yunzheng Zhou
Enable a conditon collection on a npc
description: the description of the condition collection
available: the bool that whant to set to condition collection
gameObject: the npc that contains the condition collection
*/
public class ConditionCollectionReaction : Reaction {

	public string description;
	public bool available;
	public GameObject gameObject;

	/*Find the condition collection and set the bool
	 * Creator : Yan, Yunzheng Zhou
	 */
	protected override void ImmediateReaction ()
	{
		NPC npc = gameObject.GetComponent<NPC> ();
		if (npc != null) {
			Debug.Log ("in reaction " + description + npc.name);
			npc.SetAvailableTrue (description);
			return;
		}
	}
}
