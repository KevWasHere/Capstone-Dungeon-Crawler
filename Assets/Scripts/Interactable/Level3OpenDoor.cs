/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone

*-----------------------------------------------------------------------*/

/*
 * Author: Yunzheng Zhou
 * Date: 2018-04-19
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Level3OpenDoor is inherit to interactable class that player could right click on
 * Game object to invoke the interact function and initial the coroutine of changeswitch to 
 * open the door
 */
public class Level3OpenDoor : Interactable {

    public GameObject leftdoor, rightdoor, switchpole;          // the door is combined with leftdoor and rightdoor. the switchpole is the gameobject that player need to right click
    public Mesh first, second, thrid, fouth;                    // 4 mesh to run a small animation of switch pole
    public bool click;                                          // check boolean value if the switch pole is clicked or not
    
    // interact function call opendoor 
    // if switch pole get clicked
    public override void Interact()
    {
        if (click) return;
        base.Interact();
        click = true;
        openDoor();
        //Level3MovingSpike.instance.run();
    }

    // call coroutine changeswitch
    public void openDoor()
    {
        StartCoroutine("ChangeSwitch");
        
        //Level3MovingSpike.instance.Startmoving();
    }


    // wait corountine to move door downward in a while loop
    IEnumerator Wait()
    {
        int i = 0;
        while (i < 300)
        {
            yield return new WaitForSeconds(0.01f);
            leftdoor.transform.position += 0.03f * Vector3.down;
            rightdoor.transform.position += 0.03f * Vector3.down;
            i++;
        }
    }

    // change switch mesh to make it looks like 
    // doing transformation
    IEnumerator ChangeSwitch()
    {
        yield return new WaitForSeconds(0.3f);
        MeshFilter newmesh = switchpole.GetComponent<MeshFilter>();
        newmesh.mesh = first;
        yield return new WaitForSeconds(0.3f);
        newmesh.mesh = second;
        yield return new WaitForSeconds(0.3f);
        newmesh.mesh = thrid;
        yield return new WaitForSeconds(0.2f);
        newmesh.mesh = fouth;
        switchpole.GetComponent<ParticleSystem>().enableEmission = true;
        yield return new WaitForSeconds(1.5f);
        switchpole.GetComponent<ParticleSystem>().enableEmission = false;
        switchpole.GetComponent<Level3OpenDoor>().enabled = false;
        yield return Wait();

    }
}
