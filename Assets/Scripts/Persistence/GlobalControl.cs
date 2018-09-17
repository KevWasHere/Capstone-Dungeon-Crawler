/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hagen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # GlobalControl.cs
*-----------------------------------------------------------------------*/

using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

/*
 * class used to transfer data between scenes and save and load data from disk.
 * Makes use of DontDestroyOnLoad to maintain data persistence through scenes
 * 
 * Creator: Myles, Yan , Shane , Cloud
 */

/*
 * LocalCoptOfData - reference to PlayerStatistics object used to store players data
 * isSceneBeingLoaded - bool set to true when data is loaded from disk
 * SceneChange - bool set to true when transfering between scenes
 * SceneChaneItems - bool set to true when transfering between scenes
 * savedEquipment - array to store current equipment while changing scenes
 * savedSkillAOE - array to store current aoe skill gems while changing scenes
 * savedSkillProjectile - array to store current projectile skill gems while changing scenes
 * savedSkillDash - array to store current dash skill gems while changing scenes
 * savedPotions - array to store current potions while chaning scenes
 * newTransform - bool set to true when specific player transform is required in new scene
 * newPosX, newPosY, newPosZ: floats representing the new player transform when scene is changed
 * newSceneID - index of new scene to be loaded
 * savedItems - array to store inventory items between scenes
 * SaveEvent - delegate used to store positions of all items dropped in the scene
 * SceneItemLists - dictionary of items to be instantiated in each scene
 * SceneDeathLists - dictionary of dead enemies to be deactivated in each scene
 * SceneItemNames - dictionary of static items in each scene
 * saveQuest -  the dictionary contain the key: scene number, value: all the state of quest and the dialog and current quest list on player
 * questList - the list of each quest's name
 * sceneNum - the scene number of current scene
 * questObj - the quest object parent in scene
 * questObjs - list of the quest object children
 * questObjStatus - the dictionary that hold the key: scene num, the list of bool: the quest object status
 */

public class GlobalControl : MonoBehaviour
{
    public static GlobalControl Instance;

	
    [HideInInspector]
    public PlayerStatistics savedPlayerData = new PlayerStatistics();

	/*
	 * set gameObject to DontDestroyOnLoad and initialize all arrays and dictionaries 
	 * 
	 */
    void Awake()
    {
        Application.targetFrameRate = 144;

        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        for (int i = 0; i < 12; i++)
        {
            SceneItemsLists.Add(i, new SaveDroppableList(i));
            SceneDeathLists.Add(i, new List<EnemyDeathTime>());
            SceneItemNames.Add(i, new List<string>());
        }
        savedEquipment = new Equipment[6];
        savedSkillAOE = new Equipment[4];
        savedSkillProjectile = new Equipment[5];
        savedSKillDash = new Equipment[6];
        savedPotions = new Consumable[5];

        //if (TransitionTarget == null)
        //TransitionTarget = gameObject.transform;

    }

    [HideInInspector]
    public PlayerStatistics LocalCopyOfData;
    [HideInInspector]
    public bool IsSceneBeingLoaded = false;
    [HideInInspector]
    public bool SceneChange = false;
    [HideInInspector]
    public bool SceneChangeItems = false;
    public Equipment[] savedEquipment;
    public Equipment[] savedSkillAOE;
    public Equipment[] savedSkillProjectile;
    public Equipment[] savedSKillDash;
    public Consumable[] savedPotions;
    [HideInInspector]
    public bool newTransform = false;
    [HideInInspector]
    public float newPosX, newPosY, newPosZ;
	[HideInInspector]
	public int newSceneID;
    public List<Item> savedItems = new List<Item>();
    public delegate SaveDroppableItem SaveDelegate();
    public event SaveDelegate SaveEvent;
    
    public Dictionary<int, SaveDroppableList> SceneItemsLists = new Dictionary<int, SaveDroppableList>();
    public Dictionary<int, List<EnemyDeathTime>> SceneDeathLists = new Dictionary<int, List<EnemyDeathTime>>();
    public Dictionary<int, List<string>> SceneItemNames = new Dictionary<int, List<string>>();
	public Dictionary <int, List<QuestStatus>> saveQuest = new Dictionary<int, List<QuestStatus>>();
	//the list of each quest's name
	public List<string> questList = new List<string>();

	private int sceneNum;
	//the quest object parent in scene
	public GameObject questObj;
	//list of the quest object children
	public Component[] questObjs;
	//the dictionary that hold the key: scene num, the list of bool: the quest object status
	public Dictionary<int, List<bool>> questObjStatus = new Dictionary<int, List<bool>>();
    

