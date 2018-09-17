using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using UnityEngine.UI;
/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone
  # CharMenuControl.cs
  # managescene
*-----------------------------------------------------------------------*/
/*
Creator: Yan Zhang, myles
Manage scene will manage the event happened in each scene and set up the object
sceneNum: the scene number of the current scnen
enemycount: the enemy count for the initial event
villageElder: the npc called village elder
witch: a npc
initialText: the tutorialText
portalPanel: the portal interface on canvas
grandfather: the npc
*/
public class ManageScene : MonoBehaviour {
	private int sceneNum;
	public int enemycount= 0;
	public GameObject villageElder;
	public GameObject witch;
	public GameObject initialText;
	public GameObject portalPanel;
	public GameObject grandFather;
	// Use this for initialization
	#region Singleton
	//instance of the manageScene
	public static ManageScene instance;

	void Awake () {
		instance = this;

	}
	#endregion
	/* initialize everything
	 * creator:Yan Zhang, myles
	 */
	void Start(){
		//set count to 0
		enemycount = 0;
		//get the current scene num
		sceneNum = SceneManager.GetActiveScene().buildIndex;
		//Load all the quest object in the scene
		GlobalControl.Instance.FindQuestObjectLoad (sceneNum);
		//set the portal interface for this scene
		SetUpPortal (sceneNum);
		//if it is village scene now
		//if the tutorial does happened put player in the beginnign of the village
		// if the tutorial happend enable all the npcs and disable all the screaming audio and fire in the scene
		if (sceneNum == 0) {
			//Debug.Log ("in village");
			if (!AllEventList.returnStatus("villageInitial", 0)) {
				Player.instance.GetComponent<NavMeshAgent> ().enabled = false;
				Player.instance.transform.position = new Vector3 (52.27f,0.203f,192.47f);
				Player.instance.GetComponent<NavMeshAgent> ().enabled = true;
				
				//Disable AI pathing
				GameObject.Find("Villager Paths").SetActive (false);
				GameObject.Find("Horse Paths").SetActive (false);
				
				//Music
				GameObject.Find("Village (Calm) Music").SetActive(false);
			}
			if (AllEventList.returnStatus("villageInitial", 0)) {
				GameObject.FindWithTag ("ScreamingVillagers").SetActive (false);
				GameObject.FindWithTag ("Fire").SetActive (false);
				GameObject.FindWithTag ("Villager").SetActive (false);
				villageElder.SetActive (true);
				witch.SetActive (true);
				
				//Music
				GameObject.Find("Village (Attack) Music").SetActive(false);
		
				initialText.SetActive (false);
			}
			// if a quest is complete need to set grand father enable
			if (AllConditions.returnCondition("Get the eternal flame")) {
				grandFather.SetActive (true);
			}
		}


	}

	/*increase count on the village scene enemy to detect weather the initial event is finished or not
	 * Creator: Yan
	 */
	public void increaseCount(){
		//event complete when enemy count is 9
		if (enemycount == 9) {
			AllEventList.changeStatus("villageInitial", 0, true);
			GameObject.FindWithTag ("Villager").SetActive (false);
			GameObject.FindWithTag ("ScreamingVillagers").SetActive (false);
			GameObject.FindWithTag ("Fire").SetActive (false);
			villageElder.SetActive (true);
			witch.SetActive (true);
			initialText.SetActive (false);

		} else {
			enemycount += 1;
			//Debug.Log(enemycount);
		}
	
	}

	/*Set up the portal interface butons base on the staus in the all event
	 * Creator Yan
	 */
	public void SetUpPortal(int sceneNum){
		//Debug.Log ("setting up portal");
		if (portalPanel == null ) {
			return;
		}
		Button[] b = portalPanel.GetComponentsInChildren<Button> ();
		foreach (var item in b) {
			item.interactable = AllEventList.returnStatus (item.name, sceneNum);
		}
	}

	/*Load villaeg scene
	 */
	public void GoToRestPlace() {

		if (sceneNum == 0)
			return;
		GlobalControl.Instance.SaveBetweenScenes();
		GlobalControl.Instance.IsSceneBeingLoaded = false;
		GlobalControl.Instance.newSceneID = 0;
		SceneManager.LoadScene("LoadingSceneTest3", LoadSceneMode.Single);
	}

	/*Load hell scene
	 */
	public void GoToHell() {

		if (sceneNum == 4)
			return;
		GlobalControl.Instance.SaveBetweenScenes();
		GlobalControl.Instance.IsSceneBeingLoaded = false;
		GlobalControl.Instance.newSceneID = 4;
		SceneManager.LoadScene("LoadingSceneTest3", LoadSceneMode.Single);
	}

	/*Load level3
	 */
	public void GoToCaveOfPathavania() {

		if (sceneNum == 7)
			return;
		GlobalControl.Instance.SaveBetweenScenes();
		GlobalControl.Instance.IsSceneBeingLoaded = false;
		GlobalControl.Instance.newSceneID = 7;
		SceneManager.LoadScene("LoadingSceneTest3", LoadSceneMode.Single);
	}

	/*Load level4
	 */
	public void GoToSunsetPalace() {
		if (sceneNum == 1)
			return;
		GlobalControl.Instance.SaveBetweenScenes();
		GlobalControl.Instance.IsSceneBeingLoaded = false;
		GlobalControl.Instance.newSceneID = 1;
		SceneManager.LoadScene("LoadingSceneTest3", LoadSceneMode.Single);
	}
}
