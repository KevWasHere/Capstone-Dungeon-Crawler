using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # UISlotBase.cs
*-----------------------------------------------------------------------*/

/*
* Creator: Yunzheng Zhou, Tianqi Xiao
*/


/*  class: UIslotBase
 *  It defines the bacis functioning and implements of item slots, equip slots and spell slots.
 *  It uses different Event handler to determine the mouse event to do the actions including drag, swap and drop item.
 *  According to different type of items, the right click event on a item will be varied.
 *  Cusmable item will be used; equipment will be equiped or swap with current equipment.
 *  
 */

namespace UnityEngine.UI
{
	[AddComponentMenu("UI/Icon Slots/Base Slot"), ExecuteInEditMode, DisallowMultipleComponent]
	public class UISlotBase : UIBehaviour, IEventSystemHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler {
		
		public enum Transition      // enum array to define the transition methods
		{
			None,
			ColorTint,
			SpriteSwap,
			Animation
		}
		
		public enum DragKeyModifier // enum array of drage key modifier to determine if player use combo key to drag item
		{
			None,
			Control,
			Alt,
			Shift
		}

        /*
        * m_CurrentDraggedObject: The current dragged object
        * m_CurrentDraggingPlane: The current dragging plane
        * iconGraphic: The target icon graphic
        * dragAndDropEnabled: Gets or sets a value indicating whether this drag and drop is enabled
        * isStatic: Gets or sets a value indicating whether this is static
        * m_AllowThrowAway will determine if the item could be throw away from slot or not.
        * m_dragkeyModifier will define the drag key when player wants to drag a item
        * m_Tooltipenaled will show up the tool tip if it is true
        * m_Tooltipdelay define how long the tooltip delay to show up
        */
        protected GameObject m_CurrentDraggedObject;

		protected RectTransform m_CurrentDraggingPlane;

		public Graphic iconGraphic;
		
		[SerializeField, Tooltip("Should the drag and drop functionallty be enabled.")]
		private bool m_DragAndDropEnabled = true;

		[SerializeField, Tooltip("If set to static the slot won't be unassigned when drag and drop is preformed.")]
		private bool m_IsStatic = false;
		
		[SerializeField, Tooltip("Should the icon assigned to the slot be throwable.")]
		private bool m_AllowThrowAway = true;
		
		[SerializeField, Tooltip("The key which should be held down in order to begin the drag.")]
		private DragKeyModifier m_DragKeyModifier = DragKeyModifier.None;
		
		[SerializeField, Tooltip("Should the tooltip functionallty be enabled.")]
		private bool m_TooltipEnabled = true;
		
		[SerializeField, Tooltip("How long of a delay to expect before showing the tooltip.")]
		private float m_TooltipDelay = 1f;

        public GameObject player;                               // gameobject the player
        public UIItemDatabase itemdatabase;                     // front end item database
        public ItemDatabase backupitemDataBase;                 // backend item database
        [HideInInspector]       
        public Transition hoverTransition = Transition.None;    //  set hover transition
        [HideInInspector]
        public Graphic hoverTargetGraphic;                      // set hover target graphic
        [HideInInspector]
        public Color hoverNormalColor = Color.white;            // set hover normal color to white
        [HideInInspector]
        public Color hoverHighlightColor = Color.white;         // set hover highlight color to white
        [HideInInspector]
        public float hoverTransitionDuration = 0.15f;           // set hover transition duration to 0.15f
        [HideInInspector]
        public Sprite hoverOverrideSprite;                      //  set hover override sprite
        [HideInInspector]
        public string hoverNormalTrigger = "Normal";            // set hover trigger to normal
        [HideInInspector]
        public string hoverHighlightTrigger = "Highlighted";    // set hover trigger to highlighted

        [HideInInspector]
        public Transition pressTransition = Transition.None;    // set press transition to none
        [HideInInspector]
        public Graphic pressTargetGraphic;                      // varaible graphic of pressed target
        [HideInInspector]
        public Color pressNormalColor = Color.white;            // set press normal color to white
        [HideInInspector]
        public Color pressPressColor = Color.white;             // set press pressed color to white
        [HideInInspector]
        public float pressTransitionDuration = 0.15f;           // set press transition duration to 0.15f
        [HideInInspector]
        public Sprite pressOverrideSprite;                      // the sprite of press override
        [HideInInspector]
        public string pressNormalTrigger = "Normal";            // set press trigger to normal
        [HideInInspector]
        public string pressPressTrigger = "Pressed";            // set press trigger to pressed
        
		
		[SerializeField, Tooltip("Should the pressed state transition to normal state instantly.")]
		private bool m_PressTransitionInstaOut = true;          // set pressed state transition to normal state
		
		[SerializeField, Tooltip("Should the pressed state force normal state transition on the hover target.")]
		private bool m_PressForceHoverNormal = true;            // set pressed state force normal state on the hover target
		
		private bool isPointerDown = false;                     // mouse click event boolean value
		private bool isPointerInside = false;                   // mouse hover event boolean value
		private bool m_DragHasBegan = false;                    // drag item event boolean value
		private bool m_DropPreformed = false;                   // drop item event boolean value
		private bool m_IsTooltipShown = false;                  // tooltip show event boolean value
		
