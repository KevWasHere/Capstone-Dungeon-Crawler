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
  # DialogPanel
*-----------------------------------------------------------------------*/
/*
Creator: Yan Zhang
dialogTrigger: array of buttons that in the dialog panel
DialogArray: The Array of dialog that need to display on the dialog panel
*/
public class DialogPanel : MonoBehaviour {
	public Button[] dialogTrigger;
	public DialogArray dialogArray;
	//On Awake
	//get the buttons from the children
	void Awake () {
		dialogTrigger = GetComponentsInChildren<Button>();
		//set the text on the button to empty
		foreach (var item in dialogTrigger) {
			item.GetComponentInChildren<Text>().text = "";
			item.interactable = false;
		}
	}

	// Update each button in dialog panel
	//dia: is the dialog array need to be diaplay
	//Creator: Yan Zhang
	public void UpdateDialog (Dialogue[] dia) {
		//go through the array and set the button's interaction base on the active state of each dialog
		for (int i = 0; i < dia.Length; i++) {
			if (dia[i].active == true) {				
				dialogTrigger [i].GetComponentInChildren<Text> ().text = dia [i].name;
				dialogTrigger [i].interactable = true;
			}
		}
		//Go through each button and set the button interaction to trigger the right dialog base on the name
		for (int i = 0; i < dialogTrigger.Length; i++) {
			if (dialogTrigger[i].interactable) {
				string s = dialogTrigger [i].GetComponentInChildren<Text> ().text.ToString();
				//Debug.Log(s + " is add listener.");
				dialogTrigger [i].onClick.AddListener (delegate{dialogArray.StartConversation(s);});
			}
		}
			
	}
}
