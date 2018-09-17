using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuDataSaver : MonoBehaviour {


	Camera cam;
	private int sceneNum;

	void Start() {
		cam = Camera.main;
		sceneNum = SceneManager.GetActiveScene().buildIndex;
	}

	public void SaveData()
	{
		if (!AllEventList.returnStatus ("villageInitial", 0)) {
			Debug.Log ("CANNOT SAVE DURING TUTORIAL!");
			return;
		}
		Debug.Log("Saving Data...");
		PlayerState.Instance.localPlayerData.SceneID = SceneManager.GetActiveScene().buildIndex;
		PlayerState.Instance.localPlayerData.PositionX = Player.instance.transform.position.x;
		PlayerState.Instance.localPlayerData.PositionY = Player.instance.transform.position.y;
		PlayerState.Instance.localPlayerData.PositionZ = Player.instance.transform.position.z;
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

		foreach (int i in Inventory.instance.itemIDs)
		{

			PlayerState.Instance.localPlayerData.itemIDs.Add(i);
		}
		//EquipmentManager.instance.CheckEquipmentID();
		for (int i = 0; i < EquipmentManager.instance.equipmentIDs.Length; i++)
		{

			if (EquipmentManager.instance.equipmentIDs[i] == 0)
			{
				PlayerState.Instance.localPlayerData.equipmentIDs[i] = 0;
				continue;
			}
			PlayerState.Instance.localPlayerData.equipmentIDs[i] = EquipmentManager.instance.equipmentIDs[i];
		}
		for (int i = 0; i < EquipmentManager.instance.SkillDash_IDs.Length; i++)
		{

			if (EquipmentManager.instance.SkillDash_IDs[i] == 0)
			{
				PlayerState.Instance.localPlayerData.SkillDash_IDs[i] = 0;
				continue;
			}
			PlayerState.Instance.localPlayerData.SkillDash_IDs[i] = EquipmentManager.instance.SkillDash_IDs[i];
		}

		for (int i = 0; i < EquipmentManager.instance.SkillAOE_IDs.Length; i++)
		{

			if (EquipmentManager.instance.SkillAOE_IDs[i] == 0)
			{
				PlayerState.Instance.localPlayerData.SkillAOE_IDs[i] = 0;
				continue;
			}
			PlayerState.Instance.localPlayerData.SkillAOE_IDs[i] = EquipmentManager.instance.SkillAOE_IDs[i];
		}

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
		GlobalControl.Instance.SaveData();
	}

	public void LoadData() {

		Debug.Log("Loading Data...");
		GlobalControl.Instance.LoadData();
		GlobalControl.Instance.IsSceneBeingLoaded = true;

		int whichScene = GlobalControl.Instance.LocalCopyOfData.SceneID;
		GlobalControl.Instance.newSceneID = whichScene;
		SceneManager.LoadSceneAsync(9, LoadSceneMode.Single);
        if (Player.instance.deathpanel.activeSelf)
            Player.instance.deathpanel.SetActive(false);
	}


}
