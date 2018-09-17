/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # VillagerSFX.cs
  # Plays SFX of villager
*-----------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * villagerCalls - list of villager SFX
 * villagerWaves - list of villager wave SFX
 * earlestCall - earlest a SFX call can be made
 * lastestCall - latest a SFX call can be made
 * nextVillagerCall - villager SFX call rate
 * villagerCallRate - next village SFX call
 */
 
 /*
 Creator: Kevin Ho,Tianqi Xiao, Yan Zhang, Yunzheng Zhou
 */
 
public class VillagerSFX : MonoBehaviour {

	//Audio
	public AudioClip[] villagerCalls;
	public AudioClip[] villagerWaves;

	private float earlestCall = 10.0f;
	private float lastestCall = 20.0f;
	
	private float nextVillagerCall;
	private float villagerCallRate;
	
	/*
	Initialize nextVillageCall
	*/
	void Awake() {
		nextVillagerCall = Random.Range(5.0f, earlestCall);
	}
		

	/*
	Function: Play SFX of villager calls
	Creator: Kevin Ho, ,Tianqi Xiao, Yan Zhang, Yunzheng Zhou
	*/
	public void playSFX() {
		//Debug.Log("nextHorseCall: " + nextHorseCall);
		if (Time.time > nextVillagerCall){
			//Play a randomization of clips in list if muiltple exist
			if (villagerCalls.Length > 0) {
				int x = (int)Random.Range(0.0f, villagerCalls.Length);
				//Debug.Log("Playing clip");
				AudioSource.PlayClipAtPoint(villagerCalls[x], transform.position, 0.8f);
			}
			
			//Update next attack time
			villagerCallRate = Random.Range(earlestCall, lastestCall);
			nextVillagerCall = Time.time + villagerCallRate;
		}
	}
	
	/*
	Function: Play SFX of villager wave
	Creator: Kevin Ho, ,Tianqi Xiao, Yan Zhang, Yunzheng Zhou
	*/
	public void playWaveSFX() {
		//Play a randomization of clips in list if muiltple exist
		if (villagerWaves.Length > 0) {
			int x = (int)Random.Range(0.0f, villagerWaves.Length);
			//Debug.Log("Playing clip");
			AudioSource.PlayClipAtPoint(villagerWaves[x], transform.position, 0.8f);
		}
	}
}
