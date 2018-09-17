using UnityEngine;
/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # 498 capstone
  #
*-----------------------------------------------------------------------*/
/*
* Creator: Yan Zhang
* reset the resetable scriptable object
* resettableScriptableObject: the list of resettable scriptable object that need to be reset
*/
public class DataResetter : MonoBehaviour
{
    public ResettableScriptableObject[] resettableScriptableObjects;


	private void Awake ()
    {
	    for (int i = 0; i < resettableScriptableObjects.Length; i++)
	    {
	        resettableScriptableObjects[i].Reset ();
	    }
	}
}
