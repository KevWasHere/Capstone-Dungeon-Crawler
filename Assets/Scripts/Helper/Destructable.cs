/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hagen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # Destructable.cs
  # Instaniates breakable objects
*-----------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * destroyedVersion - prefab of destroyed game object
 * destroyableObject - instance of the destroyed game object
 * expandObject - instance of the expand object in destroyed game objects
 * sphereDestoryTime - rate to destory sphere object
 * objectDestroyTime - rate to destory destroyed object
 * destroySFX - list of destroyed object SFX
 */
 
 /*
 Creator: Myles Hagen, Kevin Ho
 */

public class Destructable : Interactable {

    public GameObject destroyedVersion;
	private GameObject destroyableObject;
    private GameObject expandObject;			//Required to have a gameobject called Expand in the cracked prefab
	
	private float sphereDestoryTime = 1.0f;
	private float objectDestroyTime = 4.0f;
	
	public AudioClip[] destroySFX;
	
	/*
	Function: Replace object with destructable version of the object and remove orignal object
	Creator: Myles Hangen, Kevin Ho
	*/
	public void DestroyObject() {
		Destroy(this.gameObject);
		destroyableObject = Instantiate(destroyedVersion, transform.position, transform.rotation);
		
		//Create and delete old expand if they exist
		if (destroyedVersion.transform.Find("Expand") != null) {
			expandObject = Instantiate(destroyedVersion.transform.Find("Expand").gameObject, transform.position, transform.rotation);
			//Destory initial object and expand child objects of the destroyed prefab
			Destroy(destroyableObject.transform.Find("Expand").gameObject);
			//Scale expand to mimic exploding parts
			for (int x = 0; x < 5; x++) {
				expandObject.transform.localScale *= 1.2f;
			}
		}
		//Play a randomization of clips in list if muiltple exist
		if (destroySFX.Length > 0) {
			int y = (int)Random.Range(0.0f, destroySFX.Length);
		    AudioSource.PlayClipAtPoint(destroySFX[y], transform.position, 1.0f);
		}
		
		Destroy(expandObject, sphereDestoryTime);
		Destroy(destroyableObject, objectDestroyTime);
	}
	
	/*
	Function: Check if object has been interacted with. Call Destroy object if has
	Creator: Myles Hangen, Kevin Ho
	*/
	public override void Interact () {
		base.Interact ();
		CharacterCombat characterCombat = GameObject.Find("Player").GetComponent<CharacterCombat>();
		StartCoroutine(DestoryWait(characterCombat.attackDelay));
		characterCombat.noDamageAttack();
	}
	
	/*
	Function: Coroutine wait for destroying object
	Parameters: delay - Length of coroutine delay
	Return: Return after delay
	*/
	IEnumerator DestoryWait(float delay) {
        yield return new WaitForSeconds(delay);
		DestroyObject();
	}
}
