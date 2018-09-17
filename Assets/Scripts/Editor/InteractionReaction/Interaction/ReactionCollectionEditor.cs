using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone
  # ReactionCollection.cs
*-----------------------------------------------------------------------*/
/* Yan Zhang
 * reactionCollection: the reaction collection need to display in the editor
 * reactionProperty: the properties of the reaction 
 * reactionTypes: the Type array of different type of reactions
 * reactionTypeName: the name of the type of reaction
 * selectedIndex: the select index 
 * dropAreaHeight: the hight of the drop down box
 * controlSpacing: the control spacing between each of the selected controls
 * reactionPropName: the property name of the reaction
 * verticalSpacing: the verticle spacing in the editor
 */
[CustomEditor(typeof(ReactionCollection))]
public class ReactionCollectionEditor : EditorWithSubEditors<ReactionEditor, Reaction>
{
    private ReactionCollection reactionCollection;
    private SerializedProperty reactionsProperty;

    private Type[] reactionTypes;
    private string[] reactionTypeNames;
    private int selectedIndex;


    private const float dropAreaHeight = 50f;
    private const float controlSpacing = 5f;
    private const string reactionsPropName = "reactions";


    private readonly float verticalSpacing = EditorGUIUtility.standardVerticalSpacing;

	/* Yan
	 * On enable the editor
	 * check the reaction type and create the sub editor for each reaction type
	 */
    private void OnEnable ()
    {
        reactionCollection = (ReactionCollection)target;

        reactionsProperty = serializedObject.FindProperty(reactionsPropName);

        CheckAndCreateSubEditors (reactionCollection.reactions);

        SetReactionNamesArray ();
    }

	/* Yan
	 * clear editor when diabled
	 */
    private void OnDisable ()
    {
        CleanupEditors ();
    }

	/* Yan
	 * set up sub editor
	 */
    protected override void SubEditorSetup (ReactionEditor editor)
    {
        editor.reactionsProperty = reactionsProperty;
    }

	/* 
	 * Yan
	 * Gothrough the subeditors and set up the property name and space 
	 */
    public override void OnInspectorGUI ()
    {
        serializedObject.Update ();

        CheckAndCreateSubEditors(reactionCollection.reactions);

        for (int i = 0; i < subEditors.Length; i++)
        {
            subEditors[i].OnInspectorGUI ();
        }

        if (reactionCollection.reactions.Length > 0)
        {
            EditorGUILayout.Space();
            EditorGUILayout.Space ();
        }

        Rect fullWidthRect = GUILayoutUtility.GetRect(GUIContent.none, GUIStyle.none, GUILayout.Height(dropAreaHeight + verticalSpacing));

        Rect leftAreaRect = fullWidthRect;
        leftAreaRect.y += verticalSpacing * 0.5f;
        leftAreaRect.width *= 0.5f;
        leftAreaRect.width -= controlSpacing * 0.5f;
        leftAreaRect.height = dropAreaHeight;

        Rect rightAreaRect = leftAreaRect;
        rightAreaRect.x += rightAreaRect.width + controlSpacing;

        TypeSelectionGUI (leftAreaRect);
        DragAndDropAreaGUI (rightAreaRect);

        DraggingAndDropping(rightAreaRect, this);

        serializedObject.ApplyModifiedProperties ();
    }

	/* put the label and stuff in the select gui rectagle
	 */
    private void TypeSelectionGUI (Rect containingRect)
    {
        Rect topHalf = containingRect;
        topHalf.height *= 0.5f;
        
        Rect bottomHalf = topHalf;
        bottomHalf.y += bottomHalf.height;

        selectedIndex = EditorGUI.Popup(topHalf, selectedIndex, reactionTypeNames);

        if (GUI.Button (bottomHalf, "Add Selected Reaction"))
        {
            Type reactionType = reactionTypes[selectedIndex];
            Reaction newReaction = ReactionEditor.CreateReaction (reactionType);
            reactionsProperty.AddToObjectArray (newReaction);
        }
    }

	//draw a box on the gui and give a text area
    private static void DragAndDropAreaGUI (Rect containingRect)
    {
        GUIStyle centredStyle = GUI.skin.box;
        centredStyle.alignment = TextAnchor.MiddleCenter;
        centredStyle.normal.textColor = GUI.skin.button.normal.textColor;

        GUI.Box (containingRect, "Drop new Reactions here", centredStyle);
    }

	/*Drag and dropping the reaction collections in the corresbounding reactioncollection space in the codition collection editor
	 */
    private static void DraggingAndDropping (Rect dropArea, ReactionCollectionEditor editor)
    {
        Event currentEvent = Event.current;

        if (!dropArea.Contains (currentEvent.mousePosition))
            return;

        switch (currentEvent.type)
        {
            case EventType.DragUpdated:

                DragAndDrop.visualMode = IsDragValid () ? DragAndDropVisualMode.Link : DragAndDropVisualMode.Rejected;
                currentEvent.Use ();

                break;
			//mouse is relesed
            case EventType.DragPerform:
                
                DragAndDrop.AcceptDrag();
                //loop thrpough all the object that is draged
                for (int i = 0; i < DragAndDrop.objectReferences.Length; i++)
                {
                    MonoScript script = DragAndDrop.objectReferences[i] as MonoScript;
					//find the type
                    Type reactionType = script.GetClass();
					//create a reaction of that type 
                    Reaction newReaction = ReactionEditor.CreateReaction (reactionType);
                    editor.reactionsProperty.AddToObjectArray (newReaction);
                }

                currentEvent.Use();

                break;
        }
    }

	//detect is the object that draged is valid
    private static bool IsDragValid ()
    {
		//loop through the object is dragged that make sure it is an reaction
		for (int i = 0; i < DragAndDrop.objectReferences.Length; i++)
		{
			//first must be monoscript otherwise invalid
			if (DragAndDrop.objectReferences [i].GetType () != typeof(MonoScript)) {
				return false;
			}

			//determine is it a reaction
			MonoScript script = DragAndDrop.objectReferences[i] as MonoScript;
			//find the type of the script
			Type scriptType = script.GetClass ();
			//check the typr is not reaction return false
			if (!scriptType.IsSubclassOf (typeof(Reaction))) {
				return false;
			}
			//if the type is abstract alse return false
			if (scriptType.IsAbstract) {
				return false;
			}
		}
		return true;
    }


    private void SetReactionNamesArray ()
    {
        Type reactionType = typeof(Reaction);

        Type[] allTypes = reactionType.Assembly.GetTypes();

        List<Type> reactionSubTypeList = new List<Type>();

        for (int i = 0; i < allTypes.Length; i++)
        {
            if (allTypes[i].IsSubclassOf(reactionType) && !allTypes[i].IsAbstract)
            {
                reactionSubTypeList.Add(allTypes[i]);
            }
        }

        reactionTypes = reactionSubTypeList.ToArray();

        List<string> reactionTypeNameList = new List<string>();

        for (int i = 0; i < reactionTypes.Length; i++)
        {
            reactionTypeNameList.Add(reactionTypes[i].Name);
        }

        reactionTypeNames = reactionTypeNameList.ToArray();
    }
}
