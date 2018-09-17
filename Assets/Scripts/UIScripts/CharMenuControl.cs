/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone
  # CharMenuControl.cs
  # Character Menu UI Control
*-----------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
Creator: Yunzheng Zhou, Yan Zhang, Kevin Ho
Control the ui menu with hot key or button on the screen
charMenu: It is the character menu
charDisplay: the bool value that wether the character menu is opend or not
map: the map menu, currently disabled because not big map picture available will use later
escapMenu: The game menu pop up
vendor: the vendor's panel
uiWindow: the uiwindow object on charater menu object
spellWindow: the spell and talent window
spelltab: the spell tab in spell window
UIOpen: the audio when open a window
*/

public class CharMenuControl : MonoBehaviour {
	public GameObject charMenu;
	bool charDisplay;
	public GameObject map;
    public GameObject escapMenu;
    public GameObject vendor;
	private UIWindow uiWindow;
    public UIWindow Spellwindow;
    public UITab Spelltab;
    public AudioClip UIOpen;

	//set up uiWindow and disable the map 
    void Start () {
		uiWindow = charMenu.GetComponent<UIWindow> ();

		map.SetActive (false);
	}

	// Update is called once per frame
	//Creator: Yunzheng Zhou, Yan Zhang
	void LateUpdate () {
        //open the character menu when hit H
		if (Input.GetButtonDown ("Character")) {
			//play the audio
			playUISFX();
			//open the char menu if it is closed, open it if it is close
			ShowCharacterMenu();
		} 

		//open or close the escape menu if hot key is pressed
        if (Input.GetButtonDown("EscapMenu"))
        {
			//close vender menu if escape is open
            if (vendor != null)
            {
                if (vendor.activeSelf)
                {
                    vendor.SetActive(false);
                    return;
                }
            }
			//don't open the escape if the spell window or character window is open 
            if (Spellwindow.IsOpen || uiWindow.IsOpen)
                return;
            playUISFX();
			//enable the escape menu
            escapMenu.SetActive(!escapMenu.activeSelf);
        }

		//open the spell window with hot key G
        if (Input.GetButtonDown("Spell"))
        {
			//if the window is null, return
            if (Spellwindow == null || Spelltab == null)
                return;
            playUISFX();
            // Check if the window is open
            if (Spellwindow.IsOpen)
            {
                // Check if the tab is active
                if (Spelltab.isOn)
                {
                    // Close the window since everything was already opened
                    Spellwindow.Hide();
                    return;
                }
            }
            

            // If we have reached this part of the code, that means the we should open up things
            Spellwindow.Show();
            Spelltab.Activate();
        }

		/*
		 */
//		if (Input.GetButtonDown("Map")) {
//			playUISFX();
//			map.SetActive (!map.activeSelf);
//		}
	}

	/*open the character menu
	 * if the window is open, close it
	 * if the window is close, open it
	 */
	//Creator: Yan Zhang
	public void ShowCharacterMenu(){
		if (uiWindow.IsOpen) {            
			uiWindow.Hide ();
		} else {
			//Debug.Log ("is not open");
			GetComponent<StatValue> ().updateValue ();
			uiWindow.Show ();
		}
	}
		
	
	/*
	Function: Play SFX of opening and closing UI menus
	Creator: Kevin Ho
	*/
	public void playUISFX() {		
			AudioSource.PlayClipAtPoint(UIOpen, Player.instance.transform.position, 1.0f);
	}


}
