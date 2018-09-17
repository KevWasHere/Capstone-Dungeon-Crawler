using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hagen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # PlayerStats.cs
*-----------------------------------------------------------------------*/

/*
 * Creator: Myles, Yan, Shane 
 * 
 * Rage: stat that represents player resource (similar to mana)
 * AttackSpeed: stat that represents the attack speed of the players basic attack
 * Cooldown: stats for cooldown of abilites
 * Lifesteal: stat for life to be stolen on hit
 * MagicResist: stat for resisting magic damage
 * targetExperience: target experience to reach next level
 * currentExperience: current experience number
 * level: plays current level (based on experience gained)
 * currentRage: current amount of rage 
 * maxRage: maximum amount of rage
 * gold: current amount of gold
 * levelUpEffect: effect to be played when player levels up
 * levelUp: bool controlling when to play level up effect
 */

public class PlayerStats : CharacterStats {

    public Stat Rage;
    public Stat AttackSpeed;
    public Stat Cooldown;
    public Stat Lifesteal;
    public Stat MagicResist;
    public Stat Health;
    public int targetExperience;
    public int currentExperience;
    public int level = 1;
    public int currentRage;
    public int maxRage;
	public int gold;
	public GameObject levelUpEffect;
    private bool levelUp;
	public event System.Action OnDeath;


    // initiliaize health and rage
    public override void Awake()
    {
        base.Awake();
        currentRage = maxRage;
    }

    // subsrcribe to equipment change event, initialize maximum health and maximum rage.
    void Start () 
	{
		Debug.Log ("target exp" + targetExperience);
		EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
        maxHealth = 100 + Health.GetValue();
        maxRage = 100 + Rage.GetValue();//remove 100
        levelUp = false;
	}

    /*
	 * Function: TakeDamage   (overriden from CharacterStats)
	 * Parameters: damage
	 * Description: health reduced by amount of damage taken (reduced by fraction of armor)
     * player dies when health reaches zero.
     * 
	 */
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (currentHealth <= 0)
		{
            if (OnDeath != null)
            {
                OnDeath();
            }
            Die();
        }
    }

    /*
	 * Function: OnEquipmentChanged
	 * Parameters: new equipment item, old equipment item
	 * Description: add modifiers of various stats from gear equiped by the player.
     * makes sure current health and rage cannot be higher than the maximums.
     * Creator: Myles Hagen
	 */
    public void OnEquipmentChanged (Equipment newItem, Equipment oldItem)
	{
		if (newItem != null) {
			damage.AddModifier (newItem.damageModifier);
            armor.AddModifier (newItem.armorModifier);
            Rage.AddModifier(newItem.rageModifier);
            AttackSpeed.AddModifier(newItem.attackspeedModifier);
            Cooldown.AddModifier(newItem.cooldownModifier);
            Lifesteal.AddModifier(newItem.lifestealModifier);
            MagicResist.AddModifier(newItem.magicResistModifer);
            Health.AddModifier(newItem.healthModifier);
		}
		
		if (oldItem != null) {
            armor.RemoveModifier(oldItem.armorModifier);
			damage.RemoveModifier(oldItem.damageModifier);
            Rage.RemoveModifier(oldItem.rageModifier);
            AttackSpeed.RemoveModifier(oldItem.attackspeedModifier);
            Cooldown.RemoveModifier(oldItem.cooldownModifier);
            Lifesteal.RemoveModifier(oldItem.lifestealModifier);
            MagicResist.RemoveModifier(oldItem.magicResistModifer);
            Health.RemoveModifier(oldItem.healthModifier);
        }
        maxHealth = 100 + Health.GetValue();//remove 100
        maxRage = 100 + Rage.GetValue();//remove 100
        if (Player.instance.playerStats.currentHealth > maxHealth)
            Player.instance.playerStats.currentHealth = maxHealth;
        if (Player.instance.playerStats.currentRage > maxRage)
            Player.instance.playerStats.currentRage = maxRage;
    }

    /*
	 * Function: ClearModifiers
	 * Description: Clear all stat modifiers
     * Creator: Myles Hagen
     */
    public void ClearModifiers()
    {
        damage.modifiers.Clear();
        armor.modifiers.Clear();
        Rage.modifiers.Clear();
        AttackSpeed.modifiers.Clear();
        Cooldown.modifiers.Clear();
        Lifesteal.modifiers.Clear();
        MagicResist.modifiers.Clear();
        Health.modifiers.Clear();
    }

	/*creator: Yan Zhang
	 * check the experience see if the experience hit the target value, than call level up 
	 * exp: the experience gained
	 */
	public void UpdateExperience(int exp){
		currentExperience = Mathf.Clamp (currentExperience + exp, 0, targetExperience);
		if (currentExperience == targetExperience) {           
            LevelUp ();
		}
	}

	/*Creator: Yan Zhang, Myles Hagen
	 * increase base value of stats by base value of next level
	 */
	public void LevelUp() {
		//if level reach to maximun return
		if (level == 11) {
			return;
		}
		// call the level up effect when level up
		GameObject effect = Instantiate(levelUpEffect, Player.instance.transform);
		Destroy(effect, 4.0f);
		//Debug.Log ("player level up to" + (level +1));
		levelUp = true;
		Level nextLevel = LevelHandler.GetLevel (level + 1);
		//Debug.Log ("next level damage" + nextLevel.damage.baseValue);
		//nextLevel.damage.modifiers = damage.modifiers;
		damage.baseValue  += nextLevel.damage.baseValue;
		//nextLevel.armor.modifiers = armor.modifiers;
		armor.baseValue += nextLevel.armor.baseValue;

		//nextLevel.Rage.modifiers = Rage.modifiers;
		//Rage = nextLevel.Rage;
		maxRage += nextLevel.Rage.baseValue;
		currentRage = maxRage;
		//nextLevel.AttackSpeed.modifiers = AttackSpeed.modifiers;
		AttackSpeed.baseValue += nextLevel.AttackSpeed.baseValue;

		//nextLevel.Cooldown.modifiers = Cooldown.modifiers;
		Cooldown.baseValue += nextLevel.Cooldown.baseValue;
	

		nextLevel.MagicResist.modifiers = MagicResist.modifiers;
		MagicResist = nextLevel.MagicResist;

		//nextLevel.Health.modifiers = Health.modifiers;
		//Health = nextLevel.Health;
		//maxHealth = nextLevel.health;
		maxHealth += nextLevel.Health.baseValue;
		currentHealth = maxHealth;
		targetExperience = nextLevel.targetExp;
		currentExperience = 0;
        level++;
	}

	/*
	 * Function: Die
	 * Description: overwritten death function, calls KillPlayer from PLayerManager script
	 * which reloads the scene
	 * Creator: Myles Hagen, Yan Zhang
	 */
	public override void Die ()
	{
		base.Die ();
        Player.instance.Die();
    }
}
