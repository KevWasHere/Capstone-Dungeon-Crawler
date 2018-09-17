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


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Level3SpikeDamage control the overall damage to the player from spike and fireball
 * if player just enter the colider of spike, take 30 fixed amount of damage
 * if player stays inside of colider then take 15 ever 1 second.
 *
 */
public class Level3SpikeDamage : MonoBehaviour {

    bool enter = false;                                 // boolean value to check if the player is currently enter, then set it to true

    // when player enter the collider, set player to take damage 30
    private void OnTriggerEnter(Collider other)
    {
        if (!enter && other.tag == "Player")
        {
            StartCoroutine("DamageCooldown");
            Player.instance.playerStats.TakeDamage(30);
            enter = true;
        }
    }

    // if player is keeping inside of collider, take 15 damage every 1 second
    private void OnTriggerStay(Collider other)
    {
        if (!enter && other.tag == "Player")
        {
            StartCoroutine("DamageCooldown");
            Player.instance.playerStats.TakeDamage(15);
            enter = true;
        }
    }

    // corountine internal damage cooling down to prevent player die instantly
    IEnumerator DamageCooldown()
    {
        yield return new WaitForSeconds(1f);
        enter = false;
    }
}
