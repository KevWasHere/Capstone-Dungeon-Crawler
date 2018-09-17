using UnityEngine;
/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone
  #
*-----------------------------------------------------------------------*/
/*
 * Creator: Yan Zhang
 * Audio reaction when got interacted, there is an audio that play at the time
 * audioSource : the source of audio
 * audioClip : clip of audio
 * delay: delay the time on playing audioClip
 */
public class AudioReaction : Reaction
{
    public AudioSource audioSource;
    public AudioClip audioClip;
    public float delay;

    /*
     * WHen got interacted, play the aidioclip
     * the set delay timer.
     */
    protected override void ImmediateReaction()
    {
        audioSource.clip = audioClip;
        audioSource.PlayDelayed(delay);
    }
}