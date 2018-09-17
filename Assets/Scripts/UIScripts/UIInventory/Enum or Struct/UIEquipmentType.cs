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
using System.Collections;

namespace UnityEngine.UI
{

    /*
     * Serialized enum that contain the UI equipment type
     * currently the gem and health potion are all equipment type
     * And due to limit of our game, we set less equipment type so no bracer and etc. for now
     */
	[SerializeField]
	public enum UIEquipmentType : int
	{
		None = 0,                       // none type of equipment
		Weapon_MainHand = 1,            // main-hand weapon type
		Weapon_OffHand = 2,             // off-hand weapon type
		Head = 3,                       // head type
		Necklace = 4,                   // Necklace type
		Shoulders = 5,                  // Shoulder type
		Chest = 6,                      // Chest type
		/*Bracers = 7,
		Gloves = 8,
		Belt = 9,
		Pants = 10,
		Boots = 11,
		Trinket = 12,*/
        BigGem = 7,                     //  BigGem type
        SmallGem = 8,                   //  SmallGem type
        healthpotion = 9                //  healthpotion type
	}

}