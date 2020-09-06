using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    public Transform muzzle;
    public GameObject projectilePrefab;

    public float attackFrequency = 3;
    private float timeTillAttack = 0;

    private DetectPlayerInArea detect;

    // Start is called before the first frame update
    void Start()
    {
        detect = GetComponentInChildren<DetectPlayerInArea>();
    }

    // Update is called once per frame
    void Update()
    {

        if (detect.canAttackPlayer)
        {
            timeTillAttack += Time.deltaTime;

            if (timeTillAttack >= attackFrequency)
            {
                timeTillAttack -= attackFrequency;
                Fire(detect.target);
            }
        }
        else
        {
            timeTillAttack = 0;
        }
        
    }

    public void Fire(Transform target)
    {
        GameObject projectile = Instantiate(projectilePrefab, muzzle.position, Quaternion.identity);
        projectile.transform.LookAt(target);
    }
}
