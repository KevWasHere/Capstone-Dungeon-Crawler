using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # Stats.cs
  #
*-----------------------------------------------------------------------*/
/* Creator :  Yunzheng Zhou, Yang Zhang, Shane Weerasuriya
 * OnGUIcompletereaction: when player finish the dialog
 * or complete the quest, and interacte with NPC or enemy.
 * According to the condition, the player will get differet
 * treatment
 */
public class OnGUICompleteReaction : Reaction {
    /*
     * npc: THE NPC that player need to interacte with
     * conditionCollection: the list of conditionCollections
     * manager: the GUIManger singleton so all other script can
     * easily access to its value
     * Content: the content of quest 
     */
	public GameObject obj;
	public NPC npc;
	public ConditionCollection[] conditionCollections;
	public GUIManager manager = GUIManager.instance;
	public Content questList = Content.instance;

	protected override void ImmediateReaction ()
	{
		//guiManager.enabled = !guiManager.enabled;
		//Debug.Log (obj.GetComponents<NPC>());

		npc = obj.GetComponent<NPC>();
		conditionCollections = npc.conditionCollections;
		//panel.SetActive (true);
		//throw new System.NotImplementedException ();
		Delete(conditionCollections);
	}

    /*
     * Delete the condition from list of condition Collection
     */
	public void Delete(ConditionCollection[] conditionCollection)
	{
		//string s = "";
		for (int i = 0; i < conditionCollection.Length; i++) {
			//text.text = conditionCollection [i].description;

			if (conditionCollection[i].complete == true && conditionCollection[i].obtained == true) {
				string s = conditionCollection [i].description + "\n";
				Debug.Log ("delete        " + conditionCollection [i].description);
				questList.contentStrings.Remove (s);
				bool x = manager.content.Contains(conditionCollection[i].description);
				Debug.Log ("xxx" + x);

			}			 
		}
		manager.content = questList.Change();
	}

}
