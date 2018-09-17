using UnityEngine;
/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # 498 capstone
  # animation reaction
  #
*-----------------------------------------------------------------------*/

/*creator Yan Zhang, Tianqi Xiao
 * Inherit to the DelayedReaction
 * animator is the animator for the reaction when get triggered
 * trigger is the name of animator that will tell animator to do which animation
 */
public class AnimationReaction : DelayedReaction
{
    /* Yan, Tianqi Xiao
     * triggerHash: hash the trigger to make animator know what 
     * animation it should be
     */
	public Animator animator;
	
    public string trigger;
    private int triggerHash;
	//public string triggerBool;

    /*Yan, Tianqi Xiao
     * assign the value of triggered animator to the triggerHash
     * 
     */
    protected override void SpecificInit ()
    {
        triggerHash = Animator.StringToHash(trigger);
    }

    /* Yan, Tianqi Xiao
     * Using the triggerHash to do the animation
     * 
     */
    protected override void ImmediateReaction ()
    {
        animator.SetTrigger (triggerHash);
		
    }
}
