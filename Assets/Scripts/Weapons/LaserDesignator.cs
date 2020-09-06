using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class LaserDesignator : MonoBehaviour
{
    private Vector3 startPosition;
    private Quaternion startRotation;
    private Vector3 startScale;
    private Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags & (~Hand.AttachmentFlags.SnapOnAttach) & (~Hand.AttachmentFlags.DetachOthers) & (~Hand.AttachmentFlags.VelocityMovement);
    private Interactable interactable;
    private LineRenderer laser;
    public Transform laserOrigin;
    public Transform designatorAnchor;
    public bool inHand {get; protected set; }

    void Awake()
    {
        interactable = this.GetComponent<Interactable>();
    }

    void Start()
    {
        laser = GetComponentInChildren<LineRenderer>();
        laser.enabled = false;


        inHand = false;
    }

    private void HandHoverUpdate(Hand hand)
    {
        GrabTypes startingGrabType = hand.GetGrabStarting();
        bool isGrabEnding = hand.IsGrabEnding(this.gameObject);

        if(interactable.attachedToHand == null && startingGrabType != GrabTypes.None)
        {
            // Save position/rotation to restore when detatched from hand
            startPosition = transform.position;
            startRotation = transform.rotation;
            startScale = transform.localScale;
            
            // Call to continue receiving HandHoverUpdate messages,
            // and prevent hand from hovering over anything else
            hand.HoverLock(interactable);

            // Attatch Designator to the hand
            hand.AttachObject(gameObject, startingGrabType, attachmentFlags);

            inHand = true;
        }
        else if(isGrabEnding)
        {
            // Do the reverse of above
            inHand = false;

            hand.DetachObject(gameObject);

            hand.HoverUnlock(interactable);

            Debug.Log("Starting Position: " + startPosition + ", Anchor Position: " + designatorAnchor.position);
            if(startPosition != designatorAnchor.position)
            {
                transform.position = designatorAnchor.position;
            }
            else
            {
                transform.position = startPosition;
            }

            transform.rotation = startRotation;
            transform.localScale = startScale;
        }
    }

    private bool lastHovering = false;

    private void Update()
    {
        if(interactable.isHovering != lastHovering)
        {
            lastHovering = interactable.isHovering;
        }
/*
            // Laser Pointer
            RaycastHit hit;
            laser.SetPosition(0, laserOrigin.transform.position);

            if(Physics.Raycast(laserOrigin.transform.position, -laserOrigin.up, out hit))
            {
                if(hit.collider)
                {
                    laser.SetPosition(1, new Vector3(0,0,hit.distance));
                }
            }else{
                laser.SetPosition(1,new Vector3(0,0,5000));
            }
            
*/

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
}
