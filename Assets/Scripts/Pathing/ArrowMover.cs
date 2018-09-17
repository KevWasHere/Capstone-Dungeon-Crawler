/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hagen, Shane Weerasuriya, 
  #                 Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # 
*-----------------------------------------------------------------------*/

/*-------------------------------------------------------------------------*
# this script will iniitialize the movement of the array 
# if the arrows collider hits the object named player it will destroy the game object
# if it hits the player direct it will print a debug console line 
# Creator : shane weerasuriya
*-----------------------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMover : MonoBehaviour {

    public float speed;
    Rigidbody rb;
    float delay = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
        //rb.velocity = new Vector3(40f, 0, 0);
        float v = rb.velocity.magnitude;
        //Debug.Log("arrow velocity " + rb.velocity);
    }

    private void Update()
    {
        StartCoroutine(DestroyArrow(delay));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Test")
        {
            Debug.Log("Direct Hit!");
            Destroy(gameObject);
        }
    }

    IEnumerator DestroyArrow(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
        
        
    }
}
