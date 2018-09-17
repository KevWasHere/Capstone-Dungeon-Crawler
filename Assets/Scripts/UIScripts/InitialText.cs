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
  #initial Text
*-----------------------------------------------------------------------*/
/*
Creator: Yan Zhang, Tianqi Xiao
InitialText is the initial tutorial text when a fresh game start
instruction: array of instructions that the text will appear
state: the instruction the text is current in
*/
public class InitialText : MonoBehaviour {
	
	public string[] instruction = {"Left Click to move， go to the point." , "Right click to interact with npc/pick up item/attack the enemy.", "c"};
	public int state = 0;

	void Start(){
		
	}
	/*change the instruction on the text
	 * creator: Yan, Tianqi Xiao
	 */
	public void ChangeInstruction(int x){
		Debug.Log ("index in initial text" + instruction.Length);
		gameObject.GetComponent<Text> ().text = instruction [x];
	}
	
}
