using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;
using System.Collections;
using System.Collections.Generic;

/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # UIItemSlot.cs
*-----------------------------------------------------------------------*/

/*
 * Creator: Yunzheng Zhou, Tianqi Xiao
 */

namespace UnityEngine.UI
{
    /*
     * UIItemslot inherit basic implement of itembaseslot.
     * This class modify specific events of drag, swap, drop, and right click between 2
     * item slots or between itemslot and equipslot.
     * The events result also will be varied according to the item type
     * 
     */
	[AddComponentMenu("UI/Icon Slots/Item Slot", 12)]
	public class UIItemSlot : UISlotBase {
		
		[Serializable] public class OnAssignEvent : UnityEvent<UIItemSlot> { }
		[Serializable] public class OnUnassignEvent : UnityEvent<UIItemSlot> { }
		[Serializable] public class OnClickItemEvent : UnityEvent<UIItemSlot> { }

        /*
        * slotGroup: Gets or sets the slot group
        * ID: Gets or sets the slot ID
        * m_ItemInfo: The assigned item info
        * onAssign: The assign event delegate
        * onUnassign: The unassign event delegate
        */
        [SerializeField] public bool isVendor;
		[SerializeField] private UIItemSlot_Group m_SlotGroup = UIItemSlot_Group.None;
		[SerializeField] private int m_ID = 0;

		public UIItemSlot_Group slotGroup
		{
			get { return this.m_SlotGroup; }
			set { this.m_SlotGroup = value; }
		}
			
		public int ID
		{
			get { return this.m_ID; }
			set { this.m_ID = value; }
		}

		private UIItemInfo m_ItemInfo;

		public OnAssignEvent onAssign = new OnAssignEvent();

		public OnUnassignEvent onUnassign = new OnUnassignEvent();

		/*
		 * Creator: Yunzheng Zhou
		 * Gets the item info of the item assigned to this slot.
		 * Return: m_ItemInfo - the spell info
		 */
		public UIItemInfo GetItemInfo()
		{
			return this.m_ItemInfo;
		}

		/*
		 * Creator: Yunzheng Zhou
		 * Determines whether this slot is assigned.
		 * Return: a bool variable indicating if this instance is assigned
		 */
		public override bool IsAssigned()
		{
			return (this.m_ItemInfo != null);
		}

		/*
		 * Creator: Yunzheng Zhou
		 * Assign the slot by item info.
		 * Parameter: itemInfo - the item info
		 */
		public bool Assign(UIItemInfo itemInfo)
		{
			if (itemInfo == null)
				return false;
			
			// Make sure we unassign first, so the event is called before new assignment
			this.Unassign();
			
			// Use the base class assign to set the icon
			this.Assign(itemInfo.Icon);
			
			// Set the spell info
			this.m_ItemInfo = itemInfo;
			
			// Invoke the on assign event
			if (this.onAssign != null)
				this.onAssign.Invoke(this);
			
			// Success
			return true;
		}

		/*
		 * Creator: Yunzheng Zhou
		 * Assign the slot by the passed source slot.
		 * Parameter: source object
		 */
		public override bool Assign(Object source)
		{
			if (source is UIItemSlot)
			{
				UIItemSlot sourceSlot = source as UIItemSlot;
				
				if (sourceSlot != null)
					return this.Assign(sourceSlot.GetItemInfo());
			}
			else if (source is UIEquipSlot)
			{
				UIEquipSlot sourceSlot = source as UIEquipSlot;
				
				if (sourceSlot != null)
					return this.Assign(sourceSlot.GetItemInfo());
			}
			
			// Default
			return false;
		}

		/*
		 * Creator: Yunzheng Zhou
		 * Unassign this slot.
		 */
		public override void Unassign()
		{
			// Remove the icon
			base.Unassign();
			
			// Clear the spell info
			this.m_ItemInfo = null;
			
			// Invoke the on unassign event
			if (this.onUnassign != null)
				this.onUnassign.Invoke(this);
		}

