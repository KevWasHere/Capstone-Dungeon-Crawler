/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # AIController.cs
  # AI control for NPC pathing
*-----------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 * speed - speed of path evaluation
 * ctl - Transform of control points of path
 * targetPosition - next target position of path
 * catmull - reference to CatmullRom script
 * u - index of ctl transform
 * agent - reference to AI NavMeshAgent
 * rbody - reference to AI Rigidbody
 * enemyController - reference to enemy controller
 * isEnemy - public bool to set of NPC is enemy or not
 * OnAttack - Calls animator on attacks
 * nextWave - next wave animation
 * waveRate - rate of wave animation
 * nextWaveCall - next wave call
 * waveCallRate - rate of wave call
 * SFXRange - range of SFXRange check
 * player - reference to player transform
 * AIC_State - Enum of the AI states
 * state - AI states
 */
 
 /*
 Creator: Kevin Ho, Myles Hagen, Shane Weerasuriya
 */


[RequireComponent(typeof(CatmullRom))]
public class AIController : MonoBehaviour {
	public float speed = 0.1f;		//Play around with number for different AIs. Longer or shorter depending on length of path. (Default 0.1f)
	public Transform[] ctl;
	private Vector3 targetPosition;
	
	private CatmullRom catmull;
	private float u, previousU;
	
	private bool isStopped = false;
	
	NavMeshAgent agent;     //Reference to AI NavMeshAgent
	
	//Regular Enemy
	private EnemyController enemyController;
	private Enemy enemy;
	
	//Archer Enemy
	private EnemyArcherController enemyArcherController;
	private EnemyArcher enemyArcher;
	
	public bool isEnemy;	//Is NPC an enemy or not
	
	public event System.Action OnWave;
	
	private float nextWave;
	private float waveRate = 2.0f;
	
	private float nextWaveCall;
	private float waveCallRate = 6.0f;
	
	private float SFXRange = 40.0f;
	
	[HideInInspector]
	public Transform player;           //player's position.
	
	//FSM Variables
	private enum AIC_State {START, PATHING, INRANGE, SFX, DEAD};
	private AIC_State state;

	void Start () {
		agent = GetComponent<NavMeshAgent>();
		player = Player.instance.transform;
		//Get Enemy and EnemyController if AI is an enemy
		if (isEnemy) {
			if (this.tag == "Enemy") {
				enemyController = GetComponent<EnemyController>();
				enemy = GetComponent<Enemy>();
			} 
			else if (this.tag == "EnemyArcher") {
				enemyArcherController = GetComponent<EnemyArcherController>();
				enemyArcher = GetComponent<EnemyArcher>();
				agent.stoppingDistance = 1.0f;
			}
		}
		catmull = new CatmullRom(ctl);
		u = 0f;
	}

	void Update() {
		/*
		State machine of the AI NPCs. Will handle start, pathing, player tracking when in range, and playing SFX states (Not using currently) of the AI
		Waiting is not empty for future use
		Creator: Kevin Ho
		*/
		switch(state) {
		//Starting state. Moves straight to pathing
		case AIC_State.START:
			//Debug.Log("State: START");
			state = AIC_State.PATHING;
			break;
		//Pathing state of NPC
		case AIC_State.PATHING:
			//Debug.Log("State: PATHING");
			//If enemy, check if dead or in range of player. Otherwise path
			if (isEnemy && (this.tag == "Enemy")) {
				if (enemy.getIsDead()) {
					state = AIC_State.DEAD;
					return;
				}	
				else if (DistFromPlayer() <= enemyController.range){
					state = AIC_State.INRANGE;
					return;
				}
			} 
			//If enemyArcher, check if dead or in range of player. Otherwise path
			else if (isEnemy && (this.tag == "EnemyArcher")) {
				if (enemyArcher.getIsDead()) {
					state = AIC_State.DEAD;
					return;
				}
				else if (DistFromPlayer() <= enemyArcherController.range){
					state = AIC_State.INRANGE;
					return;
				}
				//Still pathing. Change stopping distance for pathing nodes
				else {
					agent.stoppingDistance = 1.0f;
				}
			}
			//If NPC is a horse, play SFX
			else if (this.name == "Horse") {
				if (inSFXRange()) {
					GetComponent<HorseSFX>().playSFX();
				}
			}
			//If NPC is a villager, play SFX
			else if (this.tag == "Villager") {
				if (inSFXRange()) {
					GetComponent<VillagerSFX>().playSFX();
				}
			}
			pathing();
			break;
		//NPC is in range of player
		case AIC_State.INRANGE:
			//Debug.Log("State: INRANGE");
			//If is an enemy, check if dead or still in range
			if (isEnemy) {
				if (this.tag == "Enemy") {
					if (enemy.getIsDead()) {
						state = AIC_State.DEAD;
						return;
					}
					else if (DistFromPlayer() > enemyController.range){
						state = AIC_State.PATHING;
						return;
					}
					else {
						if (!Player.instance.isDead)
							enemyController.inRange();
					}
				}
				else if (this.tag == "EnemyArcher") {
					if (enemyArcher.getIsDead()) {
						state = AIC_State.DEAD;
						return;
					}
					else if (DistFromPlayer() > enemyArcherController.range){
						state = AIC_State.PATHING;
						return;
					}
					else {
						enemyArcherController.inRange();
					}
				}
			}
			//Else, normal NPC, check if in wave range
			else {
				inWaveRange();
			}
			break;
		//Play SFX (For any future implementations)
		case AIC_State.SFX:
			//Debug.Log("State: SFX");
			//if (this.name == "Horse") {
				//GetComponent<HorseSFX>().playSFX();
			//}
			state = AIC_State.PATHING;
			break;
		//NPC is dead. Waiting state
		case AIC_State.DEAD:
			//Debug.Log("State: DEAD");
			break;
		}
	}
	
