using UnityEngine;

/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone

*-----------------------------------------------------------------------*/

/*
 * Author: Yunzheng Zhou, SHane Weerasuriya
 * Date: 2018-04-19
 */
/*
* healthGain: integer amount of health to gain
* player: player gameobject
* stats: reference to PlayerStats
*/

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Consumable")]
public class Consumable : Item {

	public int healthGain;		// How much health?
	public GameObject player;
	PlayerStats stats;
    
	/*
	 * Function: Use
	 * 
	 * Description: get reference to player and playerstats then 
	 * call heal function to heal for amount of healthGain
	 */
	public override void Use()
	{
        // Heal the player
        //player = PlayerManager.instance.player;
        if (this.itemID == 40)
        {
            Player.instance.playerStats.currentHealth += healthGain;
            Player.instance.playerStats.currentHealth = Mathf.Clamp(Player.instance.playerStats.currentHealth, Player.instance.playerStats.currentHealth, Player.instance.playerStats.maxHealth);
		}else if (this.itemID == 52) {
			AllConditions.ChangeCondition ("Drink The Potion");
		}
        //stats = player.GetComponent<PlayerStats>();
        //stats.Heal(healthGain);
        
		//RemoveFromInventory();	// Remove the item after use
	}

}