	private void Start()
    {
		sceneNum = SceneManager.GetActiveScene().buildIndex;
       

		//clear the bool list
		if (saveQuest.ContainsKey(sceneNum)) {
			foreach (var item in saveQuest[sceneNum]) {
				item.npcName = null;
				item.questBool.Clear ();
			}
		}
	}
	/*
	 * Function: SaveData
	 * 
	 * Description: saves all relevant player and quest data to disk, data contained
	 * in the PlayerStatistics class
	 * Creator: Myles Hagen
	 */
    public void SaveData()
    {
		sceneNum = SceneManager.GetActiveScene().buildIndex;
        if (!Directory.Exists("Saves"))
            Directory.CreateDirectory("Saves");

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile = File.Create("Saves/save.binary");

        LocalCopyOfData = PlayerState.Instance.localPlayerData;

        formatter.Serialize(saveFile, LocalCopyOfData);

        saveFile.Close();

		FileStream saveQuestFile = File.Create (Application.persistentDataPath+"/questInfo.dat");
		QuestStatistic qs = new QuestStatistic ();

//		foreach (var item in saveQuest) {
//			
//		}
		FindNPCSaveBool();
		qs.questStatistic = saveQuest;
		formatter.Serialize (saveQuestFile, qs);
		saveQuestFile.Close ();
    }

