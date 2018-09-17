using UnityEngine;
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
* Reaction: the basics of all the reactions
* 
*/
public abstract class Reaction : ScriptableObject
{
    public void Init ()
    {
        SpecificInit ();
    }


    protected virtual void SpecificInit()
    {}

	//the imediate react
    public void React (MonoBehaviour monoBehaviour)
    {
        ImmediateReaction ();
    }


    protected abstract void ImmediateReaction ();
}
