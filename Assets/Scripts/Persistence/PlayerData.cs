/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hagen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # PlayerData.cs
*-----------------------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.AI;

/* Creator: Myles Hagen
 * 
 * Class used to restore all of the characters properties upon loading the game or changing scenes.
 * Players current gear, health, rage, experience, level, transform etc are set upon loading a new scene.
 * Game is also saved when F7 is pressed and loaded when F10 is pressed.
*/

/* Creator Myles Hagen
 * cam: reference to main camera
 * sceneNum: reference to current scene index
 * 
 */

[Serializable]
public class PlayerData : MonoBehaviour {

    Camera cam;
	private int sceneNum;

    /*
     * in start function restore players current gear, health, rage, experience, level, transform etc. 
     * Creator: Myles Hagen
     */
    private void Start()
    {
        cam = Camera.main;
		sceneNum = SceneManager.GetActiveScene().buildIndex;
        

        // check boolean value in global control to see if game was loaded from disk 
		if (GlobalControl.Instance.IsSceneBeingLoaded)
        {
            Debug.Log("Loading Scene");
            PlayerState.Instance.localPlayerData = GlobalControl.Instance.LocalCopyOfData;

            transform.position = new Vector3(
                            GlobalControl.Instance.LocalCopyOfData.PositionX,
                            GlobalControl.Instance.LocalCopyOfData.PositionY,
                            GlobalControl.Instance.LocalCopyOfData.PositionZ);
				

            cam.transform.position = new Vector3(GlobalControl.Instance.LocalCopyOfData.camPosX,
                            GlobalControl.Instance.LocalCopyOfData.camPosY,
                            GlobalControl.Instance.LocalCopyOfData.camPosZ);
                            
            Player.instance.playerStats.currentHealth = GlobalControl.Instance.LocalCopyOfData.HP;
            Player.instance.playerStats.currentRage = GlobalControl.Instance.LocalCopyOfData.currentRage;
            Player.instance.playerStats.currentExperience = GlobalControl.Instance.LocalCopyOfData.currentXP;
            Player.instance.playerStats.level = GlobalControl.Instance.LocalCopyOfData.level;
			Player.instance.playerStats.gold = GlobalControl.Instance.LocalCopyOfData.currentgold;

            // retrieve inventory item ids
            foreach (int i in GlobalControl.Instance.LocalCopyOfData.itemIDs)
            {

                Inventory.instance.itemIDs.Add(i);

            }

            // retrieve potion ids
            for(int i = 0; i < GlobalControl.Instance.LocalCopyOfData.Potions_IDs.Length; i++)
            {
                EquipmentManager.instance.Potion_IDs[i] = GlobalControl.Instance.LocalCopyOfData.Potions_IDs[i];
            }

            // retrieve equipment ids
            for (int i = 0; i < GlobalControl.Instance.LocalCopyOfData.equipmentIDs.Length; i++)
            {
                
                EquipmentManager.instance.equipmentIDs[i] = GlobalControl.Instance.LocalCopyOfData.equipmentIDs[i];
            }

            // retrieve aoe skill gem ids
            for (int i = 0; i < GlobalControl.Instance.LocalCopyOfData.SkillAOE_IDs.Length; i++)
            {

                EquipmentManager.instance.SkillAOE_IDs[i] = GlobalControl.Instance.LocalCopyOfData.SkillAOE_IDs[i];
            }

            // retrieve dash skill gem ids
            for (int i = 0; i < GlobalControl.Instance.LocalCopyOfData.SkillDash_IDs.Length; i++)
            {
                EquipmentManager.instance.SkillDash_IDs[i] = GlobalControl.Instance.LocalCopyOfData.SkillDash_IDs[i];
            }

            // retrieve projectile skill gem ids
            for (int i = 0; i < GlobalControl.Instance.LocalCopyOfData.SkillProjectile_IDs.Length; i++)
            {

                EquipmentManager.instance.SkillProjectile_IDs[i] = GlobalControl.Instance.LocalCopyOfData.SkillProjectile_IDs[i];
            }

            Inventory.instance.OnAfterDeserialize(); // convert inventory item ids to scriptable objects
            EquipmentManager.instance.OnAfterDeserialize(); // convert equipment ids to scriptable objects

            // increase player stats from gear equiped
            for (int i = 0; i < EquipmentManager.instance.currentEquipment.Length; i++) { 
                if (EquipmentManager.instance.currentEquipment[i] == null)
                    continue;
             
                Player.instance.playerStats.OnEquipmentChanged(EquipmentManager.instance.currentEquipment[i], null);
             }
            
            // increase aoe skill stats from gems equiped
            for (int i = 0; i < EquipmentManager.instance.Skill_AOE.Length; i++)
            {
                if (EquipmentManager.instance.Skill_AOE[i] == null)
                    continue;
               
                Player.instance.aoeSkillStats.OnGemChanged(EquipmentManager.instance.Skill_AOE[i], null);
            }

            // increase projectile skill stats from gems equiped
            for (int i = 0; i < EquipmentManager.instance.Skill_Projectile.Length; i++)
            {
                if (EquipmentManager.instance.Skill_Projectile[i] == null)
                    continue;
                
                Player.instance.projSkillStats.OnGemChanged(EquipmentManager.instance.Skill_Projectile[i], null);
            }

            // increase dash skill stats from gems equiped
            for (int i = 0; i < EquipmentManager.instance.Skill_Dash.Length; i++)
            {
                if (EquipmentManager.instance.Skill_Dash[i] == null)
                    continue;
               
                Player.instance.dashSkillStats.OnGemChanged(EquipmentManager.instance.Skill_Dash[i], null);
            }

            // update gui with new values for equipment 
            EquipmentManager.instance.updatePotionOnload();
            InventoryGUI.instance.updateSlot();
            EquipmentManager.instance.loadEquipmentByID();
            EquipmentManager.instance.EquipmentUpdate(7);
            EquipmentManager.instance.UpdateGemOnload();
            EquipmentManager.instance.assignEquipmentByID();
            
        }

        // check boolean value in global control to see if player transfered between scenes
        if (GlobalControl.Instance.SceneChange)
        {
            Debug.Log("Scene Changed");

            // set players current health, experience, rage, and level after scene change
            Player.instance.playerStats.currentHealth = GlobalControl.Instance.savedPlayerData.HP; 
            Player.instance.playerStats.currentExperience = GlobalControl.Instance.savedPlayerData.currentXP;
            Player.instance.playerStats.currentRage = GlobalControl.Instance.savedPlayerData.currentRage;
			Player.instance.playerStats.level = GlobalControl.Instance.savedPlayerData.level;
			Player.instance.playerStats.targetExperience = GlobalControl.Instance.savedPlayerData.targetExp;
			Player.instance.playerStats.gold = GlobalControl.Instance.savedPlayerData.currentgold;
            Inventory.instance.items.Clear();

            // retrieve inventory items
            foreach (Item i in GlobalControl.Instance.savedItems)
            {
                Inventory.instance.Add(i);
                
            }

            // set players transform if different transform from normal is specified.
            if (GlobalControl.Instance.newTransform)
            {
                Player.instance.GetComponent<NavMeshAgent>().enabled = false;
                Player.instance.transform.position = new Vector3(GlobalControl.Instance.newPosX, GlobalControl.Instance.newPosY, GlobalControl.Instance.newPosZ);
                Player.instance.GetComponent<NavMeshAgent>().enabled = true;
                GlobalControl.Instance.newTransform = false;
            }
            

            Player.instance.playerStats.ClearModifiers();
            // restore equipment and gems to player after scene change
            for (int i = 0; i < GlobalControl.Instance.savedEquipment.Length; i++)
            {
                EquipmentManager.instance.currentEquipment[i] = GlobalControl.Instance.savedEquipment[i];
                Player.instance.playerStats.OnEquipmentChanged(EquipmentManager.instance.currentEquipment[i], null);
                EquipmentManager.instance.Skill_Dash[i] = GlobalControl.Instance.savedSKillDash[i];
                Player.instance.dashSkillStats.OnGemChanged(EquipmentManager.instance.Skill_Dash[i], null);
                if (i < 5)
                {
                    EquipmentManager.instance.Skill_Projectile[i] = GlobalControl.Instance.savedSkillProjectile[i];
                    Player.instance.projSkillStats.OnGemChanged(EquipmentManager.instance.Skill_Projectile[i], null);
                }

                if (i < 4)
                {
                    EquipmentManager.instance.Potions[i] = GlobalControl.Instance.savedPotions[i];
                    EquipmentManager.instance.Skill_AOE[i] = GlobalControl.Instance.savedSkillAOE[i];
                    Player.instance.aoeSkillStats.OnGemChanged(EquipmentManager.instance.Skill_AOE[i], null);
                }
            }
            // update gui with inventory items
            if (InventoryGUI.instance != null)
                InventoryGUI.instance.updateSlot();
 
            // update gui with equipment and gems
            EquipmentManager.instance.updatePotionOnbetweenScene();
            EquipmentManager.instance.assignEquipmentByID();
            EquipmentManager.instance.loadEquipmentByIDBetweenScene();
            EquipmentManager.instance.UpdateGemOnloadBetweenScene();
            GlobalControl.Instance.SceneChange = false;
            GlobalControl.Instance.FindNPCSetBool (sceneNum);
        }
        
    }


