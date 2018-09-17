using UnityEditor;
/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone
  # interactableEditor.cs
*-----------------------------------------------------------------------*/
/* 
* condition Collection reaction editor
*/
[CustomEditor(typeof(ConditionCollectionReaction))]
public class ConditionCollectionReactionEditor : ReactionEditor
{
	protected override string GetFoldoutLabel ()
	{
		return "ConditionCollection Reaction";
	}
}

