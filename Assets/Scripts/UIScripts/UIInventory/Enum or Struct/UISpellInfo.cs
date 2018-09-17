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
     * serializable class that contains the spell info
     */
	[Serializable]
	public class UISpellInfo
	{
		public int ID;                      // id of the spell
		public string Name;                 // name of the spell
		public Sprite Icon;                 // icon of the spell
		public string Description;          // description of the spell
		public float Range;                 // range of the spell
		public float Cooldown;              // Cooldown of the spell
		public float CastTime;              // casttime of the spell
		public float PowerCost;             // powercost of the spell
	
		[BitMask(typeof(UISpellInfo_Flags))]
		public UISpellInfo_Flags Flags;
	}
}