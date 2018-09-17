using UnityEditor;
/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone
  # interactableEditor.cs
*-----------------------------------------------------------------------*/
/* 
* audio reaction editor
*/
/* Shane*/
[CustomEditor(typeof(AudioReaction))]
public class AudioReactionEditor : ReactionEditor
{
    protected override string GetFoldoutLabel ()
    {
        return "Audio Reaction";
    }
}
