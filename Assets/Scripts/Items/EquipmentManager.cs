// manages the dialog
/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone

*-----------------------------------------------------------------------*/

/*
 * Author: Yunzheng Zhou, Myles Hagen, Kevin Ho, Shane Weerasuriya
 * Date: 2018-04-19
 */


/*
 Class that keeps track of all items in a scene allowing them
 to be deactivated when the correct conditions are met.
*/
/*
 * onEquipmentChanged: delegate to to be called on equipment changed event
 * onGemChangedSkillAOE: delegate tobe called on gem changed event for aoe skill
 * onGemChangedSkillProjectile: delegate tobe called on gem changed event for projectile skill
 * onGemChangedSkillDash: delegate to be called on gem changed event for dash skill
 * numSlots: number of equipment slots.
 * equipmentSlot: array of equipment slots for gui
 * SkillAOE: array of aoe skill gem slots for gui
 * SKillProjectile: array of projectile gem slots for gui
 * SkillDash: array of dash gem slots for gui
 * potionSlot: array of potion slots for gui
 * equipmentIDs: array for identification numbers of equipment
 * SkillAOE_IDs: array of identification numbers of gems for aoe skill
 * SkillProjectile_IDs: array of identification numbers of gems for projectile skill
 * SkillDash_IDs: array of identification numbers of gems for dash skill
 * Potion_IDs: array of identification numbers of potions
 * currentEqupment: arry of scriptable objects representing currently equiped gear
 * itemDatabase: reference to item database 
 * UIItemdatabase: referene to item database for gui
 * inventory: reference to inventory instance
 * afterDeserialize: bool marked as true after current items are deserialized
 * Skill_AOE: arry of scriptable objects representing currently equiped gems for aoe skill
 * Skill_Projectile: arry of scriptable objects representing currently equiped gems for projectile skill
 * Skill_Dash: arry of scriptable objects representing currently equiped gems for dash skill
 * Potions: arry of scriptable objects representing currently equiped potions
 */

using UnityEngine;
using UnityEngine.UI;

public class EquipmentManager : MonoBehaviour {

    #region Singleton


    public static EquipmentManager instance;

    void Awake ()
	{
		instance = this;
        currentEquipment = new Equipment[numSlots];
		Skill_AOE = new Equipment[4];
		Skill_Projectile = new Equipment[5];
		Skill_Dash = new Equipment[6];
        Potions = new Consumable[5];
		equipmentIDs = new int[6];
		SkillAOE_IDs = new int[4];
		SkillProjectile_IDs = new int[5];
		SkillDash_IDs = new int[6];
        Potion_IDs = new int[5];
		
        for (int i = 0; i < 6; i++)
        {
            currentEquipment[i] = (Equipment)itemDatabase.GetItem(0);
            Skill_Dash[i] = (Equipment)itemDatabase.GetItem(0);
            if (i < 5)
            {
                Skill_Projectile[i] = (Equipment)itemDatabase.GetItem(0);
                if (i < 4)
                {
                    Skill_AOE[i] = (Equipment)itemDatabase.GetItem(0);
                }
            }
        }
    }

    #endregion

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
	public event OnEquipmentChanged onEquipmentChanged;

    public delegate void OnGemChangedSkillAOE(Equipment newItem, Equipment oldItem);
    public event OnGemChangedSkillAOE onGemChanged_SkillAOE;

    public delegate void OnGemChangedSkillProjectile(Equipment newItem, Equipment oldItem);
    public event OnGemChangedSkillProjectile onGemChanged_SkillProjectile;

    public delegate void OnGemChangedSkillDash(Equipment newItem, Equipment oldItem);
    public event OnGemChangedSkillDash onGemChanged_SkillDash;


    //
    public const int numSlots = 6;
    public GameObject[] equipmentSlot;
    public GameObject[] SkillAOE;
    public GameObject[] SkillProjectile;
    public GameObject[] SkillDash;
    public GameObject[] potionSlot;
    //public int[] equipmentIDs = new int[numSlots];
    //public Equipment[] currentEquipment = new Equipment[numSlots];
	
    public int[] equipmentIDs;
	public int[] SkillAOE_IDs;
	public int[] SkillProjectile_IDs;
	public int[] SkillDash_IDs;
    public int[] Potion_IDs;
	
    public Equipment[] currentEquipment;
    public ItemDatabase itemDatabase;
    public UIItemDatabase UIItemdatabase;
    Inventory inventory;
    bool afterDeserialize;
	public Equipment[] Skill_AOE;
	public Equipment[] Skill_Projectile;
	public Equipment[] Skill_Dash;
	public Consumable[] Potions;


