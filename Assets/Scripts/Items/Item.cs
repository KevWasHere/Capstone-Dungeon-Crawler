using UnityEngine;
/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone

*-----------------------------------------------------------------------*/

/*
 * Author: Yunzheng Zhou, Myles Hagen
 * Date: 2018-04-19
 */

/*
* name - name of item
* weight - number representing drop chance
* goldAmount - gold amount of item
* icon - sprite icon
* showInInventory - bool to determine if item should be shown in inventory
* itemID - identification number for item
* itemPrefab - gameobject prefab for item
* isconsumable - bool determining if item is consumable or not
*/

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject{

	new public string name = "New Item";	// Name of the item
    public int weight;
	public int goldAmount;
    public Sprite icon = null;
	public bool showInInventory = true;
    public int itemID;
    public GameObject itemPrefab;
    public bool isconsumable;
    
	// Called when the item is pressed in the inventory
	public virtual void Use ()
	{
		// Use the item
		// Something may happen
	}
    
    /*
	 * Function: RemoveFromInventory
	 * 
	 * Description: remove item from inventory
	 */
    public void RemoveFromInventory ()
	{
		Inventory.instance.Remove(this);
	}

}
