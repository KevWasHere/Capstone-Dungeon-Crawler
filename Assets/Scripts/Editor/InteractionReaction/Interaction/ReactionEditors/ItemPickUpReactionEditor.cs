/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone
  # interactableEditor.cs
*-----------------------------------------------------------------------*/
/* 
* item pick up reaction editor
*/
using UnityEditor;
[CustomEditor(typeof(ItemPickUpReaction))]
public class ItemPickUpReactionEditor : ReactionEditor {

	// Use this for initialization
	protected override string GetFoldoutLabel ()
	{
		return "ItemPickUp Reaction";
	}
}
