using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone
  # levelHandler
*-----------------------------------------------------------------------*/
/*
Creator: Yan Zhang, Shane Weerasuriya
LevelHandler is a sriptable object that hold all the levels (From level 1 to level 11 for now)
levelList: the list of levels of for all the levels
creationPath: create path for this level handler
loadPath: save path for this script
instance: instance of level handler
	
*/
public class LevelHandler : ScriptableObject {

	//public Dictionary <int, List<string>> npcListInEachScene = new Dictionary<int, List<string>>();
	public List<Level> levelLists = new List<Level>();
	private const string creationPath = "Assets/Resources/LevelHandler.asset";
	private const string loadPath = "LevelHandler";

	private static LevelHandler instance;
	// Use this for initialization
	public static LevelHandler Instance
	{
		get
		{
			if (!instance)
				instance = FindObjectOfType<LevelHandler> ();
			if (!instance)
				instance = Resources.Load<LevelHandler> (loadPath);
			if (!instance)
				Debug.LogError ("LevelHandler has not been created yet.  Go to Assets > Create > LevelHandler.");
			return instance;
		}
		set { instance = value; }

	}
	/*creator: Yan, Shane Weerasuriya
	 *get the correct level base on the index
	 *levelNum: the number that need to get
	 */
	public static Level GetLevel(int levelNum){
		return Instance.levelLists [levelNum];
	}
	
	/*Creator: Yan, Shane Weerasuriya
	 * create of path in the menu in the unity
	 * and set up the instance
	*/
	[MenuItem("Assets/Create/LeveHandler")]
	private static void CreateLevelHandlerAsset()
	{
		if(LevelHandler.Instance)
			return;

		LevelHandler instance = CreateInstance<LevelHandler>();
		AssetDatabase.CreateAsset(instance, creationPath);

		LevelHandler.Instance = instance;

		instance.levelLists =new List<Level>();
	}
}
