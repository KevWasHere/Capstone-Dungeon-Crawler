using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Capstone
  # NPCList.cs
*-----------------------------------------------------------------------*/

/*Creator: Yan Zhang, Kevin Ho, SHane Weerasuriya
* NPCList is the list of npc's tag name in each scene
* sceneNum: the scene number of this list
* npcList: list of string that is the names of npcs
*/
[System.Serializable]
public class NPCList {

	public int sceneNum;
	public List<string> npcList;
}
