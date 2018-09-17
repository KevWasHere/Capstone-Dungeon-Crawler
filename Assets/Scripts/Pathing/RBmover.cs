using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script is currently not used
 * just for small testing, maybe for later use.
 */
public class RBmover : MonoBehaviour {

    public Transform[] points;
    //public Vector3 teleportPoint;
    public Rigidbody rb;
    bool p7, p8;
    public float delay = 4f;
   
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, 5f);
    }
    void FixedUpdate()
    {
        //Debug.Log(rb.velocity);
        /*if (p7)
            StartCoroutine(DelayMove(delay, -5f));
        if (p8)
            StartCoroutine(DelayMove(delay, 5f));
            */
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == points[0].name)
        {
            //Debug.Log("P7");
            p7 = true;
            p8 = false;
            //rb.AddForce(transform.forward * -20f);
            rb.velocity = new Vector3(0, 0, -5f);
            //StartCoroutine(DelayMove(delay, -5f));
        }
        if (other.name == points[1].name)
        {
            p7 = false;
            p8 = true;
            //Debug.Log("P8");
           //rb.AddForce(transform.forward * 10f);
            rb.velocity = new Vector3(0, 0, 5f);
            //StartCoroutine(DelayMove(delay, 5f));
        }
    }

    IEnumerator DelayMove(float delay, float dir)
    {
        yield return new WaitForSeconds(delay);
        rb.velocity = new Vector3(0, 0, dir);


    }
}
