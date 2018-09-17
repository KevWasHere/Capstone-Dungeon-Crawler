/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone
  # interactableEditor.cs
*-----------------------------------------------------------------------*/
/* 
* item submit reaction editor
*/
using UnityEditor;
[CustomEditor(typeof(ItemSubmitReaction))]
public class ItemSubmitReactionEditor : ReactionEditor {

	protected override string GetFoldoutLabel ()
	{
		return "ItemSubmit Reaction";
	}
}