		//AudioClip
		public AudioClip onClickSFX;                            // audio when player click on slots
		public AudioClip onUnclickSFX;                          // audio when player release mouse click
		public AudioClip onConsumeSFX;                          // audio when use consumable potions
		public AudioClip onEquipSFX;                            // audio when equip a item
		public AudioClip onDropSFX;                             // audio when drop a item

		/*
		 * Creator: Yunzheng Zhang
		 * dragAndDropEnabled: Gets or sets a value indicating whether the drag and drop is enabled.
		 */
		public bool dragAndDropEnabled
		{
			get
			{
				return this.m_DragAndDropEnabled;
			}
			set
			{
				this.m_DragAndDropEnabled = value;
			}
		}

		/*
		 * Creator: Yunzheng Zhang
		 * isStatic: Gets or sets a value indicating whether this is static
		 */
		public bool isStatic
		{
			get
			{
				return this.m_IsStatic;
			}
			set
			{
				this.m_IsStatic = value;
			}
		}

		/*
		 * Creator: Yunzheng Zhou
		 * allowThrowAway: Gets or sets a value indicating whether this can be throw away
		 */
		public bool allowThrowAway
		{
			get
			{
				return this.m_AllowThrowAway;
			}
			set
			{
				this.m_AllowThrowAway = value;
			}
		}

        /*
		 * Creator: Yunzheng Zhou, Tianqi Xiao
		 * dragKeyModifier: Gets or sets the drag key modifier
		 */
        public DragKeyModifier dragKeyModifier
		{
			get
			{
				return this.m_DragKeyModifier;
			}
			set
			{
				this.m_DragKeyModifier = value;
			}
		}

		/*
		 * Creator: Yunzheng Zhou
		 * Gets or sets a value indicating whether this tooltip should be enabled.
		 */
		public bool tooltipEnabled
		{
			get
			{
				return this.m_TooltipEnabled;
			}
			set
			{
				this.m_TooltipEnabled = value;
			}
		}
        
        /*
         * Creator: Yunzheng Zhou
         * Gets or sets the tooltip delay.
         */
		public float tooltipDelay
		{
			get
			{
				return this.m_TooltipDelay;
			}
			set
			{
				this.m_TooltipDelay = value;
			}
		}

		/*
		 * Creator: Yunzheng Zhou
		 * Gets or sets a value indicating whether this pressed state should transition out instantly.
		 */
		public bool pressTransitionInstaOut
		{
			get
			{
				return this.m_PressTransitionInstaOut;
			}
			set
			{
				this.m_PressTransitionInstaOut = value;
			}
		}

		/*
		 * Creator: Yunzheng Zhou
		 * Gets or sets a value indicating whether this pressed state should force normal state 
		 * transition on the hover target.
		 */
		public bool pressForceHoverNormal
		{
			get
			{
				return this.m_PressForceHoverNormal;
			}
			set
			{
				this.m_PressForceHoverNormal = value;
			}
		}
			
		/*
		 * Creator: Yunzheng Zhou
		 * Gets or sets a value indicating whether this drop was preformed.
		 */
		public bool dropPreformed
		{
			get
			{
				return this.m_DropPreformed;
			}
			set
			{
				this.m_DropPreformed = value;
			}
		}
	
		//Initialization
		protected override void Start()
		{
			// Check if the slot is not assigned but the icon graphic is active
			if (!this.IsAssigned() && this.iconGraphic != null && this.iconGraphic.gameObject.activeSelf)
			{
				// Disable the icon graphic object
				this.iconGraphic.gameObject.SetActive(false);
			}
		}

		/*
		 * Creator: Yunzheng Zhou
		 * Enable current states and perform instant transition
		 */
		protected override void OnEnable()
		{
			base.OnEnable();
			
			// Instant transition
			this.EvaluateAndTransitionHoveredState(true);
			this.EvaluateAndTransitionPressedState(true);
		}

		/*
		 * Creator: Yunzheng Zhou
		 * Disable current states and perform instant transition
		 */
		protected override void OnDisable()
		{
			base.OnDisable();
			
			this.isPointerInside = false;
			this.isPointerDown = false;
			
			// Instant transition
			this.EvaluateAndTransitionHoveredState(true);
			this.EvaluateAndTransitionPressedState(true);
		}
		
#if UNITY_EDITOR
		protected override void OnValidate()
		{
			this.hoverTransitionDuration = Mathf.Max(this.hoverTransitionDuration, 0f);
			this.pressTransitionDuration = Mathf.Max(this.pressTransitionDuration, 0f);
			
			if (this.isActiveAndEnabled)
			{
				this.DoSpriteSwap(this.hoverTargetGraphic, null);
				this.DoSpriteSwap(this.pressTargetGraphic, null);
				
				if (!EditorApplication.isPlayingOrWillChangePlaymode)
				{
					// Instant transition
					this.EvaluateAndTransitionHoveredState(true);
					this.EvaluateAndTransitionPressedState(true);
				}
				else
				{
					// Regular transition
					this.EvaluateAndTransitionHoveredState(false);
					this.EvaluateAndTransitionPressedState(false);
				}
			}
		}
#endif

