//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Demonstrates how to create a simple interactable object
//
//=============================================================================

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Runtime.InteropServices;
using Unity.VisualScripting;

namespace Valve.VR.InteractionSystem.Sample
{
	//-------------------------------------------------------------------------
	[RequireComponent( typeof( Interactable ) )]
	public class InteractableToggle : MonoBehaviour
    {
        private TextMesh generalText;
        private TextMesh hoveringText;
        private Vector3 oldPosition;
		private Quaternion oldRotation;

		//add Game Object here
        
        

        private bool PlayerInZone;      // checking if player is in zone
        private float attachTime;

		private Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags & ( ~Hand.AttachmentFlags.SnapOnAttach ) & (~Hand.AttachmentFlags.DetachOthers) & (~Hand.AttachmentFlags.VelocityMovement);

        private Interactable interactable;

		//-------------------------------------------------
		void Awake()
		{
			var textMeshs = GetComponentsInChildren<TextMesh>();
            generalText = textMeshs[0];
            hoveringText = textMeshs[1];

            generalText.text = "";
            hoveringText.text = "";

            interactable = this.GetComponent<Interactable>();

			PlayerInZone = false;
			
            
        }


		//-------------------------------------------------
		// Called when a Hand starts hovering over this object
		//-------------------------------------------------
		private void OnHandHoverBegin( Hand hand )
		{
			generalText.text = "";
		}


		//-------------------------------------------------
		// Called when a Hand stops hovering over this object
		//-------------------------------------------------
		private void OnHandHoverEnd( Hand hand )
		{
			generalText.text = "";
		}


		//-------------------------------------------------
		// Called every Update() while a Hand is hovering over this object
		//-------------------------------------------------
		private void HandHoverUpdate( Hand hand )
		{
            GrabTypes startingGrabType = hand.GetGrabStarting();
            bool isGrabEnding = hand.IsGrabEnding(this.gameObject);

            if (interactable.attachedToHand == null && startingGrabType != GrabTypes.None)
            {
                // Save our position/rotation so that we can restore it when we detach
                oldPosition = transform.position;
                oldRotation = transform.rotation;

                // Add Toggle Object script here

                
            }
            else if (isGrabEnding)
            {
                // Detach this object from the hand
                hand.DetachObject(gameObject);

                // Call this to undo HoverLock
                hand.HoverUnlock(interactable);

                // Restore position/rotation
                transform.position = oldPosition;
                transform.rotation = oldRotation;
            }
		}


		//-------------------------------------------------
		// Called when this GameObject becomes attached to the hand
		//-------------------------------------------------
		private void OnAttachedToHand( Hand hand )
        {
            generalText.text = string.Format("");
            attachTime = Time.time;
		}



		//-------------------------------------------------
		// Called when this GameObject is detached from the hand
		//-------------------------------------------------
		private void OnDetachedFromHand( Hand hand )
		{
            generalText.text = string.Format("");
		}


		//-------------------------------------------------
		// Called every Update() while this GameObject is attached to the hand
		//-------------------------------------------------
		private void HandAttachedUpdate( Hand hand )
		{
            generalText.text = string.Format("");
		}

        private bool lastHovering = false;
        private void Update()
        {
            if (interactable.isHovering != lastHovering) //save on the .tostrings a bit
            {
                hoveringText.text = string.Format("");
				lastHovering = interactable.isHovering;
				
                
            }
		}


		//-------------------------------------------------
		// Called when this attached GameObject becomes the primary attached object
		//-------------------------------------------------
		private void OnHandFocusAcquired( Hand hand )
		{
		}


		//-------------------------------------------------
		// Called when another attached GameObject becomes the primary attached object
		//-------------------------------------------------
		private void OnHandFocusLost( Hand hand )
		{
		}

		/* private void OnTriggerEnter(Collider other) //player in zone
		{
			if(other.gameObject.tag == "Player")
			{
				
				PlayerInZone = true;
			}
		}

        private void OnTriggerExit(Collider other) //player exit zone
        {
            if (other.gameObject.tag == "Player")
            {
                PlayerInZone = false;
                
                //toggleObject.SetActive(false);

            }
        }*/
    }
}
