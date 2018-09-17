/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone

*-----------------------------------------------------------------------*/

/*
 * Author: Yunzheng Zhou
 * Date: 2018-04-19
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * NPCItem will generate random items to the vendor NPC according to the NPC type
 * Every time Player interact with NPC, this class will regenerate the items so that
 * Player could refresh the item all the time
 */
public class NPCItem : MonoBehaviour {

    public enum NPCType { WeaponSmith, PotionVendor, GemVendor}         // enum array to control the type of NPC
    public NPCType ntype;                                               // current type of NPC
    public GameObject[] slots;                                          // array of slots that vendor shows to players
    public UIItemDatabase itemDatabase;                                 // item database for GUI item

    #region Singleton

    public static NPCItem instance;                                     

    void Awake()
    {
        instance = this;
    }

    #endregion

    // Set NPC items according to npc type
    public void setNPCItem(NPCType npc)
    {
        switch ((int)npc)
        {
            case 0:
                setItemInSlots(15, 38);
                break;
            case 1:
                setItemInSlots(40, 40);
                break;
            case 2:
                setItemInSlots(1, 9);
                break;
            default:
                break;
        }
    }

    // add item to vendor inventory and numbers of item generate will be random
    // clear item for every time player interact with NPC
    private void setItemInSlots(int pre, int end)
    {
        for (int i = 0; i < 40; i++)
        {
            slots[i].GetComponent<UIItemSlot>().Assign(itemDatabase.GetByID(0));
            slots[i].GetComponent<Test_UIItemSlot_Assign>().assignItem = 0;
        }
        int k = Random.Range(1, 10);
        int tmp;// = Random.Range(pre, end);
        for (int i = 0; i < k; i++)
        {
            tmp = Random.Range(pre, end);
            slots[i].GetComponent<UIItemSlot>().Assign(itemDatabase.GetByID(tmp));
            slots[i].GetComponent<Test_UIItemSlot_Assign>().assignItem = tmp;
        }
    }

}
