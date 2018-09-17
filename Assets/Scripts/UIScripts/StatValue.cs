using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone
  # CharMenuControl.cs
  # Dialogue
*-----------------------------------------------------------------------*/
/*
Creator: Yan Zhang, Kevin Ho
update the state value in the character menu window
health: the health uitext
rage: the rage uitext
attack: the attack uitext
armor: the armor uitext
exp: the exp uitext
atackSpeed: the attack speed ui text
magicResistence: the magic resistence text
cooldown : the cooldown text
*/
public class StatValue : MonoBehaviour {
	
	public Text levelnum;
	public Text health;
	public Text rage;
	public Text attack;
	public Text armor;
	public Text exp;
	//public Text hpRen;
	public Text attackSpeed;
	public Text magicResitance;
	public Text cooldown;

	//the instance
    #region Singleton

    public static StatValue instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

	/*update each uitext to the player stats
	 * creator Yan, Kevin Ho
	 */
    public void updateValue(){
		levelnum.text = Player.instance.playerStats.level.ToString ();
		health.text = Player.instance.playerStats.maxHealth.ToString();
		rage.text = Player.instance.playerStats.maxRage.ToString();
		attack.text = Player.instance.playerStats.damage.GetValue().ToString();
		armor.text = Player.instance.playerStats.armor.GetValue().ToString();
		exp.text = Player.instance.playerStats.currentExperience.ToString ();
		//hpRen.text = Player.instance.playerStats.Lifesteal.GetValue ().ToString ();
		attackSpeed.text = Player.instance.playerStats.AttackSpeed.GetValue().ToString();
		magicResitance.text = Player.instance.playerStats.MagicResist.GetValue().ToString();
		cooldown.text = Player.instance.playerStats.Cooldown.GetValue ().ToString ();
	}

}
