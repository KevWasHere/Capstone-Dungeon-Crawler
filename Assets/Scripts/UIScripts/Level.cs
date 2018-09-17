using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone
  # CharMenuControl.cs
  # Dialogue
*-----------------------------------------------------------------------*/
/*
Creator: Yan Zhang, Yunzheng Zhou, Tianqi Xiao
the Level contain the all the data need for player to level up
targetExp: the target experience for next level
damage: the damage for this level
armor: the armor for this level
Rage: the rage for this level
AttackSpeed: the attack speed for this level
Colldown: the cooldown for this level
MagicResist: the magic resist for this level
health: the health for this level
*/

[Serializable]
public class Level: MonoBehaviour {
	public int targetExp;
	public Stat damage;
	public Stat armor;

	public Stat Rage;
	public Stat AttackSpeed;
	public Stat Cooldown;
	public Stat MagicResist;
	public Stat Health;
}
