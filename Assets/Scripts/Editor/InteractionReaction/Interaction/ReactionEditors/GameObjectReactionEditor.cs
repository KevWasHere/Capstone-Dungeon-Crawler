using UnityEditor;
/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone
  # interactableEditor.cs
*-----------------------------------------------------------------------*/
/* 
* game object reaction editor
*/
/*Tianqi Xiao*/
[CustomEditor(typeof(GameObjectReaction))]
public class GameObjectReactionEditor : ReactionEditor
{
    protected override string GetFoldoutLabel ()
    {
        return "GameObject Reaction";
    }
}
