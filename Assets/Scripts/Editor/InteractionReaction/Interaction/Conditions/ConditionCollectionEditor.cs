using UnityEngine;
using UnityEditor;
/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # 498 capstone
  # Stats.cs
  #
*-----------------------------------------------------------------------*/
/*
* Creator: Yan Zhang, Tianqi Xiao
* collectionsProperty: the property of the collections
* conditionCollection: the condition collection
* descriptionProperty: the description property
* reactionCollectionProperty:  the reactionCollectionProperty property
* obtainedProperty: the obtainedProperty property
* availableProperty: the availableProperty property
* completeProperty: the completeProperty property
* conditionButtonWidth: width of the condition Button
* collectionButtonWidth: width of collection Button
* conditionCollectionPropObtainedName: the name of conllection property
* conditionCollectionPropDescriptionName: the label of the condition collection propety
* conditionCollectionPropRequiredConditionsName: the name of the required condition
* conditionCollectionPropReactionCollectionName: the label for reaction Collection
* conditionCollectionPropAvailableName: label for availabel bool
* conditionCollectionPropCompleteName: label of rhe complete bool
*/
[CustomEditor(typeof(ConditionCollection))]
public class ConditionCollectionEditor : EditorWithSubEditors<ConditionEditor, Condition>
{
    public SerializedProperty collectionsProperty;
    private ConditionCollection conditionCollection;
    private SerializedProperty descriptionProperty;
    private SerializedProperty conditionsProperty;
    private SerializedProperty reactionCollectionProperty;
	private SerializedProperty obtainedProperty;
	private SerializedProperty availableProperty;
	private SerializedProperty completeProperty;

    private const float conditionButtonWidth = 30f;
    private const float collectionButtonWidth = 125f;
	private const string conditionCollectionPropObtainedName = "obtained";
    private const string conditionCollectionPropDescriptionName = "description";
    private const string conditionCollectionPropRequiredConditionsName = "requiredConditions";
    private const string conditionCollectionPropReactionCollectionName = "reactionCollection";
	private const string conditionCollectionPropAvailableName = "available";
	private const string conditionCollectionPropCompleteName = "complete";


    private void OnEnable ()
    {
        conditionCollection = (ConditionCollection)target;

        if (target == null)
        {
            DestroyImmediate (this);
            return;
        }

		availableProperty = serializedObject.FindProperty (conditionCollectionPropAvailableName);
        descriptionProperty = serializedObject.FindProperty(conditionCollectionPropDescriptionName);
		obtainedProperty = serializedObject.FindProperty (conditionCollectionPropObtainedName);
        conditionsProperty = serializedObject.FindProperty(conditionCollectionPropRequiredConditionsName);
        reactionCollectionProperty = serializedObject.FindProperty(conditionCollectionPropReactionCollectionName);
		completeProperty = serializedObject.FindProperty (conditionCollectionPropCompleteName);

        CheckAndCreateSubEditors (conditionCollection.requiredConditions);
    }


    private void OnDisable ()
    {
        CleanupEditors ();
    }


    protected override void SubEditorSetup (ConditionEditor editor)
    {
        editor.editorType = ConditionEditor.EditorType.ConditionCollection;
        editor.conditionsProperty = conditionsProperty;
    }


    public override void OnInspectorGUI ()
    {
        serializedObject.Update ();

		float width = EditorGUIUtility.currentViewWidth / 3f;

        CheckAndCreateSubEditors(conditionCollection.requiredConditions);
        
        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUI.indentLevel++;

        EditorGUILayout.BeginHorizontal();

        descriptionProperty.isExpanded = EditorGUILayout.Foldout(descriptionProperty.isExpanded, descriptionProperty.stringValue);

		//EditorGUILayout.PropertyField(obtainedProperty, GUIContent.none, GUILayout.Width(width + 30f));

        if (GUILayout.Button("Remove Collection", GUILayout.Width(collectionButtonWidth)))
        {
            collectionsProperty.RemoveFromObjectArray (conditionCollection);
        }

        EditorGUILayout.EndHorizontal();

		//EditorGUILayout.PropertyField (availableProperty);
		//EditorGUILayout.PropertyField(obtainedProperty, GUIContent.none, GUILayout.Width(width + 30f));
        if (descriptionProperty.isExpanded)
        {
            ExpandedGUI ();
        }
        
        EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }


    private void ExpandedGUI ()
    {
        EditorGUILayout.Space();
		EditorGUILayout.PropertyField (obtainedProperty);
		EditorGUILayout.Space ();
		EditorGUILayout.PropertyField (availableProperty);
		EditorGUILayout.Space ();
		EditorGUILayout.PropertyField (completeProperty);

		EditorGUILayout.Space ();
        EditorGUILayout.PropertyField(descriptionProperty);

        EditorGUILayout.Space();

        float space = EditorGUIUtility.currentViewWidth / 3f;

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Condition", GUILayout.Width(space));
        EditorGUILayout.LabelField("Satisfied?", GUILayout.Width(space));
        EditorGUILayout.LabelField("Add/Remove", GUILayout.Width(space));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginVertical(GUI.skin.box);
        for (int i = 0; i < subEditors.Length; i++)
        {
            subEditors[i].OnInspectorGUI();
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace ();
        if (GUILayout.Button("+", GUILayout.Width(conditionButtonWidth)))
        {
            Condition newCondition = ConditionEditor.CreateCondition();
            conditionsProperty.AddToObjectArray(newCondition);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(reactionCollectionProperty);
    }


    public static ConditionCollection CreateConditionCollection()
    {
        ConditionCollection newConditionCollection = CreateInstance<ConditionCollection>();
        newConditionCollection.description = "New condition collection";
        newConditionCollection.requiredConditions = new Condition[1];
        newConditionCollection.requiredConditions[0] = ConditionEditor.CreateCondition();
        return newConditionCollection;
    }
}
