/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # 498 capstone
  # Content.cs
*-----------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
/*
 Creator: Yan Zhang
 */
/* 
 * contentStrings: String of contents
 * instance: Instance of this content
 * log: the text array in the pabel
 * questPanel: the quest panel in the canvas
 */
 
[Serializable]
public class Content : MonoBehaviour {
	
	public List<string> contentStrings = new List<string>();
	public Text[] log;
	public GameObject questPanel;

	#region Singleton
	[HideInInspector]
	public static Content instance;

	//Initialize components
	void Awake ()
	{
		instance = this;

	}

	#endregion
	// Use this for initialization
	void Start () {
		//contentStrings = new List<string>();
        if (questPanel != null)
		    log = questPanel.GetComponentsInChildren<Text>();
	}


	/*
	Change items in content list
	*/
	/*
	Creator: Yan Zhang
	*/
	void Update(){
		for (int i = 0; i < log.Length; i++) {
			if (i < contentStrings.Count) {
				log [i].text = contentStrings [i];
			} else {
				log [i].text = "";
			}
		}

	}
	/*
	 Creator: Yan Zhang
 	*/
	public string Change(){
		string s = "";
		for (int i = 0; i < contentStrings.Count; i++) {
			s = s + contentStrings [i];
			s = s + "\n";
		}
		return s;
	}

	/*
 	* Creator: Yan Zhang
 	* add a quest to content string 
 	*/
	public void AddTask (ConditionCollection conditionCollection){
		if (checkExist(conditionCollection.description)) {
			return;
		} else {
			contentStrings.Add (conditionCollection.description);
		}
	}

	/*Creator: Yan Zhang
	 * delete a quest from quest panel
	 */
	public void DeleteTask (string description){
		Debug.Log ("delete" + description);
		if (!checkExist(description)) {
			return;
		} else {
			contentStrings.Remove (description);
		}
	}

	/*Creator: Yan Zhang
	 * check if the quest exist already or not
	 */
	public bool checkExist(string s){

		for (int i = 0; i < contentStrings.Count; i++) {
			if (contentStrings [i].Equals (s)) {
				return true;
			}
		}
		return false;
	}
}
