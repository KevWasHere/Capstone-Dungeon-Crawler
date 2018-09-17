using UnityEngine;
using UnityEngine.AI;

/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # EnemyArcherController.cs
  # Controller for enemy archer NPCs
*-----------------------------------------------------------------------*/

/*
 * player - player's position.
 * nav - archer's navmeshagent.
 * playerNav - players navmeshagent
 * combat - reference to charactercombat script
 * range - archer's range
 * archerAttack - reference to archerAttack script
 * anim - reference to animator script
 * speed - speed of projectile
 * dist - distance between enemy and player
*/

/*
Creator: Myles Hangen, Kevin Ho, Yang Zhang
*/

public class EnemyArcherController : MonoBehaviour
{
	Transform player;           // player's position.
	NavMeshAgent nav;           // archer's navmeshagent.
    NavMeshAgent playerNav;     // players navmeshagent
	CharacterCombat combat;     // reference to charactercombat script
    public float range = 20f;   // archer's range
	ArcherAttack archerAttack;  // reference to archerAttack script
    CharacterAnimator anim;     // reference to animator script
    public float speed;     	// speed of projectile
	
	[HideInInspector]
	public float dist;
	
    /*
     * initialize references to other scripts/gameobjects
     */ 
	void Start ()
	{
        player = Player.instance.transform;
		nav = GetComponent <NavMeshAgent> ();
		combat = GetComponent <CharacterCombat> ();
        playerNav = Player.instance.GetComponent<NavMeshAgent>();
		archerAttack = GetComponent<ArcherAttack>();
        anim = GetComponent<CharacterAnimator>();
	}

    /*
     *  Detect distance to player, if player outside of range then move towards
     *  player. When in range, the Deflect function is called to calculate lead
     *  required for arrow to hit a moving target. resulting Vector3 is passed to
     *  lookrotation, which rotates the archer to correct position, fireArcherAttack
     *  is then called to launch a projectile at the player.
     * 
     */
	void Update ()
	{
        if (!Player.instance.isDead)
		    inRange();
	}
    
    /*
     * Calculates the Vector3 that must be rotated to so that the projectile
     * will hit a moving target. Solving for a triangle to find the vector that 
     * intersects where the player will be based on speed of player and speed
     * of projectile as well as the distance between the player and archer.
     * 
     */
    public Vector3 Deflect()
    {
        Vector3 dz = player.position - transform.position;
        Vector3 Ve = playerNav.velocity;
        float Vm = speed;
        float sinTheta = Vector3.Cross(Ve.normalized, dz.normalized).y;
        float sinphi = Ve.magnitude / Vm * sinTheta;
        float phi = Mathf.Asin(sinphi);
        Vector3 missileForward = dz.normalized * Vm;
        Quaternion roto = Quaternion.Euler(0, -phi * Mathf.Rad2Deg, 0);
        return roto * missileForward;

    }
	
	void FacePlayer() {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
	
	/*Function: inRange
	* Description: Check if player is within aggro range of enemy
	*/
    /*Author: Kevin Ho, Yang Zhang*/
    public void inRange() {
		dist = Vector3.Distance (player.position, transform.position);

        if (dist <= range) {
			nav.stoppingDistance = 15.0f;
			nav.SetDestination (player.position);
			FacePlayer();
			if (dist <= nav.stoppingDistance) {
				attackPlayer();
			}
		}
	}
	
	/*Function: attackPlayer
	* Description: Attack player
	*/
	/*Author: Kevin Ho, Yang Zhang*/
	public void attackPlayer() {
		Quaternion lookRotation = Quaternion.LookRotation(Deflect());
		Vector3 rotation = lookRotation.eulerAngles;
		
		transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
		PlayerStats playerStats = Player.instance.playerStats;

		if (playerStats != null) {
			archerAttack.fireArcherAttack();
		}
	}


    /*
     * draw red sphere indicating the attack range of the archer
     */
	void OnDrawGizmosSelected () {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, range);
	}
}