					/*
	 * Function: LoadData
	 * 
	 * Description: load all player data and quest data from disk
	 * Creator: Myles Hagen
	 */
    public void LoadData()
    {
		sceneNum = SceneManager.GetActiveScene().buildIndex;
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile = File.Open("Saves/save.binary", FileMode.Open);
        
        LocalCopyOfData = (PlayerStatistics)formatter.Deserialize(saveFile);
		//Debug.Log ("current hp in LoadData: " + LocalCopyOfData.HP);
        saveFile.Close();
        
		if (File.Exists(Application.persistentDataPath + "/questInfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream loadQuestFile = File.Open (Application.persistentDataPath + "/questInfo.dat", FileMode.Open);
			QuestStatistic qs = (QuestStatistic)bf.Deserialize (loadQuestFile);
			saveQuest = qs.questStatistic;
			FindNPCSetBool (sceneNum);
			loadQuestFile.Close ();
		}
    }

	/*
	 * Function: SaveBetweenScenes
	 * 
	 * Description: stores all player and quest data in global control just before scenes
	 * are changed.
	 * Creator: Myles Hagen
	 */
    public void SaveBetweenScenes()
    {
		sceneNum = SceneManager.GetActiveScene().buildIndex;
        SceneChange = true;
        SceneChangeItems = true;
		savedPlayerData.level = Player.instance.playerStats.level;
        savedPlayerData.HP = Player.instance.playerStats.currentHealth;
        savedPlayerData.currentXP = Player.instance.playerStats.currentExperience;
		savedPlayerData.targetExp = Player.instance.playerStats.targetExperience;
        savedPlayerData.currentRage = Player.instance.playerStats.currentRage;
		savedPlayerData.currentgold = Player.instance.playerStats.gold;
        savedItems.Clear();
        
        foreach (Item i in Inventory.instance.items)
        {
            savedItems.Add(i);
        }
        for (int i = 0; i < EquipmentManager.instance.Potions.Length; i++)
        {
            savedPotions[i] = EquipmentManager.instance.Potions[i];
        }
        for (int i = 0; i < EquipmentManager.instance.currentEquipment.Length; i++)
        {
            savedEquipment[i] = EquipmentManager.instance.currentEquipment[i];
            savedSKillDash[i] = EquipmentManager.instance.Skill_Dash[i];
            if (i < 5)
                savedSkillProjectile[i] = EquipmentManager.instance.Skill_Projectile[i];
            if (i < 4)
                savedSkillAOE[i] = EquipmentManager.instance.Skill_AOE[i];
        }
        int sceneID = SceneManager.GetActiveScene().buildIndex;

        foreach (KeyValuePair<int, SaveDroppableList> itemList in SceneItemsLists)
        {
            if (itemList.Key == sceneID && SaveEvent != null)
            {
                for (int i = 0; i < SaveEvent.GetInvocationList().Length; i++)
                {
                    
                    itemList.Value.SavedItems.Add((SaveDroppableItem)SaveEvent.GetInvocationList()[i].DynamicInvoke());
                }
                //Debug.Log("size of SavedItems" + itemList.Value.SavedItems.Count);
           
            }
        }
        FindNPCSaveBool ();
		FindQuestObjectSave ();

    }
	/* Creator: Yan 
	 * find the npc in the scene and save the quest status and dialog status before change to another scene
	 */
	public void FindNPCSaveBool() {

		if (Content.instance == null)
			return;
        questList = Content.instance.contentStrings;
		foreach (var item in questList) {
			Debug.Log ("q " + item);
		}
		List<string> npcList = AllNPCList.returnList (sceneNum);

		if (npcList == null) {
			return;
		}

		foreach (var item in npcList) {
			//Debug.Log (sceneNum + item);				
			QuestStatus qs = new QuestStatus (item ,new List<QuestStatus.QuestBool>(), new List<bool>());

			GameObject ve = GameObject.FindGameObjectWithTag (item);
			if (ve == null ) {
				return;
			}
			NPC veNPC = ve.GetComponent<NPC> ();
			if (veNPC != null) {
				foreach (var conCollection in veNPC.conditionCollections) {
					qs.questBool.Add(new QuestStatus.QuestBool(conCollection.description, conCollection.obtained,conCollection.available, conCollection.complete));
				}
			}

			DialogArray veDialogArray = ve.GetComponent<DialogArray> ();
			if (veDialogArray != null) {
				foreach (var dialog in veDialogArray.conversations) {
					qs.dialogBool.Add (dialog.active);
				}
			}
//			foreach (var it in qs.questBool) {
//				Debug.Log (it.description + it.available + it.obtained + it.complete);
//			}
			if (saveQuest.ContainsKey (sceneNum)) {
				saveQuest [sceneNum].Add(qs);
			} else {
				saveQuest.Add (sceneNum, new List<QuestStatus>());
				saveQuest [sceneNum].Add (qs);
			}

		} 
		 
	}
	
	/*creator: Yan Zhang
	 * find the npc in the scene and set the bool status from the global control to the npc
	*/
	public void FindNPCSetBool(int snum) {
		//find the quest list object in the scene
		if (GameObject.FindWithTag ("QuestContent") == null)
			return;
		Content content = GameObject.FindWithTag("QuestContent").GetComponent<Content>();
		content.contentStrings.Clear ();
		//add each quest the player current have to the quest list on gui
		foreach (var item in questList) {
			content.contentStrings.Add (item);
			//Debug.Log ("q " + item);
		}
		//get the list of npc that the current loading scene have
		List<string> npcList = AllNPCList.returnList (snum);
		if (npcList == null) {
			return;
		}
		//go throught the npc list in the cuurent scene and set the quest state and dialog state
		foreach (var item in npcList) {
			//Debug.Log (snum + item);
			//find the npc with teh tag
			GameObject ve = GameObject.FindGameObjectWithTag (item);
			if (ve == null) {
				//Debug.Log (item + " null when set");
				return;
			}
			QuestStatus qs = null;
            if (!saveQuest.ContainsKey(snum))
                return;

                foreach (var qstatus in saveQuest[snum]) {
                    if (qstatus.npcName == item) {
                        qs = qstatus;
                }

            }
            //get npc and set bool
			NPC veNPC = ve.GetComponent<NPC> ();
			foreach (var b in qs.questBool) {
				foreach (var conditionCollection in veNPC.conditionCollections) {
					if (b.description == conditionCollection.description) {
						conditionCollection.available = b.available;
						conditionCollection.obtained = b.obtained;
						conditionCollection.complete = b.complete;
					}
				}
			}
			//get the dialog and set the dialog array
			DialogArray veDialogArray = ve.GetComponent<DialogArray> ();
			if (veDialogArray != null) {
				for (int i = 0; i < veDialogArray.conversations.Length; i++) {
					veDialogArray.conversations [i].active = qs.dialogBool [i];
				}
			}

		}

	}
	/*Creator: Yan
	 * find the quest object in the scene and save all the satus
	 */
	public void FindQuestObjectSave(){
		questObj = GameObject.FindGameObjectWithTag ("QuestItem");
		if (questObj == null) {
			return;
		}
		questObjs = questObj.GetComponentsInChildren(typeof(BoxCollider), true);
		if (questObjStatus.ContainsKey (sceneNum)) {
			questObjStatus [sceneNum].Clear();
			foreach (var item in questObjs) {
				Debug.Log (item.gameObject.activeSelf+ " " + item.name);
				questObjStatus [sceneNum].Add (item.gameObject.activeSelf);
			}
		} else {
			questObjStatus.Add (sceneNum, new List<bool> ());
			foreach (var item in questObjs) {
				//Debug.Log (item.gameObject.activeSelf + " " + item.name);
				questObjStatus [sceneNum].Add (item.gameObject.activeSelf);
			}
		}

		Debug.Log ("save quest item status list " + questObjStatus [sceneNum].Count + " " + sceneNum);
	}
	
	/*Creator: Yan
	 * find the quest object in the scene ant set it throught the saved dictionary
	 */
	public void FindQuestObjectLoad(int num){
		questObj = GameObject.FindGameObjectWithTag ("QuestItem");
		if (questObj == null) {
			return;
		}
		questObjs = questObj.GetComponentsInChildren(typeof(BoxCollider), true);
		foreach (var item in questObjs) {
			//Debug.Log (item.gameObject.activeSelf + " " + item.name);
			//questObjStatus [sceneNum].Add (item.gameObject.activeSelf);
		}
		if (questObjStatus.ContainsKey(num)) {
			for (int i = 0; i < questObjStatus[num].Count; i++) {
				//Debug.Log (questObjs.Length  + "at" + questObjStatus[num].Count + " " +  i);
				//Debug.Log ("setting object " + i + questObjStatus [num] [i] + questObjs[i].name);
				questObjs [i].gameObject.SetActive (questObjStatus [num] [i]);
			}
		}
	}
}
