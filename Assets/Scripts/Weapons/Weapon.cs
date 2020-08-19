using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public int maxAmmo;
    protected int currAmmo;
    public float shootTime;
    protected float timeToShoot;
    public int damage;

    public float TTS()
    {
        return timeToShoot;
    }

    void Start()
    {
       timeToShoot = 0.0f; 
       currAmmo = maxAmmo;
    }

    // Fires the weapon
    public abstract void Fire();

    // Performs weapons alternate action
    public abstract void AltFire();

    // Reloads the weapon if applicable
    //public abstract void Reload();

    public void UpdateTTS()     // Updates time to shoot
    {
        timeToShoot -= Time.deltaTime;
    }

    public string AmmoToString()    // returns currAmmo/maxAmmo as a string
    {
        string ammoCount = currAmmo + "/" + maxAmmo;
        return ammoCount;
    }
}