/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hagen, Shane Weerasuriya, 
  #                 Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # 
*-----------------------------------------------------------------------*/

/*-------------------------------------------------------------------------*
# This scrip is part of the bird ai controller, this only spawns birds at the start
#
# Creator : shane weerasuriya
*-----------------------------------------------------------------------*/
using UnityEngine;
using System.Collections;

public class Village : MonoBehaviour {

	public lb_BirdController birdControl;
	// start spawn
	// initializes the spawning function from the Bird controller script
	void Start(){

		birdControl = GameObject.Find ("_livingBirdsController").GetComponent<lb_BirdController>();
		SpawnSomeBirds();
	}

	// Update is called once per frame
	void Update () {
	}

	IEnumerator SpawnSomeBirds(){
		yield return 2;
		birdControl.SendMessage ("SpawnAmount",10);
	}



}
