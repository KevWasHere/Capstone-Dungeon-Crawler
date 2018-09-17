/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # ProjectileAttackSensor.cs
  # Projectile attack sensor for projectile attack tracker
*-----------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * eye - transform of sensor object
 * target - transform of sensor target
 * targets - list of sensor transforms of targets for tracking
 * sensor - reference to SNSBase sensor script
 * tracker - reference to ProjectileAttackTracker
 * EnemyTags - array of target tags for sensor to look for 
 */
 
 /*
 Creator: Kevin Ho
 */
 
[RequireComponent(typeof(ProjectileAttackTracker))]
public class ProjectileAttackSensor : MonoBehaviour {
	public Transform eye;

	private Transform target;
	//private Renderer myRenderer;
	
	private List<Transform> targets = new List<Transform>();

	private SNSBase sensor;
	
	ProjectileAttackTracker tracker;		// Reference to tracker
	
	//Enemy tags sensor searches for. Add new tags for new enemies
	string[] EnemyTags = {
		"Enemy",
		"EnemyArcher",
	};

	void Start () {
		//Maybe change this to game object later		
		foreach (string enemy in EnemyTags) {
			targets.Add(GameObject.FindWithTag(enemy).transform);
		}

		//myRenderer = GetComponent<Renderer>();

		sensor = eye.GetComponent<SNSBase>();
		
		tracker = GetComponent<ProjectileAttackTracker>();
	}
	
	void Update () {
		//myRenderer.material.color = sensor.CanSee(target)?Color.red:Color.green; // Use of sensor
		foreach (Transform x in targets){
			target = x;
			if (sensor.CanSee(target)) {
				Debug.Log("Found Target!");
				tracker.Tracking(target);
			}
		}
	}
	
	Transform getTarget() {
		return target;
	}
}