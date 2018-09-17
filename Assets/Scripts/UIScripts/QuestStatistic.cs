using System.Collections;
using System.Collections.Generic;
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
Creator: Yan Zhang
QuestStatics: the dictionary of static of quest
it is needed for the save and load game
*/

[Serializable]
public class QuestStatistic {
	public Dictionary <int, List<QuestStatus>> questStatistic;

}