using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


public class MechWeapons : MonoBehaviour
{
    //Weapon[] weapons;     //for later when multiple weapons are finished
//    public Weapon gun;
    public Cannon gun;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gun.timeToShoot -= Time.deltaTime;
        //if(SteamVR_Input.GetAction<Valve.VR.SteamVR_Action_Boolean>("FireWeapon") && gun.timeToShoot <= 0f) 
        if(Input.GetButtonDown("Fire1"))
       {
           Debug.Log("Firing!!!!");
           gun.Fire();
       }

       if(Input.GetButtonDown("Fire2"))
       {
           gun.AltFire();
       }
    }


    // For later
    //void changeWeapon(int weapon)
}
