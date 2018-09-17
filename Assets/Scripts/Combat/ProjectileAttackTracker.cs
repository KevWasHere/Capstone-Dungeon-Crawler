/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # ProjectileAttackTracker.cs
  # Projectile attack script that moves and tracks enemy if close by
*-----------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * SENSOR_ANGLE - Sensor Angle limit of projectile tracking
 * MAX_TURN - Maximum Rate of turn of projectile
 * MIN_COSINE - Cosine of the sensor angle
 * rb - Rigidbody of projectile
 * target - target point projectile follows
 * speed - speed of projectile
 */
/*
 * Creator: Kevin Ho, Myles Hagen, Shane Weerasuriya
 */
public class ProjectileAttackTracker : MonoBehaviour {
	private const float SENSOR_ANGLE = 180.0f;
	private const float MAX_TURN = 60.0f;

	private static float MIN_COSINE = Mathf.Cos (SENSOR_ANGLE * Mathf.Deg2Rad);
	
	private Rigidbody rb;
	public float speed;
	
	public Transform target = null;
	
	void Start(){
		rb = GetComponent<Rigidbody>();
		//Shots constantly move forward at a set speed
		rb.velocity = (transform.forward * speed);
	}
	
	/*
	void FixedUpdate() {
		//Distance between follower plane and target
		Vector3 distance = target.position - transform.position;
		Vector3 heading = rb.velocity;
		
		//Follower plane rotates towards target
		float cos = Vector3.Dot(distance.normalized, heading.normalized);
		if (cos >= MIN_COSINE) {
			rb.velocity = Vector3.RotateTowards(heading, distance, MAX_TURN * Time.deltaTime, 0.0f);
		}
		
		//Rotate follower plane to face target
		//transform.LookAt(target);
	}
	*/
	
	public void Tracking(Transform target) {
		//Distance between projectile and target
		Vector3 distance = target.position - transform.position;
		Vector3 heading = rb.velocity;
		
		//Projectile rotates towards target
		float cos = Vector3.Dot(distance.normalized, heading.normalized);
		if (cos >= MIN_COSINE) {
			rb.velocity = Vector3.RotateTowards(heading, distance, MAX_TURN * Time.deltaTime, 0.0f);
		}
	}
}
