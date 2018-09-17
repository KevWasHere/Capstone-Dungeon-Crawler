/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hagen, Shane Weerasuriya, 
  #                 Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # 
*-----------------------------------------------------------------------*/

// Creator Shane Weerasuriya, Kevin Ho

using UnityEngine;
using UnityEngine.AI;

public class AControllerBackUp : MonoBehaviour
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

    private void Update()
    {
        /*Vector3 shooterPosition = transform.position;
        Vector3 targetPosition = target.transform.position;
        //velocities
        Vector3 shooterVelocity = rbody ? rbody.velocity : Vector3.zero;
        Vector3 targetVelocity = targetRB ? targetRB.velocity : Vector3.zero;

        //calculate intercept
        Vector3 interceptPoint = FirstOrderIntercept
        (
            shooterPosition,
            shooterVelocity,
            shotSpeed,
            targetPosition,
            targetVelocity
        );*/

        Vector3 delta = transform.position - target.position;
        Vector3 vr = targetRB.velocity - rbody.velocity;

        //Debug.Log("target v " + targetRB.velocity + " this v " + rbody.velocity);
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

        //Vector3 lead = CalculateLead();

        //Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(aimPoint);
        Vector3 rotation = lookRotation.eulerAngles;
        //Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        }
    }


    /*void FixedUpdate()
    {
        // Find the relative position and velocities
        Vector3 delta = targetRB.transform.position - rbody.transform.position;
        Vector3 vr = targetRB.velocity - rbody.velocity;
        
        //Debug.Log("target v " + targetRB.velocity + " this v " + rbody.velocity);
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
            //Vector3 aimPos = (target.transform.position + transform.position) * .9f;
        Vector3 relativePos = target.transform.position - transform.position;
        //Debug.Log("Deflect " + Deflect() + "relativePos " + relativePos);
        Quaternion rotation = Quaternion.LookRotation(aimPoint);
        transform.rotation = rotation;
        if (Time.time > nextFire)
       {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
       }

    }*/

    /*void FacePlayer()
    {
        Vector3 dir = target.position - transform.position;
        Vector3 heading = rbody.velocity;
        float cosine = Vector3.Dot(dir.normalized, heading.normalized);
        if (cosine >= minAngle)
        {
            rbody.velocity = Vector3.RotateTowards(heading, dir, maxAngle * Time.deltaTime, 0f);
        }
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }*/

    public Vector3 Deflect()
    {
        Vector3 a = target.transform.position - transform.position;
        Vector3 b = targetRB.velocity;
        //Debug.Log(b);
        float Vz = b.magnitude * Vector3.Cross(a, b).magnitude;

        float dZ = -a.z;
        if (Mathf.Abs(dZ) < 1e-4)
            dZ = EPSILON;

        float Vx = -a.x / dZ * (Vz - b.z) + b.x;

        return (new Vector3(Vx, 0f, Vz));
    }

    // Calculate the time when we can hit a target with a bullet
    // Return a negative time if there is no solution
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

    public static Vector3 FirstOrderIntercept
(
    Vector3 shooterPosition,
    Vector3 shooterVelocity,
    float shotSpeed,
    Vector3 targetPosition,
    Vector3 targetVelocity
)
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
    //first-order intercept using relative target position
    public static float FirstOrderInterceptTime
    (
        float shotSpeed,
        Vector3 targetRelativePosition,
        Vector3 targetRelativeVelocity
    )
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
