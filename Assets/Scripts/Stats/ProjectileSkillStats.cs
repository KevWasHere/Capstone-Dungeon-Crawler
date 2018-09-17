/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hagen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # ProjectileSkillStats.cs
*-----------------------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * class representing the stats for projectile skill
 * 
 * Creator: Myles Hagen
 */

public class ProjectileSkillStats : SkillStats
{


    void Start()
    {
        EquipmentManager.instance.onGemChanged_SkillProjectile += OnGemChanged;
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
            //MagicResist.AddModifier(newItem.magicResistModifer);
            // Health.AddModifier(newItem.healthModifier);
        }

        if (oldItem != null)
        {
            //armor.RemoveModifier(oldItem.armorModifier);
            Damage.RemoveModifier(oldItem.damageModifier);
            RageCost.RemoveModifier(oldItem.rageModifier);
            //AttackSpeed.RemoveModifier(oldItem.attackspeedModifier);
            Cooldown.RemoveModifier(oldItem.cooldownModifier);
            Lifesteal.RemoveModifier(oldItem.lifestealModifier);
            //MagicResist.RemoveModifier(oldItem.magicResistModifer);
            //Health.RemoveModifier(oldItem.healthModifier);
        }
    }

	/*
	 * Funtion: ClearModifiers (overriden from SkillStats)
	 * Description: Clear the stat modifiers for the skill
	 */
    public override void ClearModifiers()
    {
        base.ClearModifiers();
    }
}
