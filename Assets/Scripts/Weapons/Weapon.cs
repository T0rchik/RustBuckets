using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
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

    public void UpdateTTS()     // Updates time to shoot
    {
        timeToShoot -= Time.deltaTime;
    }
}