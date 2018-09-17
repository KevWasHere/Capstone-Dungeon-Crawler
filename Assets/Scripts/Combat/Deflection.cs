using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone project
  # Deflection.cs
  # Deflection Calculation Script
*-----------------------------------------------------------------------*/

/**
 * Creator: Tianqi Xiao, Yan Zhang, Shane Weerasuriya
 * 
 * ARROW: a constant scale the arrow shooting
 * EPSILON: contant used for calculations
 * target: reference to the hitting target Transform
 * targetRB: reference to the Rigidbody on target
*/
public class Deflection : MonoBehaviour {

    private const float ARROW = 20f;
    private const float EPSILON = 0.0001f;

    public Transform target;
    public Rigidbody targetRB;

	/**
	 * Creator: Shane Weerasuriya
	 * Initialize target and its tranform.
	 */
    public void SetTarget(GameObject ship)
    {
        target = ship.transform;
        targetRB = ship.GetComponent<Rigidbody>();
    }

	/**
	 * Creator: Tianqi Xiao, Yan Zhang, Shane Weerasuriya
	 * Calculation for delection.
	 */
    public Vector3 Deflect()
    {
        Vector3 a = target.position - transform.position;
        Vector3 b = targetRB.velocity;
        float Vz = b.magnitude * Vector3.Dot(a, b);

        float dZ = -a.z;
        if (Mathf.Abs(dZ) < 1e-4)
            dZ = EPSILON;

        float Vx = -a.x / dZ * (Vz - b.z) + b.x;

        return (new Vector3(Vx, 0f, Vz));
    }


}
