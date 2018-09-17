using UnityEngine;

/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # 498 capstone
  #
*-----------------------------------------------------------------------*/
/* Creator Yan Zhang,, Yunzheng Zhou
 * Inherit tp the DelayedReaction
 * enable the behaviour when the gameObject has been interacte
 */
public class BehaviourReaction : DelayedReaction
{
    /*
     * behaviour the behaviour for the reaction
     * enabledState is the boolean value to trigger the reaction
     */
    public Behaviour behaviour;
    public bool enabledState;

    /*
     * Set the enabled to give enabledState boolean value;
     */
    protected override void ImmediateReaction()
    {
        behaviour.enabled = enabledState;
    }
}
