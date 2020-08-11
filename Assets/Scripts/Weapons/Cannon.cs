using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Weapon
{
    public Transform muzzlePoint;
    public GameObject projectile;
    public float projectileLifetime = 5.0f;
    public float shootForce;

    public Cannon(int maxAmmo, float shootTime, int damage) : base(maxAmmo, shootTime, damage)
    {}

    public void Fire()
    {
        if(currAmmo > 0)
        {
            currAmmo -= 1;
            GameObject currProjectile = (GameObject)Instantiate(projectile, muzzlePoint.position, muzzlePoint.rotation);
            currProjectile.GetComponent<Rigidbody>().AddForce(muzzlePoint.up * shootForce);
            Destroy(currProjectile, projectileLifetime);
            timeToShoot = shootTime;
        } else {
            Debug.Log("Out of Ammo!");
            // Add onscreen indicator or something
        }
    }

    public void AltFire()
    {
        // Zoom in function
        Debug.Log("View zooms in.");
    }

    // Cannon is single shot and reloads between
}
