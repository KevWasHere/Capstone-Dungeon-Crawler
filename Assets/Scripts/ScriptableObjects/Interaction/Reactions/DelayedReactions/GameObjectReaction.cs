using UnityEngine;
/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # 498 capstone
  # GameObjectReaction.cs
  #
*-----------------------------------------------------------------------*/

/* Creator: Tianqi Xiao, Yunzheng Zhou
 * The class of Game Object's Reaction: child of Delayed Reaction
 * gameObject: The NPC to do the reaction
 * activeState: boolean value to check 
 */
public class GameObjectReaction : DelayedReaction
{
    public GameObject gameObject;
    public bool activeState;

    /* Creator: Tianqi Xiao, Yunzheng Zhou
     * set gameObject to the specific boolean value once been interact
     */
    protected override void ImmediateReaction()
    {
        gameObject.SetActive (activeState);
    }
}
