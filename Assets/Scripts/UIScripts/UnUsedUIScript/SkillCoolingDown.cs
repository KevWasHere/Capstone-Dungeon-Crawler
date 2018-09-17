
/*-------------------------------------------------------------------------*
 # INTR Group 2
 # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya,
 #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
 # CMPT 330 Final Project
 # DialogueTrigger.cs
 # Trigger for dialogue
 *-----------------------------------------------------------------------*/

/*
 Creator: Yunzheng, Yan Zhang
 Create skill cool down for action bar
 skills:List of skills 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCoolingDown : MonoBehaviour {

    public List<Skill> skills;
    public bool hover1 = false;
    public bool hover2 = false;
    public bool hover3 = false;
    public GUISkin skin;
    void FixedUpdate()
    {
        if (Input.GetButtonDown("Default Attack"))
        {
            if (skills[0].currentCoolDown >= skills[0].cooldown)
            {
                skills[0].skillIcon.fillAmount = 0;
                skills[0].currentCoolDown = 0;
            }
        }
        else if (Input.GetButtonDown("Projectile Attack"))
        {
            if (skills[1].currentCoolDown >= skills[1].cooldown)
            {
                skills[1].currentCoolDown = 0;
                skills[1].skillIcon.fillAmount = 0;
            }
        }
        else if (Input.GetButtonDown("AOE Attack"))
        {
            if (skills[2].currentCoolDown >= skills[2].cooldown)
            {
                skills[2].currentCoolDown = 0;
                skills[2].skillIcon.fillAmount = 0;
            }
        }
    }
    void Update()
    {
        foreach (Skill s in skills)
        {
            if (s.currentCoolDown < s.cooldown)
            {
                s.currentCoolDown += Time.deltaTime;
                s.skillIcon.fillAmount = s.currentCoolDown / s.cooldown;
            }
        }
    }

    public void changehover1()
    {
        hover1 = !hover1;
    }

    public void changehover2()
    {
        hover2 = !hover2;
    }

    public void changehover3()
    {
        hover3 = !hover3;
    }
    void OnGUI()
    {
        if (hover1)
        {
            Rect slotheader = new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y - 100, 315, 100);
            GUI.Box(slotheader, "<color=#33CEFF>" + skills[0].skilldesc + "</color>\n<color=#33333>Shoot enemy with powerful freezing wave \ndeal 40</color> + <color=#33CEFF>(100% * Attack Power)</color><color=#33333>Aoe damage within 10 yards\n1.6 second cooling down</color>", skin.GetStyle("skill"));
        }
        else if (hover2)
        {
            Rect slotheader = new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y - 100, 315, 100);
            GUI.Box(slotheader, "<color=#33CEFF>" + skills[1].skilldesc + "</color>\n<color=#33333>Shoot a powerful frost ray forward\ndeal 40</color> + <color=#33CEFF>(100% * Attack Power)</color><color=#33333>\n1.6 second Cooling down</color>", skin.GetStyle("skill"));
        }
        else if (hover3)
        {
            Rect slotheader = new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y - 100, 315, 100);
            GUI.Box(slotheader, "<color=#33CEFF>" + skills[2].skilldesc + "</color>\n<color=#33333>Summon a flame fire from sky\ndeal 40</color> + <color=#33CEFF>(100% * Attack Power)</color><color=#33333>Aoe damage within 10 yards last for 4 second\n5 second cooling down</color>", skin.GetStyle("skill"));
        }
    }	
}




[System.Serializable]
public class Skill
{
    public float cooldown;
    public Image skillIcon;
    public float currentCoolDown;
    public string skilldesc;
    
}
