using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using Valve.VR.InteractionSystem;

public class MechMovement : MonoBehaviour
{
    public LinearMapping leftControl;
    private float leftValueCurrent;
    public LinearMapping rightControl;
    private float rightValueCurrent;
    private float difference;

    public Rigidbody mech;

    [Header("Movement Settings")]
    public float maxDistancePerStep = 2f;
    public float force = 10f;
    public float jumpForce = 1f;
    public float forwardJumpDivisor = 2.5f;
    public float differenceMultiplier = 20f;
    public float forceMultiplier = 2;
    public float gravity = 2;

    [HideInInspector]
    public bool grounded = true;


    private bool movementActive = false;

    private int framesUnchanged;


    // Start is called before the first frame update
    void Start()
    {
        //mech.AddForce(Vector3.forward);

        StartCoroutine(startUp());

        leftValueCurrent = leftControl.value;
        rightValueCurrent = rightControl.value;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        grounded = Physics.Raycast(mech.transform.position, Vector3.down, 5.5f);
        if (!grounded)
        {
            Debug.Log("notGrounded");
            mech.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
        }

        if (movementActive && leftValueCurrent != leftControl.value || rightValueCurrent != rightControl.value)
        {
            float leftDif = leftValueCurrent - leftControl.value;
            float rightDif = rightValueCurrent - rightControl.value;

            leftValueCurrent = leftControl.value;
            rightValueCurrent = rightControl.value;

            difference = 0;
            if (leftDif < 0) difference -= leftDif;
            if (rightDif < 0) difference -= rightDif;

            Vector3 horizontalMove = mech.transform.forward * force;
            Vector3 verticalMove = mech.transform.up * force;

            if (leftDif < 0 && rightDif < 0)
            {
                horizontalMove *= maxDistancePerStep / forwardJumpDivisor;
                verticalMove *= jumpForce;
            }
            else
            {
                horizontalMove *= maxDistancePerStep;
                verticalMove *= Mathf.Log(jumpForce);
            }

            Vector3 currentPos = mech.transform.position;
            Vector3 targetPos = mech.transform.position + horizontalMove + verticalMove;

            Vector3 translate = Vector3.MoveTowards(currentPos, targetPos, difference * differenceMultiplier);
            mech.MovePosition(translate);
            mech.AddForce(translate * forceMultiplier, ForceMode.Impulse);
        }
        else
        {
            framesUnchanged++;
            if (framesUnchanged > 10)
            {
                //mech.velocity = Vector3.zero;
            }
        }
        
    }

    IEnumerator startUp()
    {
        yield return new WaitForFixedUpdate();
        yield return new WaitForSeconds(1.5f);
        movementActive = true;
    }
}
