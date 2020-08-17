using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class MechRotation : MonoBehaviour
{
    public Rigidbody mech;
    public float rotationSpeed = 0.7f;
    private float currentRotation = 0;
    public float maxAngularVelocity = 7;

    // Start is called before the first frame update
    void Start()
    {
        if (mech == null)
        {
            mech = GetComponent<Rigidbody>() as Rigidbody;
        }

        mech.maxAngularVelocity = maxAngularVelocity;
        //if (! SteamVR_Actions.default_Rotation.active) { Debug.LogError("Rotation Input not bound"); }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float value = SteamVR_Actions.default_Rotation.axis.x;

        if (value > 0.01f || value < -0.01f)
        {
            currentRotation = value * rotationSpeed;
        }
        else
        {
            currentRotation = 0;
        }

        if (currentRotation != 0)
        {
            //Debug.Log(value);
            mech.angularVelocity = (Vector3.up * currentRotation);
        }


    }
}
