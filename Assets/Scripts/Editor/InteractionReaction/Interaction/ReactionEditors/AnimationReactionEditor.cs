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
[CustomEditor(typeof(AnimationReaction))]
public class AnimationReactionEditor : ReactionEditor
{
    protected override string GetFoldoutLabel ()
    {
        return "Animation Reaction";
    }
}
