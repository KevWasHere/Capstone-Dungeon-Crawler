using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone
  # PlayerObject.cs
  # Design for boss to better interact with the player
*-----------------------------------------------------------------------*/

/*
* Creator: Tianqi Xiao
*/
/*
*  animator: reference to enemy Animator Controller
*  rangeSkill: reference to the projectile prefeb
*  firePoint: position for boss to fire projectile
*  combat: reference to CharacterCombat
*/
public class PlayerObject : MonoBehaviour {

	Animator animator;
	public GameObject rangeSkill;
	public GameObject firePoint;
    CharacterCombat combat;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	/**
	 * Creator: Tianqi Xiao
	 * Update Animator Controller attached to the boss
	 */
	void Update () {
		animator.SetFloat("distance", Vector3.Distance(transform.position, Player.instance.transform.position));
	}

	/**
	 * Creator: Tianqi Xiao
	 * Intantiate projectile ranged skill
	 */
	void Fire() {
		GameObject fp = Instantiate(rangeSkill, firePoint.transform.position, firePoint.transform.rotation);
		fp.GetComponent<Rigidbody>().AddForce(firePoint.transform.forward * 500);
	}

	/**
	 * Creator: Tianqi Xiao
	 * Call different Fire functions and repeatly instantiate projectile skill.
	 */
	public void StartFiring() {
		InvokeRepeating("Fire", 0.7f, 2.0f);
	}

	/**
	 * Creator: Tianqi Xiao
	 * Call different Fire functions and repeatly terminate projectile skill.
	 */
    public void StopFiring() {
		CancelInvoke("Fire");
    }

	/**
	 * Creator: Tianqi Xiao
	 * Attack and deal damages to player by calling CharacterStats script.
	 */
    public void AttackPlayer()
    {
        CharacterStats playerStats = Player.instance.playerStats;
        combat.Attack(playerStats);
        if (playerStats != null)
        {
            combat.Attack(playerStats);
        }
		animator.ResetTrigger("skillAttack");
    }
	
}
