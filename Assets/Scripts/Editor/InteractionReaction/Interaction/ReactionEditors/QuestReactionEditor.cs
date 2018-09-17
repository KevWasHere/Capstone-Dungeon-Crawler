/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone
  # interactableEditor.cs
*-----------------------------------------------------------------------*/
/* 
* quest reaction editor
*/
using UnityEditor;

[CustomEditor(typeof(QuestReaction))]
public class QuestReactionEditor : ReactionEditor {

	protected override string GetFoldoutLabel ()
	{
		return "Quest Reaction";
	}
}
