/*-------------------------------------------------------------------------*
 # INTR Group 2
 # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya,
 #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
 # CMPT 498 Final Project
 #
 *-----------------------------------------------------------------------*/
using UnityEngine;
/* Yan Zhang, Shane Weerasuriya
 * message: the text want to display
 * textColor: the color of text
 * delay: delay the text for appear on the screen,how much to delay
 * textManager: manage all the text
 * 
 */
public class TextReaction : Reaction
{
	public string message;
	public Color textColor = Color.white;
	public float delay;
	private TextManager textManager;
	//initiallization function
	protected override void SpecificInit()
	{
		textManager = FindObjectOfType<TextManager> ();
	}
	//set up imediate reaction
	protected override void ImmediateReaction()
	{
		//display the message through the text Manager
		textManager.DisplayMessage (message, textColor, delay);
	}
}
