using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testerS : MonoBehaviour {

    public const int numSlots = 10;
    public Item[] items = new Item[numSlots];

    [HideInInspector]
    public int[] itemIDs = new int[numSlots];

    void Start () {

        //Debug.Log(items[-1].name);
        for (int i = 0; i < items.Length; i++)
        {
            Debug.Log(items[i].name + " " + items[i].itemID);
        }
        
		
	}
	
	
}
