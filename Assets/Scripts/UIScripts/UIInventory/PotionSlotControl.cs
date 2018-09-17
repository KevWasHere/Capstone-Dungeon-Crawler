using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class PotionSlotControl : MonoBehaviour {

    public string hotkey;

    private void LateUpdate()
    {
        if (Input.GetButtonDown(hotkey))
        {

            if (gameObject.GetComponent<Test_UIEquipSlot_Assign>().assignItem != 0)
                gameObject.GetComponent<UIEquipSlot>().UsePotion();
        }
    }
}
