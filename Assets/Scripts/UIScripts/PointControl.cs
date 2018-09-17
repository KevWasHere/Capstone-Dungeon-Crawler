using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone
  # CharMenuControl.cs
  # pointcontrol
*-----------------------------------------------------------------------*/
/*
Creator: Yan Zhang, Kevin Ho
*/
public class PointControl : MonoBehaviour {
	public InitialText initText;
	public string[] sentence = {"Right click to interact with npc or pick up items", "B", "C", "D"};
	//public int where = 0;

	void OnTriggerEnter(Collider other){
		//FindObjectOfType<DialogueManagerTwo> ().endDialog = false;
		//Debug.Log ("on trigger enter");
		//Debug.Log ("init Text" + initText.instruction.Length);
		//Debug.Log (text.text);
		initText.ChangeInstruction(1);
		//Debug.Log (text.text);
	}

	void OnTriggerExit(Collider other) {
		//where = (where + 1) % sentence.Length;
		initText.state+=1;
		Destroy (this.gameObject);
	}
}
