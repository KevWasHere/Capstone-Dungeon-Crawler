 /*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # LevelMusic.cs
  # Presistent Level Music
*-----------------------------------------------------------------------*/

 using UnityEngine;
 using System.Collections;
 using UnityEngine.SceneManagement;
 
 /*
 Script keeps level music presistent throughout game unless on a certain scene
 Creator: Kevin Ho, Yan Zhang
 */
 
 public class LevelMusic : MonoBehaviour {
 
     void Awake ()
     {
 
         DontDestroyOnLoad(this.gameObject);
 
     }
	 
	 void Update()
     {
         if (SceneManager.GetActiveScene().name == "No Scene Yet")
         {
             Destroy(this.gameObject);
         }
     }

 }
