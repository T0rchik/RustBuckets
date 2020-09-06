using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneEnemy : MonoBehaviour
{
    public DetectPlayerInArea attackRadius;
    public float rotationStrength = 9;

    public float distance = 30;
    public float fullLoopSeconds = 7;
    private float timer;

    private Rigidbody rb;
    private Vector3 originalPos;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        rb = GetComponent<Rigidbody>();
        originalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (attackRadius.canAttackPlayer)
        {
            //turretBody.LookAt(attackRadius.target);
            Quaternion targetRotation = Quaternion.LookRotation(attackRadius.target.position - transform.position);
            float speed = Mathf.Min(Time.deltaTime * rotationStrength, 1);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, speed);
        }
        else
        {
            timer += Time.deltaTime;

            float t = Mathf.PI * 2.0f * timer / fullLoopSeconds - Mathf.PI / 2.0f;
            float x = Mathf.Cos(t) * distance;
            float z = Mathf.Sin(t) * distance;
            Vector3 v = new Vector3(x, 0, z);
            rb.MovePosition(originalPos + v);

            if(timer >= fullLoopSeconds)
            {
                timer = 0;
            }

            //transform.LookAt(new Vector3(Mathf.Cos(t), transform.position.y, Mathf.Sin(t)));
        }
    }
}
