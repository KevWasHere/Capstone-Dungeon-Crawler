using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone

*-----------------------------------------------------------------------*/

/*
Creator: Yan Zhang,
the all event list is a scriptable object that keep track of the one time event in the game
for exampl: the initial tutorial event
eventlist: a list of quest event that hold all the event
creationPath: the create path to create this scriptable object
load path: the path to load the alleventlist
istance: the instance of the all event list so it is a singleton in the game
*/

//Creator: Yan Zhang
public class AllEventList : ResettableScriptableObject {
	public List<QuestEvent> eventList = new List<QuestEvent>();
	private const string creationPath = "Assets/Resources/AllEventList.asset";
	private const string loadPath = "AllEventList";

	private static AllEventList instance;
	// Use this for the set up the singlrton, this game must contain a event list
	public static AllEventList Instance
	{
		get
		{
			if (!instance)
				instance = FindObjectOfType<AllEventList> ();
			if (!instance)
				instance = Resources.Load<AllEventList> (loadPath);
			if (!instance)
				Debug.LogError ("AllEventList has not been created yet.  Go to Assets > Create > AllEventList.");
			return instance;
		}
		set { instance = value; }

	}
	//Creator: Yan Zhang
	//reset the event list, clear the event list and set all the bool to false
	public override void Reset ()
	{
		foreach (var item in eventList) {
			item.status = false;
		}
	}

	/*changeStatus: go throught the eventlist and change the status of a certian event
	 * s: the name of the event
	 * num: the scene number the event is on
	 * status: the bool that want to set the event
	*/
	//Creator: Yan Zhang
	public static void changeStatus(string s, int num, bool status){
		foreach (var item in Instance.eventList) {
			if (item.eventName == s && item.sceneNum == num) {
				item.status = status;
			}
		}
	}

	/*returnStatus: go throught the event list and return the status of a certain event
	 * s: the name of the event
	 * num: the scene number of the event
	 */
	//Creator: Yan Zhang
	public static bool returnStatus(string s, int num){
		foreach (var item in Instance.eventList) {
			if (item.eventName == s && item.sceneNum == num) {
				return item.status;
			}
		}
		return false;
	}

	/*creat the all event list in the asssets menu
	 */
	//Creator: Yan Zhang
	[MenuItem("Assets/Create/AllEventList")]
	private static void CreateAllEventListAsset()
	{
		if(AllEventList.Instance)
			return;

		AllEventList instance = CreateInstance<AllEventList>();
		AssetDatabase.CreateAsset(instance, creationPath);

		AllEventList.Instance = instance;

		instance.eventList =new List<QuestEvent>();
	}

}

/* QuestEvent: is a variable hold all the information that a event in the game need
 * eventName: the name of the event
 * sceneNum: the scene number of the event
 * status: the status of the even, weather it is complete or not
 */
//Creator: Yan Zhang
[System.Serializable]
public class QuestEvent {
	public string eventName;
	public int sceneNum;
	public bool status;
}
