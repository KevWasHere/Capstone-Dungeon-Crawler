using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone
  # ActionBarFill.cs
*-----------------------------------------------------------------------*/

/*
//Creator: Yan Zhang, Kevin Ho, Myles Hagen
*/
/*
Action Bar fill will track and fill the health globe(left), rage globe(right) and eperience bar (middle)
healthFill: the fill image of health
expFill: the fill image of experience
rageFill: the fill image of the rage global
*/
//Creator: Yan Zhang
public class ActionBarFill : MonoBehaviour {
	[Tooltip("health image")]
	public Image healthFill;
	[Tooltip("exp image")]
	public Image expFill;
	[Tooltip("rage image")]
	public Image rageFill;
	//set the initial value in start, each fill amount is the percentage of the current value divided the total value
	void Start () {
		if (this.healthFill == null)
			return;
		healthFill.fillAmount = Player.instance.playerStats.currentHealth / Player.instance.playerStats.maxHealth;
		if (this.expFill == null)
			return;
		expFill.fillAmount = Player.instance.playerStats.currentExperience/ Player.instance.playerStats.targetExperience;
		if (this.rageFill == null)
			return;
		rageFill.fillAmount = Player.instance.playerStats.currentRage/ Player.instance.playerStats.maxRage;
	}

	//Creator: Yan Zhang, Myles Hagen
	// update the fill images
	//each fill amount is the percentage of the current value divided the total value
	void Update () {
		if (this.healthFill == null)
			return;
		healthFill.fillAmount = Player.instance.playerStats.currentHealth / Player.instance.playerStats.maxHealth;
		if (this.expFill == null)
			return;
		expFill.fillAmount = (float)Player.instance.playerStats.currentExperience / (float)Player.instance.playerStats.targetExperience;
		if (this.rageFill == null)
			return;
		rageFill.fillAmount = (float)Player.instance.playerStats.currentRage / (float)Player.instance.playerStats.maxRage;

	}
}
