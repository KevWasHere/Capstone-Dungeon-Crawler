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
 * Reference to all single condition
 * it contain description of condition
 * boolean value to show if it is satisfied
 * hash: the hash for whole block of condition
 */
public class Condition : ScriptableObject
{
    public string description;
    public bool satisfied;
    public int hash;
}
