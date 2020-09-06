using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class HomingMissile : MonoBehaviour
{
    public float turn;
    public float missileVelocity;

    Transform target;
    bool hasTarget = false;

    void Update()
    {
        if(hasTarget)
        {
            Debug.Log("Missile Fired");
            GetComponent<Rigidbody>().velocity = transform.forward * missileVelocity;
            Quaternion missileTargetRotation = Quaternion.LookRotation(target.position - transform.position);
            GetComponent<Rigidbody>().MoveRotation(Quaternion.RotateTowards(transform.rotation, missileTargetRotation, turn));
        }
    }

    public void Fire(Transform tgt)
    {
        target = tgt;
        hasTarget = true;
        Debug.Log("Set Target");
    }

    public void OnCollisionEnter(Collision collision)
    {
        //Visual Effect Explosion 
        /*
        Vector3 hitPosition = transform.position;
        Quaternion hitRotation = transform.rotation;
        Destroy(gameObject);
        VisualEffect explosion = (VisualEffect)Instantiate(explosionEffect, hitPosition, hitRotation);
        Destroy(explosion, 0.5f);
        */
        
        Debug.Log("Target Hit!");
        Destroy(gameObject, .15f);        // Delete after adding explosion effects
    }
}
