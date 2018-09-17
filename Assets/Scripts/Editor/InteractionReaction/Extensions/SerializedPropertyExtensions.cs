/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 330 Final Project
  # SerializedPropertyExtensions.cs
*-----------------------------------------------------------------------*/

using UnityEngine;
using UnityEditor;
/* Serializable property Extensions
 * extension method: add/delete to a class array that is already compiled
 * Author: Yan Zhang, YunZheng Zhou
 */
public static class SerializedPropertyExtensions
{
	/* Function: AddToObjectArray
	 * Description: add an element to a serialized property array
	 * arrayProperty: serializable array that should be call the function from
	 * elementToAdd: the element that need to be add
	 */
    public static void AddToObjectArray<T> (this SerializedProperty arrayProperty, T elementToAdd)
        where T : Object
    {
		//if the propery is not an array, throw exception
        if (!arrayProperty.isArray)
            throw new UnityException("SerializedProperty " + arrayProperty.name + " is not an array.");
		//make sure all the information of the property is up to date
        arrayProperty.serializedObject.Update();
		//insert the element at the last position of the array
        arrayProperty.InsertArrayElementAtIndex(arrayProperty.arraySize);
        arrayProperty.GetArrayElementAtIndex(arrayProperty.arraySize - 1).objectReferenceValue = elementToAdd;
		//apply the modify to the property
        arrayProperty.serializedObject.ApplyModifiedProperties();
    }

	/* Function: RemoveFromObjectArrayAt
	 * Description: remmove the i th element in ther property array
	 * arrayProperty: serializable array that should be call the function from
	 * index: the index of the element that need to be removed
	 */
    public static void RemoveFromObjectArrayAt (this SerializedProperty arrayProperty, int index)
    {
		//if index is less than 0, throw exception
        if(index < 0)
            throw new UnityException("SerializedProperty " + arrayProperty.name + " cannot have negative elements removed.");
		//if the property is not an array through the exception
        if (!arrayProperty.isArray)
            throw new UnityException("SerializedProperty " + arrayProperty.name + " is not an array.");
		//if the index is greater than the property, through exception
        if(index > arrayProperty.arraySize - 1)
            throw new UnityException("SerializedProperty " + arrayProperty.name + " has only " + arrayProperty.arraySize + " elements so element " + index + " cannot be removed.");
		//update the property
        arrayProperty.serializedObject.Update();
		//delete the element 
        if (arrayProperty.GetArrayElementAtIndex(index).objectReferenceValue)
            arrayProperty.DeleteArrayElementAtIndex(index);
        arrayProperty.DeleteArrayElementAtIndex(index);
        arrayProperty.serializedObject.ApplyModifiedProperties();
    }

	/* Function: remove a element from a serialized array
	 * arrayProperty: serializable array that should be call the function from
	 * elementToRemove: Elemet that can be removed from the array need to be an object
	 */
    public static void RemoveFromObjectArray<T> (this SerializedProperty arrayProperty, T elementToRemove)
        where T : Object
    {
		//if the property is not an array through exception
        if (!arrayProperty.isArray)
            throw new UnityException("SerializedProperty " + arrayProperty.name + " is not an array.");
		//if there is no element to remove, through an exception
        if(!elementToRemove)
            throw new UnityException("Removing a null element is not supported using this method.");
		//make sure all the information of the property is up to date
        arrayProperty.serializedObject.Update();
		//loop through the array, find each seriazed property element and remove the element
        for (int i = 0; i < arrayProperty.arraySize; i++)
        {
            SerializedProperty elementProperty = arrayProperty.GetArrayElementAtIndex(i);

            if (elementProperty.objectReferenceValue == elementToRemove)
            {
                arrayProperty.RemoveFromObjectArrayAt (i);
                return;
            }
        }
		// throw exception if no elemetn is found
        throw new UnityException("Element " + elementToRemove.name + "was not found in property " + arrayProperty.name);
    }
}
