using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Teleport : MonoBehaviour {

    public float posX, posY, posZ;
    bool teleport = true;
    float delay = 2f;
    NavMeshAgent nav;

    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        nav.enabled = false;
        Example();
        nav.enabled = true;
    }

    /*private void Update()
    {
        //Debug.Log(teleport);

        if (Player.instance.transform.position != new Vector3(posX, posY, posZ) && teleport)
        {
            Player.instance.transform.position = new Vector3(posX, posY, posZ);
            //teleport = false;
        }
        StartCoroutine(setBool(delay));
    
    //Debug.Log(Player.instance.transform.position + ", " + Player.instance.transform.localPosition);
    }*/

    void Example()
    {
        transform.position = new Vector3(-14.73056f, 74.84781f, 523.0837f);
        print(transform.position.x);
    }
    IEnumerator setBool(float delay)
    {
        yield return new WaitForSeconds(delay);
        teleport = false;


    }
}
