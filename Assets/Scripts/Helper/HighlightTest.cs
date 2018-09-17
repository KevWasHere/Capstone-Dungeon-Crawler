/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 330 Final Project
  # HighlightTest.cs
  # 
*-----------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * m_MouseOverColor - When the mouse hovers over the GameObject, it turns to this color (red)
 * m_OriginalColor - This stores the GameObject’s original color
 * m_Renderer - Get the GameObject’s mesh renderer to access the GameObject’s material and color
*/
/*
Creator: Yunzheng Zhou, Yan Zhang
*/

public class HighlightTest : MonoBehaviour 
{
	//When the mouse hovers over the GameObject, it turns to this color (red)
	Color m_MouseOverColor = Color.cyan;
	//This stores the GameObject’s original color
	Color m_OriginalColor;
	//Get the GameObject’s mesh renderer to access the GameObject’s material and color
	public MeshRenderer m_Renderer;

	//Initialize Components
	void Start()
	{
		//Fetch the mesh renderer component from the GameObject
		//m_Renderer = GetComponent<MeshRenderer>();
		//Fetch the original color of the GameObject
		m_OriginalColor = m_Renderer.material.color;
	}

	/*
	 * Change the color of the GameObject to red when the mouse is over GameObject
	*/
	void OnMouseOver()
	{
		//Change the color of the GameObject to red when the mouse is over GameObject
		m_Renderer.material.color = m_MouseOverColor;
	}

	/*
	 * Reset the color of the GameObject back to normal
	*/
	void OnMouseExit()
	{
		//Reset the color of the GameObject back to normal
		m_Renderer.material.color = m_OriginalColor;
	}
}