	/*
	Function: Enemy Pathing
	Description: Pathing for NPC
	Author: Kevin Ho
	*/
	public void pathing() {
		//Debug.Log(u);		//Use this debug to check pathing speed
		//Currently moving through path
		if ((u += Time.deltaTime * speed) < (ctl.Length)){
			targetPosition = catmull.Evaluate(u);
		}
		//Reached destination. Reset to loop
		else {
			u -= ctl.Length;
		}
		
		if (!inRange()) {
			FaceTarget(targetPosition);			//Look at target points
			
			agent.SetDestination(targetPosition);		//Move through path
		}
		
		//If in range and not a horse
		else {
			if (this.tag != "Horse") {
				state = AIC_State.INRANGE;
			}
		}
	}
	
	/*
	Function: DistFromPlayer
	Description: Check how far NPC is from player
	*/
	public float DistFromPlayer() {
		return Vector3.Distance (player.position, transform.position);
	}
	
	
	/*
	Function: inRange
	Description: Check if player is within wave range of villager
	*/
	public bool inRange() {
		float dist = DistFromPlayer();
		if (dist <= agent.stoppingDistance) {
			return true;
		}
		else {
			return false;
		}
	}
	
	/*
	Function: inSFXRange
	Description: Check if player is within SFX range
	*/
	public bool inSFXRange() {
		float dist = DistFromPlayer();
		if (dist <= SFXRange) {
			return true;
		}
		else {
			return false;
		}
	}
	
	/*
	Function: FaceTarget
	Description: Rotate Ai to face targetPosition
	Parameters: targetPosition - target to face
	*/
	/*Author: Kevin Ho*/
	void FaceTarget (Vector3 targetPosition) {
		Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
	
	 /*
	Function: inWaveRange
	Description: Check if player is within wave range of villager
	*/
    /*Author: Kevin Ho*/
    public void inWaveRange() {
		//Save previous position
		if (!isStopped) {
			previousU = u;
		}
		
		//Player in range
		if (inRange()) {
			isStopped = true;
			//Debug.Log("Wave range!");
			//Play wave animation
			if (OnWave != null) {
				agent.isStopped = true;
				FaceTarget(player.transform.position);	
				//Debug.Log("Waving!");
				if (Time.time > nextWave){
					OnWave();
					//Play Wave SFX
					if (Time.time > nextWaveCall){
						GetComponent<VillagerSFX>().playWaveSFX();
						nextWaveCall = Time.time + waveCallRate;
					}
					nextWave = Time.time + waveRate;
				}
			} 
		}
		//Out of wave range. Return back to pathing
		else { 
			Debug.Log("Out of wave range");
			u = previousU;
			isStopped = false;
			agent.isStopped = false;
			state = AIC_State.PATHING;
		}
	}
}
