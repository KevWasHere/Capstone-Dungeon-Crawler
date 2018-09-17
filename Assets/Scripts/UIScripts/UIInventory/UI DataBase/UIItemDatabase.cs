using UnityEngine;
using System.Collections;

/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone

*-----------------------------------------------------------------------*/

/*
 * Author: Yunzheng Zhou
 * Date: 2018-04-19
 */

namespace UnityEngine.UI
{
    /*
     * Class : UIItemDataBase 
     * 
     * Description:
     *      UIitemdata base contain the basic information about a item that shows on the GUI inventory
     *      including item id, item icon, item types etc.
     *      It contain internal functions that item could get the index and its id 
     *      this class is a scriptableobject that saved on the local
     */
    public class UIItemDatabase : ScriptableObject {

        #region Singleton

        public static UIItemDatabase instance;

        void Awake()
        {
            instance = this;
        }

        #endregion
        public UIItemInfo[] items;                                // iteminfo array that contain the item information
		
		/// <summary>
		/// Get the specified ItemInfo by index.
		/// </summary>
		/// <param name="index">Index.</param>
		public UIItemInfo Get(int index)                          // get item index in the iteminfo array
		{
			return (this.items[index]);
		}
		
		/// <summary>
		/// Gets the specified ItemInfo by ID.
		/// </summary>
		/// <returns>The ItemInfo or NULL if not found.</returns>
		/// <param name="ID">The item ID.</param>
		public UIItemInfo GetByID(int ID)                         // get item info by pass in the item id
		{                                                         // if no item info then return null
			for (int i = 0; i < this.items.Length; i++)
			{
				if (this.items[i].ID == ID)
					return this.items[i];
			}
			
			return null;
		}
	}
}