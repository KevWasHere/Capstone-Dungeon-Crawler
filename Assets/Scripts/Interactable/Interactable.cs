/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # Interactable.cs
*-----------------------------------------------------------------------*/

using UnityEngine;

/*
* radius - Radius range of interactable
* interactionTransform - transform of interaction object
* isFocus - Is interactable being focused on
* player - Reference to player transform
* hasINteracted - Has objected been interacted with
*/

/*
Creator: Myles Hagen, Yunzheng Zhou, Shane Weerasuriya
*/

public class Interactable : MonoBehaviour {
	
	public float radius = 3f;
	public Transform interactionTransform;

	public bool isFocus = false;
	Transform player;

	bool hasInteracted = false;

	/*
	Interact
	*/
	public virtual void Interact() 
	{
		
	}


	//Update once per frame
	void Update()
	{
		if (isFocus) {
			
            float distance = Vector3.Distance (player.position, interactionTransform.position);
			if (!hasInteracted && distance <= radius) {
             //   Debug.Log("INTERACT");
                hasInteracted = true;
                Interact();
			}
		}
	}

	/*
	Set focus to location
	Parameters: playerTransform - location of focus
	*/
	public void OnFocused(Transform playerTransform)
	{
		isFocus = true;
		//Debug.Log ("isfocus" + isFocus);
		player = playerTransform;
		hasInteracted = false;
	}

	/*
	Removes focus to location
	*/
	public void OnDefocused()
	{
        //Debug.Log("DEfocus" + isFocus);
        isFocus = false;
		player = null;
		hasInteracted = false;
	}

	/*
	Set focus to location
	Parameters: playerTransform - location of focus
	*/
	void OnDrawGizmosSelected()
	{
		if (interactionTransform == null)
			interactionTransform = transform;
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere (interactionTransform.position, radius);
	}
		

}
