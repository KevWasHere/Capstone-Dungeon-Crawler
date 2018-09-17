/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hagen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # DataSaver.cs
*-----------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 save player data on trigger enter (used for checkpoints)
*/
/*
 * cam - reference to main camera
 * 
 * Creator: Myles Hagen, Tianqi Xiao, Shane Weerasuriya
 */

public class DataSaver : MonoBehaviour {

    Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("CHECKPOINT");
        if (other.tag == "Player")
		{
            SaveData();
        }
    }

	/*
	 * Function: SaveData
	 * Description: save all relevant player data to PlayerStatistics class
	 * then save it to disk.
	 * 
	 */
    public void SaveData()
    {
        Debug.Log("Saving Data...");
        PlayerState.Instance.localPlayerData.SceneID = SceneManager.GetActiveScene().buildIndex;
        PlayerState.Instance.localPlayerData.PositionX = transform.position.x;
        PlayerState.Instance.localPlayerData.PositionY = transform.position.y;
        PlayerState.Instance.localPlayerData.PositionZ = transform.position.z;
        PlayerState.Instance.localPlayerData.HP = Player.instance.playerStats.currentHealth;
        PlayerState.Instance.localPlayerData.camPosX = cam.transform.position.x;
        PlayerState.Instance.localPlayerData.camPosY = cam.transform.position.y;
        PlayerState.Instance.localPlayerData.camPosZ = cam.transform.position.z;
        PlayerState.Instance.localPlayerData.currentXP = Player.instance.playerStats.currentExperience;
        PlayerState.Instance.localPlayerData.level = Player.instance.playerStats.level;
		PlayerState.Instance.localPlayerData.currentgold = Player.instance.playerStats.gold;
        //PlayerState.Instance.localPlayerData.currentgold = Convert.ToInt32(Inventory.instance.gold.GetComponents<Text>());
        Inventory.instance.OnBeforeSerialize();
        EquipmentManager.instance.OnBeforeSerialize();
        PlayerState.Instance.localPlayerData.itemIDs.Clear();

		// save inventory item ids
        foreach (int i in Inventory.instance.itemIDs)
        {

            PlayerState.Instance.localPlayerData.itemIDs.Add(i);
        }
        
		// save equipment item ids
        for (int i = 0; i < EquipmentManager.instance.equipmentIDs.Length; i++)
        {

            if (EquipmentManager.instance.equipmentIDs[i] == 0)
            {
                PlayerState.Instance.localPlayerData.equipmentIDs[i] = 0;
                continue;
            }
            PlayerState.Instance.localPlayerData.equipmentIDs[i] = EquipmentManager.instance.equipmentIDs[i];
        }

		// save dash skill gem ids
        for (int i = 0; i < EquipmentManager.instance.SkillDash_IDs.Length; i++)
        {

            if (EquipmentManager.instance.SkillDash_IDs[i] == 0)
            {
                PlayerState.Instance.localPlayerData.SkillDash_IDs[i] = 0;
                continue;
            }
            PlayerState.Instance.localPlayerData.SkillDash_IDs[i] = EquipmentManager.instance.SkillDash_IDs[i];
        }

		// save aoe skill gem ids
        for (int i = 0; i < EquipmentManager.instance.SkillAOE_IDs.Length; i++)
        {

            if (EquipmentManager.instance.SkillAOE_IDs[i] == 0)
            {
                PlayerState.Instance.localPlayerData.SkillAOE_IDs[i] = 0;
                continue;
            }
            PlayerState.Instance.localPlayerData.SkillAOE_IDs[i] = EquipmentManager.instance.SkillAOE_IDs[i];
        }

		// save projectile skill gem ids
        for (int i = 0; i < EquipmentManager.instance.SkillProjectile_IDs.Length; i++)
        {
            //Debug.Log(EquipmentManager.instance.SkillProjectile_IDs[i]);
            if (EquipmentManager.instance.SkillProjectile_IDs[i] == 0)
            {
                PlayerState.Instance.localPlayerData.SkillProjectile_IDs[i] = 0;
                continue;
            }
            PlayerState.Instance.localPlayerData.SkillProjectile_IDs[i] = EquipmentManager.instance.SkillProjectile_IDs[i];
        }
        GlobalControl.Instance.SaveData(); // save data to disk
    }
}
