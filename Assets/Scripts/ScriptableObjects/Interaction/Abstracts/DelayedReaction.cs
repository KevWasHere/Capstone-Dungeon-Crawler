using UnityEngine;
using System.Collections;
/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # 498 capstone
  # Stats.cs
  #
*-----------------------------------------------------------------------*/
/*
* Creator: Yan Zhang
* the Delayed reaction parent class
* the delayed reaction can put reaction delay for the time you want to set\
* delay: the time amount you want to delay
* wait: wait for seconds
*/
public abstract class DelayedReaction : Reaction
{
    public float delay;

    protected WaitForSeconds wait;

	//initially delay for wait
    public new void Init ()
    {
        wait = new WaitForSeconds (delay);

        SpecificInit ();
    }

	//react teh reaction
    public new void React (MonoBehaviour monoBehaviour)
    {
        monoBehaviour.StartCoroutine (ReactCoroutine ());
    }

	// yield the reaction 
    protected IEnumerator ReactCoroutine ()
    {
        yield return wait;

        ImmediateReaction ();
    }
}
