/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 330 Final Project
  # OutfitterTest.cs
  # //////////ADD REAL COMMENTS HERE LATER!!!
*-----------------------------------------------------------------------*/

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

/*
 * ac - Reference to PlayerController
 * oldWeaponIndex - 
 * weapons - 
*/
/*
Creator: Shane Weerasuriya, Yunzheng Zhou
*/

[Serializable]
public class OutfitterTest : MonoBehaviour 
{
	
	PlayerController2 ac;
	int oldWeaponIndex;
	[SerializeField]
	public List<WeapSlot> weapons;
	
	// Use this for initialization
	void Start () 
	{
		//Debug.Log ("weapon" + weapons [2]);
		ac = GetComponentInChildren<PlayerController2>();
		for(int i = 0;i<weapons.Count;i++)
		{
			for(int model=0;model<weapons[i].models.Count;model++)
			{
				weapons[i].models[model].enabled = false;
			}
		}
		for(int model=0;model<weapons[ac.WeaponState].models.Count;model++)
		{
			weapons[ac.WeaponState].models[model].enabled = true;
		}
		oldWeaponIndex=ac.WeaponState;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(ac.WeaponState!=oldWeaponIndex)
		{
			for(int model=0;model<weapons[oldWeaponIndex].models.Count;model++)
			{
				weapons[oldWeaponIndex].models[model].enabled = false;
			}
			for(int model=0;model<weapons[ac.WeaponState].models.Count;model++)
			{
				weapons[ac.WeaponState].models[model].enabled = true;
			}
			oldWeaponIndex=ac.WeaponState;
		}
	}
}
[Serializable]
public class WeapSlot
{
	[SerializeField]
	public List<Renderer> models = new List<Renderer>();
}
