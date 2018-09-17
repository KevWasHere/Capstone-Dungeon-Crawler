/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone
  # interactableEditor.cs
*-----------------------------------------------------------------------*/
/* 
* ongui reaction editor
*/
using UnityEditor;
/*Yan*/
[CustomEditor(typeof(OnGUIReaction))]
public class OnGUIReactionEditor : ReactionEditor {

	protected override string GetFoldoutLabel ()
	{
		return "On GUI Reaction";
	}
}
