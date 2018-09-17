/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 330 Final Project
  # QuestMenu.cs
  # Quest manager
*-----------------------------------------------------------------------*/

/*
Creator: Tianqi Xiao, Yan Zhang, Myles Hagen
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestMenu : MonoBehaviour {

	public GameObject panel;
	public Button[] questList = new Button[10];
	public ConditionCollection[] currentQuest = new ConditionCollection[10];
	//public ArrayList currentQuest = new ArrayList();
	// Use this for initialization
	void Start () {
		questList = GetComponentsInChildren<Button> ();
	}
	
	// Update is called once per frame
	void Update () {
//		for (int i = 0; i < 10; i++) {
//			if (currentQuest[i] != null) {
//				AddButton (currentQuest [i]);
//			}
//		}
	}

	public bool AddQuest(ConditionCollection conditionCollection){
		//currentQuest.Add (conditionCollection);
		Debug.Log("add quest" + conditionCollection.description);

		if (checkExist (currentQuest, conditionCollection) == true) {
			return false;
		} else {
			for (int i = 0; i < currentQuest.Length; i++) {
				if (currentQuest [i] == null) {
					conditionCollection.obtained = true;
					currentQuest [i] = conditionCollection;
					AddButton (conditionCollection);
					//Debug.Log (conditionCollection.description);
					return true;
				}
			}
		}

		return false;
	}

	public bool AddButton(ConditionCollection conditionCollection){
		Debug.Log ("add Button" + conditionCollection.description);
		for (int i = 0; i < 10; i++) {
			if (questList[i].GetComponentInChildren<Text>().text == "Button") {
				//questList [i].gameObject.SetActive (true);
				questList [i].GetComponentInChildren<Text> ().text = conditionCollection.description;
				return true;
			}
		}
		return false;
	}

	public bool checkExist(ConditionCollection[] list, ConditionCollection conditionCollection){
		
		for (int i = 0; i < list.Length; i++) {
			if (list[i]== null) {
				continue;
			}else if (list[i].description == conditionCollection.description) {
				return true;
			}
		}
		return false;
	}
}
