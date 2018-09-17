
/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # PlayerStatistics.cs
  #
  # Author: Myles Hagen, Yunzheng Zhou
*-----------------------------------------------------------------------*/
using System.Collections.Generic;
using System;

/*
Class representing all relevant player data to be serialized
*/
/*
 * currentgold - current gold of player
 * sceneID - index of scene
 * PositionX, PositionY, PositionZ - floats representing players position
 * HP - player health
 * camPosX, camPosY, camPosZ - floats representing camera position
 * itemIDs - ids of player inventory items
 * equipmentIDs - ids of players equiped items
 * skillAOE_IDs - ids of gems equiped for aoe skill
 * SkillProjectile_IDs - ids of gems equiped for projectile skill
 * SkillDash_IDs - ids of gems equiped for dash skill
 * Potions_IDs - ids of equiped potions
 * currentXP - current experience of player
 * currentRage - current rage of player (resource like mana)
 * level - level of player based on experience
 */

[Serializable]
public class PlayerStatistics
{

    public int currentgold;
    public int SceneID;
    public float PositionX, PositionY, PositionZ;
    public float HP;
    public float camPosX, camPosY, camPosZ;
    public List<int> itemIDs = new List<int>();
    public int[] equipmentIDs = new int[6];
    public int[] SkillAOE_IDs = new int[4];
    public int[] SkillProjectile_IDs = new int[5];
	public int[] SkillDash_IDs = new int[6];
    public int[] Potions_IDs = new int[5];
    public int currentXP;
    public int currentRage;
    public int level;
	public int targetExp;

}
