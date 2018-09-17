using UnityEditor;
/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone
  # interactableEditor.cs
*-----------------------------------------------------------------------*/
/* 
* dialog reaction editor
*/
/* Yann */
[CustomEditor(typeof(DialogReaction))]
public class DialogReactionEditor : ReactionEditor {

	protected override string GetFoldoutLabel ()
	{
		return "Dialog Reaction";
	}
}
