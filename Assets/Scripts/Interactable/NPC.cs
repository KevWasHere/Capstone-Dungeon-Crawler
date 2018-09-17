/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # NPC.cs
  # 
!!!*-----------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * conditionCollections - 
 * defaultReactionCollection - 
*/
/*
Creator: Tianqi Xiao, Yan Zhang, Shane Weerasuriya
*/

public class NPC : Interactable {

	public ConditionCollection[] conditionCollections = new ConditionCollection[0];

	public ReactionCollection defaultReactionCollection;

	public GameObject dialogPanel;

	public Content questList = Content.instance;

	private bool check = false;
	public override void Interact()
	{

		base.Interact();
		if (check)
			return;
		base.Interact();
		check = true;
		StartCoroutine ("reset");

        if (this.GetComponent<NPCItem>() != null)
        {
            this.GetComponent<NPCItem>().setNPCItem(this.GetComponent<NPCItem>().ntype);
            Debug.Log("set item");
        }

		Debug.Log ("interact");
		if (dialogPanel != null ) {
			dialogPanel.SetActive (true);
			DialogArray da = GetComponent<DialogArray> ();            
			DialogPanel dp = dialogPanel.GetComponentInChildren<DialogPanel> ();
			if (dp != null) {
				dp.UpdateDialog (da.conversations);

			}
		}



		for (int i = 0; i < conditionCollections.Length; i++)
		{
			if (conditionCollections[i].CheckAndReact ()){
				Debug.Log ("not default");
				//return;
				conditionCollections[i].complete = true;
				QuestListUpdate ();
				return;
			}
			//conditionCollections[i].CheckAndReact ();
		}

		QuestListUpdate ();
		Debug.Log ("default");
		if (defaultReactionCollection != null) {
			defaultReactionCollection.React ();
		}
		//manager.content = questList.Change();
	}

	IEnumerator reset() {
		yield return new WaitForSeconds (2f);
		check = false;
	}

	void OnTriggerExit(Collider other){
		Debug.Log ("exit the collider!!!!!!!!!!!" + other.name);
		if (other.tag == "Player" && dialogPanel != null) {
			Debug.Log ("exit the collider");
			dialogPanel.SetActive (false);
		}
		//check = false;
	}

//	void OnTriggerEnter(Collider c){
//		Debug.Log ("enter the collider");
//	}

	public void QuestListUpdate(){
		//Debug.Log ("in quest list");
		for (int i = 0; i < conditionCollections.Length; i++) {
			if (conditionCollections[i].available == true && conditionCollections[i].complete == false && conditionCollections[i].obtained == false) {
				questList.AddTask (conditionCollections [i]);
				conditionCollections [i].obtained = true;
			}
			if (conditionCollections[i].complete == true && conditionCollections[i].obtained == true) {
				questList.DeleteTask (conditionCollections [i].description);
			}
		} 

	}

	public void SetAvailableTrue(string d){
		//Debug.Log ("dddddddddd " + d);
		foreach (var item in conditionCollections) {
			if (item.description == d) {
				item.available = true;
				return;
			}
		}
	}
}
