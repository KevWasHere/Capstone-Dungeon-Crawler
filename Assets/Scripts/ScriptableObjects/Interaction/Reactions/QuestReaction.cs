using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # 498 capstone
  # Stats.cs
  #
*-----------------------------------------------------------------------*/
/*
* Creator: Tianqi Xiao, SHane Weerasuriya
* QuestReaction: delete a quest on the quest panel
* description: the description of the quest that need to delete
* content: the content instance
*/
public class QuestReaction : Reaction {

	public string description;
	public Content content = Content.instance;
	//Delete the quest
	protected override void ImmediateReaction (){
		//Debug.Log ("in delete action " + description + c.name);
		content.DeleteTask (description);	
	}

}
