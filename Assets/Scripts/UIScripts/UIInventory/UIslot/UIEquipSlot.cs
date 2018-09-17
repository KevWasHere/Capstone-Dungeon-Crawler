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
  # UIEquipSlot.cs
*-----------------------------------------------------------------------*/

/*
 * Creator: Yunzheng Zhou
 */

namespace UnityEngine.UI
{
    /*
     * UIequipSlot also inherit UIslotBase to gain most basic function of slots.
     * This class focus on different types of equip slots, equipment slots, healthpotion slots and gem slots.
     * Each individual equip slot will have a equip type that define what kind of equipment could be dropped in.
     * For example, chest equipment could only be drag in chest equip slot.
     */
	[AddComponentMenu("UI/Icon Slots/Equip Slot", 12)]
	public class UIEquipSlot : UISlotBase {
		
		[Serializable] public class OnAssignEvent : UnityEvent<UIEquipSlot> { }
		[Serializable] public class OnUnassignEvent : UnityEvent<UIEquipSlot> { }
        /*
         * equipType: Gets or sets the equip type of the slot
         * m_ItemInfo: The assigned item info
         * onAssign: The assign event delegate
         * onUnassign: The unassign event delegate
         */
        public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
        public event OnEquipmentChanged onEquipmentChanged;

        [SerializeField] private UIEquipmentType m_EquipType = UIEquipmentType.None;
        public int slotindex;

		public UIEquipmentType equipType
		{
			get { return this.m_EquipType; }
			set { this.m_EquipType = value; }
		}
			
		private UIItemInfo m_ItemInfo;

		public OnAssignEvent onAssign = new OnAssignEvent();

		public OnUnassignEvent onUnassign = new OnUnassignEvent();

		/*
		 * Creator: Yunzheng Zhou
		 * Gets the item info of the item assigned to this slot.
		 * Return: The spell info
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
            //Debug.Log("iteminfo is " + itemInfo);
			if (itemInfo == null)
				return false;
            //Debug.Log("check type " + this.CheckEquipType(itemInfo));
			// Check if the equipment type matches the target slot
			if (!this.CheckEquipType(itemInfo))
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
		 * Parameter: Source Object
		 */
		public override bool Assign(Object source)
		{
            
            if (source is UIItemSlot)
			{
				UIItemSlot sourceSlot = source as UIItemSlot;
                
				// Check if the equipment type matches the target slot
				if (!this.CheckEquipType(sourceSlot.GetItemInfo()))
					return false;
				
				return this.Assign(sourceSlot.GetItemInfo());
			}
			else if (source is UIEquipSlot)
			{
				UIEquipSlot sourceSlot = source as UIEquipSlot;
                
                // Check if the equipment type matches the target slot
                if (!this.CheckEquipType(sourceSlot.GetItemInfo()))
					return false;
				
				return this.Assign(sourceSlot.GetItemInfo());
			}
			
			// Default
			return false;
		}

		/* 
		 * Creator: Yunzheng Zhou
		 * Checks if the given item can assigned to this slot.
		 * Parameter: info - a UIItemInfo object
		 * Return: a bool variable indicating whether equip type was checked or not
		 */
		public virtual bool CheckEquipType(UIItemInfo info)
		{
            
            if (info.EquipType == 0)
                return true;
            
			if (info.EquipType != this.equipType)
				return false;
            //Debug.Log("niniu");
			return true;
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
					return (target as UIEquipSlot).CheckEquipType(this.GetItemInfo());
				}
                
				// It's an item slot
				return true;
			}
			
