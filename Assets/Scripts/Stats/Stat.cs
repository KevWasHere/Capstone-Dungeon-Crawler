using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # Stats.cs
  #
*-----------------------------------------------------------------------*/

/*
* baseValue: base value of stat
* modifiers: list of modifiers for a stat 
* 
*/

/*
 Creator: Kevin , Shane
 */

[Serializable]
public class Stat {
	
	[SerializeField]
	public int baseValue;
	public List<int> modifiers = new List<int>();

	public Stat (int bv, List<int> mf){
		baseValue = bv;
		modifiers = mf;
	}

	/*
	 * function: GetValue
	 * returns: integer value of total modifers
	 * Description: iterates through modifier array and returns the total value of all modifiers
	 */
	public int GetValue ()
	{
		int finalValue = baseValue;
		modifiers.ForEach(x => finalValue += x);
		return finalValue;
	}

	/*
	 * Function: AddModifier
	 * Parameter: integer value for modifier
	 * Description: add a new modifier to the modifier list
	 */
	public void AddModifier (int modifier)
	{
		if (modifier != 0)
			modifiers.Add(modifier);
	}

	/*
	 * Function: RemoveModifier
	 * Parameter: integer value for modifier
	 * Description: Remove a modifier to the modifier list
	 */
	public void RemoveModifier (int modifier)
	{
		modifiers.Remove(modifier);
	}
		
}
