using UnityEditor;
/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone
  # interactableEditor.cs
*-----------------------------------------------------------------------*/
/* 
* animation reaction editor
*/
[CustomEditor(typeof(OnGUICompleteReaction))]
public class OnGUICompleteReactionEditor : ReactionEditor {

	protected override string GetFoldoutLabel ()
	{
		return "On GUI Complete Reaction";
	}
}