		/*
		 * Creator: Yunzheng Zhou
		 * Determines whether this slot can swap with the specified target slot.
		 * Parameter: target object
		 * Return: a bool variable indicating if this instance can swap with the specified target
		 */
		public override bool CanSwapWith(Object target)
		{
            
			if ((target is UIItemSlot) || (target is UIEquipSlot))
			{
				// Check if the equip slot accpets this item
				if (target is UIEquipSlot)
				{
                    if ((target as UIEquipSlot).GetItemInfo().EquipType == 0)
                    {
                        return true;
                    }
                    
					return (target as UIEquipSlot).CheckEquipType(this.GetItemInfo());
				}
                //Debug.Log("return false");
                // It's an item slot
                return true;
			}
           
			// Default
			return false;
		}

		/*
		 * Creator: Yunzheng Zhou
		 * Description: 
         *          Performs a item swap from one slot to another slot
         *          The destination slot will varied. if the destination slot is vendor inventory
         *          then it means that the player is trading with vendor.
         *          otherwise swap the items.
		 * Parameter: sourceObject object
		 * Return a bool value indicating if slot swap was performed
		 */
		public override bool PerformSlotSwap(Object sourceObject)
		{
            // Get the source slot
            UISlotBase sourceSlot = (sourceObject as UISlotBase);
            //Debug.Log("Itemslot!");
            // Get the source item info
            UIItemInfo sourceItemInfo = null;
			bool assign1 = false;
            bool sourceVendor = false;
            bool thisVendor = false;
			// Check the type of the source slot
			if (sourceSlot is UIItemSlot)
			{
				sourceItemInfo = (sourceSlot as UIItemSlot).GetItemInfo();
                if ((sourceSlot as UIItemSlot).isVendor)
                    sourceVendor = true;
                if (this.isVendor)
                    thisVendor = true;
                Text gold = Inventory.instance.gold.GetComponent<Text>();
                if (sourceVendor && !thisVendor)
                    gold.text = Convert.ToString(Convert.ToInt32(gold.text) + this.GetItemInfo().price);
                if (!sourceVendor && thisVendor)
                {
                    if (gold.text == "0")
                        return false;
                    if (this.GetItemInfo().price > Convert.ToInt32(gold.text))
                        return false;
                    gold.text = Convert.ToString(Convert.ToInt32(gold.text) - this.GetItemInfo().price);
                }
                // Assign the source slot by this one
                assign1 = (sourceSlot as UIItemSlot).Assign(this.GetItemInfo());
                Test_UIItemSlot_Assign sourceItem = sourceSlot.GetComponent<Test_UIItemSlot_Assign>();
                Test_UIItemSlot_Assign Toitem = this.GetComponent<Test_UIItemSlot_Assign>();
                int id = sourceItem.assignItem;
                sourceItem.assignItem = Toitem.assignItem;
                Toitem.assignItem = id;
                
                /*if (sourceItem.assignItem == 0)
            {
                sourceSlot.Unassign();
            }
            if (Toitem.assignItem == 0)
            {
                this.Unassign();
            }*/
            }
			else if (sourceSlot is UIEquipSlot)
			{
				sourceItemInfo = (sourceSlot as UIEquipSlot).GetItemInfo();
                if (this.GetItemInfo().gemSlot != (sourceSlot as UIEquipSlot).slotindex)
                {
                    return false;
                }

                if (this.GetItemInfo().EquipType == 0)
                    return false;
                // Assign the source slot by this one
                    assign1 = (sourceSlot as UIEquipSlot).Assign(this.GetItemInfo());
                if (!assign1)
                {
                    return false;
                }
                Test_UIEquipSlot_Assign sourceItem = sourceSlot.GetComponent<Test_UIEquipSlot_Assign>();
                Test_UIItemSlot_Assign Toitem = this.GetComponent<Test_UIItemSlot_Assign>();
                int id = sourceItem.assignItem;
                sourceItem.assignItem = Toitem.assignItem;
                Toitem.assignItem = id;
                if ((int)sourceSlot.GetComponent<UIEquipSlot>().equipType == 1)
                {
                    Player.instance.playerController.WeaponState = 1;
                }
                //Debug.Log("source is " + sourceItem.assignItem);
                //Debug.Log("this is " + this + " id is " + Toitem.assignItem);
            }
            //Debug.Log("perform swap");
            // Assign this slot by the source slot
            for (int i = 4; i < 8; i++)
                EquipmentManager.instance.EquipmentUpdate(i);
            bool assign2 = this.Assign(sourceItemInfo);
            //Debug.Log("assign1 is " + assign1 + "assign2 is " + assign2);
            // Return the status
            //Inventory.instance.UpdateInventory();
            return (assign1 && assign2);
		}

