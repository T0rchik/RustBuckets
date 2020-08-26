using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : MonoBehaviour
{
    public DetectPlayerInArea attackRadius;
    public Transform turretBody;
    public float rotationStrength = 9;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (attackRadius.canAttackPlayer)
        {
            //turretBody.LookAt(attackRadius.target);
            Quaternion targetRotation = Quaternion.LookRotation(attackRadius.target.position - transform.position);
            float speed = Mathf.Min(Time.deltaTime * rotationStrength, 1);
            turretBody.transform.rotation = Quaternion.Lerp(turretBody.rotation, targetRotation, speed);
        }
    }
}
