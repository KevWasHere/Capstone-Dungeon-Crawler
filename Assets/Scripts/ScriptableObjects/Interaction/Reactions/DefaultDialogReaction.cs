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
* Creator: Yan Zhang,, Yunzheng Zhou
* Trigger the dialog that set on npc
* dialog: the dialog that show some information to player
*/
public class DefaultDialogReaction : Reaction {

	public Dialogue dialog;

	/*
     * Initialize the dialog 
     */
	protected override void SpecificInit()
	{

		//guiManager = FindObjectOfType<GUIManager> ();
	}

	/*
     * Once the player get interacte with NPC or specific enemy
     * the immdiate reaction to such Gameobject with a dialog
     * Pops up
     */
	protected override void ImmediateReaction ()
	{
		FindObjectOfType<DialogueManagerTwo>().StartDialogue(dialog);
	}
}
