using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # ConvertMesh.cs
  # 
*-----------------------------------------------------------------------*/

/*
Creator: shane weerasuriya
*/

public class ConvertMesh : MonoBehaviour {
	
	/*
	 * Function: Convert
	 * 
	 * Description: convert a skinnedMeshRenderer into a MeshRenderer and MeshFilter
	 */
	[ContextMenu("Convert to regular mesh")]
	void Convert()
	{
		SkinnedMeshRenderer skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer> ();
		MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer> ();
		MeshFilter meshFilter = gameObject.AddComponent<MeshFilter> ();

		meshFilter.sharedMesh = skinnedMeshRenderer.sharedMesh;
		meshRenderer.sharedMaterials = skinnedMeshRenderer.sharedMaterials;

		DestroyImmediate (skinnedMeshRenderer);
		DestroyImmediate (this);
	}
}