    // Update the equipment array, potion array, 3 gems array for 3 different skills
    // in the Game Manager by reading the index
    public void EquipmentUpdate(int index)
    {
        Test_UIEquipSlot_Assign equipment;
        for (int i = 0; i < index; i++)
        {
            if (i == 0 && index == 7)
                i = 1;
            if (index == 7)
            {
                equipment = equipmentSlot[i].GetComponent<Test_UIEquipSlot_Assign>();
                if (equipment.assignItem != 0)
                {
                    Equip((itemDatabase.GetItem(equipment.assignItem) as Equipment));
                }
                else
                {
                    currentEquipment[i - 1] = itemDatabase.GetItem(0) as Equipment;
                }
                //return;
            }
            else if (index == 6)
            {
                equipment = SkillDash[i].GetComponent<Test_UIEquipSlot_Assign>();
                EquipGem((itemDatabase.GetItem(equipment.assignItem) as Equipment), 0, i);
            }
            else if (index == 5)
            {
                equipment = SkillProjectile[i].GetComponent<Test_UIEquipSlot_Assign>();
                EquipGem((itemDatabase.GetItem(equipment.assignItem) as Equipment), 1, i);
            }
            else if (index == 4)
            {
                equipment = SkillAOE[i].GetComponent<Test_UIEquipSlot_Assign>();
                EquipGem((itemDatabase.GetItem(equipment.assignItem) as Equipment), 2, i);
            }
            
        }
    }

    // when player is load between scene or just load manaully,
    //  this function will read the gem id array from the array that contain the gem id
    public void UpdateGemOnload()
    {
        Test_UIEquipSlot_Assign equipment;
        UIEquipSlot slot;
        for (int i = 0; i < 4; i++) {
            equipment = SkillAOE[i].GetComponent<Test_UIEquipSlot_Assign>();
            equipment.assignItem = SkillAOE_IDs[i];
            slot = SkillAOE[i].GetComponent<UIEquipSlot>();
            slot.Assign(UIItemdatabase.GetByID(equipment.assignItem));
        }
        for (int i = 0; i < 5; i++)
        {
            equipment = SkillProjectile[i].GetComponent<Test_UIEquipSlot_Assign>();
            equipment.assignItem = SkillProjectile_IDs[i];
            slot = SkillProjectile[i].GetComponent<UIEquipSlot>();
            slot.Assign(UIItemdatabase.GetByID(equipment.assignItem));
        }
        for (int i = 0; i < 6; i++)
        {
            equipment = SkillDash[i].GetComponent<Test_UIEquipSlot_Assign>();
            equipment.assignItem = SkillDash_IDs[i];
            slot = SkillDash[i].GetComponent<UIEquipSlot>();
            slot.Assign(UIItemdatabase.GetByID(equipment.assignItem));
        }
    }

    // update postion slot by read the id saved in global control
    public void updatePotionOnbetweenScene() {
        for (int i = 0; i < 5; i++)
        {
            if (GlobalControl.Instance.savedPotions[i] != null) {
                potionSlot[i].GetComponent<Test_UIEquipSlot_Assign>().assignItem = GlobalControl.Instance.savedPotions[i].itemID;
                potionSlot[i].GetComponent<UIEquipSlot>().Assign(UIItemdatabase.GetByID(potionSlot[i].GetComponent<Test_UIEquipSlot_Assign>().assignItem));
            }
        }
    }

    // update potion when hit load manaually
    public void updatePotionOnload()
    {
        for (int i = 0; i < 5; i++)
        {
                potionSlot[i].GetComponent<Test_UIEquipSlot_Assign>().assignItem = GlobalControl.Instance.LocalCopyOfData.Potions_IDs[i];
                potionSlot[i].GetComponent<UIEquipSlot>().Assign(UIItemdatabase.GetByID(potionSlot[i].GetComponent<Test_UIEquipSlot_Assign>().assignItem));
        }
    }

    // update gem array when load between scene
    public void UpdateGemOnloadBetweenScene()
    {
        Test_UIEquipSlot_Assign equipment;
        UIEquipSlot slot;
        for (int i = 0; i < 4; i++)
        {
            equipment = SkillAOE[i].GetComponent<Test_UIEquipSlot_Assign>();
            equipment.assignItem = GlobalControl.Instance.savedSkillAOE[i].itemID;
            slot = SkillAOE[i].GetComponent<UIEquipSlot>();
            slot.Assign(UIItemdatabase.GetByID(equipment.assignItem));
        }
        for (int i = 0; i < 5; i++)
        {
            equipment = SkillProjectile[i].GetComponent<Test_UIEquipSlot_Assign>();
            equipment.assignItem = GlobalControl.Instance.savedSkillProjectile[i].itemID;
            slot = SkillProjectile[i].GetComponent<UIEquipSlot>();
            slot.Assign(UIItemdatabase.GetByID(equipment.assignItem));
        }
        for (int i = 0; i < 6; i++)
        {
            equipment = SkillDash[i].GetComponent<Test_UIEquipSlot_Assign>();
            equipment.assignItem = GlobalControl.Instance.savedSKillDash[i].itemID;
            slot = SkillDash[i].GetComponent<UIEquipSlot>();
            slot.Assign(UIItemdatabase.GetByID(equipment.assignItem));
        }
    }

