/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # HorseSFX.cs
  # Plays SFX of horse
*-----------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * horseCalls - list of horse SFX
 * earlestCall - earlest a SFX call can be made
 * lastestCall - latest a SFX call can be made
 * horseCallRate - horse SFX call rate
 * nextHorseCall - next horse SFX call
 */
 
 /*
 Creator: Kevin Ho, Yan Zhang
 */

public class HorseSFX : MonoBehaviour {

	//Audio
	public AudioClip[] horseCalls;

	private float earlestCall = 10.0f;
	private float lastestCall = 20.0f;
	
	private float nextHorseCall;
	private float horseCallRate;
	
	/*
	Initialize nextHorseCall
	*/
	void Awake() {
		nextHorseCall = Random.Range(5.0f, earlestCall);
	}
	

	/*
	Function: Play SFX of horse calls
	Creator: Kevin Ho, Yan Zhang
	*/
	public void playSFX() {
		//Debug.Log("nextHorseCall: " + nextHorseCall);
		if (Time.time > nextHorseCall){
			//Play a randomization of clips in list if muiltple exist
			if (horseCalls.Length > 0) {
				int x = (int)Random.Range(0.0f, horseCalls.Length);
				//Debug.Log("Playing clip");
				AudioSource.PlayClipAtPoint(horseCalls[x], transform.position, 1.0f);
			}
			
			//Update next attack time
			horseCallRate = Random.Range(earlestCall, lastestCall);
			nextHorseCall = Time.time + horseCallRate;
		}
	}
}
