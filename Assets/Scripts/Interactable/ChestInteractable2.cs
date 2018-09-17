/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hagen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # ChestInteractable2.cs
*-----------------------------------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * open - bool to determine if chest is open or not
 * table - reference to DropTable script
 * anim - reference to Animation
 * delay - delay before items are dropped from chest
 * 
 * Creator: Myles Hagen, SHane Weerasuriya
 * 
 */

public class ChestInteractable2 : Interactable {

    bool open = false;
    DropTable table;
    Animation anim;
    float delay = 1.5f;

	// initialize references to droptable and animation
    private void Start()
    {
        table = GetComponent<DropTable>();
        anim = GetComponent<Animation>();
    }

	/*
	 * Function: Interact
	 * Description: interact function overriden from Interactable
	 * if chest is not open play animaton and drop loot after delay, 
	 * if chest is open then do nothing.
	 */
    public override void Interact()
    {
        base.Interact();
        if (!open)
        {
            anim["ChestAnim"].speed = 1.0f;
            anim.Play("ChestAnim");
            open = true;
            StartCoroutine(DelaySpawn(delay));
            //table.DropItem();
        }
        

    }

	/*
	 * Function: DelaySpawn
	 * Parameters: delay
	 * Description: coroutine to delay the instaniation of items
	 * when chest is opened to keep drops in line with animation.
	 */
    IEnumerator DelaySpawn(float delay)
    {
        yield return new WaitForSeconds(delay);
        table.DropItem();

    }

}
