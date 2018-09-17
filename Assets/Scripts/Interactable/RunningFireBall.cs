using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningFireBall : MonoBehaviour {
    public GameObject fireball;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        fireball.transform.Rotate(new Vector3(5f, 0, 0));
	}
}
