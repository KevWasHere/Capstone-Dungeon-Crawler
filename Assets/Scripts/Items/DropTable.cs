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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * itemDB - reference to item database
 * numChances - number of chances for an item to be dropped
*/

public class DropTable : MonoBehaviour {

	//public Item[] items;
    public ItemDatabase itemDB;
    public int numChances = 10;
  

	/*
	 * Function: DropItem
	 * Description: for the number of chances to drop an item
	 * randomly pick an item from the database and compare the droprate
	 * with a randomly chosen number, if the weight is greater than the random
	 * value then the item is instantiated. position of drop is placed around the
	 * object that script is attached to.
	 * 
	 * Creator: Myles Hagen
	 * 
	 */
    public void DropItem ()
	{
        if (itemDB == null)
            return;
		float offset = 1.0f;
        int numChances2 = Random.Range(2, itemDB.items.Length);

        for (int i = 0; i < numChances; i++) {
			int item = Random.Range (0, 59);

			int randValue = Random.Range (1, 100);

			var pos = transform.position;
			if (itemDB.items[item].weight >= randValue) {
				if (itemDB.items[item].itemPrefab != null) {
					if (i == 0 || i == 8) {
						pos.z -= offset;
					} else if (i == 1 || i == 9) {
						pos.x -= offset;
						pos.z -= offset;
					} else if (i == 2 || i == 10) {
						pos.x -= offset;
					} else if (i == 3 || i == 11) {
						pos.x += offset;
						pos.z += offset;
					} else if (i == 4 || i == 12) {
						pos.z += offset;
					} else if (i == 5) {
						pos.x -= offset;
						pos.z += offset;
					} else if (i == 6) {
						pos.x += offset;
					} else if (i == 7) {
						pos.x += offset;
						pos.z -= offset;
					}
					pos.y += offset;
				}
					
                GameObject objToSpawn = Instantiate(itemDB.items[item].itemPrefab);
                objToSpawn.transform.position = pos;
                Physics.IgnoreCollision(objToSpawn.GetComponent<Collider>(), GetComponent<Collider>());
			}
		}
	} 
}



