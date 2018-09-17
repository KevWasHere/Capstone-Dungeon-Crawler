using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone
  # BossProjectileMover.cs
  # Projectile movement controller
*-----------------------------------------------------------------------*/

/**
 * Creator: Yunzheng Zhou, Tianqi Xiao
 * 
 * K: a constant scale the movement
 * SPEED: initial value of movement speed
 * K1: a constant scale the movement
 * target: reference to the hitting target Transform
 * rbody: reference to the Rigidbody
 * previous: previous position
 * targetVelocity: the velocity of the hitting target
 */
public class BossProjectileMover : MonoBehaviour
{
    private const float K = 0.0075f;
    private const float SPEED = 20f;
    private const float K1 = 1f - K;
    public Transform target;
    private Rigidbody rbody;
    Vector3 previous;
    private float targetVelocity;
        
	/**
	 * Creator: Yunzheng Zhou
     * Initialization. Sets up the follower rigidbody and the initial velocity
     * and also the position of leading plane
     */
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rbody = gameObject.GetComponent<Rigidbody>();
        rbody.velocity = new Vector3(0f, 0f, SPEED);
        previous = target.position;
    }

    /**
     * Creator: Yunzheng Zhou, Tianqi Xiao
     * Calculates the direction to the leading plane and the follower's current heading.  
     * Calculates the velocity of leading plane by preious leading plane's position and current position.
     * The distance between leading plane and follower is used to determine whether the target is too far away from follower or not. 
     * If the target is too far away, the follwer will speed up until 60f away the leading plane then keep flying at the minmum speed.
     * The speed of follower also speed up if leading plane speed up
     */
     void Update()
     {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        
        Vector3 location = target.position - transform.position;
         Quaternion targetHeading = Quaternion.LookRotation(location, target.up);
         targetVelocity = ((target.position - previous).magnitude) / Time.deltaTime - 14;
         targetVelocity = Mathf.Clamp(targetVelocity, 10, 50);

         float distance = Vector3.Distance(target.position, transform.position);
         targetVelocity = 10f;
         previous = target.position;
         transform.rotation = Quaternion.Lerp(transform.rotation, targetHeading, K1);
         rbody.velocity = transform.forward * targetVelocity;
     }

}