    /*
	 * Function: EquipGem
	 * Parameter: newItem: gem to be equiped
     * skill: skill that gem is to be equiped for
     * index: index in gem array
     * 
	 * Description: equips a gem in the array index for the given skill
     * changes to stat modifiers for that skill by invoking onGemChanged
     * event.
     * Creator: Myles Hagen
	 */
    public void EquipGem(Equipment newItem, int skill , int index)
    {
        Equipment oldItem = null;
		if (skill == 0 ) {
			if (Skill_Dash[index] != null) 
				oldItem = Skill_Dash[index];
			
			if (onGemChanged_SkillDash != null)
				onGemChanged_SkillDash.Invoke(newItem, oldItem);
			
			Skill_Dash[index] = newItem;
			
		}
		else if (skill == 1) {
			if (Skill_Projectile[index] != null) 
				oldItem = Skill_Projectile[index];
			
			if (onGemChanged_SkillProjectile != null)
				onGemChanged_SkillProjectile.Invoke(newItem, oldItem);
			
			Skill_Projectile[index] = newItem;
		}
		else {
			if (Skill_AOE[index] != null) 
				oldItem = Skill_AOE[index];
			
			if (onGemChanged_SkillAOE != null) 
				onGemChanged_SkillAOE.Invoke(newItem, oldItem);
			
			Skill_AOE[index] = newItem;
			
		}
    }

    /*
	 * Function: EquipPotion
	 * Parameter: potion: potion to be equiped
	 * Description: equips a potion in a slot if it is empty, updates the
     * GUI to show the new potion.
     * returns: true if potion equiped, false if not
     * creator: Myles Hagen, Yunzheng Zhou
	 */
    public bool EquipPotion(Consumable potion)
    {
        for (int i = 0; i < Potions.Length; i++)
        {
            if (Potions[i] == null)
            {
                Potions[i] = potion;
                potionSlot[i].GetComponent<UIEquipSlot>().Assign(UIItemdatabase.GetByID(potion.itemID));
                potionSlot[i].GetComponent<Test_UIEquipSlot_Assign>().assignItem = potion.itemID;
                return true;
            }
        }
        return false;
    }

    /*
    * Function: Equip
    * Parameter: newItem: item to be equiped
    * Description: equips an item in its correct slots and updates
    * the characters stats.
    * 
    * creator: Myles Hagen
    */
    public void Equip (Equipment newItem)
	{
        Equipment oldItem = (itemDatabase.GetItem(0) as Equipment);
        int slotIndex = (int)newItem.equipSlot;

       if (currentEquipment[slotIndex] != null)
		{
			oldItem = currentEquipment [slotIndex];
		}

        // An item has been equipped so we trigger the callback
        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
            StatValue.instance.updateValue();
        }

