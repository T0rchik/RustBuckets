using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class MissileLauncher : Weapon
{
    public GameObject Mech;
    public GameObject missile;
    public GameObject[] targets;
    public Transform pointOfOrigin;
    public GameObject missileMonitor;
    Text[] textBoxes;
    Image[] icons;
    
    public GameObject laserPointer;

    private Camera HMD_Cam;
    private LineRenderer laserLine;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.7f);

    int burstSize = 4;
    int targetIdx = 0;

    // Start is called before the first frame update
    void Start()
    {
       currAmmo = maxAmmo; 
       
       textBoxes = missileMonitor.GetComponentsInChildren<Text>();
       textBoxes[0].text = name;
       textBoxes[1].text = AmmoToString();

       icons = missileMonitor.GetComponentsInChildren<Image>();

       laserLine = laserPointer.GetComponent<LineRenderer>();
       HMD_Cam = Mech.GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.Space))
       {
           Fire();
       } 

       if(Input.GetKeyDown(KeyCode.LeftShift))
       {
           AltFire();
       //    laserLine.enabled = true;
       }
       else{
        //   laserLine.enabled = false;
       }

        UpdateMissileMonitor();
    }

    public override void Fire()
    {
        if(ReadyToFire() && currAmmo > 0)
        {
            for(int i = 0; i < burstSize; i++)
            {
                currAmmo--;
                Debug.Log("Firing");
                GameObject currMissile = (GameObject)Instantiate(missile, pointOfOrigin.position, pointOfOrigin.rotation);
                currMissile.GetComponent<Rigidbody>().AddForce(pointOfOrigin.up * 500f);

                HomingMissile script = currMissile.GetComponent<HomingMissile>();
                script.Fire(targets[i].transform);

                Destroy(currMissile, 5.0f);
                targets[i] = null;
            }
        }
        else{
            Debug.Log("Not Fully Locked");
        }
    }

    public override void AltFire()
    {
        float rayRange = 50f;           // Make a public variable accessible from Unity
        // Layermask
        int layerMask = 1 << 11; 
        layerMask = ~layerMask;
        //TODO: Add to targets using raycast
        //Create Laser Pointer
        Vector3 laserStart = laserPointer.transform.position;
        laserLine.SetPosition(0, laserStart);
        RaycastHit hit;
        if(Physics.Raycast(laserStart, laserPointer.transform.up* rayRange, out hit, layerMask))
        {
            if(hit.collider)
            {
                Debug.Log(hit.point.ToString());
                laserLine.SetPosition(1, hit.point);
            }
        }
        else
        {
            laserLine.SetPosition(1, laserPointer.transform.up* rayRange);
        }

        /*
        //StartCoroutine(ShotEffect());


        Vector3 rayOrigin = HMD_Cam.ViewportToWorldPoint(new Vector3 (0.5f, 0.5f, 0));
        RaycastHit hit;
        laserLine.SetPosition(0, pointOfOrigin.position);
        if(Physics.Raycast(rayOrigin, HMD_Cam.transform.forward * rayRange, out hit, rayRange, layerMask))
        {
            Debug.Log(hit.point.ToString());
            laserLine.SetPosition(1, hit.point);
                
        }
        else
        {
            laserLine.SetPosition(1, rayOrigin + (HMD_Cam.transform.forward * rayRange));
        }
        */
    }


    public bool ReadyToFire()     // Fires only when all targets are filled
    {

        if(targets[targets.Length - 1] != null)     //fastest to test if last target is filled
        {
            return true;
        }
        else { return false;}
    }
    private IEnumerator ShotEffect()
    {
        //gunAudio.Play();

        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;
    }

    private void CheckLockOn()
    {
        //Check if Target still exists
        for(int i = 0; i < 4; i++)
        {
            if(!targets[i])
            {
                targets[i] = null;
            }
        }

        //If next lock on has a lock, take it from it. Start from last to first.
        if(targets[0] == null && targets[1] != null)
        {
            Debug.Log("Lock 0 took a target from Lock 1");
            targets[0] = targets[1];
            targets[1] = null;
        }
        if(targets[1] == null && targets[2] != null)
        {
            Debug.Log("Lock 1 took a target from Lock 2");
            targets[1] = targets[2];
            targets[2] = null;
        }
        if(targets[2] == null && targets[3] != null)
        {
            Debug.Log("Lock 2 took a target from Lock 3");
            //GameObject temp = targets[3];
            targets[2] = targets[3];
            targets[3] = null;
        }

    }
    public void UpdateMissileMonitor()
    {
        textBoxes[1].text = AmmoToString();

        //Check LockOn Status
        CheckLockOn();

        for(int i = 0; i < 4; i++)
        {
            if(targets[i] == null)
            {
                icons[i].enabled = false;
            }
            else
            {
                icons[i].enabled = true;
            }
        }
    }
}
