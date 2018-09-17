/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # SkillStats.cs
*-----------------------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * RageCost - stat representing rage cost of skill
 * Damage - stat representing damage of skill
 * Cooldown - stat representing cooldown of skill
 * LifeSteal - stat representing lifeseteal of skill
 * 
 * Creator: Myles Hagen, Kevin Ho
 */

public class SkillStats : MonoBehaviour {

    public Stat RageCost;
    public Stat Damage;
    public Stat Cooldown;
    public Stat Lifesteal;


	/*
	 * Function: ClearModifiers
	 * Description: clear all modifiers associated with the stats
	 * for the given skill
	 * 
	 */
    public virtual void ClearModifiers()
    {
        Damage.modifiers.Clear();
        //armor.modifiers.Clear();
        RageCost.modifiers.Clear();
        //AttackSpeed.modifiers.Clear();
        Cooldown.modifiers.Clear();
        Lifesteal.modifiers.Clear();
        //MagicResist.modifiers.Clear();
        //Health.modifiers.Clear();
    }
}
