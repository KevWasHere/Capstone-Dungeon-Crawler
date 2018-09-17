/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # SaveDroppableItem.cs
*-----------------------------------------------------------------------*/
using System;

// Author: Myles Hagen
/*
 * class that stores the position and id of an item
 * so that it can be reinstantiated in the correct location
 * creator: Myles Hagen
 * 
 * PositionX, PositionY, PositionZ - floats represting the items location
 * itemID - item identification number
 */

[Serializable]
public class SaveDroppableItem  {

    public float PositionX, PositionY, PositionZ;
    public int itemID;

}