		/*
		 * Function: PrepareTooltip 
		 * Prepares the tooltip with the specified item info.
         * All infomation are saved in iteminfodatabase. if the specific atrribute is 0, then it will not show up in tooltip panel
		 * Parameter: itemInfo - UIItemInfo object
		 */
		public static void PrepareTooltip(UIItemInfo itemInfo)
		{
            
			if (itemInfo == null)
				return;

            // Set the title and description
            UITooltip.AddTitle(itemInfo.Name);
            UITooltip.AddLine("");
            
            
			
			// Item types
			UITooltip.AddLineColumn(itemInfo.Type);
			UITooltip.AddLineColumn(itemInfo.Subtype);

            /*if (itemInfo.ItemType == 1)
			{
				UITooltip.AddLineColumn(itemInfo.Damage.ToString() + " Damage");
				UITooltip.AddLineColumn(itemInfo.AttackSpeed.ToString("0.0") + " Attack speed");
				
				UITooltip.AddLine("(" + ((float)itemInfo.Damage / itemInfo.AttackSpeed).ToString("0.0") + " damage per second)");
			}
			else
			{
				UITooltip.AddLineColumn(itemInfo.Block.ToString() + " Block");
				UITooltip.AddLineColumn(itemInfo.Armor.ToString() + " Armor");
			}
		
			UITooltip.AddLine("+" + itemInfo.Stamina.ToString() + " Stamina", new RectOffset(0, 0, 6, 0));
			UITooltip.AddLine("+" + itemInfo.Strength.ToString() + " Strength");
			*/
            if (itemInfo.weight != 0)
            {
                UITooltip.AddLineColumn("Weight:" + itemInfo.weight.ToString());
                UITooltip.AddLine("");
            }

            if (itemInfo.gemSlot != 0)
            {
                UITooltip.AddLine("<color=#db81fc>" + (itemInfo.gemSlot == 1 ? "FlameStrike GEM" : "Rageful Laser GEM") + "</color>");
            }

            if (itemInfo.Damage != 0)
            {
                UITooltip.AddLineColumn("<color=#fee95e>AttackPower:</color>");
                UITooltip.AddLineColumn("<color=#00ff00> + " + itemInfo.Damage.ToString() + "</color>");
            }

            if (itemInfo.Cooldown != 0)
            {
                UITooltip.AddLineColumn("<color=#fee95e>CoolingDown:</color>");
                UITooltip.AddLineColumn("<color=#00ff00> + " + itemInfo.Cooldown.ToString() + "</color>");
            }

            if (itemInfo.Lifesteal != 0)
            {
                UITooltip.AddLineColumn("<color=#fee95e>Life Steal:</color>");
                UITooltip.AddLineColumn("<color=#00ff00> + " + itemInfo.Lifesteal.ToString() + "</color>");
            }

            if (itemInfo.Range != 0)
            {
                UITooltip.AddLineColumn("<color=#fee95e>Skill Range:</color>");
                UITooltip.AddLineColumn("<color=#00ff00> + " + itemInfo.Range.ToString() + "</color>");
            }

            if (itemInfo.RageGen != 0)
            {
                UITooltip.AddLineColumn("<color=#fee95e>Rage generation:</color>");
                UITooltip.AddLineColumn("<color=#00ff00> + " + itemInfo.RageGen.ToString() + "</color>");
            }

            if (itemInfo.Radius != 0)
            {
                UITooltip.AddLineColumn("<color=#fee95e>Radius:</color>");
                UITooltip.AddLineColumn("<color=#00ff00> + " + itemInfo.Radius.ToString() + "</color>");
            }

            if (itemInfo.LastTime != 0)
            {
                UITooltip.AddLineColumn("<color=#fee95e>Last Time:</color>");
                UITooltip.AddLineColumn("<color=#00ff00> + " + itemInfo.LastTime.ToString() + "</color>");
            }

            if (itemInfo.Stamina != 0)
            {
                UITooltip.AddLineColumn("<color=#fee95e>Health:</color>");
                UITooltip.AddLineColumn("<color=#00ff00> + " + itemInfo.Stamina.ToString() + "</color>");
            }

            if (itemInfo.Armor != 0)
            {
                UITooltip.AddLineColumn("<color=#fee95e>Armor:</color>");
                UITooltip.AddLineColumn("<color=#00ff00> + " + itemInfo.Armor.ToString() + "</color>");
            }

            
            

            // Set the item description if not empty
            if (!string.IsNullOrEmpty(itemInfo.Description))
				UITooltip.AddDescription(itemInfo.Description);
            UITooltip.AddLineColumn("");
            UITooltip.AddLineColumn("<color=#fee95e>Sell: " + itemInfo.price.ToString() + "</color>");
        }