    /*
     * update function that saves player data when F7 is pressed and loads player data when
     * F10 is pressed.
     * Creator: Myles Hagen
     */
    void Update () {

        
        
        if (Input.GetKeyDown(KeyCode.F7))
        {
			if (!AllEventList.returnStatus ("villageInitial", 0)) {
				Debug.Log("CANNOT SAVE DURING TUTORIAL!"); 
				return;
			}
            Debug.Log("Saving Data...");

            // save all relevant player attributes as well as scene index and camera position
            PlayerState.Instance.localPlayerData.SceneID = SceneManager.GetActiveScene().buildIndex; 
            PlayerState.Instance.localPlayerData.PositionX = transform.position.x;
            PlayerState.Instance.localPlayerData.PositionY = transform.position.y;
            PlayerState.Instance.localPlayerData.PositionZ = transform.position.z;
            PlayerState.Instance.localPlayerData.HP = Player.instance.playerStats.currentHealth;
            PlayerState.Instance.localPlayerData.currentRage = Player.instance.playerStats.currentRage;
            PlayerState.Instance.localPlayerData.camPosX = cam.transform.position.x;
            PlayerState.Instance.localPlayerData.camPosY = cam.transform.position.y;
            PlayerState.Instance.localPlayerData.camPosZ = cam.transform.position.z;
            PlayerState.Instance.localPlayerData.currentXP = Player.instance.playerStats.currentExperience;
            PlayerState.Instance.localPlayerData.level = Player.instance.playerStats.level;
			PlayerState.Instance.localPlayerData.currentgold = Player.instance.playerStats.gold;
            Inventory.instance.OnBeforeSerialize();
            EquipmentManager.instance.OnBeforeSerialize();
            PlayerState.Instance.localPlayerData.itemIDs.Clear();

            // store inventory item ids
            foreach (int i in Inventory.instance.itemIDs)
            {

                PlayerState.Instance.localPlayerData.itemIDs.Add(i);
            }

            // store equipment item ids
            for (int i = 0; i < EquipmentManager.instance.equipmentIDs.Length; i++)
            {
                
                if (EquipmentManager.instance.equipmentIDs[i] == 0) {
                    PlayerState.Instance.localPlayerData.equipmentIDs[i] = 0;
                    continue;
                }
                PlayerState.Instance.localPlayerData.equipmentIDs[i] = EquipmentManager.instance.equipmentIDs[i];
            }

            // store potion ids
            for (int i = 0; i < EquipmentManager.instance.Potion_IDs.Length; i++)
            {
                if (EquipmentManager.instance.Potion_IDs[i] == 0)
                {
                    PlayerState.Instance.localPlayerData.Potions_IDs[i] = 0;
                    continue;
                }
                PlayerState.Instance.localPlayerData.Potions_IDs[i] = EquipmentManager.instance.Potion_IDs[i];
            }

            // store dash skill gem ids
            for (int i = 0; i < EquipmentManager.instance.SkillDash_IDs.Length; i++)
            {

                if (EquipmentManager.instance.SkillDash_IDs[i] == 0)
                {
                    PlayerState.Instance.localPlayerData.SkillDash_IDs[i] = 0;
                    continue;
                }
                PlayerState.Instance.localPlayerData.SkillDash_IDs[i] = EquipmentManager.instance.SkillDash_IDs[i];
            }

            // store aoe skill gem ids
            for (int i = 0; i < EquipmentManager.instance.SkillAOE_IDs.Length; i++)
            {

                if (EquipmentManager.instance.SkillAOE_IDs[i] == 0)
                {
                    PlayerState.Instance.localPlayerData.SkillAOE_IDs[i] = 0;
                    continue;
                }
                PlayerState.Instance.localPlayerData.SkillAOE_IDs[i] = EquipmentManager.instance.SkillAOE_IDs[i];
            }

            // store projectile skill ids
            for (int i = 0; i < EquipmentManager.instance.SkillProjectile_IDs.Length; i++)
            {
                if (EquipmentManager.instance.SkillProjectile_IDs[i] == 0)
                {
                    PlayerState.Instance.localPlayerData.SkillProjectile_IDs[i] = 0;
                    continue;
                }
                PlayerState.Instance.localPlayerData.SkillProjectile_IDs[i] = EquipmentManager.instance.SkillProjectile_IDs[i];
            }

            GlobalControl.Instance.SaveData(); // save data to disk
        }

        // load data from disk and load scene
        if (Input.GetKeyDown(KeyCode.F10))
        {
            Debug.Log("Loading Data...");
            GlobalControl.Instance.LoadData();
            GlobalControl.Instance.IsSceneBeingLoaded = true;

            int whichScene = GlobalControl.Instance.LocalCopyOfData.SceneID;
			GlobalControl.Instance.newSceneID = whichScene;
            SceneManager.LoadSceneAsync(9, LoadSceneMode.Single);
            
        }

    }


}
