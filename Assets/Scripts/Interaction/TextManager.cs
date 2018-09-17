/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # TextManager.cs
  # 
*-----------------------------------------------------------------------*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/*
 * Instruction - constructor
 * text - the actual text
 * displayTimePerCharacter - dialay time
 * additionalDisplayTime - diaplay time
 * instructions - instruction for the text color and content
 * clearTime - time to clear the text
*/

/*
Creator: Yan Zhang, Shane
*/

public class TextManager : MonoBehaviour
{
    public struct Instruction
    {
        public string message;
        public Color textColor;
        public float startTime;
    }


    public Text text;
    public float displayTimePerCharacter = 0.1f;
    public float additionalDisplayTime = 0.5f;


    private List<Instruction> instructions = new List<Instruction> ();
    private float clearTime;

	//Update once per frame
    private void Update ()
    {
        if (instructions.Count > 0 && Time.time >= instructions[0].startTime)
        {
            text.text = instructions[0].message;
            text.color = instructions[0].textColor;
            instructions.RemoveAt (0);
        }
        else if (Time.time >= clearTime)
        {
            text.text = string.Empty;
        }
    }

	/*	
	Creator: Yan
	* display the text on the screen 
	* message: the text
	* textColor the color of the text
	* delay: the delay time of the text
	*/
    public void DisplayMessage (string message, Color textColor, float delay)
    {
        float startTime = Time.time + delay;
        float displayDuration = message.Length * displayTimePerCharacter + additionalDisplayTime;
        float newClearTime = startTime + displayDuration;

        if (newClearTime > clearTime)
            clearTime = newClearTime;

        Instruction newInstruction = new Instruction
        {
            message = message,
            textColor = textColor,
            startTime = startTime
        };

        instructions.Add (newInstruction);

        SortInstructions ();
    }

	/*
	Creator: Yan 
	sort the instrctions base on star time
	*/
    private void SortInstructions ()
    {
        for (int i = 0; i < instructions.Count; i++)
        {
            bool swapped = false;

            for (int j = 0; j < instructions.Count; j++)
            {
                if (instructions[i].startTime > instructions[j].startTime)
                {
                    Instruction temp = instructions[i];
                    instructions[i] = instructions[j];
                    instructions[j] = temp;

                    swapped = true;
                }
            }

            if (!swapped)
                break;
        }
    }



}

