using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;
//using UnityEngine.UIElements;

public class MissileLauncher : Weapon
{
    // Missile Firing
    public GameObject Mech;
    public GameObject missile;
    public GameObject[] targets;
    public Transform pointOfOrigin;

    // Missile Lock On
    public float lockOnTime = 1.0f;
    private GameObject _selection;
    private string selectableTag = "Enemy";
    float elapsedTime;
    private bool lockSet = false;

    // Laser Pointer Targeting
    public GameObject laserDesignator;
    public float rayRange = 50f;
    private GameObject laserStart;
    private GameObject laserEnd;
    private LaserDesignator LDscript;
    private LineRenderer laser;
    public Material[] laserColors;

    // Missile Monitor
    public GameObject missileMonitor;
    Text[] textBoxes;
    Image[] icons;
    

    private Camera HMD_Cam;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.7f);

    int burstSize = 4;
    int targetIdx = 0;

    void Start()
    {
       currAmmo = maxAmmo; 
       
       textBoxes = missileMonitor.GetComponentsInChildren<Text>();
       textBoxes[0].text = name;
       textBoxes[1].text = AmmoToString();

       icons = missileMonitor.GetComponentsInChildren<Image>();

       laserStart = GameObject.FindGameObjectWithTag("LaserStart");
       laserEnd = GameObject.FindGameObjectWithTag("LaserEnd");
       LDscript = laserDesignator.GetComponent<LaserDesignator>();
       laser = laserDesignator.GetComponentInChildren<LineRenderer>();
       laser.enabled = false;

       HMD_Cam = Mech.GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
       //laser.SetPosition(0, laserDesignator.transform.up);
       laser.SetPosition(0,laserStart.transform.position);

            laserEnd.transform.position = laserStart.transform.position;
            laserEnd.transform.rotation = laserStart.transform.rotation;
            laserEnd.transform.position += -laserEnd.transform.up * rayRange;

       if(SteamVR_Actions.default_FireMissiles[SteamVR_Input_Sources.RightHand].state)
       {
           Fire();
       }
       

       if(SteamVR_Actions.default_TargetMissiles[SteamVR_Input_Sources.RightHand].state && LDscript.inHand == true)
       {
           AltFire();
           laser.enabled = true;
       } else {
           lockSet = false;
           laser.enabled = false;
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
                GameObject currMissile = (GameObject)Instantiate(missile, pointOfOrigin.position, pointOfOrigin.rotation);
                currMissile.GetComponent<Rigidbody>().AddForce(pointOfOrigin.up * 500f);
                currMissile.GetComponent<WeaponReference>().origin = this;

                HomingMissile script = currMissile.GetComponent<HomingMissile>();
                script.Fire(targets[i].transform);

                Destroy(currMissile, 5.0f);
                targets[i] = null;
            }
        }
        else{
            //Debug.Log("Not Fully Locked");
        }
    }

    public override void AltFire()
    {
        // Layermask
        LayerMask layerMask = LayerMask.GetMask("Enemy");
        
        RaycastHit hit;
        if(Physics.Raycast(laserDesignator.transform.position, -laserDesignator.transform.up, out hit, rayRange, layerMask))
        {
            GameObject selection = hit.collider.gameObject;
            laser.SetPosition(1, hit.point);


            if(selection.CompareTag(selectableTag))
            {

                if(_selection == null)
                {
                    _selection = selection;
                    elapsedTime = lockOnTime;
                }
                else
                {
                    if(_selection != selection)
                    {
                        lockSet = false;
                        elapsedTime = lockOnTime;
                        _selection = selection;
                    }

                    elapsedTime -= Time.deltaTime;
                    if(elapsedTime <= 0f && lockSet == false)
                    {
                        AddLockOn(_selection);
                        lockSet = true;
                        _selection = null;
                    }

                if(lockSet == true)           //For color Switching
                {
                    laser.material = laserColors[2];
                }
                else
                {
                    laser.material = laserColors[1];
                }
                
                }
            }

        }
        else
        {
            lockSet = false;
            laser.sharedMaterial = laserColors[0];
            laser.SetPosition(1, laserEnd.transform.position);
        }

    }


    public bool ReadyToFire()     // Fires only when all targets are filled
    {

        if(targets[targets.Length - 1] != null)     //fastest to test if last target is filled
        {
            return true;
        }
        else { return false;}
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
            //Debug.Log("Lock 0 took a target from Lock 1");
            targets[0] = targets[1];
            targets[1] = null;
        }
        if(targets[1] == null && targets[2] != null)
        {
            //Debug.Log("Lock 1 took a target from Lock 2");
            targets[1] = targets[2];
            targets[2] = null;
        }
        if(targets[2] == null && targets[3] != null)
        {
            //Debug.Log("Lock 2 took a target from Lock 3");
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

    private void AddLockOn(GameObject target)
    {
        //Debug.Log("Set Lock " + targetIdx);
        targets[targetIdx] = target;
        NextLock();
    }

    private void NextLock()
    {
        targetIdx++;
        if(targetIdx > 3)       //Keep range between 0-3
        {
            targetIdx = 0;
        }
    }
}
