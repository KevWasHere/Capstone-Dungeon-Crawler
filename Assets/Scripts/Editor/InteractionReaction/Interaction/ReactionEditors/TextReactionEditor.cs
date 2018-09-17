using UnityEditor;
using UnityEngine;
/* Yan Zhang
 * Serialized Property variables: properties of the text reaction type
 * const String variables: the name of each property fields
 * const float variables: the size of the GUI layout 
 */
[CustomEditor(typeof(TextReaction))]
public class TextReactionEditor : ReactionEditor
{
	private SerializedProperty messageProperty;
	private SerializedProperty textColorProperty;
	private SerializedProperty delayProperty;

	private const float messageGUILines = 3f;
	private const float areaWidthOffset = 19f;

	private const string textReactionPropMessageName = "message";
	private const string textReactionPropTextColorName = "textColor";
	private const string textReactionPropDelayName = "delay";

	//initial the properties to be the name of the fields
	protected override void Init ()
	{		
		messageProperty = serializedObject.FindProperty (textReactionPropMessageName);
		textColorProperty = serializedObject.FindProperty (textReactionPropTextColorName);
		delayProperty = serializedObject.FindProperty (textReactionPropDelayName);
	}

	//draw the reaction
	protected override void DrawReaction ()
	{
		//draw horizontal layout
		EditorGUILayout.BeginHorizontal ();
		//create label field(size is the usual size minus the layout)
		EditorGUILayout.LabelField ("Message", GUILayout.Width (EditorGUIUtility.labelWidth - areaWidthOffset));
		messageProperty.stringValue = EditorGUILayout.TextArea (messageProperty.stringValue, GUILayout.Height (EditorGUIUtility.singleLineHeight * messageGUILines));
		EditorGUILayout.EndHorizontal ();
		//outside the horizontal display other properties
		EditorGUILayout.PropertyField (textColorProperty);
		EditorGUILayout.PropertyField (delayProperty);
	}

	//give the label of the fold out
	protected override string GetFoldoutLabel ()
	{
		return "Text Reaction";
	}
}