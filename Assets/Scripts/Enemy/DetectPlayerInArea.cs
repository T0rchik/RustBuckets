using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayerInArea : MonoBehaviour
{
    public bool canAttackPlayer = false;
    public Transform target;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target = other.transform;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, (target.position - transform.position), out hit))
            {
                if (hit.transform == target)
                {
                    canAttackPlayer = true;
                }
            }
            else
            {
                canAttackPlayer = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canAttackPlayer = false;
    }
}
