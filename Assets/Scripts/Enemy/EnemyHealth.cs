using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int currentHealth = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("playerProjectile"))
        {
            Weapon shot = collision.gameObject.GetComponent<WeaponReference>().origin;
            DealDamage(shot.damage);
            //Destroy(collision.gameObject);
        }
    }

    public void DealDamage(int damage)
    {
        if (damage < 0)
        {
            damage = 0;
        }

        Debug.Log("Damage: " + damage);

        currentHealth -= damage;

        CheckYoSelf();
    }

    private void CheckYoSelf()
    {
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
