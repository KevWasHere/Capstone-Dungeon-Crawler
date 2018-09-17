/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone
  # interactableEditor.cs
*-----------------------------------------------------------------------*/
/* 
* behavior reaction reaction editor
*/
/*Yunzheng Zhou*/
using UnityEditor;

[CustomEditor(typeof(BehaviourReaction))]
public class BehaviourReactionEditor : ReactionEditor
{
    protected override string GetFoldoutLabel ()
    {
        return "Behaviour Reaction";
    }
}
