using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class MechStrafe : MonoBehaviour
{
    public Rigidbody mech;
    public HoverButton leftButton;
    public HoverButton rightButton;

    public float force = 10;
    private float forceMultiplier = 1000000;
    public float rechargeSpeed = 3;
    private float timeTillStrafe = 0;


    // Start is called before the first frame update
    void Start()
    {
        leftButton.onButtonDown.AddListener(StrafeLeft);
        rightButton.onButtonDown.AddListener(StrafeRight);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StrafeLeft(Hand hand)
    {
        if (timeTillStrafe <= Time.time)
        {
            mech.AddForce(mech.transform.right * -1 * force * forceMultiplier, ForceMode.Impulse);
            timeTillStrafe = Time.time + rechargeSpeed;
        }
    }

    public void StrafeRight(Hand hand)
    {
        if (timeTillStrafe <= Time.time)
        {
            mech.AddForce(mech.transform.right * force * forceMultiplier, ForceMode.Impulse);
            timeTillStrafe = Time.time + rechargeSpeed;
        }
    }
}