		currentEquipment [slotIndex] = newItem;
		//Debug.Log(newItem.name + " equipped! " + (int)newItem.equipSlot);
	}

    /*
     * loadEquipmentByID
     * 
     * Description: load front-end equipment by read id from backend id array
     * 
     */
    public void loadEquipmentByID()
    {
        for (int i = 0; i < 6; i++)
        {
            //Equip((itemDatabase.GetItem(equipmentIDs[i]) as Equipment));
            Test_UIEquipSlot_Assign equipment = equipmentSlot[i + 1].GetComponent<Test_UIEquipSlot_Assign>();
            equipment.assignItem = equipmentIDs[i];
        }
    }

    /*
     * loadEquipmentByIDBetweenScene:
     * Description: load equipment by read id when load betweenScene
     */
    public void loadEquipmentByIDBetweenScene()
    {
        for (int i = 0; i < 6; i++)
        {
            //Equip((itemDatabase.GetItem(GlobalControl.Instance.savedEquipment[i].itemID) as Equipment));
            Test_UIEquipSlot_Assign equipment = equipmentSlot[i + 1].GetComponent<Test_UIEquipSlot_Assign>();
            equipment.assignItem = GlobalControl.Instance.savedEquipment[i].itemID;
            equipmentSlot[i + 1].GetComponent<UIEquipSlot>().Assign(UIItemdatabase.GetByID(equipment.assignItem));
        }
    }

    /*
     * assign the specific item to gui at the slot by read id in back end equipment array
     */
    public void assignEquipmentByID()
    {
        for (int i = 1; i < 7; i++)
        {
            equipmentSlot[i].GetComponent<UIEquipSlot>().Assign(UIItemdatabase.GetByID(equipmentSlot[i].GetComponent<Test_UIEquipSlot_Assign>().assignItem));
        }
    }


    /*
     * Get equipment referece by pass in the slot and get current Equipment
     */
    public Equipment GetEquipment(EquipmentSlot slot) {
		return currentEquipment [(int)slot];
	}


    /*
	 * Function: OnBeforeSerialize
	 * Description: stores the identification numbers of all equipment
     * and gems currently equiped by the player 
     * creator: Myles Hagen
	 */
    public void OnBeforeSerialize()
    {
        //Debug.Log(equipmentIDs.Length);
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            if (currentEquipment[i] == null)
                continue;
            equipmentIDs[i] = currentEquipment[i].itemID;
        }
        for (int i = 0; i < Skill_AOE.Length; i++)
        {
            if (Skill_AOE[i] == null)
                continue;
            SkillAOE_IDs[i] = Skill_AOE[i].itemID;
        }
        for (int i = 0; i < Skill_Projectile.Length; i++)
        {
            if (Skill_Projectile[i] == null)
                continue;
            SkillProjectile_IDs[i] = Skill_Projectile[i].itemID;
        }
        for (int i = 0; i < Skill_Dash.Length; i++)
        {
            if (Skill_Dash[i] == null)
                continue;
            SkillDash_IDs[i] = Skill_Dash[i].itemID;
        }
        for (int i = 0; i < Potions.Length; i++)
        {
            if (Potions[i] == null)
                continue;
            Potion_IDs[i] = Potions[i].itemID;
        }

    }


    /*
     * Function: CheckEquipmentID
     * Description: Check the id in backend equipment id with the currentEquipment id
     */
    public void CheckEquipmentID()
    {
        for (int i = 0; i < 6; i++)
        {
            equipmentIDs[i] = currentEquipment[i].itemID;
        }
    }

    /*
	 * Function: OnAfterDeserialize
	 * Description: convert item identification numbers to the scriptable objects
     * representing the equipment and gems currently equiped by the player.
     * creator: Myles Hagen
	 */
    public void OnAfterDeserialize()
    {
        
        for (int i = 0; i < Potion_IDs.Length; i++)
        {
            if (Potion_IDs[i] == 0)
                continue;
            var item = itemDatabase.GetItem(Potion_IDs[i]);
            Potions[i] = (Consumable)item;
        }
        for (int i = 0; i < equipmentIDs.Length; i++)
        {
            
            if (equipmentIDs[i] == 0)
                continue;
            var item = itemDatabase.GetItem(equipmentIDs[i]);
            currentEquipment[i] = (Equipment)item;
        }
        for (int i = 0; i < SkillAOE_IDs.Length; i++)
        {
            if (SkillAOE_IDs[i] == 0)
                continue;
            var item = itemDatabase.GetItem(SkillAOE_IDs[i]);
            Skill_AOE[i] = (Equipment)item;
        }
        for (int i = 0; i < SkillDash_IDs.Length; i++)
        {
            //Debug.Log("equip ID after deserialize: " + SkillDash_IDs[i]);
            if (SkillDash_IDs[i] == 0)
                continue;
            var item = itemDatabase.GetItem(SkillDash_IDs[i]);
            Skill_Dash[i] = (Equipment)item;
        }
        for (int i = 0; i < SkillProjectile_IDs.Length; i++)
        {
            if (SkillProjectile_IDs[i] == 0)
                continue;
            var item = itemDatabase.GetItem(SkillProjectile_IDs[i]);
            Skill_Projectile[i] = (Equipment)item;
        }
        foreach (var item in currentEquipment)
        {
            //Debug.Log("id of item in equipment: " + item.itemID);
        }
        afterDeserialize = true;


    }

    /*
	 * Function: Unequip
	 * Parameter: slotIndex - index of item to unequip
	 * Description: removes an item if one is in given slot index, and removes
     * stat modifiers.
     * creator: Myles Hagen
	 */
    public void Unequip(int slotIndex) {
		if (currentEquipment[slotIndex] != null)
		{
			Equipment oldItem = currentEquipment [slotIndex];
            inventory.Add(oldItem);

            currentEquipment[slotIndex] = null;
			if (onEquipmentChanged != null)
				onEquipmentChanged.Invoke(null, oldItem);
			
		}

	
	}

    // not used currently
    void UnequipAll() {
		for (int i = 0; i < currentEquipment.Length; i++) {
			Unequip (i);
		}
		//EquipAllDefault ();
	}

    // not used currently
    void EquipAll()
    {
        foreach (Equipment i in inventory.items)
        {
            if ((int)i.equipSlot != 6 || (int)i.equipSlot != 7)
                Equip(i);
            
            
        }
        inventory.items.Clear();

    }

}
