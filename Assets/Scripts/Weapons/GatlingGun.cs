using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatlingGun : Weapon
{
    public float rayRange = 50f;
    public float hitForce = 100f;
    public Transform muzzlePoint;

    private Camera HMD_Cam;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.7f);
    //private AudioSource gunAudio:
    private LineRenderer laserLine;
    public GameObject Mech;
    void Start()
    {
       currAmmo = maxAmmo; 
       laserLine = GetComponent<LineRenderer>();
       HMD_Cam = Mech.GetComponentInChildren<Camera>();
    }

    public override void Fire()
    {
        if(currAmmo > 0)
        {
            currAmmo -= 1;
            StartCoroutine(ShotEffect());

            // Bit shift the index of the layer (11: MechOutside) to get a bit mask
            int layerMask = 1 << 11;
            // invert layerMask to be everything but 11
            layerMask = ~layerMask;
            Vector3 rayOrigin = HMD_Cam.ViewportToWorldPoint(new Vector3 (0.5f, 0.5f, 0));
            RaycastHit hit;

            laserLine.SetPosition(0, muzzlePoint.position);
            if(Physics.Raycast(rayOrigin, HMD_Cam.transform.forward * rayRange, out hit, rayRange, layerMask))
            {
               // Debug.Log(hit.point.ToString());
                laserLine.SetPosition(1, hit.point);


                EnemyHealth enemy = hit.collider.GetComponentInParent<EnemyHealth>();

                if (enemy != null)
                {
                    enemy.DealDamage(damage);
                }

            }
            else{
                laserLine.SetPosition(1, rayOrigin + (HMD_Cam.transform.forward * rayRange));
            }
        }
        else
        {
            Debug.Log("Out of Ammo!");
        }
    }

    private IEnumerator ShotEffect()
    {
        //gunAudio.Play();

        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;
    }

    public override void AltFire()
    {
        Debug.Log("Alternate Fire used");
    }
}
