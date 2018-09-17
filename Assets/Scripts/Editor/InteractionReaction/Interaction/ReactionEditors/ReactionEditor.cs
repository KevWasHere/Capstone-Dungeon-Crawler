using System;
using UnityEngine;
using UnityEditor;
/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone
  # interactableEditor.cs
*-----------------------------------------------------------------------*/
/* 
* reaction editor
* showReaction: the bool to see weather the reaction
* reactionsProperty: the reaction property
* reaction: the reaction target editor
*/
/*Tianqi Xiao, Yan Zhang*/
public abstract class ReactionEditor : Editor
{
    public bool showReaction;
    public SerializedProperty reactionsProperty;


    private Reaction reaction;


    private const float buttonWidth = 30f;

	/* enable the editor*/
    private void OnEnable ()
    {
        reaction = (Reaction)target;
        Init ();
    }


    protected virtual void Init () {}

	/* the inspector layout of the reaction editor*/
    public override void OnInspectorGUI ()
    {
        serializedObject.Update ();

        EditorGUILayout.BeginVertical (GUI.skin.box);
        EditorGUI.indentLevel++;

        EditorGUILayout.BeginHorizontal ();
        
        showReaction = EditorGUILayout.Foldout (showReaction, GetFoldoutLabel ());
        
        if (GUILayout.Button ("-", GUILayout.Width (buttonWidth)))
        {
            reactionsProperty.RemoveFromObjectArray (reaction);
        }
        EditorGUILayout.EndHorizontal ();
        
        if (showReaction)
        {
            DrawReaction ();
        }

        EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical ();

        serializedObject.ApplyModifiedProperties ();
    }

	/* editor to create reaction*/
    public static Reaction CreateReaction (Type reactionType)
    {
        return (Reaction)CreateInstance (reactionType);
    }

	/* draw the default editor*/
    protected virtual void DrawReaction ()
    {
        DrawDefaultInspector ();
    }


    protected abstract string GetFoldoutLabel ();
}
