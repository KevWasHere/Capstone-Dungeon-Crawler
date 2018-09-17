using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone
  # CameraControllerUI.cs
  # changeCursor
*-----------------------------------------------------------------------*/

/*
Creator: Yan Zhang
Change the cursor of the the mouse when hover over the collider of a game object
cursorTexture: the texture player want to change when hover over
hotSpot: the hover spot of the mouse
cursorMode: the cursor mode of the cursor
*/
//Creator: Yan Zhang
public class ChangeCursor : MonoBehaviour {

	public Texture2D cursorTexture;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpot = Vector2.zero;
	//Change the cursor when mouse hover
	void OnMouseOver()
	{
		Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
	}
	//chaneg it back when mouse exit the collider
	void OnMouseExit()
	{
		Cursor.SetCursor(null, Vector2.zero, cursorMode);
	}
}
