/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone

*-----------------------------------------------------------------------*/

/*
 * Author: Yunzheng Zhou, Yan Zhang
 * Date: 2018-04-19
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * windowRect - 
 * close - 
 * content -
 * currentQuests - 
 * panel - 
 * instance - 
*/

public class GUIManager : MonoBehaviour {
	public Rect windowRect = new Rect(500, 500, 500, 300);

	public Texture aTexture;
	//private bool toggleTxt = false;
	//private bool toggleImg = false;
	//public GameObject panel;
	//public Text text;
	//public ConditionCollection conditionCollection; 
	// Use this for initialization
	public bool close; 
	public string content;
	public ConditionCollection[] currentQuests = new ConditionCollection[10];

	//public GameObject panel;

	public bool open = true;
	public Content questList = Content.instance;

	//public Text[] info = panel.GetComponentsInChildren<Text>();

	#region Singleton

	public static GUIManager instance;

	//Initialize components
	void Awake ()
	{
		instance = this;
	}

	#endregion

	/*
	DESCRIPTION HERE!!!
	Parameters: PARAMETERS HERE IF EXIST!!!!!!
	Return: RETURN HERE IF EXIST!!!
	Creator: CREATOR HERE!!!
	*/
	void OnGUI(){
		content = questList.Change ();
		open = GUI.Toggle(new Rect(168, 5, 100, 20), open, "Window 0");
		if (open){
		    GUI.Box (new Rect (10, 10, 168, 190), content);
		}
//		windowRect = GUI.Window(50, windowRect, DoMyWindow, "My Window");
//
//		if (!aTexture) {
//			Debug.LogError("Please assign a texture in the inspector.");
//			return;
//		}
//		toggleTxt = GUI.Toggle(new Rect(10, 10, 100, 30), toggleTxt, "A Toggle text");
//		toggleImg = GUI.Toggle(new Rect(10, 50, 50, 50), toggleImg, aTexture);
	}

	void DoMyWindow(int windowID) {
		if (GUI.Button(new Rect(10, 20, 100, 20), "Hello World"))
			print("Got a click");

		if (GUI.Button(new Rect(10, 20, 100, 20), "22222"))
			print("Got click");
//
	}


}
