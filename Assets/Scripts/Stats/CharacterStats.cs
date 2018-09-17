using UnityEngine;

/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hagen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # CharacterStats.cs
  # 
*-----------------------------------------------------------------------*/

/*
* OnHealthReachedZero: delegate to be called when health reaches zero
* OnDeath: delegate to be called on death
* maxHealth: maximum health
* currentHealth: current health
* damage: damage stat
* armor: armor stat
*
*/


/*
 Creator: Myles, shane Weerasuriya
 */

public class CharacterStats : MonoBehaviour {

	//public event System.Action OnHealthReachedZero;
	//public event System.Action OnDeath;

	public float maxHealth = 100;
	//public float currentHealth{ get; set; } 
	public float currentHealth;

	public Stat damage;
	public Stat armor;
	// set currentHealth to maxHealth
	public virtual void Awake ()
	{
		//Debug.Log ("MAXHEALTH IN cStats AWAKE: " + maxHealth);
		currentHealth = maxHealth;       
	}

	/*
	 * Function: TakeDamage
	 * Parameter: damage value to be taken
	 * Description: takes damage value and subtracts armor and subtracts total
	 * from current health. If health goes to 0 or below then call delegate functions
	 * to handle death. Drop loot and play death animation for enemies or reload level
	 * if player dies
	 */
	public virtual void TakeDamage (int damage)
	{
		//damage *= (1-armor.GetValue()/(armor.GetValue() + 100));
		//Debug.Log(armor.GetValue());
		damage -= (int)(armor.GetValue() / 4);
        if (damage <= 0)
            damage = 1;
		damage = Mathf.Clamp(damage, 0, int.MaxValue);
		currentHealth = Mathf.Clamp(currentHealth - damage, 0, currentHealth);

		//Debug.Log (transform.name + "currentHealth" + currentHealth);
		//Debug.Log (transform.name + " takes " + damage + " damage.");
		//if (currentHealth <= 0) {
		/*if (currentHealth <= 0) {

			if (OnDeath == null) 
				Die ();
			
			if (OnDeath != null) {
				OnDeath ();
				//Call delay here. Just for animation for death
				//Take out attack delay
			}
			if (OnHealthReachedZero != null) {
				OnHealthReachedZero ();
			}
		}*/
	}

	/*
	 * Function: Die
	 * 
	 * Description: death function, meant to be overwritten
	 */
	public virtual void Die ()
	{
		// Die in some way
		// This method is meant to be overwritten
		//Debug.Log(transform.name + " died.");
	}

	/*
	 * Function: Heal
	 * Paramter: integer amount to be healed
	 * Description: and heal amount to current health, but do not exceed maxHeallth
	 */ 
	public void Heal (int amount)
	{
		currentHealth += amount;
		currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
	}
}