		/* Creator: Yunzheng Zhou
		 * Raises the pointer enter event.
		 * Parameter: eventData - PointerEventData variable
		 */
		public virtual void OnPointerEnter(PointerEventData eventData)
		{
			this.isPointerInside = true;
			this.EvaluateAndTransitionHoveredState(false);
			
			// Check if tooltip is enabled
			if (this.enabled && this.IsActive() && this.m_TooltipEnabled)
			{
				// Start the tooltip delayed show coroutine
				// If delay is set at all
				if (this.m_TooltipDelay > 0f)
				{
					this.StartCoroutine("TooltipDelayedShow");
				}
				else
				{
					this.InternalShowTooltip();
				}
			}
		}

		/*
		 * Creator: Yunzheng Zhou
		 * Raises the pointer exit event
		 * Parameter: eventData - PointerEventData variable
		 */
		public virtual void OnPointerExit(PointerEventData eventData)
		{
			this.isPointerInside = false;
			this.EvaluateAndTransitionHoveredState(false);
			this.InternalHideTooltip();
		}

		/*
		 * Creator: Yunzheng Zhou
		 * Raises the tooltip event.
		 * Parameter: show - a bool variable indicatin whether the tooltip is showing
		 */
		public virtual void OnTooltip(bool show)
		{
		}

		/*
		 * Creator: Yunzheng Zhou
		 * Raises the pointer down event.
		 * Parameter: eventData - PointerEventData variable
		 */
		public virtual void OnPointerDown(PointerEventData eventData)	
		{
			this.isPointerDown = true;
			this.EvaluateAndTransitionPressedState(false);
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                UseItem(eventData.pointerCurrentRaycast);
            }
			// Hide the tooltip
			//this.InternalHideTooltip();
		}

		/*
		 * Creator: Yunzheng Zhou
		 * Insert new potion into slot and interact with GUI
		 * Parameter: slot - UIEquipSlot variable,
		 * 			id - an int variable indicating the item id
		 * 			testslot - Test_UIEquipSlot_Assign variable
		 */
        public void fillinPotionslot(UIEquipSlot slot, int id, Test_UIEquipSlot_Assign testslot)
        {
            for (int i = 0; i < 40; i++)
            {
                if (Inventory.instance.items[i].itemID == id)
                {
                    Inventory.instance.setdefault(i);
                    slot.Assign(itemdatabase.GetByID(id));
                    testslot.assignItem = id;
                    InventoryGUI.instance.items[i] = id;
                    InventoryGUI.instance.gameslot[i].GetComponent<UIItemSlot>().Assign(itemdatabase.GetByID(0));
                    InventoryGUI.instance.gameslot[i].GetComponent<Test_UIItemSlot_Assign>().assignItem = 0;
                    return;
                }
            }
            testslot.assignItem = 0;
            slot.Assign(itemdatabase.GetByID(0));
            for (int i = 0; i < 5; i++)
            {
                if (EquipmentManager.instance.potionSlot[i].GetComponent<Test_UIEquipSlot_Assign>().assignItem == 0)
                {
                    EquipmentManager.instance.Potions[i] = null;
                }
            }
        }
		
		/*
		 * Creator: Yunzheng Zhou
		 * Function: Plays inventory SFX when equiping, moving, and consuming items
		 * Parameters: clip - Audioclip to play
		 */
		public void playSFX(AudioClip clip) {
			//Play audio
			//Do item type check later when mulitple consumbles exist
			if (clip != null) {
				AudioSource.PlayClipAtPoint(clip, Player.instance.transform.position, 1.0f);
			}
			return;
		}

		/*
		 * Creator: Yunzheng Zhou
		 * Use potion from inventary and play sound effect
		 */
        public void UsePotion()
        {
            playSFX(onConsumeSFX);
            Test_UIEquipSlot_Assign equipItem = this.GetComponent<Test_UIEquipSlot_Assign>();
            Item consumable = backupitemDataBase.GetItem(equipItem.assignItem);
            consumable.Use();
            (this as UIEquipSlot).Assign(itemdatabase.GetByID(0));
            fillinPotionslot((this as UIEquipSlot), equipItem.assignItem, equipItem);
                
        }

