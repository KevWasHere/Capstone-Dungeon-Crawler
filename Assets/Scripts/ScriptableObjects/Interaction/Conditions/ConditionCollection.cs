using UnityEngine;
using UnityEditor;
/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # 498 capstone
  #
*-----------------------------------------------------------------------*/
/*
 * Creator: Yan Zhang,SHane Weerasuriya
 * Conditioncollection it contains diffenret conditions
 * description: description of condition
 * obtained: boolean value to show is the condition obtained or not
 * available: boolean value show the condition is available or not
 * complete: boolean value to show the condition is complete or not
 * requireConditions: is a list of condition that are required for player
 * reactionCollection: the list of reaction 
 */

public class ConditionCollection : ScriptableObject
{
    public string description;
	public bool obtained;
	public bool available;
	public bool complete;
    public Condition[] requiredConditions = new Condition[0];
    public ReactionCollection reactionCollection;

    /*
     * check the condition accroding to 3 different boolean value
     * if the the condition is checked return true;
     */ 
    public bool CheckAndReact()
    {
		//check if the condition are satisfied
        for (int i = 0; i < requiredConditions.Length; i++)
        {
            if (!AllConditions.CheckCondition (requiredConditions[i]))
                return false;
        }

		//if the condition collection is not completed than react
		if (reactionCollection && complete == false) {
			reactionCollection.React ();
			return true;
		}

        return false;
    }
		
}
