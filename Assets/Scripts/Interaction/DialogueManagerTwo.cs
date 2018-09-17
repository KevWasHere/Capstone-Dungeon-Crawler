// manages the dialog 
/*-------------------------------------------------------------------------*
 # INTR Group 2
 # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya,
 #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
 # CMPT 498 Final Project
 # DialogueTrigger.cs
 # Trigger for dialogue
 *-----------------------------------------------------------------------*/

/*
 Creator: Shane Weerasuriya, Yan Zhang
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/* nameText: text field on canvas to show the tile of the dialogue
 * dialogueText: text field on the cavas to shwo each sentence
 * animator: animation of the dialogue box
 * sentences: the dialogue sentences
 */
public class DialogueManagerTwo : MonoBehaviour {

	public Text nameText;
	public Text dialogueText;

	public Animator animator;
	public int initDialog;

	private Queue<string> sentences;
	public bool endDialog;
	public Text initText;

	void Awake () {
		sentences = new Queue<string>();
		endDialog = false;
	}

/* Function: StartDialogue
 * Description: trigger the dialogue to start
 * dialogue: dialogue variable contain the actural dialog
 */
	public void StartDialogue (Dialogue dialogue)
	{
		Debug.Log ("start dialog " + dialogue.name);
		animator.SetBool("IsOpen", true);

		nameText.text = dialogue.name;
		if (sentences != null) {
			sentences.Clear ();
		}
		//sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	/*Yan Zhang
	 * Dispaly the next sentence in the dialog box
	 */
	public void DisplayNextSentence ()
	{
		endDialog = false;
		if (sentences.Count == 0)
		{
			endDialog = true;
			EndDialogue();

			//SceneManager.LoadScene("CharacterMenuTestScene", LoadSceneMode.Single);
			return;
		}

		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence (string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}

	//close the dialog box if the conversation ends
	//Yan Zhang
	void EndDialogue()
	{
		animator.SetBool("IsOpen", false);
		if (initText != null && initText.GetComponent<InitialText>().state == 0) {
			initText.GetComponent<InitialText> ().ChangeInstruction (0);
		}
	}

}