		/*
		 * Creator: Yunzheng Zhou
		 * Retrieve and use item from inventary slot.
		 * Parameter: item - RaycastResult variable referencing mouse postion and action
		 */
        public void UseItem(RaycastResult item)    // right click. Use this!
        {
            if (this is UIEquipSlot && (int)(this as UIEquipSlot).equipType != 9 ) {
                return;
            }
            UIItemSlot inventorySlot = item.gameObject.GetComponent<UIItemSlot>();
            UIEquipSlot equipSlot = item.gameObject.GetComponent<UIEquipSlot>();
            Test_UIItemSlot_Assign tmpItem = item.gameObject.GetComponent<Test_UIItemSlot_Assign>();
            Test_UIEquipSlot_Assign equipItem = item.gameObject.GetComponent<Test_UIEquipSlot_Assign>();
            if (equipItem != null)
            {
                if (equipItem.assignItem == 0)
                {
                    return;
                }
            }
            UIItemInfo inven;
            Item consumable;
            if (inventorySlot == null)
            {
                //equipSlot = item.gameObject.GetComponent<UIEquipSlot>();
                //equipItem = item.gameObject.GetComponent<Test_UIEquipSlot_Assign>();
                if (equipItem == null)
                    return;
                consumable = backupitemDataBase.GetItem(equipItem.assignItem);
                inven = equipSlot.GetItemInfo();
            }
            else	//consumable
            {
                if (inventorySlot.isVendor)
                    return;
                tmpItem = item.gameObject.GetComponent<Test_UIItemSlot_Assign>();
                inven = inventorySlot.GetItemInfo();
                consumable = backupitemDataBase.GetItem(tmpItem.assignItem);
            }
            if (consumable.isconsumable) {
				playSFX(onConsumeSFX);
                consumable.Use();
                
                if (inventorySlot == null)
                {
                    equipSlot.Assign(itemdatabase.GetByID(0));
                    fillinPotionslot(equipSlot, equipItem.assignItem, equipItem);
                    return;
                }
                inventorySlot.Assign(itemdatabase.GetByID(0));
                tmpItem.assignItem = 0;
            }
            else	//equip
            {
                int i = (int)(itemdatabase.GetByID(tmpItem.assignItem).EquipType);
                if (i > 6 || i < 1)
                {
                    //Debug.Log("find gem");
                    return;
                }
                //this.InternalHideTooltip();
                UIEquipSlot EquipedSlot = EquipmentManager.instance.equipmentSlot[i].GetComponent<UIEquipSlot>();
                //Debug.Log(EquipedSlot);
                UIItemInfo currentEquiped = EquipedSlot.GetItemInfo();
                if (currentEquiped.EquipType == 0)
                {
                    //tmpItem.assignItem = 10;
                    playSFX(onEquipSFX);
                    inventorySlot.Assign(EquipedSlot);
                    if ((int)EquipedSlot.equipType == 1)
                        Player.instance.playerController.WeaponState = 1;
                }
                else
                {
                    playSFX(onEquipSFX);
                    inventorySlot.Assign(currentEquiped);
                }
                int tmpID = currentEquiped.ID;
                Test_UIEquipSlot_Assign equipedItem = EquipmentManager.instance.equipmentSlot[i].GetComponent<Test_UIEquipSlot_Assign>();             
                equipedItem.assignItem = tmpItem.assignItem;
                equipedItem.itemDatabase = itemdatabase;
                //equipedItem.slot = EquipedSlot;
                EquipedSlot.Assign(inven);
                //equipedItem.slot.Assign(inven);
                //EquipedSlot.Assign(itemdatabase.GetByID(equipedItem.assignItem));
                //equipedItem.slot.Assign(itemdatabase.GetByID(equipedItem.assignItem));
                tmpItem.assignItem = tmpID;
                Test_UIItemSlot_Assign inventoryitem = inventorySlot.GetComponent<Test_UIItemSlot_Assign>();
                inventoryitem.assignItem = tmpID;
                //Inventory.instance.UpdateInventory();
                for (int k = 4; k < 8; k++)
                    EquipmentManager.instance.EquipmentUpdate(k);
            }
            
        }

		/*
		 * Creator: Yunzheng Zhou
		 * Raises the pointer up event.
		 * Parameter: eventData - PointerEventData variable
		 */
		public virtual void OnPointerUp(PointerEventData eventData)
		{
			this.isPointerDown = false;
			this.EvaluateAndTransitionPressedState(this.m_PressTransitionInstaOut);
		}

		/*
		 * Creator: Yunzheng Zhou
		 * Raises the pointer click event.
		 * Parameter: eventData - PointerEventData variable
		 */
		public virtual void OnPointerClick(PointerEventData eventData) { }

		/*
		 * Creator: Yunzheng Zhou
		 * Determines whether this slot is highlighted based on the specified eventData
		 * Parameter: eventData - BaseEventData variable
		 * Return: a bool variable indicating if this instance is highlighted the 
		 * 			specified eventData
		 */
		protected bool IsHighlighted(BaseEventData eventData)
		{
			if (!this.IsActive())
				return false;

			if (eventData is PointerEventData)
			{
				PointerEventData pointerEventData = eventData as PointerEventData;
				return ((this.isPointerDown && !this.isPointerInside && pointerEventData.pointerPress == base.gameObject) || (!this.isPointerDown && this.isPointerInside && pointerEventData.pointerPress == base.gameObject) || (!this.isPointerDown && this.isPointerInside && pointerEventData.pointerPress == null));
			}
			
			return false;
		}

		/*
		 * Creator: Yunzheng Zhou
		 * Determines whether this slot is pressed based on the specified eventData
		 * Parameter: eventData - BaseEventData variable
		 * Return: a bool variable indicating if this instance is pressed the specified eventData
		 */
		protected bool IsPressed(BaseEventData eventData)
		{
			return this.IsActive() && this.isPointerInside && this.isPointerDown;
		}

		/*
		 * Creator: Yunzheng Zhou
		 * Evaluates and transitions the hovered state.
		 * Parameter: a bool variable indicating if set to instant.
		 */
		protected virtual void EvaluateAndTransitionHoveredState(bool instant)
		{
			if (!this.IsActive() || this.hoverTargetGraphic == null || !this.hoverTargetGraphic.gameObject.activeInHierarchy)
				return;
			
			// Determine what should the state of the hover target be
			bool highlighted = (this.m_PressForceHoverNormal ? (this.isPointerInside && !this.isPointerDown) : this.isPointerInside);
			
			// Do the transition
			switch (this.hoverTransition)
			{
				case Transition.ColorTint:
				{
					this.StartColorTween(this.hoverTargetGraphic, (highlighted ? this.hoverHighlightColor : this.hoverNormalColor), (instant ? 0f : this.hoverTransitionDuration));
					break;
				}
				case Transition.SpriteSwap:
				{
					this.DoSpriteSwap(this.hoverTargetGraphic, (highlighted ? this.hoverOverrideSprite : null));
					break;
				}
				case Transition.Animation:
				{
					this.TriggerHoverStateAnimation(highlighted ? this.hoverHighlightTrigger : this.hoverNormalTrigger);
					break;
				}
			}
		}

