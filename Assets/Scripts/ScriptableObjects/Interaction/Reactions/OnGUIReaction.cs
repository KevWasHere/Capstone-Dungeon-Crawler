using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 330 Final Project
  # Stats.cs
  #
*-----------------------------------------------------------------------*/
/* Creator :  Yunzheng Zhou, Shane Weerasuriya, Tianqi Xiao
 * OnGuireaction is when player interacte with NPC,
 * A GUI panle will pop up and show either quest
 * or dialog.
 * 
 * obj: the gameobject
 * NPC: the NPC that contain some condition and can be inter-
 * -acte with player
 * Condition: the condition for quest or dialog
 * addQuestSuccessful: the boolean value to check whether the player
 * is finishing the quest or not.
 * manage: the link to the GUImanager
 * questlist: the list of quest that could store the new quest or delete
 * old quest
 */
public class OnGUIReaction : Reaction {

	//public static NPC NPC;
	//public QuestMenu questMenu;
	public GameObject obj;
	public NPC npc;
	//public GameObject taskListPanel;
	//public GameObject taskInfoPanel;
	public ConditionCollection[] condition;
	//public Text text;
	//public Button[] list = new Button[3];

	//public GameObject panel;

	//public Text[] info = panel.GetComponentsInChildren<Text>();

	//public Button accept;
	//public Button close;

	public bool addQuestSuccesful;

	public GUIManager manager = GUIManager.instance; 

	public Content questList = Content.instance;
	//public ConditionCollection[] currentQuests = PlayerStatus.playerStatus.currentQuests;
	//private ConditionCollection[] conditionCollection[] = NPC.conditionCollections;
	//private GUIManager guiManager;

    /*
     * Initial the questline
     */
	protected override void SpecificInit()
	{
		
		//guiManager = FindObjectOfType<GUIManager> ();
	}

    /*
     * When interacte with Npc
     * the quest is either been accpeted or completed
     */
	protected override void ImmediateReaction ()
	{
		//guiManager.enabled = !guiManager.enabled;
		//Debug.Log (obj.GetComponents<NPC>());

		npc = obj.GetComponent<NPC>();
		condition = npc.conditionCollections;
		//panel.SetActive (true);
		//throw new System.NotImplementedException ();
		Display(condition);
	}

    /*
     * add quest by add condition to the condition array
     * and then add quest to the list of quest
     * 
     */
	public void Display(ConditionCollection[] conditionCollection)
	{
		//int j = 0;
		//Debug.Log (conditionCollection.requiredConditions [i].description);
		//taskListPanel.SetActive (true);
		for (int i = 0; i < conditionCollection.Length; i++) {
			//text.text = conditionCollection [i].description;

			if (conditionCollection[i].available && !conditionCollection[i].obtained) {
				
				AddQuest (conditionCollection [i]);
				Debug.Log (conditionCollection [i].description);
				string s = conditionCollection[i].description + "\n";
				questList.contentStrings.Add (s);

				//manager.content = manager.content + conditionCollection [i].description + "\n";

			}			 
		}
		manager.content = questList.Change();
	}

    /*
     * ADd a quest according to the conditionCollection
     * if the manage current quest exists, it means player does not
     * have the quest return false, otherwise return true
     */
	public bool AddQuest(ConditionCollection conditionCollection){
		//currentQuest.Add (conditionCollection);
		//Debug.Log("add quest" + conditionCollection.description);

		if (checkExist (manager.currentQuests, conditionCollection) == true) {
			return false;
		} else {
			for (int i = 0; i < manager.currentQuests.Length; i++) {
				if (manager.currentQuests [i] == null) {
					conditionCollection.obtained = true;
					manager.currentQuests [i] = conditionCollection;
					//Debug.Log (conditionCollection.description);
					return true;
				}
			}
		}

		return false;
	}

    /*
     * check the quests in the list is finish yet or not
     * if the quest is found, return true
     */
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

//
//	public void ShowTaskInfo(){
//		GUI.Box (new Rect(Screen.width/2 , Screen.height/2, 200, 200), "this is a box");
//	
//	}
//
//	public void SwitchButtonHandler(ConditionCollection conditionCollection){
//		Debug.Log("in switch handler:" + conditionCollection.description);
//		//taskInfoPanel.SetActive (true);
//		string s = null;
//		for (int k = 0; k < conditionCollection.requiredConditions.Length; k++) {
//			s = s + conditionCollection.requiredConditions [k].description + "\n";
//		}
//		//taskInfoPanel.GetComponentInChildren<Text> ().text = s;
//		//accept.gameObject.SetActive (true);
//		if (conditionCollection.obtained!= true) {
//			
////			Button[] buttons = taskInfoPanel.GetComponentsInChildren<Button> ();
////			for (int i = 0; i < buttons.Length; i++) {
////				if (buttons[i].name == "Accept") {
////					conditionCollection.obtained = true;
////					buttons [i].onClick.AddListener (delegate {
////						questMenu.AddQuest (conditionCollection);
////					});
////				}
////			}
//			accept.onClick.AddListener (delegate {
//				addQuestSuccesful = questMenu.AddQuest (conditionCollection);
//			});
//			if (addQuestSuccesful == false) {
//				Debug.Log ("quest log full can not accecpt");
//			}
//		}
//		else if (conditionCollection.obtained == true) {
//			accept.gameObject.SetActive (false);
//		}
//
//		Debug.Log ("what is this");
//
//	}
//
//	void OnGUI(){
//		
//	
//	}

}
