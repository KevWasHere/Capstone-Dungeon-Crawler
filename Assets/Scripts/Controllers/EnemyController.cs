/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hagen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # EnemyController.cs
  # Controller for enemy NPCs
*-----------------------------------------------------------------------*/

using UnityEngine;
using UnityEngine.AI;

/*
 * Creator: Tianqi Xiao, Shane Weerasuriya, Yan Zhang
 */

/*
 * target: target transform (set to play in start) 
 * agent: reference to NavMeshAgent
 * combat: reference to CharacterCombat
 * rbody: reference to enemy Rigidbody
 * eRegistry: reference to EnemyRegistry
 * registry: reference to Registry GameObject
 * sensor: how sensitive is the turn
 * maxAngle: the max angle for unit turning 
 * minAngle: the min angle for unit turning 
 * range: the range of activating attack
 * dist: the distance between enemy and player
 * enemyAttackClips: reference to a sound clip for enemy
 * nextEnemyAttack: the gap between previous and next attack
 * enemyAttackRate: the speed of attack
 */
public class EnemyController : MonoBehaviour
{
	[HideInInspector]
	public Transform player;           //player's position.
	NavMeshAgent nav;           //the nav mesh agent.
	CharacterCombat combat;

	Rigidbody rbody;
    EnemyRegistry eRegistry;
    GameObject registry;
	private const float sensor = 30.0f;
    private const float maxAngle = 60.0f;
	private static float minAngle = Mathf.Cos(
        sensor * Mathf.Deg2Rad);

	public float range = 10f;
	[HideInInspector]
	public float dist;
	
	//Audio
	//AudioSource audioSource;
	public AudioClip[] enemyAttackClips;
	
	private float nextEnemyAttack;
	private float enemyAttackRate = 1.0f;


    // Use this for initialization
    void Start ()
	{
		//audioSource = GetComponent<AudioSource>();
		rbody = GetComponent<Rigidbody>();
        player = Player.instance.transform;
		nav = GetComponent <NavMeshAgent> ();
		combat = GetComponent <CharacterCombat> ();

	}

	/* update distance to player each frame, if player is within stopping distance
	* then attack player, also faceplayer if within aggro radius
	*
	*/
	/* Author: Tianqi Xiao, SHane Weerasuriya*/
	void Update ()
	{
        if (!Player.instance.isDead)
		    inRange();		//Check if in range of player. Attack if is
	} 

	/*
	 * Function: FacePlayer
	 * 
	 * Description: rotate enemy to face target with some smoothing
	 * 
	 */
	/*Author: Tianqi Xiao*/
    void FacePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    /*Function: inRange
	* Description: Check if player is within aggro range of enemy
	*/
    /*Author: Kevin Ho*/
    public void inRange() {
		dist = Vector3.Distance (player.position, transform.position);

		if (dist <= range){
			nav.SetDestination (player.position);
			FacePlayer();
			if (dist <= nav.stoppingDistance) {
				attackPlayer();
			}
		}
	}
	
	/*Function: attackPlayer
	* Description: Get player stats and attack player
	*/
	/*Author: Kevin Ho*/
	public void attackPlayer() {
		CharacterStats playerStats = player.GetComponent<CharacterStats>();
		combat.Attack (playerStats);
		if (playerStats != null){
			combat.Attack (playerStats);
			if (Time.time > nextEnemyAttack){
				//Play a randomization of clips in list if muiltple exist
				if (enemyAttackClips.Length > 0) {
					int x = (int)Random.Range(0.0f, enemyAttackClips.Length);
					//Debug.Log(x);
				    AudioSource.PlayClipAtPoint(enemyAttackClips[x], transform.position, 0.6f);
				}
				
				//Update next attack time
				nextEnemyAttack = Time.time + enemyAttackRate;
			}
		}
		FacePlayer();
	}

	/*
	 * Function: OnDrawGizmoSelected
	 * 
	 * Description: draw red sphere around enemy showing attack radius
	 * 
	 */
	/* Author: Shane Weerasuriya */
	void OnDrawGizmosSelected () {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, range);
	}
}
