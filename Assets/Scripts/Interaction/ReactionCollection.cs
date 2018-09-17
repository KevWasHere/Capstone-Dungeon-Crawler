/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # ReactionCollection.cs
  # 
*-----------------------------------------------------------------------*/

using UnityEngine;

/*
 * reactions - the array of reactions 
*/

/*
Creator: Yan Zhang
*/

public class ReactionCollection : MonoBehaviour
{
    public Reaction[] reactions = new Reaction[0];

	//Initialize components
    private void Start ()
    {
        for (int i = 0; i < reactions.Length; i++)
        {
            DelayedReaction delayedReaction = reactions[i] as DelayedReaction;

            if (delayedReaction)
                delayedReaction.Init ();
            else
                reactions[i].Init ();
        }
    }

	/*
	Sets length of delayed reaction
	* Yan Zhang
	*/
    public void React ()
    {
        for (int i = 0; i < reactions.Length; i++)
        {
            DelayedReaction delayedReaction = reactions[i] as DelayedReaction;

            if(delayedReaction)
                delayedReaction.React (this);
            else
                reactions[i].React (this);
        }
    }
}
