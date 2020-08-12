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
    public float force = 30;
    public float forceConstant = 5;
    public float stepForce = 1;


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
        if (movementActive && leftValueCurrent != leftControl.value || rightValueCurrent != rightControl.value)
        {
            float leftDif = leftValueCurrent - leftControl.value;
            float rightDif = rightValueCurrent - rightControl.value;

            leftValueCurrent = leftControl.value;
            rightValueCurrent = rightControl.value;


            //difference = (leftValueCurrent / (1 - rightValueCurrent));
            //if (difference > 1) difference = 2 - difference;

            difference = 0;
            if (leftDif < 0) difference -= leftDif;
            if (rightDif < 0) difference -= rightDif;


            difference *= 2000;

            Debug.Log(difference);

            Vector3 horizontalMove = Vector3.forward;
            Vector3 verticalMove = Vector3.up;

            if (leftDif < 0 && rightDif < 0)
            {
                verticalMove *= difference * force * stepForce;
                horizontalMove *= (difference + forceConstant);
            }
            else
            {
                verticalMove *= stepForce;
                horizontalMove *= (force * difference + forceConstant);
            }

            

            mech.AddRelativeForce(horizontalMove + verticalMove, ForceMode.Impulse);

            framesUnchanged = 0;
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
