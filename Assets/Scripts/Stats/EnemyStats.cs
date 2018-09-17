/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # EnemyStats.cs
  # Contains all the states for the enemy NPCs
*-----------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Creator: Myles Hagen, Shane Weerasuriya
*/

/*
* health - reference to health gameobject
* OnHeathReachedZero - delegate to be called when health reaches zero
* OnDeath - delegeate to be called on death
* isAlive - bool determining if enemy is alive
*/

public class EnemyStats : CharacterStats {

	//public GameObject grandFartherDialogue;
	public GameObject health;
    public event System.Action OnHealthReachedZero;
    public event System.Action OnDeath;
    bool isAlive = true;

    public int experience;
	private float initialHealth;
    // Use this for initialization
    void Start () {
        if (health != null)
		    initialHealth = health.transform.localScale.x;
	}


	/*
	 * Function: TakeDamage (Overriden from CharacterStats)
	 * Parameters: damage
	 * Description: reduces health by damage amount, if health reaches call delegates
	 * if not null and call death function.
	 * 
	 */
	public override void TakeDamage (int damage)
	{
		base.TakeDamage (damage);
        if (currentHealth <= 0)
        {
            if (isAlive)
            {
                Die();
                if (OnDeath != null)
                {
                    OnDeath();
                }
                if (OnHealthReachedZero != null)
                {
                    OnHealthReachedZero();
                }
            }
            isAlive = false;
        }

		if (health != null) {
			health.transform.localScale = new Vector3 (initialHealth * (currentHealth / maxHealth), health.transform.localScale.y, health.transform.localScale.z);
		}
	}

	/*
	 * Function: Die
	 * Description: death function that destroys healthbar and updates player experience
	 */
	public override void Die ()
	{
		base.Die ();
		//Debug.Log ("in enemy dead");
        Destroy(health);
		Player.instance.playerStats.UpdateExperience (experience);
	}
}
