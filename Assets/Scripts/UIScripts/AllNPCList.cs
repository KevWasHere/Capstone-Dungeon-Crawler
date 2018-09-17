using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone

*-----------------------------------------------------------------------*/

/*
*Creator: Yan Zhang
*AllNPCList is a scriptable object that hold all the npc data in each scene, so when loading a new scene, 
*   the data of npc in each scene can be load correcting
* npcList: a list of npclist in each scene
* creationPath: the path that to store this scriptable object
* loadPath: the loading path of the object
* instance: the instance of the scriptable object, this is a singleton, all the same object will equals this object
*/
//Creator: Yan Zhang
public class AllNPCList : ScriptableObject {

	public List<NPCList> npcLists = new List<NPCList>();
	private const string creationPath = "Assets/Resources/AllNPCList.asset";
	private const string loadPath = "AllNPCList";

	private static AllNPCList instance;
	// Use this to initialte the instance
	public static AllNPCList Instance
	{
		get
		{
			if (!instance)
				instance = FindObjectOfType<AllNPCList> ();
			if (!instance)
				instance = Resources.Load<AllNPCList> (loadPath);
			if (!instance)
				Debug.LogError ("NPCList has not been created yet.  Go to Assets > Create > AllNPCList.");
			return instance;
		}
		set { instance = value; }

	}

	/* returnList: will return a list of string containing all the tag names of each npc in the scene
	 * num: the corisbonding scene number
	 * Go throught the list of all npcs from all the scenes and find the list of the targetting scene
	 */
	//Creator: Yan Zhang
	public static List<string> returnList(int num){
		foreach (var item in Instance.npcLists) {
			if (item.sceneNum == num) {
				return item.npcList;
			}
		}
		return null;
	}

	/*create the path to create the npc from the menu in unity
	 */
	//Creator: Yan Zhang
	[MenuItem("Assets/Create/AllNPCList")]
	private static void CreateAllNPCListAsset()
	{
		if(AllNPCList.Instance)
			return;
		//set up the instance always equal to the same one
		AllNPCList instance = CreateInstance<AllNPCList>();
		AssetDatabase.CreateAsset(instance, creationPath);

		AllNPCList.Instance = instance;

		instance.npcLists =new List<NPCList>();
	}
}


