/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # ItemPickup.cs
*-----------------------------------------------------------------------*/
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 Class that keeps track of all items in a scene allowing them
 to be deactivated when the correct conditions are met.
 Creator: Myles Hagen
*/
/*
 * item - scriptable object containing properties of the item
 * sceneID - index of current scene
 * isStatic - bool to deterimine if an item was present in scene at start or instantiated later.
 * cursorMode - change default behavior of cursor 
 * itemPickUp - Item pickup SFX clip
 */

public class ItemPickup : Interactable
{

	public Item item;   // Item to put in the inventory if picked up
    int sceneID;
    public bool isStatic = false;
    public CursorMode cursorMode = CursorMode.Auto;
	
	public AudioClip itemPickUp;
   
    // get scene index, subscribe to save event, but not if item is marked as static
    private void Start()
    {
        sceneID = SceneManager.GetActiveScene().buildIndex;
        GlobalControl.Instance.SaveEvent += SaveFunction;
        if (isStatic)
            GlobalControl.Instance.SaveEvent -= SaveFunction;
    }

    /*
	 * Function: Interact()
	 * Description: overriden from Interactable script, calls pickup function.
     *
	 */
    public override void Interact()
	{

		base.Interact();
        PickUp();

	}

    /*
	 * Function: Pickup()
	 * Description: add items to inventory, or potion slot if it is a potion. add any items
     * contained in the scenes itemregistry to the global dictionary.
	 * Creator: Myles Hagen, Yunzheng Zhou
	 */
    void PickUp ()
	{
		//Play audio
		if (itemPickUp != null) {
			//Debug.Log("Playing pick up");
			AudioSource.PlayClipAtPoint(itemPickUp, Player.instance.transform.position, 1.0f);
		}
		
        if (ItemRegistry.instance.ItemsDictionary.ContainsKey(name))
        {

            GlobalControl.Instance.SceneItemNames[sceneID].Add(name);
        }

        //Inventory.instance.change(item);	// Add to inventory
        //Inventory.instance.Add(item);
        //EquipmentManager.instance.Equip((Equipment)item);
        //EquipmentManager.instance.EquipGem((Equipment)item, 2, 0);
        //EquipmentManager.instance.EquipPotion((Consumable)item);
        if (item is Consumable)
        {
            if (EquipmentManager.instance.EquipPotion(item as Consumable))
            {
                Destroy(gameObject);
                return;
            }
        }
        Inventory.instance.exchange(item);
        InventoryGUI.instance.addNewItem(item);
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
        //Inventory2.instance.Add(item);
		
        Destroy(gameObject);	// Destroy item from scene
	}

    // unsubscribe from save event when object is destroyed.
    private void OnDestroy()
    {
        if(!isStatic)
            GlobalControl.Instance.SaveEvent -= SaveFunction;
    }

    /*
	 * Function: SaveFunction
	 * Description: Function to save postion and id of item
     * Returns: SaveDroppableItem 
	 * Creator: Myles Hagen
	 */
    public SaveDroppableItem SaveFunction()
    {
        SaveDroppableItem savedItem = new SaveDroppableItem();

        savedItem.PositionX = transform.position.x;
        savedItem.PositionY = transform.position.y;
        savedItem.PositionZ = transform.position.z;
        savedItem.itemID = item.itemID;

        return savedItem;
        //GlobalControl.Instance.GetListForScene().SavedItems.Add(savedItem);
    }

}
