/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hagen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # FollowerController.cs
  # Follower object that follows Leader object with basic missile controller
*-----------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * SENSOR_ANGLE - Sensor Angle limit of follower tracking
 * MAX_TURN - Maximum Rate of turn for
 * INITIAL_SPEED - Initial speed of the follower
 * MIN_COSINE - Cosine of the sensor angle
 * target - target point follower follows
 * rb - Rigidbody of follower
 * 
 * Creator: Myles Hagen, Kevin Ho, shane weerasuriya
 */

 [RequireComponent(typeof(Rigidbody))]
public class FollowerController : MonoBehaviour {
	private const float SENSOR_ANGLE = 180.0f;
	private const float MAX_TURN = 360.0f;
	private const float INITIAL_SPEED = 3.0f;
	public float speed;

	private static float MIN_COSINE = Mathf.Cos (SENSOR_ANGLE * Mathf.Deg2Rad);

	Transform target;
	private Rigidbody rb;

	// initialize references and set rigidbody velocity
	void Start() {
		target = Player.instance.transform.Find ("missileTarget");
		//target = Player.instance.transform;
		rb = gameObject.GetComponent<Rigidbody>();
		//Set initial speed of follower
		rb.velocity = (transform.forward * INITIAL_SPEED);
		//Physics.IgnoreCollision(this.GetComponent<Collider>(), GetComponent<Collider>());
		//Destroy (gameObject, 10f);
	}

	// move the follower towards target
	void FixedUpdate() {
		//Distance between follower and target leader
		Vector3 distance = target.position - transform.position;
		Vector3 heading = rb.velocity;
		
		//Follower rotates towards target leader
		float cos = Vector3.Dot(distance.normalized, heading.normalized);
		if (cos >= MIN_COSINE) {
			rb.velocity = Vector3.RotateTowards(heading, distance, MAX_TURN * Time.deltaTime, 0.0f);
		}
		
		//Change follower speed based on distance from leader
		/*Vector3 v = rb.velocity;
		v.z = speed;
		rb.velocity = v;*/
		
		//Rotate follower to face target leader
		transform.LookAt(target);
	}
}