		/*
		 * Creator: Yunzheng Zhou
		 * Evaluates and transitions the pressed state.
		 * Parameter: a bool variable indicating if set to instant.
		 */
		protected virtual void EvaluateAndTransitionPressedState(bool instant)
		{
			if (!this.IsActive() || this.pressTargetGraphic == null || !this.pressTargetGraphic.gameObject.activeInHierarchy)
				return;
			
			// Do the transition
			switch (this.pressTransition)
			{
				case Transition.ColorTint:
				{
					this.StartColorTween(this.pressTargetGraphic, (this.isPointerDown ? this.pressPressColor : this.pressNormalColor), (instant ? 0f : this.pressTransitionDuration));
					break;
				}
				case Transition.SpriteSwap:
				{
					this.DoSpriteSwap(this.pressTargetGraphic, (this.isPointerDown ? this.pressOverrideSprite : null));
					break;
				}
				case Transition.Animation:
				{
					this.TriggerPressStateAnimation(this.isPointerDown ? this.pressPressTrigger : this.pressNormalTrigger);
					break;
				}
			}
			
			// If we should force normal state transition on the hover target
			if (this.m_PressForceHoverNormal)
				this.EvaluateAndTransitionHoveredState(false);
		}

		/*
		 * Creator: Yunzheng Zhou
		 * Starts a color tween.
		 * Parameter: target - Graphic variable
		 * 			targetColor - Color variable
		 * 			duration - an float variable indicating the duration of time
		 */
		protected virtual void StartColorTween(Graphic target, Color targetColor, float duration)
		{
			if (target == null)
				return;
			
			target.CrossFadeColor(targetColor, duration, true, true);
		}

		/*
		 * Creator: Yunzheng Zhou
		 * Does a sprite swap.
		 * Parameter: target - Graphic variable
		 * 			newSprite - Sprite variable
		 */
		protected virtual void DoSpriteSwap(Graphic target, Sprite newSprite)
		{
			if (target == null)
				return;
			
			Image image = target as Image;
			
			if (image == null)
				return;
			
			image.overrideSprite = newSprite;
		}

		/*
		 * Creator: Yunzheng Zhou
		 * Triggers the hover state animation.
		 * Parameter: triggername - a string containing the name of the trigger
		 */
		private void TriggerHoverStateAnimation(string triggername)
		{
			if (this.hoverTargetGraphic == null)
				return;
			
			// Get the animator on the target game object
			Animator animator = this.hoverTargetGraphic.gameObject.GetComponent<Animator>();
			
			if (animator == null || !animator.isActiveAndEnabled || animator.runtimeAnimatorController == null || string.IsNullOrEmpty(triggername))
				return;
			
			animator.ResetTrigger(this.hoverNormalTrigger);
			animator.ResetTrigger(this.hoverHighlightTrigger);
			animator.SetTrigger(triggername);
		}

		/*
		 * Creator: Yunzheng Zhou
		 * Triggers the pressed state animation.
		 * Parameter: triggername - a string containing the name of the trigger
		 */
		private void TriggerPressStateAnimation(string triggername)
		{
			if (this.pressTargetGraphic == null)
				return;
			
			// Get the animator on the target game object
			Animator animator = this.pressTargetGraphic.gameObject.GetComponent<Animator>();
			
			if (animator == null || !animator.isActiveAndEnabled || animator.runtimeAnimatorController == null || string.IsNullOrEmpty(triggername))
				return;
			
			animator.ResetTrigger(this.pressNormalTrigger);
			animator.ResetTrigger(this.pressPressTrigger);
			animator.SetTrigger(triggername);
		}

		/*
		 * Creator: Yunzheng Zhou
		 * Determine whether this slot is assigned.
		 * Return: a bool variable indicating if this instance is successfully assigned
		 */
		public virtual bool IsAssigned()
		{
			return (this.GetIconSprite() != null || this.GetIconTexture() != null);
		}

		/*
		 * Creator: Yunzheng Zhou
		 * Assign the specified slot by icon sprite.
		 * Parameter: icon - a Sprite variable
		 * Return: a bool variable indicating if this instance is successfully assigned
		 */
		public bool Assign(Sprite icon)
		{
			if (icon == null)
				return false;
			
			// Set the icon
			this.SetIcon(icon);

			return true;
		}

		/*
		 * Creator: Yunzheng Zhou
		 * Assign the specified slot by icon texture.
		 * Parameter: icon - a Texture variable
		 * Return: a bool variable indicating if this instance is successfully assigned
		 */
		public bool Assign(Texture icon)
		{
			if (icon == null)
				return false;
			
			// Set the icon
			this.SetIcon(icon);

			return true;
		}

