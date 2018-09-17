using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone
  # CameraControllerUI.cs
  # bossenemy
*-----------------------------------------------------------------------*/

/*
 * Creator: Yan Zhang, Tianqi Xiao
 * itemDB: item database
 * overwirte the die function in enemy parent script
 * make the boss drop the big gem for player to equip in the equipment slot
 */
public class BossEnemy : Enemy {
	public ItemDatabase itemDB;
	//over write the die function 
	public override void Die(){
		//change the condition in all condtion object
		AllConditions.ChangeCondition ("Bring peace back to grave yard");
		//instatiate big gems from item database on the ground
		//set up the positions next to boss
		//ingnore the colliders on them
		GameObject gem = Instantiate(itemDB.items[1].itemPrefab);
		gem.transform.position = this.transform.position;
		Physics.IgnoreCollision(gem.GetComponent<Collider>(), GetComponent<Collider>());
		GameObject gem2 = Instantiate(itemDB.items[3].itemPrefab);
		gem2.transform.position = this.transform.position + new Vector3(2,0,2);
		Physics.IgnoreCollision(gem2.GetComponent<Collider>(), GetComponent<Collider>());
		base.Die ();
	}
}
