using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone
  # villager
*-----------------------------------------------------------------------*/
/*
Creator: Yan Zhang, Kevin Ho
Start a conversation when tutorial start
*/
public class Villager : MonoBehaviour {

	public DialogArray dialogArray;
	private DialogueManagerTwo dialogManager;
	public Text text;
	// Use this for initialization
	void Start () {
		dialogArray = GetComponent<DialogArray> ();
		dialogManager = FindObjectOfType<DialogueManagerTwo> ();
		if (!AllEventList.returnStatus("villageInitial", 0)) {
			dialogManager.StartDialogue (dialogArray.conversations[0]);
		}



	}
	

}
