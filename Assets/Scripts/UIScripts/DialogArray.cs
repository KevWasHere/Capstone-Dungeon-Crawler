using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone
  # CameraControllerUI.cs
  # dialog array
*-----------------------------------------------------------------------*/

/*
Creator: Yan Zhang
the array of dialogs each npc has
conversations: arrays of dialogs that each npc will has

*/
public class DialogArray : MonoBehaviour {
	public Dialogue[] conversations;
	/*startConversation: start a conversation base on the name of the dialog
	 * dialogName: the name of the dialog need to trigger in the dialog array
	 */
	public void StartConversation(string dialogName){
		//Debug.Log ("start conversation" + dialogName);
		//go through the dialog arrays and find the correct dialog that need to trigger
		for (int i = 0; i < conversations.Length; i++) {
			if (conversations[i].name == dialogName) {
				//start the dialog with dialog manager
				FindObjectOfType<DialogueManagerTwo>().StartDialogue(conversations[i]);
				//each dialog contain a list of name that need to trigger after finish this dialog 
				if (conversations[i].nextDialog.Length > 0) {
					//Debug.Log ("has next conversation");
					//trigger all the dialogs with name in the next dialog array
					foreach (var next in conversations[i].nextDialog) {
						foreach (var item in conversations) {
							if (next == item.name) {
								item.active = true;
							}
						}
					}

				}
			}
		}


	}

	//Modify a dialog's active state 
	//Creator: Yan Zhang
	public void ChangeDialogStatusToTrue(string dialogName){
		//Go through the dialog arrays and find the conversation and change status to true
		foreach (var item in conversations) {
			if (item.name == dialogName) {
				if (!item.active) {
					item.active = true;
				}
			}
		}
	}
}