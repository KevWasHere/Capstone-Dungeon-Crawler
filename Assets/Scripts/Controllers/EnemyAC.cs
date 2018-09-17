using UnityEngine;
using UnityEngine.AI;

/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hagen, Shane Weerasuriya, 
  #                 Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # EnemyAC.cs
  # The controller for Archor Enemy
*-----------------------------------------------------------------------*/

/*
* Creator: Shane Weerasuriya, Tianqi Xiao, Kevin Ho
* 
* target: reference to the target Transform
* nav: reference to the NavMeshAgent attached
* combat: reference to CharacterCombat class
* rbody: reference to Rigidbody attached
* targetRB: reference to target's Rigidbody
* targetNV: reference to target's NavMeshAgent
* sensor: the sensor of turning angle
* maxAngle: the max turning angle
* minAngle: the min turning angle
* range: the range of attack
* shot: reference to Shot projectile game object
* shotSpawn: reference to shot spawn point Transform
* fireRate: the rate of firing
* nextFire: the gap between previous fire and the next fire
* EPSILON: contant used for calculations
* muzzleV: the amount of muzzle
* aimPoint: the position for aiming
* turnSpeed: the speed of turning
* showSpawn: reference to showSpawn game object
*/
public class EnemyAC: MonoBehaviour
{
    public Transform target;           //player's position.
    //public GameObject tar;
    NavMeshAgent nav;           //the nav mesh agent.
    CharacterCombat combat;

    Rigidbody rbody;
    Rigidbody targetRB;
    NavMeshAgent targetNV;
    private const float sensor = 30.0f;
    private const float maxAngle = 60.0f;
    private static float minAngle = Mathf.Cos(
        sensor * Mathf.Deg2Rad);

    public float range = 10f;
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;

    private float nextFire;
    private const float EPSILON = 0.0001f;

    //public GameObject target;
    public float muzzleV = 15f;
    Vector3 aimPoint;
    public float turnSpeed = 10f;
    public GameObject showSpawn;
    //public float shotSpeed = 30f;
   

    // Use this for initialization
    void Start()
    {
        //player = PlayerManager.instance.player.transform;
        rbody = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
        targetRB = target.GetComponent<Rigidbody>();
        targetNV = target.GetComponent<NavMeshAgent>();


    }