		/*
		 * Creator: Yunzheng Zhou
		 * Assign the specified slot by object.
		 * Parameter: source object
		 * Return: a bool variable indicating if this instance is successfully assigned
		 */
		public virtual bool Assign(Object source)
		{
			if (source is UISlotBase)
			{
				UISlotBase sourceSlot = source as UISlotBase;
				
				if (sourceSlot != null)
				{
					// Assign by sprite or texture
					if (sourceSlot.GetIconSprite() != null)
					{
						return this.Assign(sourceSlot.GetIconSprite());
					}
					else if (sourceSlot.GetIconTexture() != null)
					{
						return this.Assign(sourceSlot.GetIconTexture());
					}
				}
			}
			
			return false;
		}

		/*
		 * Creator: Yunzheng Zhou
		 * Unassign this slot.
		 */
		public virtual void Unassign()
		{
			// Remove the icon
			this.ClearIcon();
		}

		/*
		 * Creator: Yunzheng Zhou
		 * Gets the icon sprite of this slot if it's set and the icon graphic 
		 * 	is <see cref="UnityEngine.UI.Image"/>..
		 * Return: The icon as a Sprite variable
		 */
		public Sprite GetIconSprite()
		{	
			// Check if the icon graphic valid image
			if (this.iconGraphic == null || !(this.iconGraphic is Image))
				return null;

			return (this.iconGraphic as Image).sprite;
		}

		/*
		 * Creator: Yunzheng Zhou
		 * Gets the icon texture of this slot if it's set and the icon graphic 
		 * 	is <see cref="UnityEngine.UI.RawImage"/>.
		 * Return: The icon as a Texture variable
		 */
		public Texture GetIconTexture()
		{
			// Check if the icon graphic valid image
			if (this.iconGraphic == null || !(this.iconGraphic is RawImage))
				return null;
				
			return (this.iconGraphic as RawImage).texture;
		}

		/*
		 * Creator: Yunzheng Zhou
		 * Gets the icon as object.
		 * Return: The icon as object
		 */
		public Object GetIconAsObject()
		{
			if (this.iconGraphic == null)
				return null;
			
			if (this.iconGraphic is Image)
			{
				return this.GetIconSprite();
			}
			else if (this.iconGraphic is RawImage)
			{
				return this.GetIconTexture();
			}
			
			// Default
			return null;
		}

		/*
		 * Creator: Yunzheng Zhou
		 * Sets the icon of this slot.
		 * Return: the icon sprite
		 */
		public void SetIcon(Sprite iconSprite)
		{
			// Check if the icon graphic valid image
			if (this.iconGraphic == null || !(this.iconGraphic is Image))
				return;
			
			// Set the sprite
			(this.iconGraphic as Image).sprite = iconSprite;
			
			// Enable or disabled the icon graphic game object
			if (iconSprite != null && !this.iconGraphic.gameObject.activeSelf) this.iconGraphic.gameObject.SetActive(true);
			if (iconSprite == null && this.iconGraphic.gameObject.activeSelf) this.iconGraphic.gameObject.SetActive(false);
		}

		/*
		 * Creator: Yunzheng Zhou
		 * Sets the icon of this slot.
		 * Return: The icon texture.
		 */
		public void SetIcon(Texture iconTex)
		{
			// Check if the icon graphic valid raw image
			if (this.iconGraphic == null || !(this.iconGraphic is RawImage))
				return;
			
			// Set the sprite
			(this.iconGraphic as RawImage).texture = iconTex;
			
			// Enable or disabled the icon graphic game object
			if (iconTex != null && !this.iconGraphic.gameObject.activeSelf) this.iconGraphic.gameObject.SetActive(true);
			if (iconTex == null && this.iconGraphic.gameObject.activeSelf) this.iconGraphic.gameObject.SetActive(false);
		}

		/*
		 * Creator: Yunzheng Zhou
		 * Clears the icon of this slot.
		 */
		public void ClearIcon()
		{
			// Check if the icon graphic valid
			if (this.iconGraphic == null)
				return;
			
			// In case of image
			if (this.iconGraphic is Image)
				(this.iconGraphic as Image).sprite = null;
			
			// In case of raw image
			if (this.iconGraphic is RawImage)
				(this.iconGraphic as RawImage).texture = null;
			
			// Disable the game object
			this.iconGraphic.gameObject.SetActive(false);
		}

		/*
		 * Creator: Yunzheng Zhou
		 * Raises the begin drag event.
		 * Parameter: Event data as a PointerEventData variable
		 */
		public virtual void OnBeginDrag(PointerEventData eventData)
		{
			playSFX(onClickSFX);
            //Debug.Log("begin drag!");
            //Debug.Log(this.GetComponent<UIEquipSlot>().GetItemInfo().ID);
			if (!this.enabled || !this.IsAssigned() || !this.m_DragAndDropEnabled)
			{
				eventData.Reset();
				return;
			}
			
			// Check if we have a key modifier and if it's held down
			if (!this.DragKeyModifierIsDown())
			{
				eventData.Reset();
				return;
			}
			
			// Start the drag
			this.m_DragHasBegan = true;

			// Create the temporary icon for dragging
			this.CreateTemporaryIcon(eventData);
				
			// Prevent event propagation
			eventData.Use();
		}

