/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hagen, Shane Weerasuriya, 
  #                 Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # 
*-----------------------------------------------------------------------*/

// This script is creating a node class with variables which will be used for calculating the paths
// Creator : Shane Weerasuriya, Kevin Ho
using UnityEngine;
using System.Collections;

public class Node {

	public bool walkable; // true or false
	public Vector3 worldPosition;
	public int gridX; // x position
	public int gridY; // y position

	public int gCost; 
	public int hCost;
	public Node parent; // parent node

	// overload operator for the node class
	public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY) {
		walkable = _walkable;
		worldPosition = _worldPos;
		gridX = _gridX;
		gridY = _gridY;
	}
		// returns F cost based on g + h cost
	public int fCost {
		get {
			return gCost + hCost;
		}
	}
}