	/**
	 * Creator: Shane Weerasuriya, Tianqi Xiao, Kevin Ho
	 * Use FixedUpdate() for Rigidbody use. Fire projectile and add gap between each shot.
	 */
    void FixedUpdate() {

        Vector3 delta = transform.position - target.position;
        Vector3 vr = targetRB.velocity - rbody.velocity;

        // Calculate the time a bullet will collide
        // if it's possible to hit the target.
        float t = AimAhead(delta, vr, muzzleV);

        // If the time is negative, then we didn't get a solution.
        if (t > 0f)
        {
            // Aim at the point where the target will be at the time
            // of the collision.
            //Debug.Log(t);
            aimPoint = target.transform.position + t * vr;
        }

        Quaternion lookRotation = Quaternion.LookRotation(Deflect());
        Vector3 rotation = lookRotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        }
    }
	
	/**
	 * Creator: Tianqi Xiao, Shane Weerasuriya
	 * Calculation for deflection.
	 */
    public Vector3 Deflect()
    {
        Vector3 dz = target.position - transform.position;
        Vector3 Ve = targetRB.velocity;
        float Vm = 20f;
        //float theta = Vector3.Angle(Ve, dz);
        float sinTheta = Vector3.Cross(Ve.normalized, dz.normalized).y;
        float sinphi = Ve.magnitude / Vm * sinTheta;
        float phi = Mathf.Asin(sinphi);
        Vector3 missileForward = dz.normalized * Vm;
        Quaternion roto = Quaternion.Euler(0, -phi * Mathf.Rad2Deg, 0);
        return roto * missileForward;
    }

    // Calculate the time when we can hit a target with a bullet
    // Return a negative time if there is no solution
	//Creator: Kevin Ho, Shane Weerasuriya
    protected float AimAhead(Vector3 delta, Vector3 vr, float muzzleV)
    {
        // Quadratic equation coefficients a*t^2 + b*t + c = 0
        float a = Vector3.Dot(vr, vr) - muzzleV * muzzleV;
        float b = 2f * Vector3.Dot(vr, delta);
        float c = Vector3.Dot(delta, delta);

        float det = b * b - 4f * a * c;

        // If the determinant is negative, then there is no solution
        if (det > 0f)
        {
            return 2f * c / (Mathf.Sqrt(det) - b);
        }
        else
        {
            return -1f;
        }
    }
	
	/* 
     * Creator: Kevin Ho, Shane Weerasuriya
     * First-order intercept using relative target position
     */
    public static Vector3 FirstOrderIntercept(Vector3 shooterPosition,
    										Vector3 shooterVelocity,
    										float shotSpeed,
    										Vector3 targetPosition,
    										Vector3 targetVelocity)
    {
        Vector3 targetRelativePosition = targetPosition - shooterPosition;
        Vector3 targetRelativeVelocity = targetVelocity - shooterVelocity;
        float t = FirstOrderInterceptTime
        (
            shotSpeed,
            targetRelativePosition,
            targetRelativeVelocity
        );
        return targetPosition + t * (targetRelativeVelocity);
    }

    /* 
     * Creator: Kevin Ho, Tianqi Xiao 
     * First-order intercept  time using relative target position
     */
    public static float FirstOrderInterceptTime(float shotSpeed, Vector3 targetRelativePosition,
    											Vector3 targetRelativeVelocity)
    {
        float velocitySquared = targetRelativeVelocity.sqrMagnitude;
        if (velocitySquared < 0.001f)
            return 0f;

        float a = velocitySquared - shotSpeed * shotSpeed;

        //handle similar velocities
        if (Mathf.Abs(a) < 0.001f)
        {
            float t = -targetRelativePosition.sqrMagnitude /
            (
                2f * Vector3.Dot
                (
                    targetRelativeVelocity,
                    targetRelativePosition
                )
            );
            return Mathf.Max(t, 0f); //don't shoot back in time
        }

        float b = 2f * Vector3.Dot(targetRelativeVelocity, targetRelativePosition);
        float c = targetRelativePosition.sqrMagnitude;
        float determinant = b * b - 4f * a * c;

        if (determinant > 0f)
        { //determinant > 0; two intercept paths (most common)
            float t1 = (-b + Mathf.Sqrt(determinant)) / (2f * a),
                    t2 = (-b - Mathf.Sqrt(determinant)) / (2f * a);
            if (t1 > 0f)
            {
                if (t2 > 0f)
                    return Mathf.Min(t1, t2); //both are positive
                else
                    return t1; //only t1 is positive
            }
            else
                return Mathf.Max(t2, 0f); //don't shoot back in time
        }
        else if (determinant < 0f) //determinant < 0; no intercept path
            return 0f;
        else //determinant = 0; one intercept path, pretty much never happens
            return Mathf.Max(-b / (2f * a), 0f);
    }
	
	/*
	 * Creator: Tianqi Xiao, Shane Weerasuriya
	 * Calculate the lead amount to predicate player's movement. This will increase the 
	 * accuracy of projectile.
	 */
    Vector3 CalculateLead()
    {
        Vector3 V = targetRB.velocity;
        Vector3 D = target.position - transform.position;
        float A = V.sqrMagnitude - muzzleV * muzzleV;
        float B = 2 * Vector3.Dot(D, V);
        float C = D.sqrMagnitude;
        if (A >= 0)
        {
            Debug.LogError("No solution exists");
            return target.position;
        }
        else
        {
            float rt = Mathf.Sqrt(B * B - 4 * A * C);
            float dt1 = (-B + rt) / (2 * A);
            float dt2 = (-B - rt) / (2 * A);
            float dt = (dt1 < 0 ? dt2 : dt1);
            return target.position + V * dt;
        }
    }
}

