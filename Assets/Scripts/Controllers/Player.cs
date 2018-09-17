using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hagen, Shane Weerasuriya,
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # Player.cs
  # This class just makes it faster to get certain components on the player.
*-----------------------------------------------------------------------*/

/*
 * Creator: Myles Hagen, Yunzheng Zhou, Tianqi Xiao
 * isDead: check if the player is dead
 * instance: reference to Player instance
 */

 public class Player : MonoBehaviour {

    #region Singleton
    public bool isDead = false;

	public static Player instance;

	//Call on Awake
	void Awake ()
	{
		instance = this;
	}

	#endregion

	//Initialization for all the essential components for player
	void Start() {
		//playerStats.OnHealthReachedZero += Die;
	}

	public CharacterCombat playerCombatManager;
	public PlayerStats playerStats;
    public PlayerController2 playerController;
    public AreaOfEffectSkillStats aoeSkillStats;
    public ProjectileSkillStats projSkillStats;
    public DashSkillStats dashSkillStats;
    public GameObject deathpanel;

	/*
	 * Creator: Myles Hagen, Yunzheng Zhou, Tianqi Xiao
	 * Manage player's die event and disable PlayerController2
	 */
	public void Die() {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        isDead = true;
        if (deathpanel != null )
            deathpanel.SetActive(true);
        gameObject.GetComponent<PlayerController2>().enabled = false;
        Debug.Log("You Died!");
	}
}
