using UnityEngine;
using UnityEditor;
/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone
  # interactableEditor.cs
*-----------------------------------------------------------------------*/

/* Yan Zhang, Shane*/

/* interactable the type of the editor
 * conllectionProperty: the property of conditioncollection
 * defaultReactionCollectionProperty: the refault reaction property
 * dialogPanelProperty: the property of dialog panel
 * questListProperty: the property of quest list
 * collectionButtonWidth: the width of the collection button
 * interactablePropConditionCollectionsName: the name of the property in conditioncollect 
 * interactablePropDefaultReactionCollectionName: the name of the default collection variable in condition collection
 * dialogPanelPropertyName: the property name of the dialog panel
 * questListPropertyName: the quest list of the property name
 */
[CustomEditor(typeof(NPC))]
public class InteractableEditor : EditorWithSubEditors<ConditionCollectionEditor, ConditionCollection>
{
	private NPC interactable;
    //private SerializedProperty interactionLocationProperty;
    private SerializedProperty collectionsProperty;
    private SerializedProperty defaultReactionCollectionProperty;

	private SerializedProperty dialogPanelProperty;
	private SerializedProperty questListProperty;

    private const float collectionButtonWidth = 125f;
    //private const string interactablePropInteractionLocationName = "interactionLocation";
    private const string interactablePropConditionCollectionsName = "conditionCollections";
    private const string interactablePropDefaultReactionCollectionName = "defaultReactionCollection";


	private const string dialogPanelPropertyName = "dialogPanel";
	private const string questListPropertyName = "questList";

	/* on editor enable
	 */
    private void OnEnable ()
	{
		interactable = (NPC)target;

		OnEnableHandler (interactable);
	}

	/* the handler that will enable the editor and sub editor
	 */
	protected void OnEnableHandler (NPC interactable) {
        collectionsProperty = serializedObject.FindProperty(interactablePropConditionCollectionsName);
        //interactionLocationProperty = serializedObject.FindProperty(interactablePropInteractionLocationName);
        defaultReactionCollectionProperty = serializedObject.FindProperty(interactablePropDefaultReactionCollectionName);
        
		dialogPanelProperty = serializedObject.FindProperty (dialogPanelPropertyName);
		questListProperty = serializedObject.FindProperty (questListPropertyName);

		CheckAndCreateSubEditors(interactable.conditionCollections);


    }

	/*clear editor when disable
	 */
    private void OnDisable ()
    {
        CleanupEditors ();
    }

	/* set up the sub editor
	 */
    protected override void SubEditorSetup(ConditionCollectionEditor editor)
    {
        editor.collectionsProperty = collectionsProperty;
    }

	/* the layout on the inspector
	 * Yan
	 */
    public override void OnInspectorGUI ()
    {
        serializedObject.Update ();
        
		CheckAndCreateSubEditors(interactable.conditionCollections);
        
        //EditorGUILayout.PropertyField (interactionLocationProperty);

        for (int i = 0; i < subEditors.Length; i++)
        {
            subEditors[i].OnInspectorGUI ();
            EditorGUILayout.Space ();
        }

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace ();
        if (GUILayout.Button("Add Collection", GUILayout.Width(collectionButtonWidth)))
        {
            ConditionCollection newCollection = ConditionCollectionEditor.CreateConditionCollection ();
            collectionsProperty.AddToObjectArray (newCollection);
        }
        EditorGUILayout.EndHorizontal ();

        EditorGUILayout.Space ();

        EditorGUILayout.PropertyField (defaultReactionCollectionProperty);

		EditorGUILayout.Space ();
		EditorGUILayout.PropertyField (dialogPanelProperty);

		EditorGUILayout.Space ();
		EditorGUILayout.PropertyField (questListProperty);

        serializedObject.ApplyModifiedProperties ();
    }
}
