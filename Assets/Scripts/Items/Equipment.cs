using UnityEngine;
using System;

/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone

*-----------------------------------------------------------------------*/

/*
 * Author: Yunzheng Zhou, Tianqi Xiao
 * Date: 2018-04-19
 */


/*
* armorModifier: integer value for armor modifier
* damageModifier: integer value for damage modifier
* equipSlot: referenct to EquipmentSlot enum specify what slot item goes in
* rageModifier: integer value for rage modifier
* attackspeedModifier: integer value for attacspeed modifier
* lifestealModifier: integer value for lifesteal modifier
* magicResistModifier: integer value for magic resist modifier
* healthModifier: integer value for health modifier
* rangeModifier: integer value for range modifier
* rageGenerationModifier: integer value for rage generation modifier
* radiusModifier: integer value for radius modifier
* lastTimeModifier: integer value for armormodifier
*/

[Serializable]
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Equipment")]
public class Equipment : Item {

	public EquipmentSlot equipSlot;
    
    public int armorModifier;
	public int damageModifier;
    public int rageModifier;
    public int attackspeedModifier;
    public int cooldownModifier;
    public int lifestealModifier;
    public int magicResistModifer;
    public int healthModifier;
    public int rangeModifier;
    public int rageGenerationModifer;
    public int radiusModifier;
    public int lastTimeModifier;

	// singleton pattern makes sure only one instance of Equipment exists
    #region Singleton

    public static Equipment instance;

    void Awake()
    {
        instance = this;
    }

    #endregion
    //public SkinnedMeshRenderer prefab;
    
    /*
     * Function: Use
     * 
     * Description: equip and item and remove it from inventory
     */
    public override void Use ()
	{
		EquipmentManager.instance.Equip(this);	// Equip
        
        //RemoveFromInventory();	// Remove from inventory
	}
}

public enum EquipmentSlot { Weapon, Shield, Head, Necklace, Shoulders, Chest, BigGem, SmallGem}

