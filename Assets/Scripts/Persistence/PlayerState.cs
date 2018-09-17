
/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # PlayerState.cs
*-----------------------------------------------------------------------*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 *
 * Class PlayerState
 * Creator: Myles Hagen, Tianqi Xiao
 * 
 * Description:
 *          state of player that refer to the playerstatistic
 */
public class PlayerState : MonoBehaviour
{
    public static PlayerState Instance;                         // instance of playerState in the current scene

    //public Transform playerPosition;

    //TUTORIAL
    [HideInInspector]
    public PlayerStatistics localPlayerData = new PlayerStatistics();

    void Awake()
    {
        if (Instance == null)
            Instance = this;

        if (Instance != this)
            Destroy(gameObject);

        //GlobalControl.Instance.Player = gameObject;
    }

    //At start, load data from GlobalControl.
    void Start()
    {
        //localPlayerData = GlobalControl.Instance.savedPlayerData;
        //Player.instance.playerStats.currentHealth = localPlayerData.HP;
    }

}