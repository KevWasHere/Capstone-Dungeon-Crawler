/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone

*-----------------------------------------------------------------------*/

/*
 * Author: Yunzheng Zhou
 * Date: 2018-04-19
 */

using UnityEngine;
using System;

namespace UnityEngine.UI
{
    /*
     * Serialized class that contain the infomation of item
     * The ID, name, icon, weight and equipType is must need stat for item
     * the prefeb is for dropping the item to the ground
     * The cooldown, lefsteal and range, ragegen, radius, and lasttime are only for gem type of equipment
     */
	[Serializable]
	public class UIItemInfo
	{
		public int ID;                                              // Item ID
		public string Name;                                         // Name of item
		public Sprite Icon;                                         // icon of item
        public int weight;                                          // weight of item that will be check in the droptable
		public string Description;                                  // description of item
		public UIEquipmentType EquipType;                           // equiptype of item
		public int ItemType;                                        // int item type that define in the ui itemdatabase
		public string Type;                                         // string type that show on the tool tip
		public string Subtype;                                      // subtype that show on the tool tip
		public int Damage;                                          // the attack power
		public int AttackSpeed;                                     // the attack speed
		public int Block;                                           // currently not used
		public int Armor;                                           // the armor modifier
		public int Stamina;                                         // the actual rage value
		public int Strength;                                        // another value that affect the attack power
        public GameObject prefeb;                                   // prefeb of the item that attack to a gameobject
        public int price;                                           // price of item
        public int gemSlot;                                         // currently only 2 different gem slot, 1 for AOE, 2 for projectile
        public int Cooldown;                                        // cooldown that will reduce the cooldown of a skill
        public int Lifesteal;                                       // add lifesteal to a skill
        public int MagicResist;                                     // add magicResist
        public int Health;                                          // Add Health
        public int Range;                                           // add range of a skill
        public int RageGen;                                         // add regeGeneration to a skill
        public int Radius;                                          // add Radius to a skill
        public int LastTime;                                        // elongate the lasttime of a skill
        
	}
}