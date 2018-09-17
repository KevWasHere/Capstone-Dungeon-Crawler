using UnityEditor;
/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone
  # interactableEditor.cs
*-----------------------------------------------------------------------*/
/* 
* default dialog reaction editor
*/
[CustomEditor(typeof(DefaultDialogReaction))]
public class DefaultDialogReactionEditor : ReactionEditor
{
	protected override string GetFoldoutLabel ()
	{
		return "DefaultDialogReaction Reaction";
	}
}