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
Creator: Yan Zhang
Dialog variable for each dialog
active: the active state of the dialog
name: the name of the dialog
nextDialog: a array of dialog names that this dialog will trigger
sentences: a array of string of sentences
*/
[System.Serializable]
public class Dialogue {

	public bool active;
	public string name;
	public string[] nextDialog;
	[TextArea(3, 10)]
	public string[] sentences;

}
