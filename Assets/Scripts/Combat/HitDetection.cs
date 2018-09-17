/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # HitDetection.cs
  # Checks for collider collusion and acts accordingly
*-----------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Hit detection. Contains all the code to detect a hit for colliders for
 * the attack skills. Calls character states and does damage accordingly
 * Creator: Kevin Ho, Myles Hagen, SHane Weerasuriya
 */

/*
 * myStats - Object stats
 * enemyStats - Enemy stats
 * playerManager - Instance of object
 * enemyArcher - reference to EnemyArcher script
 * enemy - reference to Enemy script
 * playerMotor - reference to PlayerMotor2 script
 * originalDebuffValue - reference to enemy stats value
*/

public class HitDetection : MonoBehaviour {
	
	CharacterStats myStats;
	EnemyStats enemyStats;
	PlayerManager playerManager;
    EnemyArcher enemyArcher;
    Enemy enemy;
	PlayerMotor2 playerMotor;

	private int originalDebuffValue;

    //Initialize components
    void Start () {
		myStats = GetComponent<CharacterStats>();
		playerMotor = Player.instance.GetComponent<PlayerMotor2>();
		playerManager = PlayerManager.instance;


	}
	
	/*
	Detects a collision between two coliders and acts accordingly
	Parameters: other - collider that hit the target
	Creator: Kevin Ho, Myles Hagen, SHane Weerasuriya
	*/
	void OnTriggerEnter(Collider other){
		//Enemy hit player
		//if (other.tag == "Player"){
			//Debug.Log("ATTACK HIT Player!");
		//}
		//Player hit enemy
		//if (other.tag == "Enemy"){
			//Debug.Log("ATTACK HIT ENEMY:" + other.gameObject.name);
		//}
		//Player attack hit enemy
		if ((other.tag == "PlayerAttack") && (tag != "Player")){
            //Debug.Log("PLAYER ATTACK HIT " + this.name + ": " + other.gameObject.name);
            if (tag == "DestructableObject") {
				GetComponent<Destructable>().DestroyObject();
			}
			//Get stats of the enemy
            else if (tag == "EnemyArcher")
            {
                enemyArcher = GetComponent<EnemyArcher>();
                enemyStats = enemyArcher.getMyStats();
            }
            else
            {
                enemy = GetComponent<Enemy>();
                enemyStats = enemy.getMyStats();
            }

            //Debug.Log("Enemy Attack: " + enemyStats.damage.GetValue());

            //Get stats of the player and deal damage
            //CharacterCombat playerCombat = playerManager.player.GetComponent<CharacterCombat> ();
            CharacterCombat playerCombat = Player.instance.playerCombatManager;
            //Debug.Log(other.name);
            if (other.name == "AOE Attack hitbox (Effect test)(Clone)")
            {
               
                playerCombat.AOESkillAttack(enemyStats);
            }
                
            else if (other.name == "Dash Attack Hitbox(Clone)")
            {
                playerCombat.DashSkillAttack(enemyStats);
            }
			
			else if (other.name == "Debuff Attack Hitbox(Clone)")
			{
				originalDebuffValue = enemyStats.damage.baseValue;
				enemyStats.damage.baseValue -= playerMotor.debuffValue;
				
				//Coroutine to remove debuff
				StartCoroutine(removeDebuff(playerMotor.debuffAttackDestroy));	
			}
                
            else
            {
                playerCombat.ProjSkillAttack(enemyStats);
            }
                
           // CharacterCombat playerCombat = Player.instance.playerCombatManager; // use new Player script rather than playerManager
            
            return;
		}
		//Enemy Archer hit player
		if ((other.tag == "EnemyArcherAttack") && (tag == "Player")){
			//Debug.Log("ENEMY ARCHER HIT " + this.name + ": " + other.gameObject.name);		
			PlayerStats playerStats = Player.instance.playerStats;
            playerStats.TakeDamage(5);
            return;
		}
		//Enemy Mage hit player
		if ((other.tag == "EnemyMageAttack") && (tag == "Player")){
			//Debug.Log("ENEMY MAGE HIT " + this.name + ": " + other.gameObject.name);
			PlayerStats playerStats = Player.instance.playerStats;
            playerStats.TakeDamage(5);
            return;
		}
	}
	
	/*
	Coroutine to wait before removing debuff effect
	Parameters: delay - float of time delay of attack
	Creator: Kevin Ho
	*/
	public IEnumerator removeDebuff(float delay) {
		yield return new WaitForSeconds(delay);
		enemyStats.damage.baseValue = originalDebuffValue;
	}
}
