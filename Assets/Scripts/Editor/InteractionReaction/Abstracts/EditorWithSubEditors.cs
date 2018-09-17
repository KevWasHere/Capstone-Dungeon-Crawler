using UnityEngine;
using UnityEditor;
/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 330 Final Project
  # TextManager.cs
*-----------------------------------------------------------------------*/
/* Class for create costomize editor with a sub contomize editor inside
 * TEditor: is the parent editor, 
* TTarget: the sub editor in the parent editor(need to be unity object)
 */
/* Author: Yan Zhang*/
public abstract class EditorWithSubEditors<TEditor, TTarget> : Editor
    where TEditor : Editor
    where TTarget : Object
{
	/* SubEditor: Array of subeditors
	 */
    protected TEditor[] subEditors;

	/* Function: check and create subeditor
	 * SubEditorTarget: array of the sub editor targrt
	 */
    protected void CheckAndCreateSubEditors (TTarget[] subEditorTargets)
    {
		//if the subeditor is not null and have right number, return
        if (subEditors != null && subEditors.Length == subEditorTargets.Length)
            return;
		//clean up old editor
        CleanupEditors ();
		//create new array of editors
        subEditors = new TEditor[subEditorTargets.Length];
		//go throught the array and create that sub editor
        for (int i = 0; i < subEditors.Length; i++)
        {
            subEditors[i] = CreateEditor (subEditorTargets[i]) as TEditor;
            SubEditorSetup (subEditors[i]);
        }
    }

	/* Function: CleanupEditors
	 * Description: clean up all the subeditor, destroy
	 */
    protected void CleanupEditors ()
    {
        if (subEditors == null)
            return;

        for (int i = 0; i < subEditors.Length; i++)
        {
            DestroyImmediate (subEditors[i]);
        }

        subEditors = null;
    }

	/* Function: SubEditorSetup
	 * Description: need to create in the inheritence
	 */
    protected abstract void SubEditorSetup (TEditor editor);
}