		/*
		 * Creator: Yunzheng Zhou
		 * Is the drag key modifier down.
		 * Return: a bool variable indicating if key modifier is down
		 */
		public virtual bool DragKeyModifierIsDown()
		{
			switch (this.m_DragKeyModifier)
			{
				case DragKeyModifier.Control:
					return (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl));
				case DragKeyModifier.Alt:
					return (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt));
				case DragKeyModifier.Shift:
					return (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));
			}
			
			// Default should be true
			return true;
		}

		/*
		 * Creator: Yunzheng Zhou
		 * Raises the drag event.
		 * Parameter: Event data as a PointerEventData variable
		 */
		public virtual void OnDrag(PointerEventData eventData)
		{
            
            // Check if the dragging has been started
            if (this.m_DragHasBegan)
			{
				// Update the dragged object's position
				if (this.m_CurrentDraggedObject != null)
					this.UpdateDraggedPosition(eventData);
				
				// Use the event
				eventData.Use();
			}
		}

		/*
		 * Creator: Yunzheng Zhou
		 * Raises the drop event.
		 * Parameter: Event data as a PointerEventData variable
		 */
		public virtual void OnDrop(PointerEventData eventData)
		{
			playSFX(onUnclickSFX);
			// Get the source slot
			UISlotBase source = (eventData.pointerPress != null) ? eventData.pointerPress.GetComponent<UISlotBase>() : null;
            
            // Make sure we have the source slot
            if (source == null || !source.IsAssigned())
            {
               
                return;
            }
			
			// Notify the source that a drop was performed so it does not unassign
			source.dropPreformed = true;
			
			// Check if this slot is enabled and it's drag and drop feature is enabled
			if (!this.enabled || !this.m_DragAndDropEnabled)
				return;
			
			// Prepare a variable indicating whether the assign process was successful
			bool assignSuccess = false;
			
			// Normal empty slot assignment
			if (!this.IsAssigned())
			{

                // Assign the target slot with the info from the source
                //UISlotBase tmpslot = this;
                source.Assign(this);
				assignSuccess = this.Assign(source);
                //source.Assign(tmpslot);
                // Unassign the source on successful assignment and the source is not static
                //Debug.Log("disappear" + assignSuccess);
                if (assignSuccess && !source.isStatic)
                {
                    int tmp = 0;
                    if (source is UIEquipSlot)
                    {
                        tmp = (source as UIEquipSlot).GetComponent<Test_UIEquipSlot_Assign>().assignItem;
                        (source as UIEquipSlot).GetComponent<Test_UIEquipSlot_Assign>().assignItem = 0;
                        (source as UIEquipSlot).Assign((source as UIEquipSlot).GetComponent<Test_UIEquipSlot_Assign>().itemDatabase.GetByID(0));
                    }
                    if (this is UIItemSlot)
                    {
                        (this as UIItemSlot).GetComponent<Test_UIItemSlot_Assign>().assignItem = tmp;
                        (this as UIItemSlot).Assign((this as UIItemSlot).GetComponent<Test_UIItemSlot_Assign>().itemDatabase.GetByID(0));
                    }
                    
                    //source.Unassign();
                }
			}
			// The target slot is assigned
			else
			{
                //Debug.Log("isAssigned!");
				// If the target slot is not static
				// and we have a source slot that is not static
				if (!this.isStatic && !source.isStatic)
				{
                    
                    // Check if we can swap
                    if (this.CanSwapWith(source) && source.CanSwapWith(this))
					{
                        // Swap the slots
                        //Debug.Log("swapperform" + this.CanSwapWith(source) + "swaop" + source.CanSwapWith(this));
						assignSuccess = source.PerformSlotSwap(this);
					}
				}
				// If the target slot is not static
				// and the source slot is a static one
				else if (!this.isStatic && source.isStatic)
				{
					assignSuccess = this.Assign(source);
				}
			}
			
			// If this slot failed to be assigned
			if (!assignSuccess)
			{
				this.OnAssignBySlotFailed(source);
			}
            
            // Use the event
            eventData.Use();
            //Inventory.instance.UpdateInventory();
        }

		/*
		 * Creator: Yunzheng Zhou
		 * Raises the end drag event.
		 * Parameter: Event data as a PointerEventData variable
		 */
		public virtual void OnEndDrag(PointerEventData eventData)
		{
			// Check if a drag was initialized at all
			if (!this.m_DragHasBegan)
				return;
			
			// Reset the drag begin bool
			this.m_DragHasBegan = false;
			
			// Destroy the dragged icon object
			if (this.m_CurrentDraggedObject != null)
			{
				Destroy(this.m_CurrentDraggedObject);
			}
			
			// Reset the variables
			this.m_CurrentDraggedObject = null;
			this.m_CurrentDraggingPlane = null;
			
			// Use the event
			eventData.Use();
			
			// Check if we are returning the icon to the same slot
			// By checking if the slot is highlighted
			if (this.IsHighlighted(eventData))
				return;
			
			// Check if no drop was preformed
			if (!this.m_DropPreformed)
			{
				// Try to throw away the assigned content
				this.OnThrowAway();
			}
			else
			{
				// Reset the drop preformed variable
				this.m_DropPreformed = false;
			}
            Inventory.instance.UpdateInventory();
        }

		/*
		 * Creator: Yunzheng Zhou
		 * Determines whether this slot can swap with the specified target slot.
		 * Parameter: a target object
		 * Return: a bool variable indicating if this instance can swap with the specified target
		 */
		public virtual bool CanSwapWith(Object target)
		{
            
            return (target is UISlotBase);
		}

		/*
		 * Creator: Yunzheng Zhou
		 * Performs a slot swap.
		 * Parameter: a targetObject variable
		 * Return: a bool variable indicating if this instance can swap with the specified target
		 */
		public virtual bool PerformSlotSwap(Object targetObject)
		{
            // Get the source slot
            //Debug.Log("baseitem swap");
			UISlotBase targetSlot = (targetObject as UISlotBase);
            
            // Get the target slot icon
            Object targetIcon = targetSlot.GetIconAsObject();
			
			// Assign the target slot with this one
			bool assign1 = targetSlot.Assign(this);
			
			// Assign this slot by the target slot icon
			bool assign2 = this.Assign(targetIcon);
			
			// Return the status
			return (assign1 && assign2);
		}

		/*
		 * Creator: Yunzheng Zhou
		 * Called when the slot fails to assign by another slot.
		 */
		protected virtual void OnAssignBySlotFailed(Object source)
		{
			Debug.Log("UISlotBase (" + this.gameObject.name + ") failed to get assigned by (" + (source as UISlotBase).gameObject.name + ").");
		}

		/*
		 * Creator: Yunzheng Zhou
		 * This method is raised to confirm throwing away the slot.
		 */
		protected virtual void OnThrowAway()		//Use this. THrow on ground
		{
			// Check if throwing away is allowed
			if (this.m_AllowThrowAway)
			{
				playSFX(onDropSFX);
                // Throw away successful, unassign the slot
                
                Test_UIItemSlot_Assign item = this.GetComponent<Test_UIItemSlot_Assign>();
                
                Instantiate(itemdatabase.GetByID(item.assignItem).prefeb, new Vector3(Player.instance.transform.position.x, Player.instance.transform.position.y, Player.instance.transform.position.z), Quaternion.identity);
                item.assignItem = 0;
                
                (this as UIItemSlot).Assign(itemdatabase.GetByID(0));
			}
			else
			{
				// Throw away was denied
				this.OnThrowAwayDenied();
			}
		}

		/*
		 * Creator: Yunzheng Zhou
		 * This method is raised when the slot is denied to be thrown away 
		 * 	and returned to it's source.
		 */
		protected virtual void OnThrowAwayDenied() { }

		/*
		 * Creator: Yunzheng Zhou
		 * Creates the temporary icon.
		 * Return: the temporary icon
		 */
		protected virtual void CreateTemporaryIcon(PointerEventData eventData)
		{
			Canvas canvas = UIUtility.FindInParents<Canvas>(this.gameObject);
			
			if (canvas == null || this.iconGraphic == null)
				return;
			
			// Create temporary panel
			GameObject iconObj = (GameObject)Instantiate(this.iconGraphic.gameObject);
			
			iconObj.transform.SetParent(canvas.transform, false);
			iconObj.transform.SetAsLastSibling();
			(iconObj.transform as RectTransform).pivot = new Vector2(0.5f, 0.5f);
			
			// The icon will be under the cursor.
			// We want it to be ignored by the event system.
			iconObj.AddComponent<UIIgnoreRaycast>();
			
			// Save the dragging plane
			this.m_CurrentDraggingPlane = canvas.transform as RectTransform;
			
			// Set as the current dragging object
			this.m_CurrentDraggedObject = iconObj;
			
			// Update the icon position
			this.UpdateDraggedPosition(eventData);
		}

		/*
		 * Creator: Yunzheng Zhou
		 * Updates the dragged icon position.
		 * Parameter: data - a PointerEventData variable
		 */
		private void UpdateDraggedPosition(PointerEventData data)
		{
			var rt = this.m_CurrentDraggedObject.GetComponent<RectTransform>();
			Vector3 globalMousePos;
			
			if (RectTransformUtility.ScreenPointToWorldPointInRectangle(this.m_CurrentDraggingPlane, data.position, data.pressEventCamera, out globalMousePos))
			{
				rt.position = globalMousePos;
				rt.rotation = this.m_CurrentDraggingPlane.rotation;
			}
		}

		/*
		 * Creator: Yunzheng Zhou
		 * Internal call for show tooltip.
		 */
		protected void InternalShowTooltip()
		{
            // Call the on tooltip only if it's currently not shown
            if (!this.m_IsTooltipShown)
			{
                if (this is UIItemSlot)
                {
                    Test_UIItemSlot_Assign tmp1 = this.GetComponent<Test_UIItemSlot_Assign>();
                    if (tmp1.assignItem == 0)
                    {
                        return;
                    }
                }
                if (this is UIEquipSlot)
                {
                    Test_UIEquipSlot_Assign tmp1 = this.GetComponent<Test_UIEquipSlot_Assign>();
                    if (tmp1.assignItem == 0)
                    {
                        return;
                    }
                }
                this.m_IsTooltipShown = true;
				this.OnTooltip(true);
			}
		}

		/*
		 * Creator: Yunzheng Zhou
		 * Internal call for hide tooltip.
		 */
		protected void InternalHideTooltip()
		{
            
            // Cancel the delayed show coroutine
            this.StopCoroutine("TooltipDelayedShow");
			
			// Call the on tooltip only if it's currently shown
			if (this.m_IsTooltipShown)
			{
				this.m_IsTooltipShown = false;
				this.OnTooltip(false);
			}
		}

		/*
		 * Creator: Yunzheng Zhou
		 * Add time gap between display
		 */
		protected IEnumerator TooltipDelayedShow()
		{
			yield return new WaitForSeconds(this.m_TooltipDelay);
			this.InternalShowTooltip();
		}
	}
}
