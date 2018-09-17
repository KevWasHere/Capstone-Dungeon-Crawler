using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # Stats.cs
  #
*-----------------------------------------------------------------------*/
/* Creator : SHane Weerasuriya
 * when the player interacte with NPC or at begining of 
 * the game, the player has dialog show up on the screen
 * dialog: the dialog that show some information to player
 */
public class DialogReaction : Reaction {

	public DialogArray dialogArray;
	//public Dialogue dialog;
	public string dialogName;

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
		//guiManager.enabled = !guiManager.enabled;
		Debug.Log (dialogName);
		dialogArray.StartConversation(dialogName);
		//FindObjectOfType<DialogueManagerTwo>().StartDialogue(dialog);
		dialogArray.ChangeDialogStatusToTrue(dialogName);

	}
}
