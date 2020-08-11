using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected int maxAmmo;
    protected int currAmmo;
    protected float shootTime;
    protected float timeToShoot;
    protected int damage;

    public Weapon(int maxAmmo, float shootTime, int damage)
    {
        this.maxAmmo = maxAmmo;
        this.currAmmo = maxAmmo;
        this.shootTime = shootTime;
        this.damage = damage;
    }

    void Start()
    {
       timeToShoot = 0.0f; 
    }

    public void Fire()
    {
        // Fires the weapon
    }

    public void AltFire()
    {
        // Performs weapons alternate action
    }

    public void Reload()
    {
        // Reloads the weapon if applicable
    }
}