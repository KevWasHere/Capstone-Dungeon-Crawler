using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hagen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # aiFlock.cs
*-----------------------------------------------------------------------*/

/*
* Creator: Kevin Ho, Myles Hagen, Tianqi Xiao, Shane Weerasuriya
*
* prefab - object that will make up the flock
* goalPrefab - location that flock will move towards
* areaCenter - center area of the flock (controls boundaries of movement)
* ac - transform of areaCenter
* numObjects - number of objects to instantiate for the flock
* allObjects - all instantiated objects
* goalPos - position of the goal that flock will move towards
* centerPos - position of the center of the area the flock moves in
*/

public class aiFlock : MonoBehaviour {

    public GameObject prefab;
    public GameObject goalPrefab;
    public GameObject areaCenter;
    public static Transform ac;
    static int numObjects = 10;
    public static GameObject[] allObjects = new GameObject[numObjects];
    public static int areaSize = 5;
    public static Vector3 goalPos;
    Vector3 centerPos;

	// initialize positions and transforms
    private void Awake()
    {
        ac = areaCenter.transform;
        goalPos = ac.position;
        centerPos = ac.position;
    }

	// instantitate flock within the boundaries
    void Start ()
    {
        
	    for (int i = 0; i < numObjects; i++)
        {
            Vector3 pos = new Vector3(Random.Range(centerPos.x-areaSize, centerPos.x+areaSize),
                Random.Range(centerPos.y - areaSize, centerPos.y+areaSize), Random.Range(centerPos.z - areaSize, centerPos.z+areaSize));

            allObjects[i] = Instantiate(prefab, pos, Quaternion.identity);
            //Debug.Log("num objects " + i);
        }	

	}

	// randomly change the goal position to keep flock moving around the area
	void Update ()
    {
		if (Random.Range(0,1000) < 50)
        {
            goalPos = new Vector3(Random.Range(centerPos.x - areaSize, centerPos.x+areaSize),
                Random.Range(centerPos.y - areaSize, centerPos.y+areaSize), Random.Range(centerPos.z - areaSize, centerPos.z+areaSize));

            goalPrefab.transform.position = goalPos;
        }
	}
}