		/*
		 * Creator: Yunzheng Zhou
		 * Raises the tooltip event.
		 * Parameter: a bool variable indicating if set to.
		 */
		public override void OnTooltip(bool show)
		{
			// Make sure we have spell info, otherwise game might crash
			if (this.m_ItemInfo == null)
				return;
			
			// If we are showing the tooltip
			if (show)
			{
				// Prepare the tooltip
				UIItemSlot.PrepareTooltip(this.m_ItemInfo);
				
				// Anchor to this slot
				UITooltip.AnchorToRect(this.transform as RectTransform);
				
				// Show the tooltip
				UITooltip.Show();
			}
			else
			{
				// Hide the tooltip
				UITooltip.Hide();
			}
		}
		
		#region Static Methods

		/*
		 * Creator: Yunzheng Zhou
		 * Gets all the item slots.
		 * Return: a list of slots
		 */
		public static List<UIItemSlot> GetSlots()
		{
			List<UIItemSlot> slots = new List<UIItemSlot>();
			UIItemSlot[] sl = Resources.FindObjectsOfTypeAll<UIItemSlot>();
			
			foreach (UIItemSlot s in sl)
			{
				// Check if the slow is active in the hierarchy
				if (s.gameObject.activeInHierarchy)
					slots.Add(s);
			}
			
			return slots;
		}

		/*
		 * Creator: Yunzheng Zhou
		 * Gets all the item slots with the specified ID.
		 * Parameter: the slot ID
		 * Return: a list of slots
		 */
		public static List<UIItemSlot> GetSlotsWithID(int ID)
		{
			List<UIItemSlot> slots = new List<UIItemSlot>();
			UIItemSlot[] sl = Resources.FindObjectsOfTypeAll<UIItemSlot>();
			
			foreach (UIItemSlot s in sl)
			{
				// Check if the slow is active in the hierarchy
				if (s.gameObject.activeInHierarchy && s.ID == ID)
					slots.Add(s);
			}
			
			return slots;
		}

		/*
		 * Creator: Yunzheng Zhou
		 * Gets all the item slots in the specified group.
		 * Parameter: a group item slot - UIItemSlot_Group
		 * Return: a list of slot variables
		 */
		public static List<UIItemSlot> GetSlotsInGroup(UIItemSlot_Group group)
		{
			List<UIItemSlot> slots = new List<UIItemSlot>();
			UIItemSlot[] sl = Resources.FindObjectsOfTypeAll<UIItemSlot>();
			
			foreach (UIItemSlot s in sl)
			{
				// Check if the slow is active in the hierarchy
				if (s.gameObject.activeInHierarchy && s.slotGroup == group)
					slots.Add(s);
			}
			
			return slots;
		}

		/*
		 * Creator: Yunzheng Zhou
		 * Gets the slot with the specified ID and Group.
		 * Parameter: a group item slot - UIItemSlot_Group
		 * 			the slot ID - int
		 * Return: a list of slot variables
		 */
		public static UIItemSlot GetSlot(int ID, UIItemSlot_Group group)
		{
			UIItemSlot[] sl = Resources.FindObjectsOfTypeAll<UIItemSlot>();
			
			foreach (UIItemSlot s in sl)
			{
				// Check if the slow is active in the hierarchy
				if (s.gameObject.activeInHierarchy && s.ID == ID && s.slotGroup == group)
					return s;
			}
			
			return null;
		}
		#endregion
	}
}