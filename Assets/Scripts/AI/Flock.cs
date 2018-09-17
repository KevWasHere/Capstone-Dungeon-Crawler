using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hagen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # Flock.cs
*-----------------------------------------------------------------------*/

/* script that controls the individual members of the flock, keeps members from
 * colliding, keeeps members moving towards center, and towards a target location.
 * 
 * Creator: Myles Hagen, Shane Weerasuriya , Tianqi Xiao
/*
 * speed - speed of flock members
 * rotationSpeed - rotation speed of flock members
 * test - position of flock center
 * neighbourDistance - distance to neighbours
 * turning - bool to control if flock member is turning
 * dist - distance between the player and flock member
 * range - range that flock members will be attracted to eachother
 */

public class Flock : MonoBehaviour {

    public float speed = 5.0f;
    float rotationSpeed = 4.0f;
    Vector3 test;
    float neighbourDistance = 3.0f;
    bool turning = false;
    float dist;
    float range = 5f;

	// insitialize speed and center position
	void Start ()
    {
        test = aiFlock.ac.position;
        speed = Random.Range(5.0f, 1);
	}
	
	// keep flock members within boundaries, applyrules randomly, and translate members at given speed
	void Update ()
    {
        inRange();

        if (Vector3.Distance(transform.position, test) >= aiFlock.areaSize)
        {
            turning = true;
        }
        else
        {
            turning = false;
        }

        if (turning)
        {
            Vector3 directionn = test - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(directionn), rotationSpeed * Time.deltaTime);
            speed = Random.Range(0.5f, 1);
        }
        else
        {
            if (Random.Range(0, 5) < 1)
                ApplyRules();
        }
        transform.Translate(0, 0, Time.deltaTime * speed);
    }

	/*
	 * Function: inRange 
	 * Description: track distance between flock member and player
	 * 
	 */
    public void inRange()
    {
        dist = Vector3.Distance(Player.instance.transform.position, transform.position);

        if (dist <= range)
        {
            //Debug.Log("IN RANGE");
        }
    }

	/*
	 * Function: ApplyRules
	 * Description: apply the rules of flock behavior, keeps flock members
	 * from colliding, keeps them moving toward center, and keeps them moving
	 * toward goal position, the three rules in conjunction creates interesting flock/swarm
	 * behavior.
	 * 
	 */
    void ApplyRules()
    {
        GameObject[] gos;
        gos = aiFlock.allObjects;
        Vector3 vcenter = test;
        Vector3 vavoid = test;
        float gSpeed = 0.1f;

        Vector3 goalPos = aiFlock.goalPos;
        float dist;
        int groupSize = 0;
        foreach (GameObject go in gos)
        {
            if (go != gameObject)
            {
                dist = Vector3.Distance(go.transform.position, transform.position);
                if (dist <= neighbourDistance)
                {
                    vcenter += go.transform.position;
                    groupSize++;

                    if (dist < 1.0f)
                    {
                        vavoid = vavoid + (transform.position - go.transform.position);
                    }

                    Flock anotherFlock = go.GetComponent<Flock>();
                    gSpeed = gSpeed + anotherFlock.speed;
                }
                
            }
        }
        //Debug.Log("Group size: " + groupSize);
        if (groupSize > 0)
        {
            vcenter = vcenter / groupSize + (goalPos - transform.position);
            speed = gSpeed / groupSize;
            Vector3 direction = (vcenter + vavoid) - transform.position;
            if (direction != test)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
        }
    }
}