			// Default
			return false;
		}

		/*
		 * Creator: Yunzheng Zhou
		 * Performs a slot swap.
		 * Parameter: Source slot object
		 * Return: a bool variable indicating if slot swap was performed
		 */
		public override bool PerformSlotSwap(Object sourceObject)
		{
			// Get the source item info
			UIItemInfo sourceItemInfo = null;
			bool assign1 = false;
            //Debug.Log(sourceObject + " equipment");
            // Check the type of the source slot
            if (sourceObject is UIItemSlot)
            {
                sourceItemInfo = (sourceObject as UIItemSlot).GetItemInfo();
                /*if (sourceItemInfo.EquipType == 0)
                {
                    Debug.Log("source is the ! " + sourceItemInfo);
                    (sourceObject as UIItemSlot).Unassign();
                    assign1 = true;
                }*/
                // Assign the source slot by this one
                //else
                //{
                assign1 = (sourceObject as UIItemSlot).Assign(this.GetItemInfo());
                Test_UIItemSlot_Assign sourceitem = (sourceObject as UIItemSlot).GetComponent<Test_UIItemSlot_Assign>();
                Test_UIEquipSlot_Assign thisitem = this.GetComponent<Test_UIEquipSlot_Assign>();
                Player.instance.playerStats.OnEquipmentChanged(null, backupitemDataBase.GetItem(thisitem.assignItem) as Equipment);
                if ((int)this.GetComponent<UIEquipSlot>().equipType == 1)
                    Player.instance.playerController.WeaponState = 0;
                int tmp = sourceitem.assignItem;
                sourceitem.assignItem = thisitem.assignItem;
                thisitem.assignItem = tmp;
                
                //Debug.Log("source is " + sourceitem.assignItem);
                
                //Debug.Log("assign1 " + assign1);
                //}
            }
            else if (sourceObject is UIEquipSlot)
            {
                return false;
                sourceItemInfo = (sourceObject as UIEquipSlot).GetItemInfo();
                
                // Assign the source slot by this one
                assign1 = (sourceObject as UIEquipSlot).Assign(this.GetItemInfo());
                
            }
            else
            {
                for (int i = 4; i < 8; i++)
                    EquipmentManager.instance.EquipmentUpdate(i);
                return false;
            }
            for (int i = 4; i < 8; i++)
                EquipmentManager.instance.EquipmentUpdate(i);
            // Assign this slot by the source slot
            bool assign2 = this.Assign(sourceItemInfo);
            
            // Return the status
            return (assign1 && assign2);
		}

		/*
		 * Creator: Yunzheng Zhou
		 * Raises the tooltip event.
		 * Parameter: a bool variable indicating if it sets to.
		 */
		public override void OnTooltip(bool show)
		{
			// Handle unassigned
			if (!this.IsAssigned())
			{
				// If we are showing the tooltip
				if (show)
				{
					UITooltip.AddTitle(UIEquipSlot.EquipTypeToString(this.m_EquipType));
					UITooltip.SetHorizontalFitMode(ContentSizeFitter.FitMode.PreferredSize);
					UITooltip.AnchorToRect(this.transform as RectTransform);
					UITooltip.Show();
				}
				else UITooltip.Hide();
			}
			else
			{
				// Make sure we have spell info, otherwise game might crash
				if (this.m_ItemInfo == null)
					return;
				
				// If we are showing the tooltip
				if (show)
				{
					UIItemSlot.PrepareTooltip(this.m_ItemInfo);
                    
					UITooltip.AnchorToRect(this.transform as RectTransform);
					UITooltip.Show();
				}
				else UITooltip.Hide();
			}
		}
		
		#region Static Methods

		/*
		 * Creator: Yunzheng Zhou
		 * Equip type to string convertion.
		 * Parameter: type - UIEquipmentType
		 * Return: a string variable
		 */
		public static string EquipTypeToString(UIEquipmentType type)
		{
			string str = "Undefined";
			
			switch (type)
			{
				case UIEquipmentType.Head: 				str = "Head"; 		break;
				case UIEquipmentType.Necklace:			str = "Necklace"; 	break;
				case UIEquipmentType.Shoulders: 		str = "Shoulders"; 	break;
				case UIEquipmentType.Chest: 			str = "Chest"; 		break;
				//case UIEquipmentType.Bracers: 			str = "Bracers"; 	break;
				//case UIEquipmentType.Gloves: 			str = "Gloves"; 	break;
				//case UIEquipmentType.Belt: 				str = "Belt"; 		break;
				//case UIEquipmentType.Pants: 			str = "Pants"; 		break;
				//case UIEquipmentType.Boots: 			str = "Boots"; 		break;
				//case UIEquipmentType.Trinket: 			str = "Trinket"; 	break;
				case UIEquipmentType.Weapon_MainHand: 	str = "Main Hand"; 	break;
				case UIEquipmentType.Weapon_OffHand: 	str = "Off Hand"; 	break;
			}
			
			return str;
		}

		/*
		 * Creator: Yunzheng Zhou
		 * Gets all the equip slots.
		 * Return: a list of slot variables
		 */
		public static List<UIEquipSlot> GetSlots()
		{
			List<UIEquipSlot> slots = new List<UIEquipSlot>();
			UIEquipSlot[] sl = Resources.FindObjectsOfTypeAll<UIEquipSlot>();
			
			foreach (UIEquipSlot s in sl)
			{
				// Check if the slow is active in the hierarchy
				if (s.gameObject.activeInHierarchy)
					slots.Add(s);
			}
			
			return slots;
		}

		/*
		 * Creator: Yunzheng Zhou
		 * Gets the first equip slot found with the specified Equipment Type.
		 * Parameter: type - UIEquipmentType
		 * Return: a slot variable
		 */
		public static UIEquipSlot GetSlotWithType(UIEquipmentType type)
		{
			UIEquipSlot[] sl = Resources.FindObjectsOfTypeAll<UIEquipSlot>();
			
			foreach (UIEquipSlot s in sl)
			{
				// Check if the slow is active in the hierarchy
				if (s.gameObject.activeInHierarchy && s.equipType == type)
					return s;
			}
			
			// Default
			return null;
		}

		/*
		 * Creator: Yunzheng Zhou
		 * Gets all the equip slots with the specified Equipment Type.
		 * Parameter: type - UIEquipmentType
		 * Return: a list of slot variables
		 */
		public static List<UIEquipSlot> GetSlotsWithType(UIEquipmentType type)
		{
			List<UIEquipSlot> slots = new List<UIEquipSlot>();
			UIEquipSlot[] sl = Resources.FindObjectsOfTypeAll<UIEquipSlot>();
			
			foreach (UIEquipSlot s in sl)
			{
				// Check if the slow is active in the hierarchy
				if (s.gameObject.activeInHierarchy && s.equipType == type)
					slots.Add(s);
			}		
			return slots;
		}
		#endregion
	}
}