/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hagen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # GoldPickup.cs
*-----------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;


/*
 * gold - Gold scriptable object reference
 * isStatic - determines if object started in scene or was instantiated during runtime
 * cursorMode - change default behavior of cursor
 * sceneID - scene index
 * itemPickUp - Item pickup SFX clip
*/

/* 
Creator: Myles Hangen , Shane Weerasuriya, Yan Zhang, Yunzheng Zhou
*/

public class GoldPickup : Interactable {

	public bool isStatic = false;
	int sceneID;
	public CursorMode cursorMode = CursorMode.Auto;
	public Item gold;
	
	public AudioClip itemPickUp;

	// get scene index and subscribe to SaveEvent if object is not marked as static
	private void Start()
	{
		sceneID = SceneManager.GetActiveScene().buildIndex;
		GlobalControl.Instance.SaveEvent += SaveFunction;
		if (isStatic)
			GlobalControl.Instance.SaveEvent -= SaveFunction;
	}

	/*
	 * Function Interact (overriden from Interactable)
	 * increase players gold count and destroy gameobject
	 * when interacted with.
	 * 
	 */
	public override void Interact()
	{
		//Play audio
		if (itemPickUp != null) {
			//Debug.Log("Playing pick up");
			AudioSource.PlayClipAtPoint(itemPickUp, Player.instance.transform.position, 1.0f);
		}
        Text money = Inventory.instance.gold.GetComponent<Text>();
        money.text = Convert.ToString(Player.instance.playerStats.gold);

		Player.instance.playerStats.gold += gold.goldAmount;
		base.Interact();
		Destroy(gameObject);
	}

	/*
	 * Function - SaveFuntion
	 * Description - function used to subscribe to SaveEvent which
	 * tracks ID and position of item so it can be reinstantiated later.
	 * 
	 */
	public SaveDroppableItem SaveFunction()
	{	
		SaveDroppableItem savedItem = new SaveDroppableItem();

		savedItem.PositionX = transform.position.x;
		savedItem.PositionY = transform.position.y;
		savedItem.PositionZ = transform.position.z;
		savedItem.itemID = gold.itemID;

		return savedItem;
	}

	// unsubscribe from save event when object is destroyed
	private void OnDestroy()
	{
		if(!isStatic)
			GlobalControl.Instance.SaveEvent -= SaveFunction;
	}
}
