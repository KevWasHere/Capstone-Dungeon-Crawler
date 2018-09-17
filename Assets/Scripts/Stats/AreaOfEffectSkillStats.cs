/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hagen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # AreaOfEffectSkillStats.cs
*-----------------------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Radius - stat representing radius of skill
 * LastTime - stats representing hit rate of skill
 * 
 * Creator: Myles Hagen, Shane Weerasuriya
 */

public class AreaOfEffectSkillStats : SkillStats
{
    public Stat Radius;
    public Stat LastTime;
    

	// subscribe to gem changed event in equipmentmanager
    void Start()
    {
        EquipmentManager.instance.onGemChanged_SkillAOE += OnGemChanged;
    }

	/*
	 * Function: OnGemChanged
	 * Parameters: newItem, oldItem
	 * Description: when a new gem is equiped, the stats for the given skill are updated
	 * and the stats for the old gem are removed if one was equiped.
	 */
    public void OnGemChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {

            Damage.AddModifier(newItem.damageModifier);
            //armor.AddModifier(newItem.armorModifier);
            RageCost.AddModifier(newItem.rageModifier);
            //AttackSpeed.AddModifier(newItem.attackspeedModifier);
            Cooldown.AddModifier(newItem.cooldownModifier);
            Lifesteal.AddModifier(newItem.lifestealModifier);
            Radius.AddModifier(newItem.radiusModifier);
            LastTime.AddModifier(newItem.lastTimeModifier);
            
        }

        if (oldItem != null)
        {
            //armor.RemoveModifier(oldItem.armorModifier);
            Damage.RemoveModifier(oldItem.damageModifier);
            RageCost.RemoveModifier(oldItem.rageModifier);
            //AttackSpeed.RemoveModifier(oldItem.attackspeedModifier);
            Cooldown.RemoveModifier(oldItem.cooldownModifier);
            Lifesteal.RemoveModifier(oldItem.lifestealModifier);
            Radius.RemoveModifier(oldItem.radiusModifier);
            LastTime.RemoveModifier(oldItem.lastTimeModifier);
        }
    }

	/*
	 * Funtion: ClearModifiers (overriden from SkillStats)
	 * Description: Clear the stat modifiers for the skill
	 */
    public override void ClearModifiers()
    {
        base.ClearModifiers();
        Radius.modifiers.Clear();
        LastTime.modifiers.Clear();
    }
}
