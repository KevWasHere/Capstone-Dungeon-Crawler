using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone
  # CharMenuControl.cs
  # Dialogue
*-----------------------------------------------------------------------*/
/*
Creator: Yan Zhang, Shane Weerasuriya, Myles Hagen
QuestStatus: the quest status of a npc
npcName: the name of the npc that this quest status belongs to
questBool: the list of quest bool for this npc
dialogBool: the list of dialog bool of this npc
*/
[Serializable]
public class QuestStatus {
	public string npcName;
	public List<QuestBool> questBool;
	public List <bool> dialogBool;

	/*questBool constructor
	 * name: the name of npc
	 * qb: the questbool 
	 * db: the dialogbool
	 */
	public QuestStatus(string Name, List<QuestBool> qb, List<bool> db){
		npcName = Name;
		questBool = qb;
		dialogBool = db;
	}

	/*QuestBool (condition collection)
	 * description: the description of the quest
	 * obtained: the obtained of the quest
	 * available: is the quest availble
	 * complete: is the quest completed
	 * Creator: Yan Zhang, Shane Weerasuriya, Myles Hagen
	 */
	[Serializable]
	public class QuestBool {
		public string description;
		public bool obtained;
		public bool available;
		public bool complete;

		//Questbool constructor
		public QuestBool (string d, bool o, bool a, bool c){
			description = d;
			obtained = o;
			available = a;
			complete = c;
		}

	}
}


